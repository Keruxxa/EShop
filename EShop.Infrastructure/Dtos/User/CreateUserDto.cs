using EShop.Domain.Enums;

namespace EShop.Infrastructure.Dtos.User
{
    /// <summary>
    ///     Представляет объект DTO для создания пользователя
    /// </summary>
    /// <param name="FirstName"> Имя </param>
    /// <param name="LastName"> Фамилия </param>
    /// <param name="Phone"> Телефон </param>
    /// <param name="Email"> Электронная почта </param>
    /// <param name="Password"> Пароль </param>
    /// <param name="RoleId"> Id роли </param>
    public record CreateUserDto(
        string? FirstName,
        string? LastName,
        string? Phone,
        string Email,
        string Password,
        RoleType RoleId);
}
