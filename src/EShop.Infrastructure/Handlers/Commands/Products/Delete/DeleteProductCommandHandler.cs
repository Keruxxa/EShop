using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Commands.Products;
using EShop.Application.Interfaces;
using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Entities;
using EShop.Domain.Exceptions;
using MediatR;
using static EShop.Application.Constants;

namespace EShop.Infrastructure.Handlers.Commands.Products.Delete;

/// <summary>
///     Представляет обработчик команды <see cref="DeleteProductCommand"/>
/// </summary>
public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result>
{
    private readonly IEShopDbContext _dbContext;
    private readonly IProductRepository _productRepository;

    public DeleteProductCommandHandler(IEShopDbContext dbContext, IProductRepository productRepository)
    {
        _dbContext = dbContext;
        _productRepository = productRepository;
    }


    public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);

        if (product is null)
        {
            throw new NotFoundException(nameof(Product), request.Id);
        }

        _dbContext.Products.Remove(product);

        var saved = await _productRepository.SaveChangesAsync(cancellationToken) > 0;

        return saved
            ? Result.Success()
            : Result.Failure(SERVER_SIDE_ERROR);
    }
}
