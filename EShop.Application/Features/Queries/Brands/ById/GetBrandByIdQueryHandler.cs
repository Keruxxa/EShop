using EShop.Application.Interfaces;
using EShop.Domain.Entities;
using EShop.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EShop.Application.Features.Queries.Brands.ById
{
    /// <summary>
    ///     Представляет обработчик запроса <see cref="GetBrandByIdQuery"/>
    /// </summary>
    public class GetBrandByIdQueryHandler : IRequestHandler<GetBrandByIdQuery, Brand>
    {
        private readonly IEShopDbContext _dbContext;

        public GetBrandByIdQueryHandler(IEShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<Brand> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
        {
            var brand = await _dbContext.Brands
                .Include(brand => brand.BrandProducts)
                .FirstOrDefaultAsync(brand => brand.Id == request.Id, cancellationToken);

            if (brand is null)
            {
                throw new NotFoundException(nameof(Brand), request.Id);
            }

            return brand;
        }
    }
}
