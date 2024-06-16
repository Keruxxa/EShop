using EShop.Domain.Entities;
using MediatR;

namespace EShop.Application.Features.Commands.Categories
{
    public class DeleteCategoryCommand : EntityBase<int>, IRequest<bool>
    {
    }
}
