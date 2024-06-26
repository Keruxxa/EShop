namespace EShop.Domain.Entities
{
    /// <summary>
    ///     Представляет коллекцию избранного
    /// </summary>
    public class Favorite : EntityBase<Guid>
    {
        /// <summary>
        ///     Id пользователя, с которым связана коллекция избранного
        /// </summary>
        public Guid UserId { get; private set; }

        /// <summary>
        ///     Товары, содержащиеся в коллекции избранного
        /// </summary>
        public List<Product> Products { get; private set; }


        private Favorite() { }


        /// <param name="userId">Id пользователя</param>
        public Favorite(Guid userId)
        {
            UserId = userId;
        }

        /// <summary>
        ///     Добавляет объект <see cref="Product"/> в коллекцию <see cref="Products"/>
        /// </summary>
        public void AddItem(Product product)
        {
            Products ??= [];

            Products.Add(product);
        }

        /// <summary>
        ///     Удаляет объект <see cref="Product"/> из коллекции <see cref="Products"/>
        /// </summary>
        public bool RemoveItem(Product product)
        {
            return Products.Remove(product);
        }
    }
}
