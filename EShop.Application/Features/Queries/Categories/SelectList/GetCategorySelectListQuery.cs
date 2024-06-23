using EShop.Application.Features.Models;
using MediatR;

namespace EShop.Application.Features.Queries.Categories.SelectList
{
    public class GetCategorySelectListQuery : IRequest<IEnumerable<SelectListItem<int>>>;
}
