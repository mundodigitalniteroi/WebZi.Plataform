using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Banco;

public class RetornoBancarioAtendimentoDTO
{
    public MensagemDTO Mensagem { get; set; } = new();

    public string? Status { get; set; }

    public string? NumeroDat { get; set; }

    public decimal? Valor { get; set; }

    public string? DataVencimento { get; set; }

    public string? DataPagamento { get; set; }
}