using EShop.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using EShop.Domain.Exceptions;
using MediatR;
using EShop.Domain.Entities;

namespace EShop.Application.Features.Commands.Products
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
            var product = await _dbContext.Products
                .FirstOrDefaultAsync(product => product.Id == request.Id, cancellationToken);

            if (product == null)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }

            product.UpdateEntity(request.Name, request.Description, request.ReleaseDate,
                request.Price, request.CategoryId, request.BrandId, request.CountryManufacturerId);

            return await _dbContext.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
