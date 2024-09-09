namespace EShop.Application.Dtos.User
{
    /// <summary>
    ///     Представляет объект-ответ DTO зарегистрировавшегося пользователя
    /// </summary>
    /// <param name="Id"> Id </param>
    /// <param name="Token"> JWT-токен </param>
    public record SignUpUserResponseDto(Guid Id, string Token);
}
