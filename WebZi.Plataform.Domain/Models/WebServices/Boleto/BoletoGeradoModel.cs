namespace WebZi.Plataform.Domain.Models.WebServices.Boleto
{
    public class BoletoGeradoModel
    {
        public byte[] Boleto { get; set; }

        public int BoletoId { get; set; }

        public int DiasConfiguracaoDataVencimento { get; set; }

        public DateTime DataVencimento { get; set; }

        public string Linha { get; set; }
    }
}