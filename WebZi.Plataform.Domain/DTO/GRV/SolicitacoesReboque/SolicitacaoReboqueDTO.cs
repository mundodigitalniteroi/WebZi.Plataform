using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.GRV.SolicitacoesReboque;

public class SolicitacaoReboqueDTO
{
    public MensagemDTO Mensagem { get; set; } = new();
    public string NumeroProcesso { get; set; }
    public string Cliente { get; set; }
    public string Deposito { get; set; }
    public SolicitacaoReboqueVeiculoDTO Veiculo { get; set; }
    public string Status { get; set; }
    public string MotivoApreensao { get; set; }
    public string LocalApreenssao { get; set; }
    public DateTime DataCriacaoSolicitacao { get; set; }
    public DateTime? DataSolicitacaoAceita { get; set; }
}
