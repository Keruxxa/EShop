namespace EShop.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        /// <summary>
        ///     Представляет исключение, указывающая, что сущность <paramref name="entityType"/> 
        ///     с данным <paramref name="id"/> не существует
        /// </summary>
        public NotFoundException(string entityType, object id)
            : base($"Entity '{entityType}' with id '{id}' not found")
        {
        }
    }
}
