namespace EShop.Application.Dtos.User
{
    /// <summary>
    ///     Представляет объект DTO пользователя
    /// </summary>
    /// <param name="FirstName"> Имя </param>
    /// <param name="LastName"> Фамилия </param>
    /// <param name="Phone"> Телефон </param>
    /// <param name="Email"> Электронная почта </param>
    /// <param name="RoleTypeName"> Роль </param>
    public record UserDto(
        string? FirstName,
        string? LastName,
        string? Phone,
        string Email,
        string RoleTypeName);
}
