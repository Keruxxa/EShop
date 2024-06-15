using EShop.Application.Interfaces;
using EShop.Domain.Entities;
using EShop.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EShop.Application.Features.Commands.Brands
{
    /// <summary>
    ///     Представляет обработчик команды <see cref="CreateBrandCommand"/>
    /// </summary>
    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, int>
    {
        private readonly IEShopDbContext _dbContext;

        public CreateBrandCommandHandler(IEShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<int> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            var existBrand = await _dbContext.Brands
                .FirstOrDefaultAsync(brand =>
                    brand.Name.ToLower() == request.Name.ToLower(), cancellationToken);

            if (existBrand != null)
            {
                throw new DuplicateEntityException(nameof(Brand));
            }

            var brand = new Brand(request.Name);

            await _dbContext.Brands.AddAsync(brand, cancellationToken);

            var saved = await _dbContext.SaveChangesAsync(cancellationToken) > 0;

            return saved ? brand.Id : 0;
        }
    }
}
