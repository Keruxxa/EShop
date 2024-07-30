using MediatR;

namespace EShop.Application.Features.Commands.Categories.Delete
{
    /// <summary>
    ///     Представляет команду для удаления категории
    /// </summary>
    public record DeleteCategoryCommand(int Id) : IRequest<bool>
    {
    }
}
