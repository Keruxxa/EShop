using EShop.Application.Interfaces.Services;
using EShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.Services
{
    class BrandService : IBrandService
    {
        private readonly EShopDbContext _dbContext;

        public BrandService(EShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<bool> IsNameUniqueAsync(string name, CancellationToken cancellationToken)
        {
            return !await _dbContext.Brands.AnyAsync(brand => brand.Name.Equals(name), cancellationToken);
        }
    }
}
