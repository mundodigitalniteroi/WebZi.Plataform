using Newtonsoft.Json;

namespace WebZi.Plataform.Domain.Models.Banco.PIX.Dinamico.Geracao.Retorno
{
    public class PixDinamicoRetornoModel
    {
        [JsonProperty("txId")]
        public string TransactionId { get; set; }

        public int Revisao { get; set; }

        public string Status { get; set; }

        public string Pix { get; set; }

        public string QrString { get; set; }

        public string QrCode { get; set; }

        public PixDinamicoRetornoCalendarioModel Calendario { get; set; }

        public string Devedor { get; set; }

        public string Location { get; set; }

        [JsonProperty("loc")]
        public PixDinamicoRetornoLocationModel LocationAttributes { get; set; }

        public string Chave { get; set; }

        public string SolicitacaoPagador { get; set; }

        public string InfoAdicionais { get; set; }

        public PixDinamicoRetornoValorModel Valor { get; set; }

        public string Merchant { get; set; }
    }
}