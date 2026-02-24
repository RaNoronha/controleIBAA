using System;
using System.Collections.Generic;
using System.Text;

namespace ControleMaterialIBAA.DTO
{
    public class DepartamentoDTO
    {
        public Guid id { get; set; }
        public string nome { get; set; }
        public bool ativo { get; set; } = true;
    }
}
