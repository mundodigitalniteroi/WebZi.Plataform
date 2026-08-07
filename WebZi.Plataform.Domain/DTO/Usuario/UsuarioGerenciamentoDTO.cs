using WebZi.Plataform.Domain.DTO.Cliente;
using WebZi.Plataform.Domain.DTO.Deposito;
using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Usuario;

public class UsuarioGerenciamentoDTO
{
    public MensagemDTO Mensagem { get; set; } = new();
    public string Login { get; set; }
    public string Nome { get; set; }
    public string Matricula { get; set; }
    public string DataUltimoAcesso { get; set; }
    public string FlagAtivo { get; set; }
    public List<PerfilAcessoDTO> PerfisDeAcessoVinculados { get; set; }
    public List<ClienteVincularUsuarioDTO> ClientesVinculados { get; set; }
    public List<DepositoVincularAUsuariosDTO> DepositosVinculados { get; set; }
}
