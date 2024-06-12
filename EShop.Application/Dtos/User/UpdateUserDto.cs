namespace EShop.Infrastructure.Dtos.User
{
    /// <summary>
    ///     Представляет объект DTO для обновления пользователя
    /// </summary>
    /// <param name="FirstName"> Имя </param>
    /// <param name="LastName"> Фамилия </param>
    /// <param name="Phone"> Телефон </param>
    public record UpdateUserDto(
        string? FirstName,
        string? LastName,
        string? Phone);
}
