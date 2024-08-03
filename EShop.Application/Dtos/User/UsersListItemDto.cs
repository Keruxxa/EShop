namespace EShop.Application.Dtos.User;

/// <summary>
///     Представляет объект DTO списка пользователей
/// </summary>
/// <param name="FirstName"> Имя </param>
/// <param name="LastName"> Фамилия </param>
/// <param name="Phone"> Телефон </param>
/// <param name="Email"> Электронная почта </param>
public record UsersListItemDto(
    string? FirstName,
    string? LastName,
    string? Phone,
    string Email);
