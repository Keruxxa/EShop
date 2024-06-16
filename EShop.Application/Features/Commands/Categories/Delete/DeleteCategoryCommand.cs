using EShop.Domain.Entities;
using MediatR;

namespace EShop.Application.Features.Commands.Categories.Delete
{
    public class DeleteCategoryCommand : EntityBase<int>, IRequest<bool>
    {
    }
}
