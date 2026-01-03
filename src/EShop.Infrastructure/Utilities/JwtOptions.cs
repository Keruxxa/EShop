namespace EShop.Infrastructure.Utilities;

public class JwtOptions
{
    public string Issuer { get; set; } = string.Empty;

    public string Audience { get; set; } = string.Empty;

    public string AccessSecretKey { get; set; } = string.Empty;

    public string RefreshSecretKey { get; set; } = string.Empty;

    public int AccessTokenExpiresMinutes { get; set; }

    public int RefreshTokenExpiresDays { get; set; }
}
