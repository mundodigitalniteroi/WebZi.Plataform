namespace WebZi.Plataform.Domain.ViewModel.Banco.PIX
{
    public class PixEstaticoGeradoViewModel
    {
        public MensagemViewModel Mensagem { get; set; } = new();

        public int PixId { get; set; }

        public string Chave { get; set; }

        public string SolicitacaoPagador { get; set; }

        public decimal Valor { get; set; }

        public string MerchantName { get; set; }

        public string MerchantCity { get; set; }

        public string QRString { get; set; }

        public string QRCode { get; set; }
    }
}