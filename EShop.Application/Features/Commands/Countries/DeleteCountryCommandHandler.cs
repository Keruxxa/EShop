using EShop.Application.Interfaces;
using EShop.Domain.Entities;
using EShop.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EShop.Application.Features.Commands.Countries
{
    public class DeleteCountryCommandHandler : IRequestHandler<DeleteCountryCommand, bool>
    {
        private readonly IEShopDbContext _dbContext;

        public DeleteCountryCommandHandler(IEShopDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(IEShopDbContext));
        }

        public async Task<bool> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
        {
            var country = await _dbContext
                .Countries.FirstOrDefaultAsync(country => country.Id == request.Id, cancellationToken);

            if (country == null)
            {
                throw new NotFoundException(nameof(Country), request.Id);
            }

            _dbContext.Countries.Remove(country);

            return await _dbContext.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
