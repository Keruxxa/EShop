using EShop.Application.Dtos.Product;

namespace EShop.Application.Dtos.Orders;

/// <summary>
///     Представляет объект DTO заказа
/// </summary>
public record OrderDto(DateTime OrderingDate, decimal TotalPrice, List<ProductInOrderDto> productDtos);
