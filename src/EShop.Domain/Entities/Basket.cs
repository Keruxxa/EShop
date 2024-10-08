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
    public void AddItem(BasketItem basketItemToAdd)
    {
        var basketItem = _busketItems.FirstOrDefault(item => item.BasketId == basketItemToAdd.BasketId && item.ProductId == basketItemToAdd.ProductId);

        if (basketItem is not null)
        {
            basketItem.IncrementItemCount();
            return;
        }

        _busketItems.Add(basketItemToAdd);
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
