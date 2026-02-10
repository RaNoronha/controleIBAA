using System;
using System.Collections.Generic;
using System.Text;

namespace ControleMaterialIBAA.Modelos
{
    public class ModelosMovimentacoes
    {
        public Guid id { get; set; }
        public Guid materialId { get; set; }
        public Guid departamentoId { get; set; }
        public string tipo { get; set; } // entrada, baixa, descarte, dano
        public int quantidade { get; set; }
        public string motivo { get; set; }
        public Guid usuario_id { get; set; }
        public DateTime dtmovimentacao { get; set; }
    }
}
