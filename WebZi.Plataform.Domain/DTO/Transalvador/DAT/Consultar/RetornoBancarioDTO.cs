using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace WebZi.Plataform.Domain.DTO.Transalvador.DAT.Consultar;

public sealed class RetornoBancarioDTO : TransalvadorBaseDTO
{
    [JsonProperty("data")]
    [JsonPropertyName("data")]
    public RetornoBancarioDataDTO Data { get; set; }
}