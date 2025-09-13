namespace EShop.Infrastructure.Extensions;

public static class StringExtension
{
    public static string? ToNullIfEmpty(this string value)
    {
        var trimedValue = value.Trim();

        if (string.IsNullOrEmpty(trimedValue))
        {
            return null;
        }

        return trimedValue;
    }
}
