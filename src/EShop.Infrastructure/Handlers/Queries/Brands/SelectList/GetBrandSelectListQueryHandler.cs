using EShop.Application.CQRS.Queries.Brands;
using EShop.Application.Interfaces;
using EShop.Application.Interfaces.Repositories;
using EShop.Application.Models;
using MediatR;

namespace EShop.Infrastructure.Handlers.Queries.Brands.SelectList;

/// <summary>
///     Представляет обработчик запроса <see cref="GetBrandSelectListQuery"/>
/// </summary>
public class GetBrandSelectListQueryHandler :
    IRequestHandler<GetBrandSelectListQuery, IEnumerable<SelectListItem<int>>>
{
    private readonly IEShopDbContext _dbContext;
    private readonly IBrandRepository _brandRepository;

    public GetBrandSelectListQueryHandler(IEShopDbContext dbContext, IBrandRepository brandRepository)
    {
        _dbContext = dbContext;
        _brandRepository = brandRepository;
    }


    public async Task<IEnumerable<SelectListItem<int>>> Handle(
        GetBrandSelectListQuery request,
        CancellationToken cancellationToken)
    {
        var brands = await _brandRepository.GetListAsync(cancellationToken);

        return brands
            .Select(SelectListItem<int>.CreateItem)
            .OrderBy(brand => brand.Name);
    }
}
