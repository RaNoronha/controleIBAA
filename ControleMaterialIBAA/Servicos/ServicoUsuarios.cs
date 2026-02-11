using ControleMaterialIBAA.Config;
using ControleMaterialIBAA.Helper;
using ControleMaterialIBAA.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
namespace ControleMaterialIBAA.Servicos
{
    public class ServicoUsuarios : ServicoBase
    {
        public async Task<ModelosUsuarios?> LoginAsync(string usuario, string senha)
        {
            var usuarioEncoded = WebUtility.UrlEncode(usuario);

            var url = $"{Conexao.BaseUrl}/usuarios" + $"?usuario=eq.{usuarioEncoded}" + $"&ativo=eq.true" + $"&limit=1"; 

            var response = await _http.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var lista = JsonConvert.DeserializeObject<List<ModelosUsuarios>>(json);

            var usuarioDB = lista?.FirstOrDefault();

            bool senhaOk = SenhaHelper.Verificar(senha, usuarioDB.hash);

            if (usuarioDB == null)
            {
                return null;
            }
            //if (!senhaOk)
            //{
            //    return null;
            //}       

            return usuarioDB;
        }

        public async Task<List<ModelosUsuarios>> ListarAsync(bool ativos = true)
        {
            var url = $"{Conexao.BaseUrl}/usuarios";

            if (ativos)
            {
                url += "?ativo=eq.true";
            }
            var response = await _http.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ModelosUsuarios>>(json);
        }

        public async Task CriarAsync(ModelosUsuarios material)
        {
            var json = JsonConvert.SerializeObject(material);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _http.PostAsync($"{Conexao.BaseUrl}/usuarios", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task AtualizarAsync(Guid id, ModelosUsuarios usuario)
        {
            var json = JsonConvert.SerializeObject(usuario);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _http.PostAsync($"{Conexao.BaseUrl}/usuarios?id=eq.{id}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task InativarAsync(Guid id)
        {
            var json = JsonConvert.SerializeObject(new { ativo = false });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _http.PatchAsync($"{Conexao.BaseUrl}/usuarios?id=eq.{id}", content);
            response.EnsureSuccessStatusCode();
        }
    }
}
