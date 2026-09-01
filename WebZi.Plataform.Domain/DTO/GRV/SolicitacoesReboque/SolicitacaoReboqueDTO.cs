using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.GRV.SolicitacoesReboque;

public class SolicitacaoReboqueDTO
{
    public MensagemDTO Mensagem { get; set; } = new();
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public int DepositoId { get; set; }
    public int? GrvId { get; set; }
    public string? NumeroFormularioGrv { get; set; }
    public string? Placa { get; set; }
    public string? Chassi { get; set; }

    public int? ReboqueId { get; set; }
    public string? ReboquePlaca { get; set; }

    public int? ReboquistaId { get; set; }
    public string? ReboquistaNome { get; set; }

    public byte SolicitacaoReboqueTipoId { get; set; }
    public string? SolicitacaoReboqueTipoDescricao { get; set; }

    public byte SolicitacaoReboqueStatusId { get; set; }
    public string? SolicitacaoReboqueStatusDescricao { get; set; }

    public string? LocalRemocaoCompleto { get; set; }
    public string? LocalRemocaoReferencia { get; set; }
    public string? LocalRemocaoLatitude { get; set; }
    public string? LocalRemocaoLongitude { get; set; }

    public int UsuarioCadastroId { get; set; }
    public string? UsuarioCadastroNome { get; set; }

    public DateTime DataCadastro { get; set; }

    public int? UsuarioAlteracaoId { get; set; }
    public string? UsuarioAlteracaoNome { get; set; }

    public DateTime? DataAlteracao { get; set; }
}