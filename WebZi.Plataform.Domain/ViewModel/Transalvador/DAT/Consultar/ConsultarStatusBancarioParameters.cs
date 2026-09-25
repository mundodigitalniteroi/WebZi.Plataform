using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace WebZi.Plataform.Domain.ViewModel.Transalvador.DAT.Consultar;
public class ConsultarStatusBancarioParameters

{
    [JsonProperty("numero_dat")]
    [JsonPropertyName("numero_dat")]
    public string? NumeroDat { get; set; }

    [JsonProperty("receita_id")]
    [JsonPropertyName("receita_id")]
    public int? ReceitaId { get; set; }
}