using MediatR;

namespace EShop.Application.Features.Commands.Products.Delete
{
    public record DeleteProductCommand(Guid Id) : IRequest<bool>
    {
    }
}
