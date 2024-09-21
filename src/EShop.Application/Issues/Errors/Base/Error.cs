namespace EShop.Application.Issues.Errors.Base;

/// <summary>
///     Представляет ошибку, которая может возникнуть
///     при выполнении операций над сущностью в базе данных
/// </summary>
public class Error
{
    /// <summary>
    ///     Ошибка
    /// </summary>
    public IEntityError EntityError { get; }

    /// <summary>
    ///     Тип ошибки
    /// </summary>
    public ErrorType ErrorType { get; }

    public Error(IEntityError entityError, ErrorType errorType)
    {
        EntityError = entityError;
        ErrorType = errorType;
    }
}
