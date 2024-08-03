using EShop.Domain.Entities;
using MediatR;

namespace EShop.Application.Features.Queries.Categories.ById;

/// <summary>
///     Представляет запрос для получения категории по ее Id
/// </summary>
public record GetCategoryByIdQuery(int Id) : IRequest<Category>;
