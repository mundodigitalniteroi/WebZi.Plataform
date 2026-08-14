using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Atendimento;

public class SaidaParaReparoDTO
{
    public MensagemDTO Mensagem { get; set; } = new();
    public int? IdentificadorFaturamento { get; set; }
}