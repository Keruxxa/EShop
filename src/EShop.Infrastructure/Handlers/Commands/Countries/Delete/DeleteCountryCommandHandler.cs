using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Commands.Countries;
using EShop.Application.Interfaces;
using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Entities;
using MediatR;
using EShop.Application.Issues.Errors.Base;
using EShop.Application.Issues.Errors;

namespace EShop.Infrastructure.Handlers.Commands.Countries.Delete;

/// <summary>
///     Представляет обработчик команды <see cref="DeleteCountryCommand"/>
/// </summary>
public class DeleteCountryCommandHandler : IRequestHandler<DeleteCountryCommand, Result<Unit, Error>>
{
    private readonly IEShopDbContext _dbContext;
    private readonly ICountryRepository _countryRepository;

    public DeleteCountryCommandHandler(IEShopDbContext dbContext, ICountryRepository countryRepository)
    {
        _dbContext = dbContext;
        _countryRepository = countryRepository;
    }

    public async Task<Result<Unit, Error>> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
    {
        var country = await _countryRepository.GetByIdAsync(request.Id, cancellationToken);

        if (country is null)
        {
            return Result.Failure<Unit, Error>(new Error(new NotFoundEntityError(nameof(Country), request.Id), ErrorType.NotFound));
        }

        _countryRepository.Delete(country);

        var saved = await _countryRepository.SaveChangesAsync(cancellationToken) > 0;

        return saved
            ? Result.Success<Unit, Error>(Unit.Value)
            : Result.Failure<Unit, Error>(new Error(new ServerEntityError(), ErrorType.ServerError));
    }
}
