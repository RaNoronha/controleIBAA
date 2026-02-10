using System;
using System.Collections.Generic;
using System.Text;

namespace ControleMaterialIBAA.Modelos
{
    public class ModelosDepartamentos
    {
        public Guid id { get; set; }
        public string nome { get; set; }
        public string sub_departamento { get; set; }
        public bool ativo { get; set; }
    }
}
