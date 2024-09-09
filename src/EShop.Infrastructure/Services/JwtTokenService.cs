using EShop.Application.Interfaces.Security;
using EShop.Application.Interfaces.Services;
using EShop.Domain.Entities;
using EShop.Infrastructure.Utilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EShop.Infrastructure.Services;

public class JwtTokenService : IJwtTokenService
{
    private readonly JwtOptions _options;
    private readonly IRoleTypeService _roleTypeService;

    public JwtTokenService(IOptions<JwtOptions> options, IRoleTypeService roleTypeService)
    {
        _options = options.Value;
        _roleTypeService = roleTypeService;
    }


    public string Generate(User user)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Aud, _options.Audience),
            new(JwtRegisteredClaimNames.Iss, _options.Issuer),
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.Role, _roleTypeService.GetRoleTypeName(user.RoleId))
        };

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_options.ExpiresMinutes),
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
