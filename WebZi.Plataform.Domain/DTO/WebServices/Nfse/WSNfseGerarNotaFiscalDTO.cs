using Newtonsoft.Json;
using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.WebServices.Nfse
{
    public class WSNfseGerarNotaFiscalDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();

        [JsonProperty("cnpj_prestador")]
        public string CnpjPrestador { get; set; }
        [JsonProperty("ref")]
        public string Ref { get; set; }
        [JsonProperty("numero_rps")]
        public string NumeroRps { get; set; }
        [JsonProperty("serie_rps")]
        public string SerieRps { get; set; }
        [JsonProperty("tipo_rps")]
        public string TipoRps { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
