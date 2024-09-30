using CSharpFunctionalExtensions;
using EShop.Application.Issues.Errors.Base;
using MediatR;

namespace EShop.Application.CQRS.Commands.Countries;

/// <summary>
///     Представляет команду для удаления страны
/// </summary>
public record DeleteCountryCommand(int Id) : IRequest<Result<Unit, Error>>
{
}
