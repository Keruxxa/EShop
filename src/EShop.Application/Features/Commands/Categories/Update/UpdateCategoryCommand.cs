using CSharpFunctionalExtensions;
using MediatR;

namespace EShop.Application.Features.Commands.Categories.Update;

/// <summary>
///     Представляет команду для обновления категории
/// </summary>
public record UpdateCategoryCommand(int Id, string Name) : IRequest<Result<int>>;
