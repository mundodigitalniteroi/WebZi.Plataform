using WebZi.Plataform.Domain.DTO.DetranHub.ResponseAPI;
using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.DetranHub;

public class ConsultarPorPlacaOuChassiDTO
{
    public MensagemDTO Mensagem { get; set; } = new();
    public VeiculoDetranHubResponse Veiculo { get; set; }
}