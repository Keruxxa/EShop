using EShop.Application.Issues.Errors.Base;

namespace EShop.Application.Issues.Errors;

/// <summary>
///     Представляет ошибку, указывающую, что сущность не найдена
/// </summary>
public class NotFoundEntityError : IEntityError
{
    public string Message { get; }

    public NotFoundEntityError(string entityType, object id)
        => Message = $"Entity '{entityType}' with id '{id}' not found";
}
