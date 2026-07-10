using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.WebServices.Nfe;

public class NfeJsonEnvioDTO
{
    public MensagemDTO Mensagem { get; set; } = new();
    public string Json { get; set; }
}