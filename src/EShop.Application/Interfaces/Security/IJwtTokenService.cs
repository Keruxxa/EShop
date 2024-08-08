using EShop.Domain.Entities;

namespace EShop.Application.Interfaces.Security;

public interface IJwtTokenService
{
    /// <summary>
    ///     Генерирует Jwt-токен
    /// </summary>
    string Generate(User user);
}
