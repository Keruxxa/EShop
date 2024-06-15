using EShop.Application.Features.Models;
using MediatR;

namespace EShop.Application.Features.Queries.Brands
{
    /// <summary>
    ///     Представляет запрос для получения списка брендов
    /// </summary>
    public class GetBrandSelectListQuery : IRequest<IEnumerable<SelectListItem<int>>>
    {
    }
}
