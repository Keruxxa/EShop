namespace EShop.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string entityType, object id)
            : base($"Entity '{entityType}' with id '{id}' not found")
        {
        }
    }
}
