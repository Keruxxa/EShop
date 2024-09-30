using CSharpFunctionalExtensions;
using EShop.Application.Issues.Errors.Base;
using MediatR;

namespace EShop.Application.CQRS.Commands.Categories;

/// <summary>
///     Представляет команду для удаления категории
/// </summary>
public record DeleteCategoryCommand(int Id) : IRequest<Result<Unit, Error>>
{
}
