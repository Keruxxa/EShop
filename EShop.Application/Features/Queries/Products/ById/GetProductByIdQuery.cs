using EShop.Application.Dtos.Product;
using MediatR;

namespace EShop.Application.Features.Queries.Products.ById
{
    /// <summary>
    ///     Представляет запрос для получения товара по его <see cref="Guid"/>
    /// </summary>
    public record GetProductByIdQuery(Guid Id) : IRequest<ProductDto>
    {
    }
}
