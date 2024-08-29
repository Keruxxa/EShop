using EShop.Application.Models;
using MediatR;

namespace EShop.Application.CQRS.Queries.Brands;

/// <summary>
///     Представляет запрос для получения списка брендов
/// </summary>
public class GetBrandSelectListQuery : IRequest<IEnumerable<SelectListItem<int>>>;
