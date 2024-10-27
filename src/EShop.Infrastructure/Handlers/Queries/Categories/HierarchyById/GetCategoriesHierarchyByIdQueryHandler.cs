using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Queries.Categories;
using EShop.Application.Interfaces.Repositories;
using EShop.Application.Interfaces.Services;
using EShop.Application.Issues.Errors;
using EShop.Application.Issues.Errors.Base;
using EShop.Application.Models;
using EShop.Domain.Entities;
using MediatR;

namespace EShop.Infrastructure.Handlers.Queries.Categories.HierarchyById;

/// <summary>
///     Представляет обработчик запроса <see cref="GetCategoriesHierarchyByIdQuery"/>
/// </summary>
public class GetCategoriesHierarchyByIdQueryHandler : IRequestHandler<GetCategoriesHierarchyByIdQuery, Result<List<SelectListItem<int>>, Error>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICategoryService _categoryService;

    public GetCategoriesHierarchyByIdQueryHandler(ICategoryRepository categoryRepository, ICategoryService categoryService)
    {
        _categoryRepository = categoryRepository;
        _categoryService = categoryService;
    }


    public async Task<Result<List<SelectListItem<int>>, Error>> Handle(GetCategoriesHierarchyByIdQuery request, CancellationToken cancellationToken)
    {
        if (!await _categoryService.IsCategoryExistAsync(request.Id, cancellationToken))
        {
            return new Error(new NotFoundEntityError(nameof(Category), request.Id), ErrorType.NotFound);
        }

        var categoriesHierarchy = await _categoryRepository.GetHierarchyByIdAsync(request.Id, cancellationToken);

        return Result.Success<List<SelectListItem<int>>, Error>(categoriesHierarchy);
    }
}
