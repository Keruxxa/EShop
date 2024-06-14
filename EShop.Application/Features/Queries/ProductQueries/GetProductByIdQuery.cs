using EShop.Application.Dtos.Product;
using MediatR;

namespace EShop.Application.Features.Queries.ProductQueries
{
    /// <summary>
    ///     Представляет запрос для получения товара по его <see cref="Guid"/>
    /// </summary>
    public class GetProductByIdQuery : IRequest<ProductDto>
    {
        public Guid Id { get; set; }
    }
}
