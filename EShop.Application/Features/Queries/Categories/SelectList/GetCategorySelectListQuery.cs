using EShop.Application.Models;
using MediatR;

namespace EShop.Application.Features.Queries.Categories.SelectList
{
    /// <summary>
    ///     Представляет запрос для получения списка категорий
    /// </summary>
    public class GetCategorySelectListQuery : IRequest<IEnumerable<SelectListItem<int>>>;
}
