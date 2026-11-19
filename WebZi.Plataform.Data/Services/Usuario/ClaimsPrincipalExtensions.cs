using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace WebZi.Plataform.Data.Services.Usuario;

public static class ClaimsPrincipalExtensions
{
    public static int? GetUserId(this ClaimsPrincipal user)
    {
        if (user == null)
        {
            return null;
        }

        var value = user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value 
                    ?? user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return int.TryParse(value, out int userId) ? userId : null;
    }

    public static bool TryGetUserId(this ClaimsPrincipal user, out int userId)
    {
        userId = 0;
        int? id = user.GetUserId();
        if (id.HasValue && id.Value > 0)
        {
            userId = id.Value;
            return true;
        }

        return false;
    }
}