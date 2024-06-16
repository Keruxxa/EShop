using EShop.Domain.Entities;
using MediatR;

namespace EShop.Application.Features.Queries.Categories
{
    public record GetCategoryByIdQuery(int Id) : IRequest<Category>
    {
    }
}
