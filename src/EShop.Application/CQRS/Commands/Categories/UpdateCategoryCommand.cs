using CSharpFunctionalExtensions;
using MediatR;

namespace EShop.Application.CQRS.Commands.Categories;

/// <summary>
///     Представляет команду для обновления категории
/// </summary>
public record UpdateCategoryCommand(int Id, string Name) : IRequest<Result>;
