using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace WebZi.Plataform.Domain.ViewModel.Transalvador.DAT.Gerar;

public sealed class GerarDATParameters
{
    [JsonProperty("cod_fonte_receita")]
    [JsonPropertyName("cod_fonte_receita")]
    public int CodFonteReceita { get; set; }

    [JsonProperty("nome_requerente")]
    [JsonPropertyName("nome_requerente")]
    public string NomeRequerente { get; set; }

    [JsonProperty("cpf")]
    [JsonPropertyName("cpf")]
    public string Cpf { get; set; }

    [JsonProperty("valor")]
    [JsonPropertyName("valor")]
    public decimal Valor { get; set; }

    [JsonProperty("receita_id")]
    [JsonPropertyName("receita_id")]
    public int ReceitaId { get; set; }
}