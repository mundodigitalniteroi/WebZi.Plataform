using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace WebZi.Plataform.Domain.ViewModel.Transalvador.Liberacao;

public sealed class LiberacaoPatioParameters
{
    [JsonProperty("id")]
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonProperty("data_liberacao")]
    [JsonPropertyName("data_liberacao")]
    public DateTime DataLiberacao { get; set; }

    [JsonProperty("valor_diaria")]
    [JsonPropertyName("valor_diaria")]
    public decimal ValorDiaria { get; set; }

    [JsonProperty("valor_guincho")]
    [JsonPropertyName("valor_guincho")]
    public decimal ValorGuincho { get; set; }

    [JsonProperty("num_dias")]
    [JsonPropertyName("num_dias")]
    public int NumDias { get; set; }

    [JsonProperty("desconto")]
    [JsonPropertyName("desconto")]
    public decimal Desconto { get; set; }

    [JsonProperty("valor_total")]
    [JsonPropertyName("valor_total")]
    public decimal ValorTotal { get; set; }
}