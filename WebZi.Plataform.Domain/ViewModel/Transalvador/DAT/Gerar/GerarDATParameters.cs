using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace WebZi.Plataform.Domain.ViewModel.Transalvador.DAT.Gerar;

public sealed class GerarDATParameters
{
    [JsonProperty("cod_fonte_receita")]
    [JsonPropertyName("cod_fonte_receita")]
    public string CodFonteReceita { get; set; } = "04";

    [JsonProperty("id_vpa")]
    [JsonPropertyName("id_vpa")]
    public string IdVpa { get; set; }

    [JsonProperty("nome_requerente")]
    [JsonPropertyName("nome_requerente")]
    public string NomeRequerente { get; set; }

    [JsonProperty("cpf")]
    [JsonPropertyName("cpf")]
    public string Cpf { get; set; }

    [JsonProperty("email")]
    [JsonPropertyName("email")]
    public string? Email { get; set; } = null;

    [JsonProperty("valor")]
    [JsonPropertyName("valor")]
    public decimal Valor { get; set; }

    [JsonProperty("receita_id")]
    [JsonPropertyName("receita_id")]
    public int ReceitaId { get; set; }

    /// <summary>
    /// Data de vencimento do DAT (formato: dd/MM/yyyy).
    /// Se não for informada, o vencimento padrão é de 3 dias.
    /// </summary>
    [JsonProperty("data_vencimento")]
    [JsonPropertyName("data_vencimento")]
    public string? DataVencimento { get; set; } = null;

    [JsonProperty("referencia")]
    [JsonPropertyName("referencia")]
    public string? Referencia { get; set; } = null;

    [JsonProperty("num_processo")]
    [JsonPropertyName("num_processo")]
    public string? NumeroProcesso { get; set; } = null;

    [JsonProperty("external_reference")]
    [JsonPropertyName("external_reference")]
    public string? ExternalReference { get; set; } = null;
}