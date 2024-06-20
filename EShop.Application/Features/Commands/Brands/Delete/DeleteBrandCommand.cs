using MediatR;

namespace EShop.Application.Features.Commands.Brands.Delete
{
    /// <summary>
    ///     Представляет команду для удаления бренда
    /// </summary>
    public record DeleteBrandCommand(int Id) : IRequest<bool>
    {
    }
}
