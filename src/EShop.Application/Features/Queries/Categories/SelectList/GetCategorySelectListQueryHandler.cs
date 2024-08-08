using EShop.Application.Interfaces;
using EShop.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EShop.Application.Features.Queries.Categories.SelectList;

/// <summary>
///     Представляет обработчик запроса <see cref="GetCategorySelectListQuery"/>
/// </summary>
public class GetCategorySelectListQueryHandler
    : IRequestHandler<GetCategorySelectListQuery, IEnumerable<SelectListItem<int>>>
{
    private readonly IEShopDbContext _dbContext;

    public GetCategorySelectListQueryHandler(IEShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<IEnumerable<SelectListItem<int>>> Handle(
        GetCategorySelectListQuery request,
        CancellationToken cancellationToken)
    {
        var categories = await _dbContext.Categories
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return categories
            .Select(SelectListItem<int>.CreateItem)
            .OrderBy(category => category.Name);
    }
}
