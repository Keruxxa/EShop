using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Commands.Categories;
using EShop.Application.Interfaces;
using EShop.Domain.Entities;
using EShop.Domain.Exceptions;
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

    public UpdateCategoryCommandHandler(IEShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _dbContext.Categories
            .FirstOrDefaultAsync(category => category.Id == request.Id, cancellationToken);

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

        _dbContext.Categories.Update(category);
        var saved = await _dbContext.SaveChangesAsync(cancellationToken) > 0;

        return saved
            ? Result.Success()
            : Result.Failure(SERVER_SIDE_ERROR);
    }
}

