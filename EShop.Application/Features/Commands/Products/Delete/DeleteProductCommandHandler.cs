using CSharpFunctionalExtensions;
using EShop.Application.Interfaces;
using EShop.Domain.Entities;
using EShop.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

using static EShop.Domain.Constants;

namespace EShop.Application.Features.Commands.Products.Delete
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result<bool>>
    {
        private readonly IEShopDbContext _dbContext;

        public DeleteProductCommandHandler(IEShopDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(IEShopDbContext));
        }


        public async Task<Result<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _dbContext
                .Products.FirstOrDefaultAsync(product => product.Id == request.Id, cancellationToken);

            if (product is null)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }

            _dbContext.Products.Remove(product);

            var saved = await _dbContext.SaveChangesAsync(cancellationToken) > 0;

            return saved
                ? Result.Success(saved)
                : Result.Failure<bool>(SERVER_SIDE_ERROR);
        }
    }
}
