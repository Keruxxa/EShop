using EShop.Application.Dtos.Product;
using EShop.Domain.Entities;
using MediatR;

namespace EShop.Application.Features.Queries.Products
{
    /// <summary>
    ///     Представляет запрос для получения товара по его <see cref="Guid"/>
    /// </summary>
    public class GetProductByIdQuery : EntityBase<Guid>, IRequest<ProductDto>
    {
    }
}
