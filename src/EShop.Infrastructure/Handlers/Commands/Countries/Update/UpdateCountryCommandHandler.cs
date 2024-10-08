using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Commands.Countries;
using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Entities;
using MediatR;
using EShop.Application.Issues.Errors.Base;
using EShop.Application.Issues.Errors;
using EShop.Application.Interfaces.Services;

namespace EShop.Infrastructure.Handlers.Commands.Countries.Update;

/// <summary>
///     Представляет обработчик команды <see cref="UpdateCountryCommand"/>
/// </summary>
public class UpdateCountryCommandHandler : IRequestHandler<UpdateCountryCommand, Result<Unit, Error>>
{
    private readonly ICountryRepository _countryRepository;
    private readonly ICountryService _countryService;

    public UpdateCountryCommandHandler(ICountryRepository countryRepository, ICountryService countryService)
    {
        _countryRepository = countryRepository;
        _countryService = countryService;
    }


    public async Task<Result<Unit, Error>> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
    {
        var country = await _countryRepository.GetByIdAsync(request.Id, cancellationToken);

        if (country is null)
        {
            return Result.Failure<Unit, Error>(new Error(new NotFoundEntityError(nameof(Country), request.Id), ErrorType.NotFound));
        }

        if (!await _countryService.IsNameUniqueAsync(request.Name, cancellationToken))
        {
            return Result.Failure<Unit, Error>(new Error(new DuplicateEntityError(nameof(Country)), ErrorType.Duplicate));
        }

        country.UpdateName(request.Name);

        _countryRepository.Update(country);

        var isSaved = await _countryRepository.SaveChangesAsync(cancellationToken) > 0;

        return isSaved
            ? Result.Success<Unit, Error>(Unit.Value)
            : Result.Failure<Unit, Error>(new Error(new ServerEntityError(), ErrorType.ServerError));
    }
}