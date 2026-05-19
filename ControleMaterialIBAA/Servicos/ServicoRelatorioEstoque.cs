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
        public async Task<List<RelatorioMaterialDTO>> ListarAsync(
    string codigo = null,
    Guid? materialId = null,
    string status = null)
        {
            var listaCompleta = new List<RelatorioMaterialDTO>();

            try
            {
                // 1. Busca Consumo
                var listaConsumo = await BuscarDadosView("vw_relatorio_consumo");
                listaCompleta.AddRange(listaConsumo);

                // 2. Busca Permanente
                var listaPermanente = await BuscarDadosView("vw_relatorio_permanente");
                listaCompleta.AddRange(listaPermanente);

                // 3. Aplica Filtros na lista unificada
                if (!string.IsNullOrWhiteSpace(codigo))
                {
                    listaCompleta = listaCompleta.Where(x => x.cod.Contains(codigo, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                if (materialId.HasValue)
                {
                    listaCompleta = listaCompleta.Where(x => x.id == materialId.Value).ToList();
                }

                if (!string.IsNullOrWhiteSpace(status) && status != "TODOS")
                {
                    listaCompleta = listaCompleta.Where(x => x.status_estoque == status).ToList();
                }

                // 4. Ordena por nome
                return listaCompleta.OrderBy(x => x.nome).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao unificar relatórios: {ex.Message}");
            }
        }

        // 🔹 Método auxiliar para evitar repetição
        private async Task<List<RelatorioMaterialDTO>> BuscarDadosView(string nomeView)
        {
            var url = $"{Conexao.BaseUrl}/{nomeView}";
            var response = await _http.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode) return new List<RelatorioMaterialDTO>();

            return JsonConvert.DeserializeObject<List<RelatorioMaterialDTO>>(json)
                   ?? new List<RelatorioMaterialDTO>();
        }
    }
}