namespace WebZi.Plataform.Domain.Options;

public record JwtOptions
{
    public string Issuer { get; init; } = string.Empty;
    public string Audience { get; init; } = string.Empty;
    public string Secret { get; init; } = string.Empty;
    public string ExpirationMinutes { get; init; } = string.Empty;
}