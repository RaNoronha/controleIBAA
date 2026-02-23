using System;
using System.Collections.Generic;
using System.Text;

namespace ControleMaterialIBAA.Modelos
{
    public class ModelosDepartamentos
    {
        public Guid id { get; set; }
        public string nome { get; set; }
        public bool ativo { get; set; } = true;        
        public ICollection<ModelosSubDepartamentos> subDepartamentos { get; set; } = new List<ModelosSubDepartamentos>();       
        public ICollection<ModelosMovimentacoes> movimentacoes { get; set; }
    }
}
