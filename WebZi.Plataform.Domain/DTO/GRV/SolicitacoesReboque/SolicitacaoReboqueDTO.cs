using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.GRV.SolicitacoesReboque;

public class SolicitacaoReboqueDTO : SolicitacaoReboqueResumoDTO
{
    public MensagemDTO Mensagem { get; set; } = new();

    public string? Renavam { get; set; }
    public string? VeiculoUF { get; set; }
    public byte? TipoVeiculoId { get; set; }
    public int? CorId { get; set; }
    public int? MarcaModeloId { get; set; }

    public int? AutoridadeResponsavelId { get; set; }
    public string? MatriculaAutoridadeResponsavel { get; set; }
    public string? NomeAutoridadeResponsavel { get; set; }

    public SolicitacaoReboqueCondutorDTO? Condutor { get; set; }
    public List<SolicitacaoReboqueEnquadramentoInfracaoDTO>? ListagemEnquadramentoInfracao { get; set; }
    public List<string>? ListagemLacre { get; set; }
    public List<string>? ListagemFoto { get; set; }
}

public class SolicitacaoReboqueCondutorDTO
{
    public int Id { get; set; }
    public int? PessoaId { get; set; }
    public int? EnquadramentoInfracaoId { get; set; }
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
    public string? StatusAssinaturaCondutor { get; set; }
    public string? FlagChaveVeiculo { get; set; }
    public string? FlagDocumentacaoVeiculo { get; set; }
    public string? Celular { get; set; }
    public string? CelularDDD { get; set; }
}

public class SolicitacaoReboqueEnquadramentoInfracaoDTO
{
    public int Id { get; set; }
    public int EnquadramentoInfracaoId { get; set; }
    public string? NumeroInfracao { get; set; }
    public string? CodigoInfracao { get; set; }
    public string? DescricaoInfracao { get; set; }
}