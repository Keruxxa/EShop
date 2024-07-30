using EShop.Application.Dtos.Product;
using MediatR;

namespace EShop.Application.Features.Queries.Products.List
{
    /// <summary>
    ///     Представляет запрос для получения списка товаров
    /// </summary>
    public record GetProductListQuery : IRequest<IEnumerable<ProductListItemDto>>;
}
