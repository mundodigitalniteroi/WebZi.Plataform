using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace WebZi.Plataform.Domain.DTO.Transalvador.Liberacao;

public sealed class LiberacaoPatioDTO : TransalvadorBaseDTO
{
    [JsonProperty("data")]
    [JsonPropertyName("data")]
    public LiberacaoPatioDataDTO Data { get; set; }
}