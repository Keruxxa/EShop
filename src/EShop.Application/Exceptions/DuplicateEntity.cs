namespace EShop.Application.Exceptions;

public class DuplicateEntity
{
    public string Message { get; }

    public DuplicateEntity(string entityType)
        => Message = $"Entity '{entityType}' with such parameters already exists";

}
