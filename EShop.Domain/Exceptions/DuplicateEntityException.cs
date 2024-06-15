namespace EShop.Domain.Exceptions
{
    public class DuplicateEntityException : Exception
    {
        /// <summary>
        ///     Представляет исключение, указывающая, что сущность <paramref name="entityType"/> уже существует
        /// </summary>
        public DuplicateEntityException(string entityType)
            : base($"Entity '{entityType}' with such parameters already exists")
        {
        }
    }
}
