using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Media3D;

namespace ControleMaterialIBAA.Modelos
{
    public class ModelosPatrimonios
    {
        public Guid id { get; set; }

        public int? numeroPatrimonial { get; set; }

        public Guid materialId { get; set; }
        public string? responsavel { get; set; }
        public DateTime? dtTransferencia { get; set; }

        public Guid departamentoId { get; set; }

        public Guid? subDepartamentoId { get; set; }

        public bool ativo { get; set; } = true;
    }
}
