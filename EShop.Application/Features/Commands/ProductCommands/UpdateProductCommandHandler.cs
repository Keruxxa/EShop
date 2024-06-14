using EShop.Application.Interfaces;
using EShop.Domain.Exceptions;
using MediatR;

namespace EShop.Application.Features.Commands.ProductCommands
{
    /// <summary>
    ///     Представляет обработчик команды <see cref="UpdateProductCommand"/>
    /// </summary>
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IEShopDbContext _dbContext;

        public UpdateProductCommandHandler(IEShopDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _dbContext.Products.FirstOrDefault(product => product.Id == request.Id);

            if (product == null)
            {
                throw new NotFoundException(nameof(product), request.Id);
            }

            product.UpdateEntity(request.Name, request.Description, request.ReleaseDate, request.Price,
                request.Rating, request.CategoryId, request.BrandId, request.CountryManufacturerId);

            return await _dbContext.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
