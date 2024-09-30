using CSharpFunctionalExtensions;
using EShop.Application.Issues.Errors.Base;
using MediatR;

namespace EShop.Application.CQRS.Commands.Products;

/// <summary>
///     Представляет команду для удаления товара
/// </summary>
public record DeleteProductCommand(Guid Id) : IRequest<Result<Unit, Error>>;
