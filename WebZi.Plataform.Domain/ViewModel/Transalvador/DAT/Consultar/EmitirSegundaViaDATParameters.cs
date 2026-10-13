using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace WebZi.Plataform.Domain.ViewModel.Transalvador.DAT.Consultar;

public sealed class EmitirSegundaViaDATParameters
{
    [JsonProperty("num_dat")]
    [JsonPropertyName("num_dat")]
    public string? NumDat { get; set; }

    [JsonProperty("dat_id")]
    [JsonPropertyName("dat_id")]
    public string? DatId { get; set; }
}
