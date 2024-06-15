using EShop.Application.Features.Models;
using EShop.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EShop.Application.Features.Queries.Countries
{
    public class GetCountriesSelectListQueryHandler
        : IRequestHandler<GetCountriesSelectListQuery, IEnumerable<SelectListItem<int>>>
    {
        private readonly IEShopDbContext _dbContext;

        public GetCountriesSelectListQueryHandler(IEShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<IEnumerable<SelectListItem<int>>> Handle(
            GetCountriesSelectListQuery request,
            CancellationToken cancellationToken)
        {
            var countriesEntities = await _dbContext.Countries.ToListAsync(cancellationToken);

            var countriesSelectList = countriesEntities
                .Select(SelectListItem<int>.CreateItem)
                .OrderBy(country => country.Name);

            return countriesSelectList;
        }
    }
}
