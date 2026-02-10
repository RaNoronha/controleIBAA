using ControleMaterialIBAA.Config;
using ControleMaterialIBAA.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Media.Media3D;

namespace ControleMaterialIBAA.Servicos
{
    public class ServicoMateriais : ServicoBase
    {     
        public async Task<List<ModelosMateriais>> ListarAsync()
        {
            var response = await _http.GetAsync($"{Conexao.BaseUrl}/materiais");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ModelosMateriais>>(json);
        }

        public async Task CriarAsync(ModelosMateriais material)
        {
            var json = JsonConvert.SerializeObject(material);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _http.PostAsync($"{Conexao.BaseUrl}/materiais", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task ExcluirAsync(int id)
        {
            var response = await _http.DeleteAsync($"{Conexao.BaseUrl}/materiais?id=eq.{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
