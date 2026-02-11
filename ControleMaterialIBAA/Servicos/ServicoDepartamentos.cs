using ControleMaterialIBAA.Config;
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

        public async Task<ModelosDepartamentos?> ObterAsync(int id)
        {
            var response = await _http.GetAsync($"{Conexao.BaseUrl}/departamentos?id=eq.{id}&limit=1");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var lista = JsonConvert.DeserializeObject<List<ModelosDepartamentos>>(json);

            return lista?.FirstOrDefault();
        }

        public async Task AtualizarAsync(Guid id, ModelosDepartamentos material)
        {
            var json = JsonConvert.SerializeObject(material);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _http.PatchAsync($"{Conexao.BaseUrl}/departamentos?id=eq.{id}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task CriarAsync(ModelosDepartamentos material)
        {
            var json = JsonConvert.SerializeObject(material);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _http.PostAsync($"{Conexao.BaseUrl}/departamentos", content);
            response.EnsureSuccessStatusCode();
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
