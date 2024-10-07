using CSharpFunctionalExtensions;
using EShop.Application.Issues.Errors.Base;
using EShop.Domain.Entities;
using MediatR;

namespace EShop.Application.CQRS.Queries.Categories;

/// <summary>
///     Представляет запрос для получения категории по ее Id
/// </summary>
public record GetCategoryByIdQuery(int Id) : IRequest<Result<Category, Error>>;
