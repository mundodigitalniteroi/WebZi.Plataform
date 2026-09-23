using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace WebZi.Plataform.Domain.DTO.Transalvador.Entrada;

public class EntradaPatioDataDTO
{
    [JsonProperty("id")]
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonProperty("placa")]
    [JsonPropertyName("placa")]
    public string Placa { get; set; }

    [JsonProperty("data_entrada")]
    [JsonPropertyName("data_entrada")]
    public string DataEntrada { get; set; }
}