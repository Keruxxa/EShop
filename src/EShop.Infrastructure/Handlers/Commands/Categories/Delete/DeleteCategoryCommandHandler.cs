using EShop.Application.CQRS.Commands.Categories;
using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Entities;
using MediatR;
using CSharpFunctionalExtensions;
using EShop.Application.Issues.Errors.Base;
using EShop.Application.Issues.Errors;

namespace EShop.Infrastructure.Handlers.Commands.Categories.Delete;

/// <summary>
///     Представляет обработчик команды <see cref="DeleteCategoryCommand"/>
/// </summary>
public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result<Unit, Error>>
{
    private readonly ICategoryRepository _categoryRepository;

    public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }


    public async Task<Result<Unit, Error>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.Id, cancellationToken);

        if (category == null)
        {
            return Result.Failure<Unit, Error>(new Error(new NotFoundEntityError(nameof(Category), request.Id), ErrorType.NotFound));
        }

        _categoryRepository.Delete(category);

        var saved = await _categoryRepository.SaveChangesAsync(cancellationToken) > 0;

        return saved
            ? Result.Success<Unit, Error>(Unit.Value)
            : Result.Failure<Unit, Error>(new Error(new ServerEntityError(), ErrorType.ServerError));
    }
}
