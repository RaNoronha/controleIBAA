using ControleMaterialIBAA.Config;
using ControleMaterialIBAA.DTO;
using ControleMaterialIBAA.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using static System.Net.WebRequestMethods;

namespace ControleMaterialIBAA.Servicos
{
    public class ServicoDepartamentos : ServicoBase
    {
        public async Task<List<ModelosDepartamentos>> ListarAsync(bool ativos = true)
        {
            var url = $"{Conexao.BaseUrl}/departamentos";

            if (ativos)
            {
                url += "?ativo=eq.true";
            }
            var response = await _http.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ModelosDepartamentos>>(json);
        }

        public async Task<ModelosDepartamentos?> ObterAsync(Guid id)
        {
            var response = await _http.GetAsync($"{Conexao.BaseUrl}/departamentos?id=eq.{id}&limit=1");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var lista = JsonConvert.DeserializeObject<List<ModelosDepartamentos>>(json);

            return lista?.FirstOrDefault();
        }

        public async Task<ModelosDepartamentos> ObterOuCriarEstoque()
        {            
            var response = await _http.GetAsync($"{Conexao.BaseUrl}/departamentos?tipo=eq.ESTOQUE&limit=1");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var lista = JsonConvert.DeserializeObject<List<ModelosDepartamentos>>(json);

            var estoque = lista?.FirstOrDefault();

            if (estoque != null) {return estoque;}
                
            var responseNome = await _http.GetAsync($"{Conexao.BaseUrl}/departamentos?nome=ilike.*estoque*&limit=1");
            responseNome.EnsureSuccessStatusCode();

            var jsonNome = await responseNome.Content.ReadAsStringAsync();
            var listaNome = JsonConvert.DeserializeObject<List<ModelosDepartamentos>>(jsonNome);

            var estoquePorNome = listaNome?.FirstOrDefault();

            if (estoquePorNome != null)
            {
                estoquePorNome.tipo = "ESTOQUE";

                var contentUpdate = new StringContent(JsonConvert.SerializeObject(estoquePorNome),Encoding.UTF8,"application/json");

                await _http.PatchAsync($"{Conexao.BaseUrl}/departamentos?id=eq.{estoquePorNome.id}", contentUpdate);

                return estoquePorNome;
            }

            var novoEstoque = new ModelosDepartamentos
            {
                id = Guid.NewGuid(),
                nome = "Estoque",
                tipo = "ESTOQUE"
            };

            var content = new StringContent(JsonConvert.SerializeObject(novoEstoque),Encoding.UTF8,"application/json");

            var createResponse = await _http.PostAsync($"{Conexao.BaseUrl}/departamentos", content);
            createResponse.EnsureSuccessStatusCode();

            return novoEstoque;
        }

        public async Task<DepartamentoDTO?> ObterPorCodigoAsync(int codigo)
        {
            var url = $"{Conexao.BaseUrl}/departamentos" +
                      $"?cod=eq.{codigo}" +                    
                      $"&limit=1";

            var response = await _http.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var lista = JsonConvert.DeserializeObject<List<DepartamentoDTO>>(json);

            return lista?.FirstOrDefault();
        }
        public async Task AtualizarAsync(Guid id, ModelosDepartamentos departamento)
        {
            var json = JsonConvert.SerializeObject(departamento);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _http.PatchAsync($"{Conexao.BaseUrl}/departamentos?id=eq.{id}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task<bool> CriarAsync(DepartamentoDTO departamento)
        {
            try
            {
                var json = JsonConvert.SerializeObject(departamento);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _http.PostAsync($"{Conexao.BaseUrl}/departamentos", content);

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task InativarAsync(Guid id)
        {
            var json = JsonConvert.SerializeObject(new { ativo = false });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _http.PatchAsync($"{Conexao.BaseUrl}/departamentos?id=eq.{id}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task ExcluirAsync(Guid id)
        {
            var response = await _http.DeleteAsync($"{Conexao.BaseUrl}/departamentos?id=eq.{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
