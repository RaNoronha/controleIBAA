using ControleMaterialIBAA.Config;
using ControleMaterialIBAA.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace ControleMaterialIBAA.Servicos
{
    public class ServicoSubDepartamentos :ServicoBase
    {
        public async Task<List<ModelosSubDepartamentos>> ListarAsync(bool ativos = true)
        {
            var url = $"{Conexao.BaseUrl}/sub_departamentos";

            if (ativos)
            {
                url += "?ativo=eq.true";
            }
            var response = await _http.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ModelosSubDepartamentos>>(json);
        }

        public async Task<ModelosSubDepartamentos?> ObterAsync(int id)
        {
            var response = await _http.GetAsync($"{Conexao.BaseUrl}/sub_departamentos?id=eq.{id}&limit=1");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var lista = JsonConvert.DeserializeObject<List<ModelosSubDepartamentos>>(json);

            return lista?.FirstOrDefault();
        }

        public async Task AtualizarAsync(Guid id, ModelosSubDepartamentos material)
        {
            var json = JsonConvert.SerializeObject(material);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _http.PatchAsync($"{Conexao.BaseUrl}/sub_departamentos?id=eq.{id}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task<bool> CriarAsync(ModelosSubDepartamentos subdepartamento)
        {
            try
            {
                var json = JsonConvert.SerializeObject(subdepartamento);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _http.PostAsync($"{Conexao.BaseUrl}/sub_departamentos", content);

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

            var response = await _http.PatchAsync($"{Conexao.BaseUrl}/sub_departamentos?id=eq.{id}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task ExcluirAsync(Guid id)
        {
            var response = await _http.DeleteAsync($"{Conexao.BaseUrl}/sub_departamentos?id=eq.{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
