using EShop.Application.Issues.Errors.Base;
using static EShop.Application.Constants;

namespace EShop.Application.Issues.Errors;

/// <summary>
///     Представляет ошибку, указывающую, что ошибка произошла на стороне сервера
/// </summary>
public class ServerEntityError : IEntityError
{
    public string Message { get; }

    public ServerEntityError()
        => Message = SERVER_SIDE_ERROR;
}
