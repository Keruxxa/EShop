using EShop.Application.Dtos.Product;

namespace EShop.Application.Dtos.Favorite;

/// <summary>
///     Представляет объект DTO избранного
/// </summary>
public record FavoriteDto(Guid Id, List<ProductInFavoriteDto> ProductDtos);
