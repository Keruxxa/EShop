using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Commands.Countries;
using EShop.Application.Interfaces;
using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Entities;
using EShop.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static EShop.Application.Constants;

namespace EShop.Infrastructure.Handlers.Commands.Countries.Update;

/// <summary>
///     Представляет обработчик команды <see cref="UpdateCountryCommand"/>
/// </summary>
public class UpdateCountryCommandHandler : IRequestHandler<UpdateCountryCommand, Result>
{
    private readonly IEShopDbContext _dbContext;
    private readonly ICountryRepository _countryRepository;

    public UpdateCountryCommandHandler(IEShopDbContext dbContext, ICountryRepository countryRepository)
    {
        _dbContext = dbContext;
        _countryRepository = countryRepository;
    }


    public async Task<Result> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
    {
        var country = await _countryRepository.GetByIdAsync(request.Id, cancellationToken);

        if (country is null)
        {
            throw new NotFoundException(nameof(Country), request.Id);
        }

        var nameIsTaken = await _dbContext.Countries
            .AnyAsync(country =>
                country.Name.Equals(request.Name) &&
                country.Id != request.Id, cancellationToken);

        if (nameIsTaken)
        {
            throw new DuplicateEntityException(nameof(Country));
        }

        country.UpdateName(request.Name);

        _countryRepository.Update(country);

        var saved = await _countryRepository.SaveChangesAsync(cancellationToken) > 0;

        return saved
            ? Result.Success()
            : Result.Failure<int>(SERVER_SIDE_ERROR);
    }
}