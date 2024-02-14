namespace WebZi.Plataform.Domain.Models.Banco.PIX.Dinamico.Geracao.Envio
{
    public class PixDinamicoEnvioParametroModel
    {
        public string BaseUrl { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string Certificate { get; set; }

        public string SenhaCertificado { get; set; }

        public string Banco { get; set; }
    }
}