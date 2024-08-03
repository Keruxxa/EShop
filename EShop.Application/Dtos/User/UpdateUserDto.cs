namespace EShop.Application.Dtos.User;

/// <summary>
///     Представляет объект DTO для обновления пользователя
/// </summary>
/// <param name="FirstName"> Имя </param>
/// <param name="LastName"> Фамилия </param>
public record UpdateUserDto(
    Guid Id,
    string? FirstName,
    string? LastName);
