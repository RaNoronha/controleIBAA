using ControleMaterialIBAA.Config;
using ControleMaterialIBAA.DTO;
using ControleMaterialIBAA.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Media.Media3D;

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

        public async Task<List<HistoricoMaterialDTO>> BuscarHistoricoAsync(Guid materialId)
        {
            var url = $"{Conexao.BaseUrl}/vw_historico_material?materialId=eq.{materialId}&order=dtmovimentacao.desc";

            var response = await _http.GetAsync(url);

            var json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception(json);

            return JsonConvert.DeserializeObject<List<HistoricoMaterialDTO>>(json);
        }

        public async Task AtualizarAsync(Guid id, ModelosMovimentacoes material)
        {
            var json = JsonConvert.SerializeObject(material);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _http.PatchAsync($"{Conexao.BaseUrl}/movimentacoes?id=eq.{id}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task<bool> CriarAsync(ModelosMovimentacoes movimentacao)
        {
            try
            {
                var json = JsonConvert.SerializeObject(movimentacao);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _http.PostAsync($"{Conexao.BaseUrl}/movimentacoes", content);

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task RegistrarMovimentacaoAsync(ModelosMovimentacoes mov)
        {
            var json = JsonConvert.SerializeObject(mov);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _http.PostAsync($"{Conexao.BaseUrl}/movimentacoes", content);

            var resp = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(resp);
            }

        }
       
    }
}
