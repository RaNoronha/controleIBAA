using ControleMaterialIBAA.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleMaterialIBAA.DTO
{
    public class HistoricoMaterialDTO
    {
        public Guid id { get; set; }
        public Guid materialId { get; set; }
        public string material { get; set; }
        public string numPat { get; set; }
        public string departamento { get; set; }
        public TipoMovimentacao tipo { get; set; }
        public string observacao { get; set; }
        public DateTime dtmovimentacao { get; set; }
    }
}
