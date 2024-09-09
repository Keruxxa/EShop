using MediatR;

namespace EShop.Application.CQRS.Commands.Countries;

/// <summary>
///     Представляет команду для создания страны
/// </summary>
public record CreateCountryCommand(string Name) : IRequest<int>
{
}
