using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace WebZi.Plataform.Domain.DTO.Transalvador.DAT.Gerar;

public sealed class GerarDATDTO : TransalvadorBaseDTO
{
    private GerarDATDataDTO _data;

    [JsonProperty("data")]
    [JsonPropertyName("data")]
    public GerarDATDataDTO Data
    {
        get => _data;
        set => _data = value;
    }

    [JsonProperty("dados")]
    [JsonPropertyName("dados")]
    public GerarDATDataDTO Dados
    {
        get => _data;
        set => _data = value;
    }
}