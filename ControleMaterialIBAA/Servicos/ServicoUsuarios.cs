using ControleMaterialIBAA.Config;
using ControleMaterialIBAA.Helper;
using ControleMaterialIBAA.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
namespace ControleMaterialIBAA.Servicos
{
    public class ServicoUsuarios : ServicoBase
    {
        public async Task<ModelosUsuarios?> LoginAsync(string usuario, string senha)
        {
            var login = usuario.Trim().ToLower();
            var usuarioEncoded = WebUtility.UrlEncode(login);

            var url = $"{Conexao.BaseUrl}/usuarios" + $"?usuario=eq.{usuarioEncoded}" + $"&ativo=eq.true" + $"&limit=1"; 

            var response = await _http.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var lista = JsonConvert.DeserializeObject<List<ModelosUsuarios>>(json);

            var usuarioDB = lista?.FirstOrDefault();

            if (usuarioDB == null)
            {
                return null;
            }

            bool senhaOk = SenhaHelper.Verificar(senha, usuarioDB.hash);

            if (!senhaOk)
            {
                return null;
            }

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

        public async Task<bool> CriarAsync(ModelosUsuarios usuario)
        {
            try
            {
                var json = JsonConvert.SerializeObject(usuario);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _http.PostAsync($"{Conexao.BaseUrl}/usuarios", content);

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }

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
