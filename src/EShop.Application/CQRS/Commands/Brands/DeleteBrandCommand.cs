using CSharpFunctionalExtensions;
using EShop.Application.Issues.Errors.Base;
using MediatR;

namespace EShop.Application.CQRS.Commands.Brands;

/// <summary>
///     Представляет команду для удаления бренда
/// </summary>
public record DeleteBrandCommand(int Id) : IRequest<Result<Unit, Error>>
{
}
