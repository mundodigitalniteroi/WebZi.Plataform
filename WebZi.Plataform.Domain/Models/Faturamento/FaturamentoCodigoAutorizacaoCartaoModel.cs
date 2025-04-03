namespace WebZi.Plataform.Domain.Models.Faturamento
{
    public class FaturamentoCodigoAutorizacaoCartaoModel
    {
        public int FaturamentoCodigoAutorizacaoCartaoId { get; set; }

        public int FaturamentoId { get; set; }

        public int CartaoId { get; set; }

        public string CodigoAutorizacaoCartao { get; set; }

        public decimal Valor { get; set; }

        public string NumeroCartao { get; set; }

        public virtual FaturamentoModel Faturamento { get; set; }
    }
}