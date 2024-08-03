namespace EShop.Application.Dtos.User;

/// <summary>
///     Представляет объект DTO для создания пользователя
/// </summary>
/// <param name="FirstName"> Имя </param>
/// <param name="LastName"> Фамилия </param>
/// <param name="Phone"> Телефон </param>
/// <param name="Email"> Электронная почта </param>
/// <param name="Password"> Пароль </param>
public record CreateUserDto(
    string? FirstName,
    string? LastName,
    string? Phone,
    string Email,
    string Password);
