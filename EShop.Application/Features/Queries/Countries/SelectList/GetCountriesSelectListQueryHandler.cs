using EShop.Application.Interfaces;
using EShop.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EShop.Application.Features.Queries.Countries.SelectList
{
    /// <summary>
    ///     Представляет обработчик запроса <see cref="GetCountriesSelectListQuery"/>
    /// </summary>
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
            var countriesEntities = await _dbContext.Countries
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return countriesEntities
                .Select(SelectListItem<int>.CreateItem)
                .OrderBy(country => country.Name);
        }
    }
}
