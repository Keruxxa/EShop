namespace EShop.Application.Exceptions;

public class NotFoundEntity
{
    public string Message { get; }

    public NotFoundEntity(string entityType, object id)
        => Message = $"Entity '{entityType}' with id '{id}' not found";
}
