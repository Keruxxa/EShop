using MediatR;

namespace EShop.Application.Features.Commands.Countries
{
    public record CreateCountryCommand(string Name) : IRequest<int>
    {
    }
}
