using ControleMaterialIBAA.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleMaterialIBAA.DTO
{
    public class ConsultaResumidaDTO
    {
        public string material { get; set; }
        public string departamento { get; set; }
        public int quantidade { get; set; }
        public TipoMaterial tipoMaterial { get; set; }
    }
}
