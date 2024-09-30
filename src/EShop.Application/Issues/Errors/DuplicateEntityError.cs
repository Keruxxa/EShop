using EShop.Application.Issues.Errors.Base;

namespace EShop.Application.Issues.Errors;

/// <summary>
///     Представляет ошибку, указывающую, что сущность с такими параметрами уже существует
/// </summary>
public class DuplicateEntityError : IEntityError
{
    public string Message { get; }

    public DuplicateEntityError(string entityType)
        => Message = $"Entity '{entityType}' with such parameters already exists";

    public DuplicateEntityError(string entityType, string errorMessage)
        => Message = $"Error on '{entityType}'. {errorMessage}";
}
