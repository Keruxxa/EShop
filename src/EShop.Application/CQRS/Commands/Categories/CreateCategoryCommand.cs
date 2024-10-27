using CSharpFunctionalExtensions;
using EShop.Application.Issues.Errors.Base;
using MediatR;

namespace EShop.Application.CQRS.Commands.Categories;

/// <summary>
///     Представляет команду для создания категории
/// </summary>
/// <param name="AncestorCategoryId"> Id категории-предка </param>
/// <param name="Name"> Имя новой категории </param>
public record CreateCategoryCommand(int AncestorCategoryId, string Name) : IRequest<Result<int, Error>>
{
}
