namespace WebZi.Plataform.Domain.ViewModel.Pessoa;

public class ConsultaPessoaParameters
{
    public string? Nome { get; set; }
    public string? NomeDoMeio { get; set; }
    public string? Sobrenome { get; set; }
    public int? IdentificadorTipoDocumento { get; set; }
    public string? ValorDocumento { get; set; }
    public byte? Skip { get; set; }
    public byte? Take { get; set; }
    
}