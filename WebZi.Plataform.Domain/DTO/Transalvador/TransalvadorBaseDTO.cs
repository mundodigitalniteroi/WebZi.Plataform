using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace WebZi.Plataform.Domain.DTO.Transalvador;

public class TransalvadorBaseDTO
{
    [JsonProperty("success")]
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonProperty("message")]
    [JsonPropertyName("message")]
    public string Message { get; set; }

    [JsonProperty("errors")]
    [JsonPropertyName("errors")]
    public object Errors { get; set; }
}

public class TransalvadorResponseDTO<T> : TransalvadorBaseDTO
{
    [JsonProperty("data")]
    [JsonPropertyName("data")]
    public T Data { get; set; }
}
