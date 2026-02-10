using ControleMaterialIBAA.Config;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace ControleMaterialIBAA.Servicos
{
    public class ServicoBase
    {
        protected readonly HttpClient _http;

        protected ServicoBase()
        {   
            _http = new HttpClient();
            _http.DefaultRequestHeaders.Add("apikey", Conexao.ApiKey);
            _http.DefaultRequestHeaders.Add("Authorization", $"Bearer {Conexao.ApiKey}");
        }
    }
}
