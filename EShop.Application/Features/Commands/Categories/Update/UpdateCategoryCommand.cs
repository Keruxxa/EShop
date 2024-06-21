using CSharpFunctionalExtensions;
using MediatR;

namespace EShop.Application.Features.Commands.Categories.Update
{
    public record UpdateCategoryCommand(int Id, string Name) : IRequest<Result<int>>;
}
