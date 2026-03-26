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
        public Guid? subDepartamentoId { get; set; }
        public int quantidade { get; set; }
        public TipoMovimentacao tipo { get; set; }             
        public Guid usuarioId { get; set; }
        public DateTime dtMovimentacao { get; set; }
        public string observacao { get; set; }
    }


}
