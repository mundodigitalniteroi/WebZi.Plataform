namespace WebZi.Plataform.Domain.Models.Faturamento
{
    public class FaturamentoCartaoModel
    {
        public int FaturamentoCartaoId { get; set; }

        public int FaturamentoId { get; set; }

        public string ReferenceId { get; set; }

        public int UsuarioCadastroId { get; set; }

        public decimal Valor { get; set; }

        public DateTime DataIntencao { get; set; }

        public DateTime DataExpiration { get; set; }

        public virtual FaturamentoModel Faturamento { get; set; }
    }
}