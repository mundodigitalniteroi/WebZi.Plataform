using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.GRV.SolicitacoesReboque;

public class SolicitacaoEmAndamentoDTO
{
    public MensagemDTO Mensagem { get; set; } = new();

    public bool SolicitacaoEmAndamento { get; set; }
}