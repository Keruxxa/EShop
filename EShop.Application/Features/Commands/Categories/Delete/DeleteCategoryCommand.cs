using MediatR;

namespace EShop.Application.Features.Commands.Categories.Delete
{
    public record DeleteCategoryCommand(int Id) : IRequest<bool>
    {
    }
}
