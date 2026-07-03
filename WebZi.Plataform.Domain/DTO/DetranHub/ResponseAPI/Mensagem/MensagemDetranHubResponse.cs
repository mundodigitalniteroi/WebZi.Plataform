namespace WebZi.Plataform.Domain.DTO.DetranHub.Mensagem;

public class MensagemDetranHubResponse
{
    public List<string> Informativos { get; set; }
    public List<string> Alertas { get; set; }
    public List<string> Impeditivas { get; set; }
    public List<string> Erros { get; set; }
}