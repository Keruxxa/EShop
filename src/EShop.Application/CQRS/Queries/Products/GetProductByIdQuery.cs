using CSharpFunctionalExtensions;
using EShop.Application.Dtos.Product;
using EShop.Application.Issues.Errors.Base;
using MediatR;

namespace EShop.Application.CQRS.Queries.Products;

/// <summary>
///     Представляет запрос для получения товара по его Id
/// </summary>
public record GetProductByIdQuery(Guid Id) : IRequest<Result<ProductDto, Error>>;
