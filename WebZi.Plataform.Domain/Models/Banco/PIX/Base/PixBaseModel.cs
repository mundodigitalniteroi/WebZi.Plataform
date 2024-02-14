namespace WebZi.Plataform.Domain.Models.Banco.PIX.Base
{
    public class PixBaseModel
    {
        public string Chave { get; set; }

        public string SolicitacaoPagador { get; set; }

        public string InfoAdicionais { get; set; }

        public PixBaseValorModel Valor { get; set; } = new PixBaseValorModel();

        public PixBaseMerchantModel Merchant { get; set; } = new PixBaseMerchantModel();
    }
}