using MediatR;

namespace EShop.Application.Features.Commands.Countries
{
    public record DeleteCountryCommand(int Id) : IRequest<bool>
    {
    }
}
