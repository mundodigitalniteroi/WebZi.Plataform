namespace WebZi.Plataform.Domain.Options;

public record EvolutionOptions
{
    public string BaseUrl { get; set; } = string.Empty;
    public string Instance { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
}