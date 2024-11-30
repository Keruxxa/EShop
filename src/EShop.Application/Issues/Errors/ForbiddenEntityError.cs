using EShop.Application.Issues.Errors.Base;

namespace EShop.Application.Issues.Errors;

/// <summary>
///     Представляет ошибку, указывающую, что сущность не найдена
/// </summary>
public class ForbiddenEntityError : IEntityError
{
    public string Message { get; }

    public ForbiddenEntityError(object id)
        => Message = $"User with id '{id}' does not have permission to perform this action";
}
