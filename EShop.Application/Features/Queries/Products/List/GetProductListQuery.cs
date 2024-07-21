using EShop.Application.Dtos.Product;
using MediatR;

namespace EShop.Application.Features.Queries.Products.List
{
    public class GetProductListQuery : IRequest<IEnumerable<ProductListItemDto>>
    {
    }
}
