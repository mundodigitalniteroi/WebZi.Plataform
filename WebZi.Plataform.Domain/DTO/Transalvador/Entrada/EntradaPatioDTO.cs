using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace WebZi.Plataform.Domain.DTO.Transalvador.Entrada;

public sealed class EntradaPatioDTO : TransalvadorBaseDTO
{
    [JsonProperty("data")]
    [JsonPropertyName("data")]
    public EntradaPatioDataDTO Data { get; set; }
}