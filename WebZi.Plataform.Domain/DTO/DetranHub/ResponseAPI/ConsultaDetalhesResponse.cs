using WebZi.Plataform.Domain.DTO.DetranHub.ResponseAPI;

namespace WebZi.Plataform.Domain.DTO.DetranHub;

public class ConsultaDetalhesResponse
{
    public string Tipo { get; set; }
    public string Valor { get; set; }
    public DateTime DataHora { get; set; }
    public VeiculoDetranHubResponse VeiculoDetranHub { get; set; }
}