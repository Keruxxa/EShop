using CSharpFunctionalExtensions;
using EShop.Application.Issues.Errors.Base;
using EShop.Application.Models;
using MediatR;

namespace EShop.Application.CQRS.Queries.Categories;

/// <summary>
///     Представляет запрос для получения иерархии категорий по Id
/// </summary>
/// <remarks>
///     Получение всех родительских категорий, включая <paramref name="Id"/>
/// </remarks>
public record GetCategoriesHierarchyByIdQuery(int Id) : IRequest<Result<List<SelectListItem<int>>, Error>>;
