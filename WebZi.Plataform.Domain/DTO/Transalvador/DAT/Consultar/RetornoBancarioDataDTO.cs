using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace WebZi.Plataform.Domain.DTO.Transalvador.DAT.Consultar;

public sealed class RetornoBancarioDataDTO
{
    [JsonProperty("numero_dat")]
    [JsonPropertyName("numero_dat")]
    public string NumeroDat { get; set; }

    [JsonProperty("receita_id")]
    [JsonPropertyName("receita_id")]
    public int ReceitaId { get; set; }

    [JsonProperty("receita_descricao")]
    [JsonPropertyName("receita_descricao")]
    public string ReceitaDescricao { get; set; }

    [JsonProperty("cpf_cnpj")]
    [JsonPropertyName("cpf_cnpj")]
    public string CpfCnpj { get; set; }

    [JsonProperty("nome_requerente")]
    [JsonPropertyName("nome_requerente")]
    public string NomeRequerente { get; set; }

    [JsonProperty("valor")]
    [JsonPropertyName("valor")]
    public decimal Valor { get; set; }

    [JsonProperty("data_vencimento")]
    [JsonPropertyName("data_vencimento")]
    public string DataVencimento { get; set; }

    [JsonProperty("data_pagamento")]
    [JsonPropertyName("data_pagamento")]
    public string DataPagamento { get; set; }

    [JsonProperty("status")]
    [JsonPropertyName("status")]
    public string Status { get; set; }
}