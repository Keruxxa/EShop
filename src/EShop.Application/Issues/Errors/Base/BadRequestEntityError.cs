namespace EShop.Application.Issues.Errors.Base;

public class BadRequestEntityError : IEntityError
{
    public string Message { get; set; }

    public BadRequestEntityError(string errorMessage)
        => Message = errorMessage;
}
