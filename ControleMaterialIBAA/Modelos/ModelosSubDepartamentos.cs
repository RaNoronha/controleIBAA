using System;
using System.Collections.Generic;
using System.Text;

namespace ControleMaterialIBAA.Modelos
{
    public class ModelosSubDepartamentos
    {
        public Guid id { get; set; }

        public string nome { get; set; }

        public bool ativo { get; set; } = true;
       
        public Guid departamentoId { get; set; }

        public ModelosDepartamentos departamento { get; set; }
    }
}
