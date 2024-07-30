using EShop.Application.Interfaces;
using EShop.Domain.Entities;
using EShop.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EShop.Application.Features.Queries.Categories.ById
{
    /// <summary>
    ///     Представляет обработчик запроса <see cref="GetCategoryByIdQuery"/>
    /// </summary>
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Category>
    {
        private readonly IEShopDbContext _dbContext;

        public GetCategoryByIdQueryHandler(IEShopDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(IEShopDbContext));
        }


        public async Task<Category> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _dbContext.Categories
                .FirstOrDefaultAsync(category => category.Id == request.Id, cancellationToken);

            if (category is null)
            {
                throw new NotFoundException(nameof(Category), request.Id);
            }

            return category;
        }
    }
}
