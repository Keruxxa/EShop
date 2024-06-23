using EShop.Application.Models;
using MediatR;

namespace EShop.Application.Features.Queries.Brands.SelectList
{
    /// <summary>
    ///     Представляет запрос для получения списка брендов
    /// </summary>
    public class GetBrandSelectListQuery : IRequest<IEnumerable<SelectListItem<int>>>;
}
