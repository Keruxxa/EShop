namespace EShop.Application.Dtos.User;

/// <summary>
///     Представляет объект DTO для входа пользователя в систему
/// </summary>
/// <param name="Email"> Электронная почта </param>
/// <param name="Password"> Пароль </param>
public record SignInUserDto(string Email, string Password);
