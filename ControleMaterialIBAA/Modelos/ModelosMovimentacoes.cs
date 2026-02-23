using ControleMaterialIBAA.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Media3D;

namespace ControleMaterialIBAA.Modelos
{
    public class ModelosMovimentacoes
    {
        public Guid id { get; set; }
        public Guid materialId { get; set; }
        public Material material { get; set; }
        public Guid departamentoId { get; set; }
        public ModelosDepartamentos departamento { get; set; }
        public TipoMovimentacao tipo { get; set; } // entrada, baixa, descarte, dano
        public int quantidade { get; set; }
        public string motivo { get; set; }
        public Guid usuario_id { get; set; }
        public DateTime dtmovimentacao { get; set; }
    }


}
