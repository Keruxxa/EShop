using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Commands.Countries;
using EShop.Application.Interfaces;
using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using EShop.Application.Issues.Errors.Base;
using EShop.Application.Issues.Errors;

namespace EShop.Infrastructure.Handlers.Commands.Countries.Update;

/// <summary>
///     Представляет обработчик команды <see cref="UpdateCountryCommand"/>
/// </summary>
public class UpdateCountryCommandHandler : IRequestHandler<UpdateCountryCommand, Result<Unit, Error>>
{
    private readonly IEShopDbContext _dbContext;
    private readonly ICountryRepository _countryRepository;

    public UpdateCountryCommandHandler(IEShopDbContext dbContext, ICountryRepository countryRepository)
    {
        _dbContext = dbContext;
        _countryRepository = countryRepository;
    }


    public async Task<Result<Unit, Error>> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
    {
        var country = await _countryRepository.GetByIdAsync(request.Id, cancellationToken);

        if (country is null)
        {
            return Result.Failure<Unit, Error>(new Error(new NotFoundEntityError(nameof(Country), request.Id), ErrorType.NotFound));
        }

        var nameIsTaken = await _dbContext.Countries
            .AnyAsync(country =>
                country.Name.Equals(request.Name) &&
                country.Id != request.Id, cancellationToken);

        if (nameIsTaken)
        {
            return Result.Failure<Unit, Error>(new Error(new DuplicateEntityError(nameof(Country)), ErrorType.Duplicate));
        }

        country.UpdateName(request.Name);

        _countryRepository.Update(country);

        var saved = await _countryRepository.SaveChangesAsync(cancellationToken) > 0;

        return saved
            ? Result.Success<Unit, Error>(Unit.Value)
            : Result.Failure<Unit, Error>(new Error(new ServerEntityError(), ErrorType.ServerError));
    }
}