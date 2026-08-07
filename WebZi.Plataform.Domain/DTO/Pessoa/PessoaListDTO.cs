using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Pessoa;

public class PessoaListDTO
{
    public MensagemDTO Mensagem { get; set; } = new();
    public List<PessoaDTO> Listagem { get; set; }
}