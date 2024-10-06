using EShop.Application.CQRS.Queries.Countries;
using EShop.Application.Interfaces;
using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Entities;
using MediatR;
using CSharpFunctionalExtensions;
using EShop.Application.Issues.Errors;
using EShop.Application.Issues.Errors.Base;

namespace EShop.Infrastructure.Handlers.Queries.Countries.ById;

/// <summary>
///     Представляет обработчик запроса <see cref="GetCountryByIdQuery"/>
/// </summary>
public class GetCountryByIdQueryHandler : IRequestHandler<GetCountryByIdQuery, Result<Country, Error>>
{
    private readonly IEShopDbContext _dbContext;
    private readonly ICountryRepository _countryRepository;

    public GetCountryByIdQueryHandler(IEShopDbContext dbContext, ICountryRepository countryRepository)
    {
        _dbContext = dbContext;
        _countryRepository = countryRepository;
    }


    public async Task<Result<Country, Error>> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
    {
        var country = await _countryRepository.GetByIdAsync(request.Id, cancellationToken);

        if (country is null)
        {
            return Result.Failure<Country, Error>(new Error(new NotFoundEntityError(nameof(Country), request.Id), ErrorType.NotFound));
        }

        return country;
    }
}