using WebZi.Plataform.Domain.Models.ClienteDeposito;
using WebZi.Plataform.Domain.Models.Servico;
using WebZi.Plataform.Domain.Models.Usuario;

namespace WebZi.Plataform.Domain.Models.GRV.SolicitacaoReboque;

public class SolicitacaoReboqueModel
{
    public int Id { get; set; }
    public int ClienteDepositoId { get; set; }
    public int? ReboqueId { get; set; }
    public int? ReboquistaId { get; set; }
    public byte SolicitacaoReboqueTipoId { get; set; }
    public byte SolicitacaoReboqueStatusId { get; set; }
    public int? GrvId { get; set; }
    public int UsuarioCadastroId { get; set; }
    public int? UsuarioAlteracaoId { get; set; }
    public string? LocalRemocaoCompleto { get; set; }
    public string? LocalRemocaoReferencia { get; set; }
    public string? LocalRemocaoLatitude { get; set; }
    public string? LocalRemocaoLongitude { get; set; }
    public DateTime DataCadastro { get; set; }
    public DateTime? DataAlteracao { get; set; }

    public GrvModel Grv { get; set; }
    
    public UsuarioModel UsuarioCadastro { get; set; }
    public UsuarioModel UsuarioAlteracao { get; set; }
    
    public ClienteDepositoModel ClienteDeposito { get; set; }
    
    public ReboqueModel Reboque { get; set; }
    public ReboquistaModel Reboquista { get; set; }

    public SolicitacaoReboqueTipoModel SolicitacaoReboqueTipo { get; set; }
    public SolicitacaoReboqueStatusModel SolicitacaoReboqueStatus { get; set; }
}