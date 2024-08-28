using CSharpFunctionalExtensions;
using EShop.Application.Interfaces;
using EShop.Domain.Entities;
using EShop.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

using static EShop.Application.Constants;

namespace EShop.Application.Features.Commands.Countries.Update;

/// <summary>
///     Представляет обработчик команды <see cref="UpdateCountryCommand"/>
/// </summary>
public class UpdateCountryCommandHandler : IRequestHandler<UpdateCountryCommand, Result>
{
    private readonly IEShopDbContext _dbContext;

    public UpdateCountryCommandHandler(IEShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<Result> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
    {
        var country = await _dbContext.Countries
            .FirstOrDefaultAsync(country => country.Id == request.Id, cancellationToken);

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

        _dbContext.Countries.Update(country);
        var saved = await _dbContext.SaveChangesAsync(cancellationToken) > 0;

        return saved
            ? Result.Success()
            : Result.Failure<int>(SERVER_SIDE_ERROR);
    }
}
