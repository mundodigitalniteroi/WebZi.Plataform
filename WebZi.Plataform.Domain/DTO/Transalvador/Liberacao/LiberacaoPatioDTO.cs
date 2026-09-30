using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace WebZi.Plataform.Domain.DTO.Transalvador.Liberacao;

public sealed class LiberacaoPatioDTO : TransalvadorBaseDTO
{
    private LiberacaoPatioDataDTO _data;

    [JsonProperty("data")]
    [JsonPropertyName("data")]
    public LiberacaoPatioDataDTO Data
    {
        get => _data;
        set => _data = value;
    }

    [JsonProperty("dados")]
    [JsonPropertyName("dados")]
    public LiberacaoPatioDataDTO Dados
    {
        get => _data;
        set => _data = value;
    }
}