using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace WebZi.Plataform.Domain.DTO.Transalvador.DAT.Gerar;

public sealed class GerarDATDTO : TransalvadorBaseDTO
{
    [JsonProperty("data")]
    [JsonPropertyName("data")]
    public GerarDATDataDTO Data { get; set; }
}