using WebZi.Plataform.Domain.Models.Usuario;

namespace WebZi.Plataform.Domain.Models.GRV.SolicitacaoReboque;

public class SolicitacaoReboqueLacreModel
{
    public int Id { get; set; }
    public int SolicitacaoReboqueId { get; set; }
    public int? LacreMotivoDesassociacaoId { get; set; }
    public int UsuarioCadastroId { get; set; }
    public int? UsuarioAtualizacaoId { get; set; }
    public string Lacre { get; set; }
    public string? LacreAnterior { get; set; }
    public DateTime DataCadastro { get; set; }
    public DateTime? DataAtualizacao { get; set; }

    public SolicitacaoReboqueModel SolicitacaoReboque { get; set; }
    public UsuarioModel UsuarioCadastro { get; set; }
    public UsuarioModel? UsuarioAtualizacao { get; set; }
}
