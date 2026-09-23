using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace WebZi.Plataform.Domain.ViewModel.Transalvador.Entrada;

public sealed class EntradaPatioParameters
{
    [JsonProperty("placa")]
    [JsonPropertyName("placa")]
    public string Placa { get; set; }

    [JsonProperty("marca_modelo")]
    [JsonPropertyName("marca_modelo")]
    public string MarcaModelo { get; set; }

    [JsonProperty("uf")]
    [JsonPropertyName("uf")]
    public string Uf { get; set; }

    [JsonProperty("tipo_veiculo")]
    [JsonPropertyName("tipo_veiculo")]
    public string TipoVeiculo { get; set; }

    [JsonProperty("data_entrada")]
    [JsonPropertyName("data_entrada")]
    public DateTime DataEntrada { get; set; }

    [JsonProperty("cod_guincho")]
    [JsonPropertyName("cod_guincho")]
    public string IdReboque { get; set; }

    [JsonProperty("numero_trrv")]
    [JsonPropertyName("numero_trrv")]
    public string NumeroProcesso { get; set; }

    [JsonProperty("id_patio")]
    [JsonPropertyName("id_patio")]
    public int IdPatio { get; set; }

    [JsonProperty("id_motivo")]
    [JsonPropertyName("id_motivo")]
    public int IdMotivo { get; set; }
}