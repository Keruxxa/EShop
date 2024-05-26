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
        }
    }
}
