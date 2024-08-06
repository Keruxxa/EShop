using EShop.Application.Interfaces;
using EShop.Domain.Entities;
using EShop.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EShop.Application.Features.Commands.Countries.Create;

/// <summary>
///     Представляет обработчик команды <see cref="CreateCountryCommand"/>
/// </summary>
public class CreateCountryCommandHandler : IRequestHandler<CreateCountryCommand, int>
{
    private readonly IEShopDbContext _dbContext;

    public CreateCountryCommandHandler(IEShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<int> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
    {
        var countryExists = await _dbContext.Countries
            .AnyAsync(country => country.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase), cancellationToken);

        if (countryExists)
        {
            throw new DuplicateEntityException(nameof(Country));
        }

        var country = new Country(request.Name);

        _dbContext.Countries.Add(country);

        var saved = await _dbContext.SaveChangesAsync(cancellationToken) > 0;

        return saved ? country.Id : 0;
    }
}
