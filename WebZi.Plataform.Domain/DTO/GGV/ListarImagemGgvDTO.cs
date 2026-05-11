using WebZi.Plataform.Domain.DTO.Generic;
using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.GGV;

public class ListarImagemGgvDTO
{
    public MensagemDTO Mensagem { get; set; } = new();

    public List<ImageGgvDTO> Listagem { get; set; }
}