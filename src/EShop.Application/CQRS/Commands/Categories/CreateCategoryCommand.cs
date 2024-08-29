using MediatR;

namespace EShop.Application.CQRS.Commands.Categories;

/// <summary>
///     Представляет команду для создания категории
/// </summary>
public record CreateCategoryCommand(string Name) : IRequest<int>
{
}
