namespace EShop.Application.Issues.Errors.Base;

/// <summary>
///     Представляет перечисление ошибок, которые могут возникать
///     при выполнении операций над сущностями в базе данных
/// </summary>
public enum ErrorType
{
    NotFound,
    InvalidRequest,
    Duplicate,
    BadRequest,
    ServerError
}
