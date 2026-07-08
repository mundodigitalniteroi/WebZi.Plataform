using WebZi.Plataform.Domain.DTO.DetranHub.Mensagem;
using WebZi.Plataform.Domain.DTO.DetranHub.ResponseAPI;

namespace WebZi.Plataform.Domain.DTO.DetranHub;

public class ConsultarDetranHubResponse
{
    public bool Sucesso { get; set; }
    public string Fonte { get; set; }
    public int CodigoHttpOrigem { get; set; }
    public int QuantidadeRegistros { get; set; }
    public MensagemDetranHubResponse Mensagens { get; set; }
    public ConsultaDetalhesResponse Consulta { get; set; }
    public VeiculoDetranHubResponse Veiculo { get; set; }
}