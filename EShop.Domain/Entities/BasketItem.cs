namespace EShop.Domain.Entities
{
    /// <summary>
    ///     Представляет отдельный товар корзины
    /// </summary>
    public class BasketItem : EntityBase<Guid>
    {
        /// <summary>
        ///     Id корзины, с которой связан данный отдельный товар
        /// </summary>
        public Guid BasketId { get; }

        /// <summary>
        ///     Id товара
        /// </summary>
        public Guid ProductId { get; }

        /// <summary>
        ///     Товар
        /// </summary>
        public Product Product { get; }

        /// <summary>
        ///     Количество товара в корзине
        /// </summary>
        public int Count { get; set; }


        private BasketItem() { }

        public BasketItem(Guid basketId, Guid productId)
        {
            BasketId = basketId;
            ProductId = productId;
            Count = 1;
        }
    }
}
