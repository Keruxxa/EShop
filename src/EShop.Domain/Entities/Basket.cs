namespace EShop.Domain.Entities;

/// <summary>
///     Представляет корзину
/// </summary>
public class Basket : EntityBase<Guid>
{
    /// <summary>
    ///     Товары корзины
    /// </summary>
    private List<BasketItem> _busketItems = [];

    /// <summary>
    ///     Товары корзины
    /// </summary>
    public IReadOnlyCollection<BasketItem> BasketItems => _busketItems;

    /// <summary>
    ///     Суммарная стоимость корзины
    /// </summary>
    public decimal TotalPrice => BasketItems.Sum(basketItem => basketItem.Product.Price * basketItem.Count);


    private Basket() { }

    public Basket(Guid userId)
    {
        Id = userId;
    }

    /// <summary>
    ///     Добавляет объект <see cref="BasketItem"/> в коллекцию <see cref="BasketItems"/>
    /// </summary>
    public void AddItem(Guid productId)
    {
        var basketItem = _busketItems.FirstOrDefault(item => item.BasketId == Id && item.ProductId == productId);

        if (basketItem is null)
        {
            _busketItems.Add(new BasketItem(Id, productId));
            return;
        }

        basketItem.IncrementItemCount();
    }

    /// <summary>
    ///     Удаляет объект <see cref="BasketItem"/> из коллекции <see cref="BasketItems"/>
    /// </summary>
    public bool DeleteItem(BasketItem basketItemToDelete)
    {
        var basketItem = _busketItems.FirstOrDefault(item => item.BasketId == basketItemToDelete.BasketId && item.ProductId == basketItemToDelete.ProductId);

        if (basketItem is not null)
        {
            var isDecremented = basketItem.DecrementItemCount();

            if (isDecremented)
            {
                return true;
            }
            else
            {
                return _busketItems.Remove(basketItem);
            }
        }

        return false;
    }
}
