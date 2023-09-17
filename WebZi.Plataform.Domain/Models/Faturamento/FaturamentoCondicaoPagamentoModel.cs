namespace WebZi.Plataform.Domain.Models.Faturamento
{
    public class FaturamentoCondicaoPagamentoModel
    {
        public byte FaturamentoCondicaoPagamentoId { get; set; }

        public string Descricao { get; set; }

        public virtual ICollection<FaturamentoTipoComposicaoModel> FaturamentoTiposComposicoes { get; set; }
    }
}