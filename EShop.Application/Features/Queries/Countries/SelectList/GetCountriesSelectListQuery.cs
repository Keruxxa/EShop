using EShop.Application.Features.Models;
using MediatR;

namespace EShop.Application.Features.Queries.Countries.SelectList
{
    /// <summary>
    ///     Представляет запрос для получения списка стран
    /// </summary>
    public class GetCountriesSelectListQuery : IRequest<IEnumerable<SelectListItem<int>>>
    {
    }
}
