using EShop.Application.CQRS.Queries.Countries;
using EShop.Application.Interfaces;
using EShop.Application.Interfaces.Repositories;
using EShop.Application.Models;
using MediatR;

namespace EShop.Infrastructure.Handlers.Queries.Countries.SelectList;

/// <summary>
///     Представляет обработчик запроса <see cref="GetCountriesSelectListQuery"/>
/// </summary>
public class GetCountriesSelectListQueryHandler
    : IRequestHandler<GetCountriesSelectListQuery, IEnumerable<SelectListItem<int>>>
{
    private readonly IEShopDbContext _dbContext;
    private readonly ICountryRepository _countryRepository;

    public GetCountriesSelectListQueryHandler(IEShopDbContext dbContext, ICountryRepository countryRepository)
    {
        _dbContext = dbContext;
        _countryRepository = countryRepository;
    }


    public async Task<IEnumerable<SelectListItem<int>>> Handle(
        GetCountriesSelectListQuery request,
        CancellationToken cancellationToken)
    {
        var countriesEntities = await _countryRepository.GetListAsync(cancellationToken);

        return countriesEntities
            .Select(SelectListItem<int>.CreateItem)
            .OrderBy(country => country.Name);
    }
}
