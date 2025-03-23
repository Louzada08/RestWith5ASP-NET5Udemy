using Microsoft.IdentityModel.Tokens;
using RestWithASPNET.Domain.Configuration;
using RestWithASPNET.Domain.Entities;
using RestWithASPNET.Domain.Enums.Extension;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RestWithASPNET.Application.Services.Token;

public class TokenService : ITokenService
{
    private TokenConfiguration _configuration;

    public TokenService(TokenConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateAccessToken(User user)
    {
        //classe usada para criar e validar tokens de segurança de fato 
        var tokenHandler = new JwtSecurityTokenHandler();

        var secretKey = Encoding.UTF8.GetBytes(_configuration.Secret);
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256);
        var claims = new ClaimsIdentity();

        claims.AddClaim(new Claim(ClaimTypes.Role, EnumExtensions.GetEnumDescription(user.UserRole)));

        //var options = new JwtSecurityToken(
        //    issuer: _configuration.Issuer,
        //    audience: _configuration.Audience,
        //    claims: claims,
        //    expires: DateTime.Now.AddMinutes(_configuration.Minutes),
        //    signingCredentials: signinCredentials


        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _configuration.Issuer,
            Audience = _configuration.Audience,
            Expires = DateTime.Now.AddMinutes(_configuration.Minutes),
            Subject = claims,
            SigningCredentials = signingCredentials
        };

        //pega os dados do usuário from database pelo email;

        // tokenDescriptor.Subject = claimsIdentity;
        
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Secret)),
            ValidateLifetime = false
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken securityToken;

        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken == null || 
            !jwtSecurityToken.Header.Alg.Equals(
                SecurityAlgorithms.HmacSha256, 
                StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");
        return principal;
    }

    public ClaimsPrincipal GetPrincipalToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
        };

        ClaimsPrincipal principal = null;
        return principal;
    }
}
