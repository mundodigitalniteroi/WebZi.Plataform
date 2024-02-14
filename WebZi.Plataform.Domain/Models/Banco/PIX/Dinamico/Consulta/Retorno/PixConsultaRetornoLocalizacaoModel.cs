using Newtonsoft.Json;

namespace WebZi.Plataform.Domain.Models.Banco.PIX.Dinamico.Consulta.Retorno
{
    public class PixConsultaRetornoLocalizacaoModel
    {
        public int Id { get; set; }

        public string Location { get; set; }

        [JsonProperty("tipoCob")]
        public string TipoCobranca { get; set; }
    }
}