using EShop.Application.CQRS.Queries.Countries;
using EShop.Application.Interfaces;
using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Entities;
using MediatR;
using CSharpFunctionalExtensions;
using EShop.Application.Issues.Errors;

namespace EShop.Infrastructure.Handlers.Queries.Countries.ById;

/// <summary>
///     Представляет обработчик запроса <see cref="GetCountryByIdQuery"/>
/// </summary>
public class GetCountryByIdQueryHandler : IRequestHandler<GetCountryByIdQuery, Result<Country>>
{
    private readonly IEShopDbContext _dbContext;
    private readonly ICountryRepository _countryRepository;

    public GetCountryByIdQueryHandler(IEShopDbContext dbContext, ICountryRepository countryRepository)
    {
        _dbContext = dbContext;
        _countryRepository = countryRepository;
    }


    public async Task<Result<Country>> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
    {
        var country = await _countryRepository.GetByIdAsync(request.Id, cancellationToken);

        if (country is null)
        {
            return Result.Failure<Country>(new NotFoundEntityError(nameof(Country), request.Id).Message);
        }

        return country;
    }
}