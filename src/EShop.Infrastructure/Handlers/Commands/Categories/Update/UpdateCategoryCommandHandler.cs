using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Commands.Categories;
using EShop.Application.Interfaces;
using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Entities;
using EShop.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static EShop.Application.Constants;

namespace EShop.Infrastructure.Handlers.Commands.Categories.Update;

/// <summary>
///     Представляет обработчик команды <see cref="UpdateCategoryCommand"/>
/// </summary>
public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result>
{
    private readonly IEShopDbContext _dbContext;
    private readonly ICategoryRepository _categoryRepository;

    public UpdateCategoryCommandHandler(IEShopDbContext dbContext, ICategoryRepository categoryRepository)
    {
        _dbContext = dbContext;
        _categoryRepository = categoryRepository;
    }


    public async Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.Id, cancellationToken);

        if (category is null)
        {
            throw new NotFoundException(nameof(Category), request.Id);
        }

        var nameIsTaken = await _dbContext.Categories
            .AnyAsync(category => category.Name.Equals(request.Name), cancellationToken);

        if (nameIsTaken)
        {
            throw new DuplicateEntityException(nameof(Country));
        }

        category.UpdateName(request.Name);

        _categoryRepository.Update(category);

        var saved = await _categoryRepository.SaveChangesAsync(cancellationToken) > 0;

        return saved
            ? Result.Success()
            : Result.Failure(SERVER_SIDE_ERROR);
    }
}

