using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Commands.Categories;
using EShop.Application.Interfaces;
using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using EShop.Application.Issues.Errors.Base;
using EShop.Application.Issues.Errors;

namespace EShop.Infrastructure.Handlers.Commands.Categories.Update;

/// <summary>
///     Представляет обработчик команды <see cref="UpdateCategoryCommand"/>
/// </summary>
public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result<Unit, Error>>
{
    private readonly IEShopDbContext _dbContext;
    private readonly ICategoryRepository _categoryRepository;

    public UpdateCategoryCommandHandler(IEShopDbContext dbContext, ICategoryRepository categoryRepository)
    {
        _dbContext = dbContext;
        _categoryRepository = categoryRepository;
    }


    public async Task<Result<Unit, Error>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.Id, cancellationToken);

        if (category is null)
        {
            return Result.Failure<Unit, Error>(new Error(new NotFoundEntityError(nameof(Category), request.Id), ErrorType.NotFound));
        }

        var nameIsTaken = await _dbContext.Categories
            .AnyAsync(category => category.Name.Equals(request.Name), cancellationToken);

        if (nameIsTaken)
        {
            return Result.Failure<Unit, Error>(new Error(new DuplicateEntityError(nameof(Category)), ErrorType.Duplicate));
        }

        category.UpdateName(request.Name);

        _categoryRepository.Update(category);

        var saved = await _categoryRepository.SaveChangesAsync(cancellationToken) > 0;

        return saved
            ? Result.Success<Unit, Error>(Unit.Value)
            : Result.Failure<Unit, Error>(new Error(new ServerEntityError(), ErrorType.ServerError));
    }
}

