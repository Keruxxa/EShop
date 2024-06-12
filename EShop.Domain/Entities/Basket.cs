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
        public List<BasketItem> BasketItems { get; private set; }

        /// <summary>
        ///     Суммарная стоимость корзины
        /// </summary>
        public decimal? TotalPrice => BasketItems?.Sum(basketItem => basketItem.Product.Price * basketItem.Count);


        private Basket() { }

        public Basket(Guid userId)
        {
            UserId = userId;
        }

        /// <summary>
        ///     Добавляет объект <see cref="BasketItem"/> в коллекцию <see cref="BasketItems"/>
        /// </summary>
        public void AddItem(BasketItem basketItem)
        {
            BasketItems ??= new List<BasketItem>();

            BasketItems.Add(basketItem);
        }

        /// <summary>
        ///     Удаляет объект <see cref="BasketItem"/> из коллекции <see cref="BasketItems"/>
        /// </summary>
        public bool RemoveItem(BasketItem basketItem)
        {
            return BasketItems.Remove(basketItem);
        }
    }
}
