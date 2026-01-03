using EShop.Application.Interfaces.Security;
using EShop.Application.Interfaces.Services;
using EShop.Domain.Entities;
using EShop.Domain.Enums;
using EShop.Infrastructure.Utilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EShop.Infrastructure.Services;

public class JwtTokenService(IOptions<JwtOptions> options, IRoleTypeService roleTypeService) : IJwtTokenService
{
    private readonly JwtOptions _options = options.Value;


    public string GenerateAccessToken(Guid userId, RoleType roleType)
    {
        var claims = new List<Claim>(6)
        {
            new(JwtRegisteredClaimNames.Aud, _options.Audience),
            new(JwtRegisteredClaimNames.Iss, _options.Issuer),
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.Role, roleTypeService.GetRoleTypeName(roleType))
        };

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.AccessSecretKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_options.AccessTokenExpiresMinutes),
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public RefreshToken GenerateRefreshToken(Guid userId)
    {
        var expireDays = _options.RefreshTokenExpiresDays;

        var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));

        return new RefreshToken(userId, DateTime.UtcNow.AddDays(expireDays), token);
    }
}
