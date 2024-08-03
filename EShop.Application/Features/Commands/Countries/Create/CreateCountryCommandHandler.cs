using EShop.Application.Interfaces;
using EShop.Domain.Entities;
using MediatR;

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
        var country = new Country(request.Name);

        await _dbContext.Countries.AddAsync(country, cancellationToken);

        var saved = await _dbContext.SaveChangesAsync(cancellationToken) > 0;

        return saved ? country.Id : 0;
    }
}
