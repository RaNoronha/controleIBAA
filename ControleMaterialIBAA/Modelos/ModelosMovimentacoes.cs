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
        public Guid departamentoId { get; set; }        
        public TipoMovimentacao tipo { get; set; }             
        public Guid usuario_id { get; set; }
        public DateTime dtmovimentacao { get; set; }
        public string observacao { get; set; }
    }


}
