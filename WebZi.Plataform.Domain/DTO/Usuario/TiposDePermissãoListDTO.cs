using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Usuario;

public class TiposDePermissãoListDTO
{
    public MensagemDTO Mensagem { get; set; } = new();
    public List<TipoPermissaoDTO> Listagem { get; set; }
}