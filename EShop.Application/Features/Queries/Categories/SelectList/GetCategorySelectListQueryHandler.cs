using EShop.Application.Features.Models;
using EShop.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EShop.Application.Features.Queries.Categories.SelectList
{
    public class GetCategorySelectListQueryHandler
        : IRequestHandler<GetCategorySelectListQuery, IEnumerable<SelectListItem<int>>>
    {
        private readonly IEShopDbContext _dbContext;

        public GetCategorySelectListQueryHandler(IEShopDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(IEShopDbContext));
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
}
