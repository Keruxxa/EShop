using CSharpFunctionalExtensions;
using EShop.Application.Interfaces;
using EShop.Domain.Entities;
using MediatR;

namespace EShop.Application.Features.Commands.Products
{
    /// <summary>
    ///     Представляет обработчик команды <see cref="CreateProductCommand"/>
    /// </summary>
    public class CreateProductCommandHanlder : IRequestHandler<CreateProductCommand, Result<Guid>>
    {
        private readonly IEShopDbContext _dbContext;

        public CreateProductCommandHanlder(IEShopDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
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
            var saved = await _dbContext.SaveChangesAsync(cancellationToken) > 0;

            return saved
                ? Result.Success(product.Id)
                : Result.Failure<Guid>("An error occured on the server side");
        }
    }
}
