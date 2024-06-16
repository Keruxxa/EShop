using EShop.Application.Interfaces;
using EShop.Domain.Entities;
using EShop.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EShop.Application.Features.Commands.Categories
{
    /// <summary>
    ///     Представляет обработчик команды <see cref="CreateCategoryCommand"/>
    /// </summary>
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
    {
        private readonly IEShopDbContext _dbContext;

        public CreateCategoryCommandHandler(IEShopDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(IEShopDbContext));
        }


        public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _dbContext
                .Categories.FirstOrDefaultAsync(category =>
                    category.Name.ToLower() == request.Name.ToLower(), cancellationToken);

            if (category != null)
            {
                throw new DuplicateEntityException(nameof(Category));
            }

            var newCategory = new Category(request.Name);

            await _dbContext.Categories.AddAsync(newCategory);

            var saved = await _dbContext.SaveChangesAsync(cancellationToken) > 0;

            return saved ? newCategory.Id : 0;
        }
    }
}
