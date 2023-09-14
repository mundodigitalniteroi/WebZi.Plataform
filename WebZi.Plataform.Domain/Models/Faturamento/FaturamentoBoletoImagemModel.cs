namespace WebZi.Plataform.Domain.Models.Faturamento
{
    public class FaturamentoBoletoImagemModel
    {
        public int IdFaturamentoBoletoImagem { get; set; }

        public int IdFaturamentoBoleto { get; set; }

        public byte[] Imagem { get; set; }

        public virtual FaturamentoBoletoModel FaturamentoBoleto { get; set; }
    }
}