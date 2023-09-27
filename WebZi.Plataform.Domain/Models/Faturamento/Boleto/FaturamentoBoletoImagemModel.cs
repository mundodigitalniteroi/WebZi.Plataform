namespace WebZi.Plataform.Domain.Models.Faturamento.Boleto
{
    public class FaturamentoBoletoImagemModel
    {
        public int FaturamentoBoletoImagemId { get; set; }

        public int FaturamentoBoletoId { get; set; }

        public byte[] Imagem { get; set; }

        public virtual FaturamentoBoletoModel FaturamentoBoleto { get; set; }
    }
}