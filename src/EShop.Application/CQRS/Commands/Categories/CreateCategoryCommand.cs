using CSharpFunctionalExtensions;
using EShop.Application.Issues.Errors.Base;
using MediatR;

namespace EShop.Application.CQRS.Commands.Categories;

/// <summary>
///     Представляет команду для создания категории
/// </summary>
public record CreateCategoryCommand(string Name) : IRequest<Result<int, Error>>
{
}
