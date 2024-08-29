using EShop.Application.CQRS.Queries.Countries;
using EShop.Application.Interfaces;
using EShop.Domain.Entities;
using EShop.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.Handlers.Queries.Countries.ById;

/// <summary>
///     Представляет обработчик запроса <see cref="GetCountryByIdQuery"/>
/// </summary>
public class GetCountryByIdQueryHandler : IRequestHandler<GetCountryByIdQuery, Country>
{
    private readonly IEShopDbContext _dbContext;

    public GetCountryByIdQueryHandler(IEShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<Country> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
    {
        var country = await _dbContext.Countries
            .FirstOrDefaultAsync(country => country.Id == request.Id, cancellationToken);

        if (country is null)
        {
            throw new NotFoundException(nameof(Country), request.Id);
        }

        return country;
    }
}