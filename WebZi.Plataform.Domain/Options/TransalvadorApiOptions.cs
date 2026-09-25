namespace WebZi.Plataform.Domain.Options;

public record TransalvadorApiOptions
{
    public string Url { get; set; } = string.Empty;
    public string EntradaPatioToken { get; set; } = string.Empty;
    public string LiberacaoToken { get; set; } = string.Empty;
    public string DATToken { get; set; } = string.Empty;
    public string RetornoBancarioToken { get; set; } = string.Empty;
}