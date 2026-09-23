using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace WebZi.Plataform.Domain.DTO.Transalvador.DAT.Gerar;

public sealed class GerarDATDataDTO
{
    [JsonProperty("titulo_documento")]
    [JsonPropertyName("titulo_documento")]
    public string TituloDocumento { get; set; }

    [JsonProperty("numero_documento_dat")]
    [JsonPropertyName("numero_documento_dat")]
    public string NumeroDocumentoDat { get; set; }

    [JsonProperty("numero_processo")]
    [JsonPropertyName("numero_processo")]
    public string NumeroProcesso { get; set; }

    [JsonProperty("data_documento")]
    [JsonPropertyName("data_documento")]
    public string DataDocumento { get; set; }

    [JsonProperty("data_vencimento")]
    [JsonPropertyName("data_vencimento")]
    public string DataVencimento { get; set; }

    [JsonProperty("cedente")]
    [JsonPropertyName("cedente")]
    public CedenteDTO Cedente { get; set; }

    [JsonProperty("sacado")]
    [JsonPropertyName("sacado")]
    public SacadoDTO Sacado { get; set; }

    [JsonProperty("servico")]
    [JsonPropertyName("servico")]
    public ServicoDTO Servico { get; set; }

    [JsonProperty("discriminacao_iss")]
    [JsonPropertyName("discriminacao_iss")]
    public DiscriminacaoIssDTO DiscriminacaoIss { get; set; }

    [JsonProperty("valores")]
    [JsonPropertyName("valores")]
    public ValoresDTO Valores { get; set; }

    [JsonProperty("pagamento")]
    [JsonPropertyName("pagamento")]
    public PagamentoDTO Pagamento { get; set; }

    [JsonProperty("instrucoes")]
    [JsonPropertyName("instrucoes")]
    public string Instrucoes { get; set; }

    [JsonProperty("referencia")]
    [JsonPropertyName("referencia")]
    public string Referencia { get; set; }

    [JsonProperty("pdf_url")]
    [JsonPropertyName("pdf_url")]
    public string PdfUrl { get; set; }
}

public sealed class CedenteDTO
{
    [JsonProperty("razao_social")]
    [JsonPropertyName("razao_social")]
    public string RazaoSocial { get; set; }

    [JsonProperty("sigla")]
    [JsonPropertyName("sigla")]
    public string Sigla { get; set; }

    [JsonProperty("cnpj")]
    [JsonPropertyName("cnpj")]
    public string Cnpj { get; set; }
}

public sealed class SacadoDTO
{
    [JsonProperty("nome")]
    [JsonPropertyName("nome")]
    public string Nome { get; set; }

    [JsonProperty("cpf_cnpj")]
    [JsonPropertyName("cpf_cnpj")]
    public string CpfCnpj { get; set; }

    [JsonProperty("endereco")]
    [JsonPropertyName("endereco")]
    public string Endereco { get; set; }

    [JsonProperty("cep")]
    [JsonPropertyName("cep")]
    public string Cep { get; set; }

    [JsonProperty("cidade")]
    [JsonPropertyName("cidade")]
    public string Cidade { get; set; }

    [JsonProperty("uf")]
    [JsonPropertyName("uf")]
    public string Uf { get; set; }
}

public sealed class ServicoDTO
{
    [JsonProperty("codigo")]
    [JsonPropertyName("codigo")]
    public string Codigo { get; set; }

    [JsonProperty("descricao")]
    [JsonPropertyName("descricao")]
    public string Descricao { get; set; }

    [JsonProperty("rotulo_completo")]
    [JsonPropertyName("rotulo_completo")]
    public string RotuloCompleto { get; set; }
}

public sealed class DiscriminacaoIssDTO
{
    [JsonProperty("possui_iss")]
    [JsonPropertyName("possui_iss")]
    public bool? PossuiIss { get; set; }

    [JsonProperty("aliquota_percentual")]
    [JsonPropertyName("aliquota_percentual")]
    public decimal? AliquotaPercentual { get; set; }

    [JsonProperty("percentual_base_servico")]
    [JsonPropertyName("percentual_base_servico")]
    public decimal? PercentualBaseServico { get; set; }

    [JsonProperty("valor_iss_retido")]
    [JsonPropertyName("valor_iss_retido")]
    public decimal? ValorIssRetido { get; set; }

    [JsonProperty("texto_referencia")]
    [JsonPropertyName("texto_referencia")]
    public string TextoReferencia { get; set; }
}

public sealed class ValoresDTO
{
    [JsonProperty("valor_documento")]
    [JsonPropertyName("valor_documento")]
    public decimal? ValorDocumento { get; set; }

    [JsonProperty("desconto_abatimento")]
    [JsonPropertyName("desconto_abatimento")]
    public decimal? DescontoAbatimento { get; set; }

    [JsonProperty("outras_deducoes")]
    [JsonPropertyName("outras_deducoes")]
    public decimal? OutrasDeducoes { get; set; }

    [JsonProperty("juros_multas")]
    [JsonPropertyName("juros_multas")]
    public decimal? JurosMultas { get; set; }

    [JsonProperty("outros_acrescimos")]
    [JsonPropertyName("outros_acrescimos")]
    public decimal? OutrosAcrescimos { get; set; }

    [JsonProperty("valor_cobrado")]
    [JsonPropertyName("valor_cobrado")]
    public decimal? ValorCobrado { get; set; }
}

public sealed class PagamentoDTO
{
    [JsonProperty("linha_digitavel")]
    [JsonPropertyName("linha_digitavel")]
    public string LinhaDigitavel { get; set; }

    [JsonProperty("codigo_barras")]
    [JsonPropertyName("codigo_barras")]
    public string CodigoBarras { get; set; }

    [JsonProperty("pix_copia_e_cola")]
    [JsonPropertyName("pix_copia_e_cola")]
    public string PixCopiaECola { get; set; }

    [JsonProperty("txid")]
    [JsonPropertyName("txid")]
    public string TxId { get; set; }

    [JsonProperty("qr_code_base64")]
    [JsonPropertyName("qr_code_base64")]
    public string QrCodeBase64 { get; set; }
}