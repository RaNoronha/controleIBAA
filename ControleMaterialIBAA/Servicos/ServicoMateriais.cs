using ControleMaterialIBAA.Config;
using ControleMaterialIBAA.DTO;
using ControleMaterialIBAA.Enums;
using ControleMaterialIBAA.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Windows;
using System.Windows.Media.Media3D;

namespace ControleMaterialIBAA.Servicos
{
    public class ServicoMateriais : ServicoBase
    {
        public async Task<List<GerenciaDTO>> ListarAsync(bool ativos = true,string? numeroPatrimonial = null,Guid? departamentoId = null,Guid? subDepartamentoId = null,TipoMaterial? tipo = null)
        {
            var parametros = new List<string>();

            // Agora select é simples porque é VIEW
            parametros.Add("select=*");

            if (ativos)
                parametros.Add("ativo=eq.true");

            if (!string.IsNullOrWhiteSpace(numeroPatrimonial))
                parametros.Add($"numPat=eq.{numeroPatrimonial}");

            if (departamentoId.HasValue)
                parametros.Add($"departamentoId=eq.{departamentoId}");

            if (subDepartamentoId.HasValue)
                parametros.Add($"subDepartamentoId=eq.{subDepartamentoId}");

            if (tipo.HasValue)
                parametros.Add($"tipoMaterial=eq.{(int)tipo.Value}");

            var queryString = "?" + string.Join("&", parametros);
            
            var url = $"{Conexao.BaseUrl}/vw_materiais_consulta{queryString}";

            var response = await _http.GetAsync(url);
           
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show($"Erro: {response.StatusCode}\n\n{content}");
                throw new Exception(content);
            }

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<GerenciaDTO>>(json);
        }

        public async Task<List<ConsultaResumidaDTO>> ConsultaResumidaAsync(string? material = null, Guid? departamentoId = null,TipoMaterial? tipo = null)
        {
            var parametros = new List<string>();

            parametros.Add("select=*");

            if (!string.IsNullOrWhiteSpace(material))
                parametros.Add($"material=ilike.*{material}*");

            if (departamentoId.HasValue && departamentoId != Guid.Empty)
                parametros.Add($"departamentoId=eq.{departamentoId}");

            if (tipo.HasValue)
                parametros.Add($"tipoMaterial=eq.{(int)tipo.Value}");

            parametros.Add("order=departamento.asc,material.asc");

            var queryString = "?" + string.Join("&", parametros);

            var url = $"{Conexao.BaseUrl}/vw_materiais_resumo{queryString}";

            var response = await _http.GetAsync(url);

            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show($"Erro: {response.StatusCode}\n\n{content}");
                throw new Exception(content);
            }

            return JsonConvert.DeserializeObject<List<ConsultaResumidaDTO>>(content);
        }

        public async Task<ModelosMateriais?> ObterAsync(int id)
        {
            var response = await _http.GetAsync($"{Conexao.BaseUrl}/materiais?id=eq.{id}&limit=1");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var lista = JsonConvert.DeserializeObject<List<ModelosMateriais>>(json);

            return lista?.FirstOrDefault();
        }

        public async Task AtualizarAsync(Guid materialId,Guid departamentoId,Guid subDepartamentoId,string responsavel)
        {
            {
                var obj = new
                {
                    departamentoId = departamentoId,
                    subDepartamentoId = subDepartamentoId,
                    responsavel = responsavel
                };

                var json = JsonConvert.SerializeObject(obj);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _http.PatchAsync($"{Conexao.BaseUrl}/materiais?id=eq.{materialId}", content);

                response.EnsureSuccessStatusCode();
            }
        }

        public async Task<bool> CriarAsync(List<ModelosMateriais> materiais)
        {
            try
            {
                var json = JsonConvert.SerializeObject(materiais);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _http.PostAsync($"{Conexao.BaseUrl}/materiais", content);

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

            var response = await _http.PatchAsync($"{Conexao.BaseUrl}/materiais?id=eq.{id}",content);
            response.EnsureSuccessStatusCode();
        }
        
        public async Task ExcluirAsync(Guid id)
        {
            var response = await _http.DeleteAsync($"{Conexao.BaseUrl}/materiais?id=eq.{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
