using ControleMaterialIBAA.Config;
using ControleMaterialIBAA.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ControleMaterialIBAA.Servicos
{
    public class ServicoRelatorioEstoque : ServicoBase
    {
        public async Task<List<RelatorioPermanenteDTO>> ListarAsync(string codigo = null, Guid? materialId = null, string status = null)
        {
            var filtros = new List<string>();

            if (!string.IsNullOrWhiteSpace(codigo))
            {
                filtros.Add($"cod=ilike.*{codigo}*");
            }

            if (materialId.HasValue)
            {
                filtros.Add($"id=eq.{materialId.Value}");
            }                

            var url = $"{Conexao.BaseUrl}/vw_relatorio_permanente";
            if (filtros.Count > 0)
            {
                url += "?" + string.Join("&", filtros);
            }

            var response = await _http.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(json);
            }
            var lista = JsonConvert.DeserializeObject<List<RelatorioPermanenteDTO>>(json) ?? new List<RelatorioPermanenteDTO>();

            if (!string.IsNullOrWhiteSpace(status) && status != "TODOS")
            {
                lista = lista.Where(x => x.status_estoque == status).ToList();
            }

            return lista;
        }
    }
}