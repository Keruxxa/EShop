namespace EShop.Application.Dtos.Orders;

/// <summary>
///     Представляет объект DTO товара в заказе
/// </summary>
public record CreateOrderItemDto(Guid ProductId, int Count);
