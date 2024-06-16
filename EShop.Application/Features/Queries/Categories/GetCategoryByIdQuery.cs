using EShop.Domain.Entities;
using MediatR;

namespace EShop.Application.Features.Queries.Categories
{
    public class GetCategoryByIdQuery : EntityBase<int>, IRequest<Category>
    {
    }
}
