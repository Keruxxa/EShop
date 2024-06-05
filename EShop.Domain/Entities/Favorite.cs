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
        public Guid UserId { get; set; }

        /// <summary>
        ///     Товары, содержащиеся в коллекции избранного
        /// </summary>
        public ICollection<Product>? Products { get; set; }


        private Favorite() { }

        public Favorite(Guid userId)
        {
            UserId = userId;
            Products = new List<Product>();
        }

        /// <summary>
        ///     Добавляет объект <see cref="Product"/> в коллекцию <see cref="Products"/>
        /// </summary>
        public void AddItem(Product product)
        {
            Products ??= new List<Product>();

            Products.Add(product);
        }

        /// <summary>
        ///     Удаляет объект <see cref="Product"/> из коллекции <see cref="Products"/>
        /// </summary>
        public bool RemoveItem(Product product)
        {
            Products ??= new List<Product>();

            return Products.Remove(product);
        }
    }
}
