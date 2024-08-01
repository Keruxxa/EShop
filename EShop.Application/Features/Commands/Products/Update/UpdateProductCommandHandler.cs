using EShop.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using EShop.Domain.Exceptions;
using MediatR;
using EShop.Domain.Entities;
using CSharpFunctionalExtensions;

namespace EShop.Application.Features.Commands.Products.Update
{
    /// <summary>
    ///     Представляет обработчик команды <see cref="UpdateProductCommand"/>
    /// </summary>
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result>
    {
        private readonly IEShopDbContext _dbContext;

        public UpdateProductCommandHandler(IEShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _dbContext.Products
                .FirstOrDefaultAsync(product => product.Id == request.Id, cancellationToken);

            if (product is null)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }

            product.UpdateEntity(request.Name, request.Description, request.ReleaseDate,
                request.Price, request.CategoryId, request.BrandId, request.CountryManufacturerId);

            _dbContext.Products.Update(product);

            var saved = await _dbContext.SaveChangesAsync(cancellationToken) > 0;

            return saved
                ? Result.Success()
                : Result.Failure("An error occured on the server side");
        }
    }
}
