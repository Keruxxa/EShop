namespace EShop.Domain.Entities;

/// <summary>
///     Представляет Refresh Jwt-токен
/// </summary>
public class RefreshToken(Guid userId, DateTime expireDate, string token)
{

    /// <summary>
    ///     Уникальный идентификатор
    /// </summary>
    public Guid Id { get; private set; } = Guid.CreateVersion7();

    /// <summary>
    ///     Id пользователя, с которым связан токен
    /// </summary>
    public Guid UserId { get; set; } = userId;

    /// <summary>
    ///     Дата просрочки
    /// </summary>
    public DateTime ExpireDate { get; set; } = expireDate;

    /// <summary>
    ///     Токен
    /// </summary>
    public string Token { get; set; } = token;

    /// <summary>
    ///     Отозван
    /// </summary>
    public bool IsRevoked { get; set; }
}
