using EShop.Application.CQRS.Commands.Categories;
using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Entities;
using MediatR;
using CSharpFunctionalExtensions;
using EShop.Application.Issues.Errors.Base;
using EShop.Application.Issues.Errors;
using EShop.Application.Interfaces.Services;

namespace EShop.Infrastructure.Handlers.Commands.Categories.Create;

/// <summary>
///     Представляет обработчик команды <see cref="CreateCategoryCommand"/>
/// </summary>
public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Result<int, Error>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICategoryService _categoryService;

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, ICategoryService categoryService)
    {
        _categoryRepository = categoryRepository;
        _categoryService = categoryService;
    }


    public async Task<Result<int, Error>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        if (!await _categoryService.IsNameUniqueAsync(request.Name, cancellationToken))
        {
            return Result.Failure<int, Error>(new Error(new DuplicateEntityError(nameof(Category)), ErrorType.Duplicate));
        }

        var category = new Category(request.Name);

        _categoryRepository.Create(category);

        var saved = await _categoryRepository.SaveChangesAsync(cancellationToken) > 0;

        return saved
            ? Result.Success<int, Error>(category.Id)
            : Result.Failure<int, Error>(new Error(new ServerEntityError(), ErrorType.ServerError));
    }
}
