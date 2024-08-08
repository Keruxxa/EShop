using CSharpFunctionalExtensions;
using MediatR;

namespace EShop.Application.Features.Commands.Countries.Update;

/// <summary>
///     Представляет команду для обновления страны
/// </summary>
public record UpdateCountryCommand(int Id, string Name) : IRequest<Result<int>>;
