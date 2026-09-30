using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Banco;

public class DATDTO
{
    public MensagemDTO Mensagem { get; set; } = new();

    #region Dados de Identificação
    public int? IdentificadorAtendimento { get; set; }
    public int? IdentificadorFaturamento { get; set; }
    #endregion Dados de Identificação

    #region Dados do Documento DAT
    public string TituloDocumento { get; set; }
    public string NumeroDocumentoDat { get; set; }
    public string NumeroProcesso { get; set; }
    public string DataDocumento { get; set; }
    public string DataVencimento { get; set; }
    public string Referencia { get; set; }
    public string TipoSaldo { get; set; }
    public string PdfUrl { get; set; }
    #endregion Dados do Documento DAT

    #region Dados do Cedente
    public string CedenteRazaoSocial { get; set; }
    public string CedenteSigla { get; set; }
    public string CedenteCnpj { get; set; }
    #endregion Dados do Cedente

    #region Dados do Sacado / Contribuinte
    public string SacadoNome { get; set; }
    public string SacadoCpfCnpj { get; set; }
    public string SacadoEndereco { get; set; }
    public string SacadoCep { get; set; }
    public string SacadoCidade { get; set; }
    public string SacadoUf { get; set; }
    #endregion Dados do Sacado / Contribuinte

    #region Dados do Serviço
    public int? ServicoCodigo { get; set; }
    public string ServicoDescricao { get; set; }
    public string ServicoRotuloCompleto { get; set; }
    #endregion Dados do Serviço

    #region Discriminação do ISS
    public bool? PossuiIss { get; set; }
    public decimal? IssAliquotaPercentual { get; set; }
    public decimal? IssPercentualBaseServico { get; set; }
    public decimal? IssValorBaseServicoDesconto { get; set; }
    public decimal? IssValorRetido { get; set; }
    public string IssTextoReferencia { get; set; }
    #endregion Discriminação do ISS

    #region Valores do Documento
    public decimal? ValorDocumento { get; set; }
    public decimal? DescontoAbatimento { get; set; }
    public decimal? OutrasDeducoes { get; set; }
    public decimal? JurosMultas { get; set; }
    public decimal? OutrosAcrescimos { get; set; }
    public decimal? ValorCobrado { get; set; }
    #endregion Valores do Documento

    #region Dados para Pagamento e Arrecadação
    public string LinhaDigitavel { get; set; }
    public string CodigoBarras { get; set; }
    public string PixCopiaECola { get; set; }
    public string TxId { get; set; }
    public string QrCodeBase64 { get; set; }
    public byte[] QrCode { get; set; }
    #endregion Dados para Pagamento e Arrecadação

    #region Instruções
    public string Instrucoes { get; set; }
    #endregion Instruções
}