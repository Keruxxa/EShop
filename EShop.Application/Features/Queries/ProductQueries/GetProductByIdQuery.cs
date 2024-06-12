using EShop.Application.Dtos.Product;
using MediatR;

namespace EShop.Application.Features.Queries.ProductQueries
{
    public class GetProductByIdQuery : IRequest<ProductDto>
    {
        public Guid Id { get; set; }
    }
}
