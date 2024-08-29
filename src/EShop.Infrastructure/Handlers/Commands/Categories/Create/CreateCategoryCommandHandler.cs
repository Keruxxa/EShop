using EShop.Application.CQRS.Commands.Categories;
using EShop.Application.Interfaces;
using EShop.Domain.Entities;
using EShop.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.Handlers.Commands.Categories.Create;

/// <summary>
///     Представляет обработчик команды <see cref="CreateCategoryCommand"/>
/// </summary>
public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
{
    private readonly IEShopDbContext _dbContext;

    public CreateCategoryCommandHandler(IEShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var categoryExists = await _dbContext.Categories
            .AnyAsync(category => category.Name.Equals(request.Name), cancellationToken);

        if (categoryExists)
        {
            throw new DuplicateEntityException(nameof(Category));
        }

        var category = new Category(request.Name);

        _dbContext.Categories.Add(category);

        var saved = await _dbContext.SaveChangesAsync(cancellationToken) > 0;

        return saved ? category.Id : 0;
    }
}
