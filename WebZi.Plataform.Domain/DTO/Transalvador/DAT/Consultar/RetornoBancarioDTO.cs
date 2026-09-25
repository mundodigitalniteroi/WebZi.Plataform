using Newtonsoft.Json;
using System.Text.Json.Serialization;
using WebZi.Plataform.CrossCutting.Web;

namespace WebZi.Plataform.Domain.DTO.Transalvador.DAT.Consultar;

public sealed class RetornoBancarioDTO : TransalvadorBaseDTO
{
    [JsonProperty("dados")]
    [JsonPropertyName("dados")]
    [Newtonsoft.Json.JsonConverter(typeof(SingleOrArrayConverter<RetornoBancarioDataDTO>))]
    public List<RetornoBancarioDataDTO> Dados { get; set; } = new();
}