using EShop.Application.CQRS.Commands.Countries;
using EShop.Application.Interfaces;
using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CSharpFunctionalExtensions;
using EShop.Application.Issues.Errors.Base;
using EShop.Application.Issues.Errors;

namespace EShop.Infrastructure.Handlers.Commands.Countries.Create;

/// <summary>
///     Представляет обработчик команды <see cref="CreateCountryCommand"/>
/// </summary>
public class CreateCountryCommandHandler : IRequestHandler<CreateCountryCommand, Result<int, Error>>
{
    private readonly IEShopDbContext _dbContext;
    private readonly ICountryRepository _countryRepository;

    public CreateCountryCommandHandler(IEShopDbContext dbContext, ICountryRepository countryRepository)
    {
        _dbContext = dbContext;
        _countryRepository = countryRepository;
    }


    public async Task<Result<int, Error>> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
    {
        var countryExists = await _dbContext.Countries
            .AnyAsync(country => country.Name.Equals(request.Name), cancellationToken);

        if (countryExists)
        {
            return Result.Failure<int, Error>(new Error(new DuplicateEntityError(nameof(Country)), ErrorType.Duplicate));
        }

        var country = new Country(request.Name);

        _countryRepository.Create(country);

        var saved = await _countryRepository.SaveChangesAsync(cancellationToken) > 0;

        return saved
            ? Result.Success<int, Error>(country.Id)
            : Result.Failure<int, Error>(new Error(new ServerEntityError(), ErrorType.ServerError));
    }
}
