using EShop.Application.CQRS.Commands.Countries;
using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Entities;
using MediatR;
using CSharpFunctionalExtensions;
using EShop.Application.Issues.Errors.Base;
using EShop.Application.Issues.Errors;
using EShop.Application.Interfaces.Services;

namespace EShop.Infrastructure.Handlers.Commands.Countries.Create;

/// <summary>
///     Представляет обработчик команды <see cref="CreateCountryCommand"/>
/// </summary>
public class CreateCountryCommandHandler : IRequestHandler<CreateCountryCommand, Result<int, Error>>
{
    private readonly ICountryRepository _countryRepository;
    private readonly ICountryService _countryService;

    public CreateCountryCommandHandler(ICountryRepository countryRepository, ICountryService countryService)
    {
        _countryRepository = countryRepository;
        _countryService = countryService;
    }


    public async Task<Result<int, Error>> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
    {
        if (!await _countryService.IsNameUniqueAsync(request.Name, cancellationToken))
        {
            return Result.Failure<int, Error>(new Error(new DuplicateEntityError(nameof(Country)), ErrorType.Duplicate));
        }

        var country = new Country(request.Name);

        _countryRepository.Create(country);

        var isSaved = await _countryRepository.SaveChangesAsync(cancellationToken) > 0;

        return isSaved
            ? Result.Success<int, Error>(country.Id)
            : Result.Failure<int, Error>(new Error(new ServerEntityError(), ErrorType.ServerError));
    }
}
