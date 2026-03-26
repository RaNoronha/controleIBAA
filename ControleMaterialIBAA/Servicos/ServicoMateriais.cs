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
        private List<ModelosPatrimonios> _patrimonios;
        ServicoDepartamentos _servicoDep = new ServicoDepartamentos();
        ServicoSubDepartamentos _servicoSub = new ServicoSubDepartamentos();
        ServicoMovimentacoes _servicoMovimentacoes = new ServicoMovimentacoes();
        public async Task<List<ModelosMateriais>> ListarAsync(bool ativos = true,string? cod = null ,TipoMaterial? tipo = null)
        {
            var parametros = new List<string>();
           
            parametros.Add("select=*");

            if (ativos) {parametros.Add("ativo=eq.true");}                            

            if (!string.IsNullOrWhiteSpace(cod)) {parametros.Add($"cod=ilike.*{cod}*");}

            if (tipo.HasValue) {parametros.Add($"tipoMaterial=eq.{(int)tipo.Value}");}

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

            return JsonConvert.DeserializeObject<List<ModelosMateriais>>(json);
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

        public async Task AtualizarAsync(Guid materialId, bool ativo)
        {
            var obj = new
            {
                ativo = ativo
            };

            var json = JsonConvert.SerializeObject(obj);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _http.PatchAsync( $"{Conexao.BaseUrl}/materiais?id=eq.{materialId}", content);

            response.EnsureSuccessStatusCode();
        }

        public async Task<bool> CriarAsync(ModelosMateriais materiais)
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
        public async Task<OrigemMaterialDto> ObterOrigemMaterial(ModelosMateriais mat)
        {
            // 🔹 MATERIAL PERMANENTE → usa patrimônio
            if (mat.tipoMaterial == TipoMaterial.Permanente)
            {
                var patrimonio = _patrimonios?
                    .FirstOrDefault(p => p.materialId == mat.id && p.ativo);

                if (patrimonio != null)
                {
                    var deptObj = await _servicoDep.ObterAsync(patrimonio.departamentoId);

                    string subDepNome = "-";
                    if (patrimonio.subDepartamentoId.HasValue)
                    {
                        var subdepObj = await _servicoSub.ObterAsync(patrimonio.subDepartamentoId.Value);
                        subDepNome = subdepObj?.nome ?? "-";
                    }

                    return new OrigemMaterialDto
                    {
                        departamento = deptObj?.nome ?? "Desconhecido",
                        subDepartamento = subDepNome,
                        responsavel = patrimonio.responsavel ?? "-"
                    };
                }

                // sem patrimônio ainda
                return new OrigemMaterialDto
                {
                    departamento = "Sem patrimônio",
                    subDepartamento = "-",
                    responsavel = "-"
                };
            }

            // 🔹 MATERIAL DE CONSUMO → usa movimentação
            else
            {
                var mov = await _servicoMovimentacoes.ObterUltimaPorMaterial(mat.id);

                if (mov != null)
                {
                    var deptObj = await _servicoDep.ObterAsync(mov.departamentoId);

                    string subDepNome = "-";
                    if (mov.subDepartamentoId.HasValue)
                    {
                        var subdepObj = await _servicoSub.ObterAsync(mov.subDepartamentoId.Value);
                        subDepNome = subdepObj?.nome ?? "-";
                    }

                    return new OrigemMaterialDto
                    {
                        departamento = deptObj?.nome ?? "Desconhecido",
                        subDepartamento = subDepNome,
                        responsavel = "-" // consumo não tem responsável fixo
                    };
                }

                // sem movimentação ainda
                return new OrigemMaterialDto
                {
                    departamento = "Sem movimentação",
                    subDepartamento = "-",
                    responsavel = "-"
                };
            }
        }
    }
}
