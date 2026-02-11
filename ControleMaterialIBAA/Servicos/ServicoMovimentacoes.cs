using ControleMaterialIBAA.Config;
using ControleMaterialIBAA.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace ControleMaterialIBAA.Servicos
{
    public class ServicoMovimentacoes : ServicoBase
    {
        public async Task<List<ModelosMovimentacoes>> ListarAsync(bool ativos = true)
        {
            var url = $"{Conexao.BaseUrl}/movimentacoes";

            if (ativos)
            {
                url += "?ativo=eq.true";
            }
            var response = await _http.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ModelosMovimentacoes>>(json);
        }

        public async Task<ModelosMovimentacoes?> ObterAsync(int id)
        {
            var response = await _http.GetAsync($"{Conexao.BaseUrl}/movimentacoes?id=eq.{id}&limit=1");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var lista = JsonConvert.DeserializeObject<List<ModelosMovimentacoes>>(json);

            return lista?.FirstOrDefault();
        }

        public async Task AtualizarAsync(Guid id, ModelosMovimentacoes material)
        {
            var json = JsonConvert.SerializeObject(material);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _http.PatchAsync($"{Conexao.BaseUrl}/movimentacoes?id=eq.{id}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task CriarAsync(ModelosMovimentacoes material)
        {
            var json = JsonConvert.SerializeObject(material);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _http.PostAsync($"{Conexao.BaseUrl}/movimentacoes", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task InativarAsync(Guid id)
        {
            var json = JsonConvert.SerializeObject(new { ativo = false });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _http.PatchAsync($"{Conexao.BaseUrl}/movimentacoes?id=eq.{id}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task ExcluirAsync(Guid id)
        {
            var response = await _http.DeleteAsync($"{Conexao.BaseUrl}/movimentacoes?id=eq.{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
