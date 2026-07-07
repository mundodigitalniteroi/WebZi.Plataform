using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Liberacao;

public class TipoLiberacaoEspecialListDTO
{
    public MensagemDTO Mensagem { get; set; } = new();
    public List<TipoLiberacaoEspecialDTO> Listagem { get; set; } = new();
}