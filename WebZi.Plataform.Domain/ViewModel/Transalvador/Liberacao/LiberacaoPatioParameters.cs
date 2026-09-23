using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace WebZi.Plataform.Domain.ViewModel.Transalvador.Liberacao;

public sealed class LiberacaoPatioParameters
{
    [JsonProperty("id")]
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonProperty("data_liberacao")]
    [JsonPropertyName("data_liberacao")]
    public string DataLiberacao { get; set; }

    [JsonProperty("valor_diaria")]
    [JsonPropertyName("valor_diaria")]
    public decimal ValorDiaria { get; set; }

    [JsonProperty("num_dias")]
    [JsonPropertyName("num_dias")]
    public int NumDias { get; set; }

    [JsonProperty("valor_guincho")]
    [JsonPropertyName("valor_guincho")]
    public decimal ValorGuincho { get; set; }

    [JsonProperty("valor_total")]
    [JsonPropertyName("valor_total")]
    public decimal ValorTotal { get; set; }
}