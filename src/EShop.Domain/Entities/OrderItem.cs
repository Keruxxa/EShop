namespace EShop.Domain.Entities;

/// <summary>
///     Представляет товар в заказе
/// </summary>
public class OrderItem : EntityBase<Guid>
{
    /// <summary>
    ///     Id товара
    /// </summary>
    public Guid ProductId { get; }

    /// <summary>
    ///     Товар
    /// </summary>
    public Product Product { get; }

    /// <summary>
    ///     Id заказа
    /// </summary>
    public Guid OrderId { get; }

    /// <summary>
    ///     Количество товара
    /// </summary>
    public int Count { get; }


    private OrderItem() { }

    public OrderItem(Guid orderId, Guid productId, int count)
    {
        OrderId = orderId;
        ProductId = productId;
        Count = count;
    }
}
