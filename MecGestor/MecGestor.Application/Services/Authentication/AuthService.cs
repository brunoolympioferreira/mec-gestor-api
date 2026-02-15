using MecGestor.Application.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MecGestor.Application.Services.Authentication;

public class AuthService : IAuthService
{
    public string GenerateJwtToken(Domain.Entities.User user)
    {
        var issuer = Environment.GetEnvironmentVariable("mec-gestor-issuer");
        var audience = Environment.GetEnvironmentVariable("mec-gestor_audience");
        var key = Environment.GetEnvironmentVariable("mec-gestor_key");

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        List<Claim> claims =
        [
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email.Address),
            new Claim(ClaimTypes.Role, user.Role.Name)
        ];

        var token = new JwtSecurityToken(
        issuer: issuer,
        audience: audience,
        expires: DateTime.Now.AddHours(12),
        signingCredentials: credentials,
        claims: claims);

        var tokenHandler = new JwtSecurityTokenHandler();

        var stringToken = tokenHandler.WriteToken(token);

        return stringToken;
    }
}
