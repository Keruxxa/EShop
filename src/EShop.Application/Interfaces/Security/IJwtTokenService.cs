using EShop.Domain.Entities;
using EShop.Domain.Enums;

namespace EShop.Application.Interfaces.Security;

public interface IJwtTokenService
{
    /// <summary>
    ///     Генерирует Access Jwt-токен
    /// </summary>
    string GenerateAccessToken(Guid userId, RoleType roleType);

    RefreshToken GenerateRefreshToken(Guid UserId);
}
