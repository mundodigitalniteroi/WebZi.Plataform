using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.GRV.SolicitacoesReboque;

public class SolicitacoesReboqueListDTO
{
    public MensagemDTO Mensagem { get; set; } = new();
    public List<SolicitacaoReboqueDTO> Listagem { get; set; }
}