namespace EShop.Domain.Entities
{
    /// <summary>
    ///     Представляет корзину
    /// </summary>
    public class Basket : EntityBase<Guid>
    {
        /// <summary>
        ///     Id пользователя, связанного с корзиной
        /// </summary>
        public Guid UserId { get; }

        /// <summary>
        ///     Товары корзины
        /// </summary>
        public ICollection<BasketItem>? BasketItems { get; set; }

        /// <summary>
        ///     Суммарная стоимость корзины
        /// </summary>
        public decimal? TotalPrice => BasketItems?.Sum(basketItem => basketItem.Product.Price * basketItem.Count);


        private Basket() { }

        public Basket(Guid userId)
        {
            UserId = userId;
        }


        public void AddItemToBasket(BasketItem basketItem)
        {
            if (BasketItems == null)
            {
                BasketItems = new List<BasketItem>();
            }

            BasketItems.Add(basketItem);
        }
    }
}
