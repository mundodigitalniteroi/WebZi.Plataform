namespace WebZi.Plataform.Domain.Models.Banco.PIX.Dinamico.Consulta.Envio
{
    public class PixConsultaEnvioParametrosModel
    {
        public string BaseUrl { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string Certificate { get; set; }

        public string SenhaCertificado { get; set; }
    }
}