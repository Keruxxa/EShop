using EShop.Application.CQRS.Queries.Categories;
using EShop.Application.Interfaces;
using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Entities;
using MediatR;
using CSharpFunctionalExtensions;
using EShop.Application.Issues.Errors;
using EShop.Application.Issues.Errors.Base;

namespace EShop.Infrastructure.Handlers.Queries.Categories.ById;

/// <summary>
///     Представляет обработчик запроса <see cref="GetCategoryByIdQuery"/>
/// </summary>
public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Result<Category, Error>>
{
    private readonly IEShopDbContext _dbContext;
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoryByIdQueryHandler(IEShopDbContext dbContext, ICategoryRepository categoryRepository)
    {
        _dbContext = dbContext;
        _categoryRepository = categoryRepository;
    }


    public async Task<Result<Category, Error>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.Id, cancellationToken);

        if (category is null)
        {
            return Result.Failure<Category, Error>(new Error(new NotFoundEntityError(nameof(Category), request.Id), ErrorType.NotFound));
        }

        return category;
    }
}
