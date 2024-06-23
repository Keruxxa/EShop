using CSharpFunctionalExtensions;
using MediatR;

namespace EShop.Application.Features.Commands.Countries.Update
{
    public record UpdateCountryCommand(int Id, string Name) : IRequest<Result<int>>;
}
