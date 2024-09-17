using EShop.Application.CQRS.Queries.Categories;
using EShop.Application.Interfaces;
using EShop.Application.Interfaces.Repositories;
using EShop.Application.Models;
using MediatR;

namespace EShop.Infrastructure.Handlers.Queries.Categories.SelectList;

/// <summary>
///     Представляет обработчик запроса <see cref="GetCategorySelectListQuery"/>
/// </summary>
public class GetCategorySelectListQueryHandler
    : IRequestHandler<GetCategorySelectListQuery, IEnumerable<SelectListItem<int>>>
{
    private readonly IEShopDbContext _dbContext;
    private readonly ICategoryRepository _categoryRepository;

    public GetCategorySelectListQueryHandler(IEShopDbContext dbContext, ICategoryRepository categoryRepository)
    {
        _dbContext = dbContext;
        _categoryRepository = categoryRepository;
    }


    public async Task<IEnumerable<SelectListItem<int>>> Handle(
        GetCategorySelectListQuery request,
        CancellationToken cancellationToken)
    {
        var categories = await _categoryRepository.GetListAsync(cancellationToken);

        return categories
            .Select(SelectListItem<int>.CreateItem)
            .OrderBy(category => category.Name);
    }
}
