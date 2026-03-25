using ControleMaterialIBAA.Config;
using ControleMaterialIBAA.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace ControleMaterialIBAA.Servicos
{
    public class ServicoPatrimonios : ServicoBase
    {
        public async Task<bool> CriarAsync(List<ModelosPatrimonios> lista)
        {
            try
            {
                var json = JsonConvert.SerializeObject(lista);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _http.PostAsync($"{Conexao.BaseUrl}/patrimonios", content);

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
