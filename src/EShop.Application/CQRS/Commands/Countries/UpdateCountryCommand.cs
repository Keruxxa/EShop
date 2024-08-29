using CSharpFunctionalExtensions;
using MediatR;

namespace EShop.Application.CQRS.Commands.Countries;

/// <summary>
///     Представляет команду для обновления страны
/// </summary>
public record UpdateCountryCommand(int Id, string Name) : IRequest<Result>;
