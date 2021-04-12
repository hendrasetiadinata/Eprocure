using ApplicationCore.Models;
using System.Security.Claims;

namespace ApplicationCore.Interfaces
{
    public interface IJwtManager
    {
        string GenerateToken(ClaimsIdentity claimsIdentity, JwtOptions options);
        ClaimsPrincipal GetPrincipal(JwtOptions options, string token, bool requireExpTime);
    }
}
