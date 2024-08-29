using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Commands.Countries;
using EShop.Application.Interfaces;
using EShop.Domain.Entities;
using EShop.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static EShop.Application.Constants;

namespace EShop.Infrastructure.Handlers.Commands.Countries.Delete;

/// <summary>
///     Представляет обработчик команды <see cref="DeleteCountryCommand"/>
/// </summary>
public class DeleteCountryCommandHandler : IRequestHandler<DeleteCountryCommand, Result>
{
    private readonly IEShopDbContext _dbContext;

    public DeleteCountryCommandHandler(IEShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
    {
        var country = await _dbContext.Countries
            .FirstOrDefaultAsync(country => country.Id == request.Id, cancellationToken);

        if (country is null)
        {
            throw new NotFoundException(nameof(Country), request.Id);
        }

        _dbContext.Countries.Remove(country);

        var saved = await _dbContext.SaveChangesAsync(cancellationToken) > 0;

        return saved
            ? Result.Success()
            : Result.Failure(SERVER_SIDE_ERROR);
    }
}
