using RestWithASPNET.Domain.Entities;
using System.Security.Claims;

namespace RestWithASPNET.Application.Services.Token;

public interface ITokenService
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    ClaimsPrincipal GetPrincipalToken(string token);
}
