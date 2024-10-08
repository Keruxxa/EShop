using EShop.Application.Dtos.Product;

namespace EShop.Application.Dtos.Basket;

/// <summary>
///     Представляет объект DTO корзины
/// </summary>
public record BasketDto(Guid Id, decimal TotalPrice, List<ProductInBasketDto> ProductDtos);
