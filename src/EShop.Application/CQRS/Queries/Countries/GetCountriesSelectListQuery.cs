using EShop.Application.Models;
using MediatR;

namespace EShop.Application.CQRS.Queries.Countries;

/// <summary>
///     Представляет запрос для получения списка стран
/// </summary>
public class GetCountriesSelectListQuery : IRequest<IEnumerable<SelectListItem<int>>>;
