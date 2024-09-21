using EShop.Application.CQRS.Commands.Countries;
using EShop.Application.Interfaces;
using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Entities;
using EShop.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.Handlers.Commands.Countries.Create;

/// <summary>
///     Представляет обработчик команды <see cref="CreateCountryCommand"/>
/// </summary>
public class CreateCountryCommandHandler : IRequestHandler<CreateCountryCommand, int>
{
    private readonly IEShopDbContext _dbContext;
    private readonly ICountryRepository _countryRepository;

    public CreateCountryCommandHandler(IEShopDbContext dbContext, ICountryRepository countryRepository)
    {
        _dbContext = dbContext;
        _countryRepository = countryRepository;
    }


    public async Task<int> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
    {
        var countryExists = await _dbContext.Countries
            .AnyAsync(country => country.Name.Equals(request.Name), cancellationToken);

        if (countryExists)
        {
            throw new DuplicateEntityException(nameof(Country));
        }

        var country = new Country(request.Name);

        _countryRepository.Create(country);

        var saved = await _countryRepository.SaveChangesAsync(cancellationToken) > 0;

        return saved ? country.Id : 0;
    }
}
