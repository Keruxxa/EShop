using EShop.Domain.Entities;
using MediatR;

namespace EShop.Application.Features.Queries.Categories.ById
{
    public record GetCategoryByIdQuery(int Id) : IRequest<Category>
    {
    }
}
