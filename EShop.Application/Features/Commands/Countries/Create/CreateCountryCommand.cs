using MediatR;

namespace EShop.Application.Features.Commands.Countries.Create
{
    public record CreateCountryCommand(string Name) : IRequest<int>
    {
    }
}
