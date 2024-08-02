using EShop.Domain.Entities;

namespace EShop.Application.Interfaces.Security
{
    public interface IJwtTokenGenerator
    {
        /// <summary>
        ///     Генерирует Jwt-токен
        /// </summary>
        string Generate(User user);
    }
}
