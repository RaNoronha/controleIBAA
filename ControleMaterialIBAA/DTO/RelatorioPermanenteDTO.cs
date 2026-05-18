public class RelatorioPermanenteDTO
{
    public Guid id { get; set; }
    public string cod { get; set; }
    public string nome { get; set; }
    public int tipo_material { get; set; }

    public int total_entrada { get; set; }
    public int total_saida { get; set; }
    public int estoque_atual { get; set; }
    public decimal percentual_restante { get; set; }

    public string status_estoque
    {
        get
        {
            if (percentual_restante < 10m) return "CRITICO";
            if (percentual_restante <= 15m) return "ATENCAO";
            return "NORMAL";
        }
    }
}