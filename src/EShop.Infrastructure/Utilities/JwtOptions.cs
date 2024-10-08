﻿namespace EShop.Infrastructure.Utilities;

public class JwtOptions
{
    public string Issuer { get; set; }

    public string Audience { get; set; }

    public string Key { get; set; }

    public int ExpiresMinutes { get; set; }
}
