namespace EShop.Domain.Entities
{
    /// <summary>
    ///     Представляет промежуточную сущность, связывающую сущности 
    ///     <see cref="Product"/> и <see cref="Favorite"/>
    /// </summary>
    public class FavoriteProducts
    {
        /// <summary>
        ///     Id товара
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        ///     Id избранного
        /// </summary>
        public Guid FavoriteId { get; set; }
    }
}
