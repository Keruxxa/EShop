using EShop.Application.CQRS.Commands.Categories;
using EShop.Application.Interfaces;
using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Entities;
using EShop.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.Handlers.Commands.Categories.Create;

/// <summary>
///     Представляет обработчик команды <see cref="CreateCategoryCommand"/>
/// </summary>
public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
{
    private readonly IEShopDbContext _dbContext;
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategoryCommandHandler(IEShopDbContext dbContext, ICategoryRepository categoryRepository)
    {
        _dbContext = dbContext;
        _categoryRepository = categoryRepository;
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

        _categoryRepository.Create(category);

        var saved = await _categoryRepository.SaveChangesAsync(cancellationToken) > 0;

        return saved ? category.Id : 0;
    }
}
