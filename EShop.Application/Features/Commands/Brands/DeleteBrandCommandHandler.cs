using EShop.Application.Interfaces;
using EShop.Domain.Entities;
using EShop.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EShop.Application.Features.Commands.Brands
{
    /// <summary>
    ///     Представляет обработчик команды <see cref="DeleteBrandCommand"/>
    /// </summary>
    public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, bool>
    {
        private readonly IEShopDbContext _dbContext;

        public DeleteBrandCommandHandler(IEShopDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(IEShopDbContext));
        }


        public async Task<bool> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = await _dbContext
                .Brands.FirstOrDefaultAsync(brand => brand.Id == request.Id, cancellationToken);

            if (brand == null)
            {
                throw new NotFoundException(nameof(Brand), request.Id);
            }

            _dbContext.Brands.Remove(brand);

            return await _dbContext.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
