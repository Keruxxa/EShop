namespace EShop.Application.Dtos.User
{
    /// <summary>
    ///     Представляет объект-ответ DTO аутентифицированного пользователя
    /// </summary>
    /// <param name="Id"> Id </param>
    /// <param name="AccessToken"> JWT-токен </param>
    public record SignInUserResponseDto(Guid Id, string AccessToken, string RefreshToken);
}
