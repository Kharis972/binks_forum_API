using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using binks_forum_API.Models;
using Microsoft.IdentityModel.Tokens;

namespace binks_forum_API.Services;

public sealed class TokenService(IConfiguration configuration)
{
    public (string Token, DateTime ExpiresAt) Create(User user)
    {
        var expiresAt = DateTime.UtcNow.AddHours(8);
        var key = configuration["Jwt:SigningKey"];
        if (string.IsNullOrWhiteSpace(key))
            throw new InvalidOperationException("JWT signing key is not configured.");

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Name, user.NickName),
            new(ClaimTypes.Email, user.Mail)
        };
        var role = user switch
        {
            Admin => "Admin",
            Modo => "Modo",
            _ => "User"
        };
        claims.Add(new Claim(ClaimTypes.Role, role));

        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256);
        var jwt = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"] ?? "BinksForumAPI",
            audience: configuration["Jwt:Audience"] ?? "BinksForumWeb",
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: expiresAt,
            signingCredentials: credentials);
        return (new JwtSecurityTokenHandler().WriteToken(jwt), expiresAt);
    }
}
