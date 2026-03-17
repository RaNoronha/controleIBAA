using ControleMaterialIBAA.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;


namespace ControleMaterialIBAA.Modelos
{
    public class ModelosMateriais
    {
        public Guid id { get; set; }
        public string cod { get; set; }
        public string nome { get; set; } = null!;
        public string? descricao { get; set; }
        public string? marca { get; set; }
        public decimal? valorUnitario { get; set; }
        public FormaAquisicao? aquisicao { get; set; }
        public TipoMaterial tipoMaterial { get; set; }
        public bool ativo { get; set; } = true;
        public DateTime? dtVerificacao { get; set; }

        [JsonIgnore]
        public bool selecionado { get; set; }

    }
}
