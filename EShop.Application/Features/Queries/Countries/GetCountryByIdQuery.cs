using EShop.Domain.Entities;
using MediatR;

namespace EShop.Application.Features.Queries.Countries
{
    public record GetCountryByIdQuery(int Id) : IRequest<Country>
    {
    }
}
