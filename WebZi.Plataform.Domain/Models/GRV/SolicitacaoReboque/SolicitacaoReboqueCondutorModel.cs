using WebZi.Plataform.Domain.Models.Pessoa;

namespace WebZi.Plataform.Domain.Models.GRV.SolicitacaoReboque;

public class SolicitacaoReboqueCondutorModel
{
    public int Id { get; set; }
    public int SolicitacaoReboqueGrvId { get; set; }
    public long? PessoaId { get; set; }
    public decimal? EnquadramentoInfracaoId { get; set; }
    public string? Documento { get; set; }
    public string? Identidade { get; set; }
    public string? OrgaoExpedidor { get; set; }
    public string? Nome { get; set; }
    public string? Telefone { get; set; }
    public string? TelefoneDDD { get; set; }
    public string? Email { get; set; }
    public string? NumeroChaveVeiculo { get; set; }
    public string? NumeroInfracao { get; set; }
    public string? InformacoesAdicionais { get; set; }
    public string? OutrosEquipamentos1 { get; set; }
    public string? OutrosEquipamentos2 { get; set; }
    public string? OutrosEquipamentos3 { get; set; }
    public string? OutrosEquipamentos4 { get; set; }
    public string? OutrosEquipamentos5 { get; set; }
    public string? StatusAssinaturaCondutor { get; set; }
    public string? FlagChaveVeiculo { get; set; }
    public string? FlagDocumentacaoVeiculo { get; set; }
    public string? Celular { get; set; }
    public string? CelularDDD { get; set; }

    public SolicitacaoReboqueGrvModel SolicitacaoReboqueGrv { get; set; }
    public PessoaModel? Pessoa { get; set; }
    public EnquadramentoInfracaoModel? EnquadramentoInfracao { get; set; }
}
