namespace WebZi.Plataform.Domain.Models.Banco.PIX.Work
{
    public class PixEstaticoEnvioModel
    {
        public string Chave { get; set; }

        public string SolicitacaoPagador { get; set; }

        public PixEstaticoEnvioValorModel Valor { get; set; } = new PixEstaticoEnvioValorModel();

        public PixEstaticoEnvioMerchantModel Merchant { get; set; } = new PixEstaticoEnvioMerchantModel();
    }
}