using MediatR;

namespace EShop.Application.Features.Commands.Products
{
    public record DeleteProductCommand(Guid Id) : IRequest<bool>
    {
    }
}
