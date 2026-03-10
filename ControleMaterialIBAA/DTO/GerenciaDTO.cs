using ControleMaterialIBAA.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleMaterialIBAA.DTO
{
    public class GerenciaDTO
    {
        public Guid id { get; set; }
        public string numPat { get; set; }
        public string material { get; set; }
        public Guid departamentoId { get; set; }

        public string departamento_nome { get; set; }
        public string subdepartamento_nome { get; set; }
        public string responsavel { get; set; }
        public TipoMaterial tipoMaterial { get; set; }
        public bool Selecionado { get; set; }
    }
}
