namespace WebZi.Plataform.Domain.Models.Nfe;

public class NfeWsErrosModel
{
    public int ErroId { get; set; }
    public int GrvId { get; set; }
    public int? IdentificadorNota { get; set; }
    public int UsuarioId { get; set; }
    public char Acao { get; set; }
    public char OrigemErro { get; set; }
    public string? Status { get; set; }
    public string? CodigoErro { get; set; }
    public string? MensagemErro { get; set; }
    public string? CorrecaoErro { get; set; }
    public DateTime DataHoraCadastro { get; set; }
}