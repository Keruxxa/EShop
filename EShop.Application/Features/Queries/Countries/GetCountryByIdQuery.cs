using EShop.Domain.Entities;
using MediatR;

namespace EShop.Application.Features.Queries.Countries
{
    public class GetCountryByIdQuery : EntityBase<int>, IRequest<Country>
    {
    }
}
