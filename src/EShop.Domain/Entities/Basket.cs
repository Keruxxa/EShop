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
    public IReadOnlyCollection<BasketItem> BasketItems => _busketItems.AsReadOnly();

    /// <summary>
    ///     Суммарная стоимость корзины
    /// </summary>
    public decimal? TotalPrice => BasketItems?.Sum(basketItem => basketItem.Product.Price * basketItem.Count);


    private Basket() { }

    public Basket(Guid userId)
    {
        Id = userId;
    }

    /// <summary>
    ///     Добавляет объект <see cref="BasketItem"/> в коллекцию <see cref="BasketItems"/>
    /// </summary>
    public void AddItem(BasketItem basketItem)
    {
        if (_busketItems.Contains(basketItem))
        {
            basketItem.IncrementItemCount();
            return;
        }

        _busketItems.Add(basketItem);
    }

    /// <summary>
    ///     Удаляет объект <see cref="BasketItem"/> из коллекции <see cref="BasketItems"/>
    /// </summary>
    public bool RemoveItem(BasketItem basketItem)
    {
        if (_busketItems.Contains(basketItem))
        {
            basketItem.DecrementItemCount();
            return true;
        }

        return _busketItems.Remove(basketItem);
    }
}
