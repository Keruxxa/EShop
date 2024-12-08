using EShop.Domain.Enums;

namespace EShop.Domain.Entities;

public class OrderDeliveryStatus : EntityBase<DeliveryStatus>
{
    /// <summary>
    ///     Статус
    /// </summary>
    public string Status { get; }


    private OrderDeliveryStatus() { }


    /// <param name="status"> Статус </param>
    public OrderDeliveryStatus(string status)
    {
        Status = status;
    }
}
