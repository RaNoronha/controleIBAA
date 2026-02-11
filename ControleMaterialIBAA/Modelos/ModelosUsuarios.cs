using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Text;

namespace ControleMaterialIBAA.Modelos
{
    public class ModelosUsuarios
    {
        public Guid id { get; set; }        
        public string usuario { get; set; }        
        public string hash { get; set; }
        public bool ativo { get; set; }
    }
}
