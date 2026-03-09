using ControleMaterialIBAA.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleMaterialIBAA.Modelos
{
    public class ModelosMateriais
    {
        public Guid id { get; set; }
        public string material { get; set; }        
        public string descricao { get; set; }
        public string marca { get; set; }
        public string numPat { get; set; }
        public decimal valorUnitario { get; set; }       
        public FormaAquisicao aquisicao { get; set; }
        public TipoMaterial tipoMaterial { get; set; }
        public string responsavel { get; set; }
        public DateTime? dtVerificacao { get; set; }
        public bool ativo { get; set; } = true;
        public Guid departamentoId { get; set; }
        public Guid subDepartamentoId { get; set; }

        public string departamento_nome { get; set; }
        public string subdepartamento_nome { get; set; }

        public string departamento { get; set; }
        public int quantidade { get; set; }

        public bool Selecionado { get; set; }
    }
}
