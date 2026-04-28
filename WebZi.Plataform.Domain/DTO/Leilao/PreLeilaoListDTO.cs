using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Leilao;

public class PreLeilaoListDTO
{
    public MensagemDTO Mensagem { get; set; } = new();
    public List<PreLeilaoDTO> Listagem { get; set; }
}