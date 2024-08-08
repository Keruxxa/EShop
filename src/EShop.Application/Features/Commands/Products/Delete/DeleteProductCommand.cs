using CSharpFunctionalExtensions;
using MediatR;

namespace EShop.Application.Features.Commands.Products.Delete;

/// <summary>
///     Представляет команду для удаления товара
/// </summary>
public record DeleteProductCommand(Guid Id) : IRequest<Result<bool>>;
