using EShop.Application.Issues.Errors.Base;

namespace EShop.Application.Issues.Errors;

public class BadRequestEntityError : IEntityError
{
    public string Message { get; set; }

    public BadRequestEntityError(string errorMessage)
        => Message = errorMessage;
}
