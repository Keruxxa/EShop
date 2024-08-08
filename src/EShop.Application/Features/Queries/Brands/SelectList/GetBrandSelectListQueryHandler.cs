using EShop.Application.Interfaces;
using EShop.Application.Models;
using EShop.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EShop.Application.Features.Queries.Brands.SelectList;

/// <summary>
///     Представляет обработчик запроса <see cref="GetBrandSelectListQuery"/>
/// </summary>
public class GetBrandSelectListQueryHandler :
    IRequestHandler<GetBrandSelectListQuery, IEnumerable<SelectListItem<int>>>
{
    private readonly IEShopDbContext _dbContext;

    public GetBrandSelectListQueryHandler(IEShopDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentException(nameof(Brand));
    }


    public async Task<IEnumerable<SelectListItem<int>>> Handle(
        GetBrandSelectListQuery request,
        CancellationToken cancellationToken)
    {
        var brands = await _dbContext.Brands
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return brands
            .Select(SelectListItem<int>.CreateItem)
            .OrderBy(brand => brand.Name);
    }
}
