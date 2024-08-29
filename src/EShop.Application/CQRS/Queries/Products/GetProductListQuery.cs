using EShop.Application.Dtos.Product;
using MediatR;

namespace EShop.Application.CQRS.Queries.Products;

/// <summary>
///     Представляет запрос для получения списка товаров
/// </summary>
public record GetProductListQuery : IRequest<IEnumerable<ProductListItemDto>>;
