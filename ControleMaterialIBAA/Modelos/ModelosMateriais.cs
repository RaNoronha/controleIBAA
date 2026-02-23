using System;
using System.Collections.Generic;
using System.Text;

namespace ControleMaterialIBAA.Modelos
{
    public class ModelosMateriais
    {
        public Guid id { get; set; }
        public string tipo { get; set; }        
        public string descricao { get; set; }
        public string marca { get; set; }
        public string numPat { get; set; }
        public decimal valor { get; set; }
        public int qtd { get; set; }
        public string aquisicao { get; set; }        
        public string responsavel { get; set; }
        public DateTime dtVerificacao { get; set; }
        public bool ativo { get; set; } = true;

        public ICollection<ModelosMovimentacoes> movimentacoes { get; set; } = new List<ModelosMovimentacoes>();
    }
}
