using EShop.Application.Interfaces;
using EShop.Domain.Entities;
using MediatR;

namespace EShop.Application.Features.Commands.ProductCommands
{
    public class CreateProductCommandHanlder : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IEShopDbContext _dbContext;

        public CreateProductCommandHanlder(IEShopDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product(
                request.Name,
                request.Description,
                request.ReleaseDate,
                request.Price,
                request.Rating,
                request.CategoryId,
                request.BrandId,
                request.CountryManufacturerId)
            {
                Id = Guid.NewGuid()
            };

            await _dbContext.Products.AddAsync(product, cancellationToken);
            var saved = await _dbContext.SaveChangesAsync(cancellationToken) > 0;

            if (saved)
            {
                return product.Id;
            }
            else
            {
                return Guid.Empty;
            }
        }
    }
}
