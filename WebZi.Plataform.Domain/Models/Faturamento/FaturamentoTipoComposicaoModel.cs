namespace WebZi.Plataform.Domain.Models.Faturamento
{
    public class FaturamentoTipoComposicaoModel
    {
        public byte FaturamentoTipoComposicaoId { get; set; }

        public byte? FaturamentoCondicaoPagamentoId { get; set; }

        public string Origem { get; set; }

        public string Tipo { get; set; }

        public string CodigoSap { get; set; }

        public string Descricao { get; set; }

        public string DescricaoSap { get; set; }

        public virtual FaturamentoCondicaoPagamentoModel FaturamentoCondicaoPagamento { get; set; }
    }
}