namespace WebZi.Plataform.Domain.Models.WebServices.Boleto
{
    public class BoletoImagemModel
    {
        public int FaturamentoBoletoImagemId { get; set; }

        public int FaturamentoBoletoId { get; set; }

        public byte[] Imagem { get; set; }

        public virtual BoletoModel FaturamentoBoleto { get; set; }
    }
}