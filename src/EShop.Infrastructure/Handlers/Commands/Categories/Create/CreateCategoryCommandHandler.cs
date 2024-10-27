using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Commands.Categories;
using EShop.Application.Interfaces.Repositories;
using EShop.Application.Interfaces.Services;
using EShop.Application.Issues.Errors;
using EShop.Application.Issues.Errors.Base;
using EShop.Domain.Entities;
using MediatR;

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

        var ancestorsHierarchy = await _categoryRepository.GetHierarchyByIdAsync(request.AncestorCategoryId, cancellationToken);

        var ancestorIds = ancestorsHierarchy.Select(ancestor => ancestor.Id).ToList();

        var isSaved = await _categoryRepository.CreateAsync(category, ancestorIds, cancellationToken);

        return isSaved
            ? Result.Success<int, Error>(category.Id)
            : Result.Failure<int, Error>(new Error(new ServerEntityError(), ErrorType.ServerError));
    }
}
