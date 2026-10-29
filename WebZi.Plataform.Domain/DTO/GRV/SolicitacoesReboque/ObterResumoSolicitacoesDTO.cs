using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.GRV.SolicitacoesReboque;

public class ObterResumoSolicitacoesDTO
{
    public MensagemDTO Mensagem { get; set; } = new();
    public int SolicitacoesDisponiveis { get; set; }
    public int SolicitacoesConcluidasHoje { get; set; }
    public int SolicitacoesConcluidasNaSemana { get; set; }
    public int SolicitacoesConcluidasNoMes { get; set; }
}