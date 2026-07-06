namespace WebZi.Plataform.Domain.Options;

public record DetranHubOptions()
{
    public string BaseUrl { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
}