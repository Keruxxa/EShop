using MediatR;

namespace EShop.Application.Features.Commands.Countries.Delete
{
    /// <summary>
    ///     Представляет команду для удаления страны
    /// </summary>
    public record DeleteCountryCommand(int Id) : IRequest<bool>
    {
    }
}
