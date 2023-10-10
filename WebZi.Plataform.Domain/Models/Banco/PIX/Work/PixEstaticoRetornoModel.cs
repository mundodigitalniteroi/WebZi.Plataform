namespace WebZi.Plataform.Domain.Models.Banco.PIX.Work
{
    public class PixEstaticoRetornoModel : PixEstaticoEnvioModel
    {
        public int PixId { get; set; }

        public string QrString { get; set; }

        public string QrCode { get; set; }

        public string Location { get; set; }
    }
}