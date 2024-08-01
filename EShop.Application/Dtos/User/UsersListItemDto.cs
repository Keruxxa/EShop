namespace EShop.Application.Dtos.User
{
    /// <summary>
    ///     Представляет объект DTO списка пользователей
    /// </summary>
    public record UsersListItemDto(
        string? FirstName,
        string? LastName,
        string? Phone,
        string Email);

}
