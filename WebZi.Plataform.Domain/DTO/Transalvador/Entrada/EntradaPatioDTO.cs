using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace WebZi.Plataform.Domain.DTO.Transalvador.Entrada;

public sealed class EntradaPatioDTO : TransalvadorBaseDTO
{
    private EntradaPatioDataDTO _data;

    [JsonProperty("data")]
    [JsonPropertyName("data")]
    public EntradaPatioDataDTO Data
    {
        get => _data;
        set => _data = value;
    }

    [JsonProperty("dados")]
    [JsonPropertyName("dados")]
    public EntradaPatioDataDTO Dados
    {
        get => _data;
        set => _data = value;
    }
}