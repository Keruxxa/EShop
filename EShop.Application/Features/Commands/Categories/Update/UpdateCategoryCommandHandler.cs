using CSharpFunctionalExtensions;
using EShop.Application.Interfaces;
using EShop.Domain.Entities;
using EShop.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static EShop.Domain.Constants;

namespace EShop.Application.Features.Commands.Categories.Update
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result<int>>
    {
        private readonly IEShopDbContext _dbContext;

        public UpdateCategoryCommandHandler(IEShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<Result<int>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _dbContext.Categories
                .FirstOrDefaultAsync(category => category.Id == request.Id, cancellationToken);

            if (category is null)
            {
                throw new NotFoundException(nameof(Category), request.Id);
            }

            var nameIsTaken = await _dbContext.Categories
                .AnyAsync(category
                    => category.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase), cancellationToken);

            if (nameIsTaken)
            {
                throw new DuplicateEntityException(nameof(Country));
            }

            category.UpdateName(request.Name);

            _dbContext.Categories.Update(category);
            var saved = await _dbContext.SaveChangesAsync(cancellationToken) > 0;

            return saved
                ? Result.Success(category.Id)
                : Result.Failure<int>(SERVER_SIDE_ERROR);
        }
    }
}
