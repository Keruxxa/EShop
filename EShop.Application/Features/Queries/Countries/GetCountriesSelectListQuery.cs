using EShop.Application.Features.Models;
using MediatR;

namespace EShop.Application.Features.Queries.Countries
{
    public class GetCountriesSelectListQuery : IRequest<IEnumerable<SelectListItem<int>>>
    {
    }
}
