using EShop.Application.Features.Models;
using EShop.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EShop.Application.Features.Queries.Countries.SelectList
{
    public class GetCountriesSelectListQueryHandler
        : IRequestHandler<GetCountriesSelectListQuery, IEnumerable<SelectListItem<int>>>
    {
        private readonly IEShopDbContext _dbContext;

        public GetCountriesSelectListQueryHandler(IEShopDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(IEShopDbContext));
        }


        public async Task<IEnumerable<SelectListItem<int>>> Handle(
            GetCountriesSelectListQuery request,
            CancellationToken cancellationToken)
        {
            var countriesEntities = await _dbContext.Countries.ToListAsync(cancellationToken);

            return countriesEntities
                .Select(SelectListItem<int>.CreateItem)
                .OrderBy(country => country.Name);
        }
    }
}
