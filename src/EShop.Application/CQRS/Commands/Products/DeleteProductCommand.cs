using CSharpFunctionalExtensions;
using MediatR;

namespace EShop.Application.CQRS.Commands.Products;

/// <summary>
///     Представляет команду для удаления товара
/// </summary>
public record DeleteProductCommand(Guid Id) : IRequest<Result>;
