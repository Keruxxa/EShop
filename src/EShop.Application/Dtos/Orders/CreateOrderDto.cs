namespace EShop.Application.Dtos.Orders;

/// <summary>
///     Представляет объект DTO для создания заказа
/// </summary>
public record CreateOrderDto(Guid UserId, IEnumerable<CreateOrderItemDto> CreateOrderItemDtos);
