namespace EShop.Domain.Entities;

/// <summary>
///     Представляет заказ
/// </summary>
public class Order : EntityBase<Guid>
{
    /// <summary>
    ///     Товары заказа
    /// </summary>
    private List<OrderItem> _orderItems = [];

    /// <summary>
    ///     Пользователь, с которым связан заказ
    /// </summary>
    public Guid UserId { get; }

    /// <summary>
    ///     Товары заказа
    /// </summary>
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

    /// <summary>
    ///     Суммарная стоимость
    /// </summary>
    public decimal TotalPrice
    {
        get => _orderItems.Sum(orderItem => orderItem.Product.Price * orderItem.Count);
    }

    /// <summary>
    ///     Дата оформления
    /// </summary>
    public DateTime OrderingDate { get; }

    /// <summary>
    ///     Дата доставки
    /// </summary>
    public DateTime? DeliveryDate { get; private set; }

    /// <summary>
    ///     Дата получения
    /// </summary>
    public DateTime? ReceiptDate { get; private set; }


    private Order() { }

    public Order(Guid userId)
    {
        UserId = userId;
        OrderingDate = DateTime.Now.ToUniversalTime();
    }


    public void AddOrderItem(OrderItem orderItem)
    {
        _orderItems.Add(orderItem);
    }

    /// <summary>
    ///     Устанавливает дату доставки
    /// </summary>
    public void SetDeliveryDate() => DeliveryDate ??= DateTime.Now.ToUniversalTime();

    /// <summary>
    ///     Устанавливает дату получения
    /// </summary>
    public void SetReceiptDate() => ReceiptDate ??= DateTime.Now.ToUniversalTime();
}
