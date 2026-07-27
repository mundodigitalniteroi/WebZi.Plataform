using WebZi.Plataform.Domain.DTO.DetranHub.Mensagem;
using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Liberacao;

public class GrvConsultaLiberacaoDTO
{
    public MensagemDTO Mensagem { get; set; } = new();
    public string DataLiberacao { get; set; }
    public string Status { get; set; }
}