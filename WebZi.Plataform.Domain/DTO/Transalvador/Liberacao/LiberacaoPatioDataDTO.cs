using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace WebZi.Plataform.Domain.DTO.Transalvador.Liberacao;

public class LiberacaoPatioDataDTO
{
    [JsonProperty("id")]
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonProperty("data_liberacao")]
    [JsonPropertyName("data_liberacao")]
    public DateTime DataEntrada { get; set; }

    [JsonProperty("valor_diaria")]
    [JsonPropertyName("valor_diaria")]
    public decimal ValorDiaria { get; set; }

    [JsonProperty("valor_guincho")]
    [JsonPropertyName("valor_guincho")]
    public decimal ValorGuincho { get; set; }

    [JsonProperty("desconto")]
    [JsonPropertyName("desconto")]
    public int Desconto { get; set; }

    [JsonProperty("valor_total")]
    [JsonPropertyName("valor_total")]
    public decimal ValorTotal { get; set; }
}