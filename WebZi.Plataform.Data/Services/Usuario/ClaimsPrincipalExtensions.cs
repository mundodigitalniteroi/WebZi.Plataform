using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace WebZi.Plataform.Data.Services.Usuario;

public static class ClaimsPrincipalExtensions
{
    public static int? GetUserId(this ClaimsPrincipal user)
    {
        var value = user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value 
                    ?? user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return int.TryParse(value, out int userId) ? userId : null;
    }
}