using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Usuario;

public class PerfilAcessoListDTO
{
    public MensagemDTO Mensagem { get; set; } = new();
    public List<PerfilAcessoDTO> Listagem { get; set; }
}