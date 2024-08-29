using EShop.Application.Models;
using MediatR;

namespace EShop.Application.CQRS.Queries.Categories;

/// <summary>
///     Представляет запрос для получения списка категорий
/// </summary>
public class GetCategorySelectListQuery : IRequest<IEnumerable<SelectListItem<int>>>;
