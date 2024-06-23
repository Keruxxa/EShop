using CSharpFunctionalExtensions;
using MediatR;

namespace EShop.Application.Features.Commands.Brands.Update
{
    public record UpdateBrandCommand(int Id, string Name) : IRequest<Result<int>>;
}
