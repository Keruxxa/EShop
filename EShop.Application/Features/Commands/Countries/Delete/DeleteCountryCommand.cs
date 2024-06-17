using MediatR;

namespace EShop.Application.Features.Commands.Countries.Delete
{
    public record DeleteCountryCommand(int Id) : IRequest<bool>
    {
    }
}
