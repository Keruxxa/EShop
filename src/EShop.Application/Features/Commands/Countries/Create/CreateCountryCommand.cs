using MediatR;

namespace EShop.Application.Features.Commands.Countries.Create;

/// <summary>
///     Представляет команду для создания страны
/// </summary>
public record CreateCountryCommand(string Name) : IRequest<int>
{
}
