namespace WebZi.Plataform.Domain.Models.Faturamento.Boleto
{
    public class FaturamentoBoletoGeradoModel
    {
        public byte[] Boleto { get; set; }

        public int BoletoId { get; set; }

        public int DiasConfiguracaoDataVencimento { get; set; }

        public DateTime DataVencimento { get; set; }

        public string Linha { get; set; }
    }
}