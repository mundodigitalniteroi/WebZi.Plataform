namespace WebZi.Plataform.Domain.Models.GRV.SolicitacaoReboque;

public class SolicitacaoReboqueEnquadramentoInfracaoModel
{
    public int Id { get; set; }
    public int SolicitacaoReboqueGrvId { get; set; }
    public decimal EnquadramentoInfracaoId { get; set; }
    public string NumeroInfracao { get; set; }

    public SolicitacaoReboqueGrvModel SolicitacaoReboqueGrv { get; set; }
    public EnquadramentoInfracaoModel EnquadramentoInfracao { get; set; }
}
