using CSharpFunctionalExtensions;
using EShop.Application.Interfaces;
using EShop.Domain.Entities;
using MediatR;

using static EShop.Application.Constants;

namespace EShop.Application.Features.Commands.Products.Create
{
    /// <summary>
    ///     Представляет обработчик команды <see cref="CreateProductCommand"/>
    /// </summary>
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<Guid>>
    {
        private readonly IEShopDbContext _dbContext;

        public CreateProductCommandHandler(IEShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product(
                request.Name,
                request.Description,
                request.ReleaseDate,
                request.Price,
                request.CategoryId,
                request.BrandId,
                request.CountryManufacturerId)
            {
                Id = Guid.NewGuid()
            };

            await _dbContext.Products.AddAsync(product, cancellationToken);
            await _dbContext.BrandProducts.AddAsync(new(request.BrandId, product.Id), cancellationToken);

            var saved = await _dbContext.SaveChangesAsync(cancellationToken) > 0;

            return saved
                ? Result.Success(product.Id)
                : Result.Failure<Guid>(SERVER_SIDE_ERROR);
        }
    }
}
