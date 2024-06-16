using EShop.Application.Interfaces;
using EShop.Domain.Entities;
using EShop.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EShop.Application.Features.Queries.Countries.ById
{
    public class GetCountryByIdQueryHandler : IRequestHandler<GetCountryByIdQuery, Country>
    {
        private readonly IEShopDbContext _dbContext;

        public GetCountryByIdQueryHandler(IEShopDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(IEShopDbContext));
        }


        public async Task<Country> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
        {
            var country = await _dbContext.Countries
                .FirstOrDefaultAsync(country => country.Id == request.Id, cancellationToken);

            if (country == null)
            {
                throw new NotFoundException(nameof(Country), request.Id);
            }

            return country;
        }
    }
}
