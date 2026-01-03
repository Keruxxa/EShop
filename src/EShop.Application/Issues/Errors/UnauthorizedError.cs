using EShop.Application.Issues.Errors.Base;

namespace EShop.Application.Issues.Errors;

/// <summary>
///     Представляет ошибку отказа доступа
/// </summary>
public class UnauthorizedError : IEntityError
{
    public string Message
        => "Permission denied";
}
