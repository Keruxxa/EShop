using EShop.Application.CQRS.Commands.Categories;
using EShop.Application.Interfaces;
using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Entities;
using EShop.Application.Exceptions;
using MediatR;

namespace EShop.Infrastructure.Handlers.Commands.Categories.Delete;

/// <summary>
///     Представляет обработчик команды <see cref="DeleteCategoryCommand"/>
/// </summary>
public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
{
    private readonly IEShopDbContext _dbContext;
    private readonly ICategoryRepository _categoryRepository;

    public DeleteCategoryCommandHandler(IEShopDbContext dbContext, ICategoryRepository categoryRepository)
    {
        _dbContext = dbContext;
        _categoryRepository = categoryRepository;
    }


    public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.Id, cancellationToken);

        if (category == null)
        {
            throw new NotFoundException(nameof(Category), request.Id);
        }

        _categoryRepository.Delete(category);

        return await _categoryRepository.SaveChangesAsync(cancellationToken) > 0;
    }
}
