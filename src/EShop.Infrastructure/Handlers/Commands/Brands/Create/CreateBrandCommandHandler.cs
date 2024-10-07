using EShop.Application.CQRS.Commands.Brands;
using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Entities;
using MediatR;
using CSharpFunctionalExtensions;
using EShop.Application.Issues.Errors;
using EShop.Application.Issues.Errors.Base;
using EShop.Application.Interfaces.Services;

namespace EShop.Infrastructure.Handlers.Commands.Brands.Create;

/// <summary>
///     Представляет обработчик команды <see cref="CreateBrandCommand"/>
/// </summary>
public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, Result<int, Error>>
{
    private readonly IBrandRepository _brandRepository;
    private readonly IBrandService _brandService;

    public CreateBrandCommandHandler(IBrandRepository brandRepository, IBrandService brandService)
    {
        _brandRepository = brandRepository;
        _brandService = brandService;
    }


    public async Task<Result<int, Error>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        if (!await _brandService.IsNameUniqueAsync(request.Name, cancellationToken))
        {
            return Result.Failure<int, Error>(new Error(new DuplicateEntityError(nameof(Brand)), ErrorType.Duplicate));
        }

        var brand = new Brand(request.Name);

        _brandRepository.Create(brand);

        var saved = await _brandRepository.SaveChangesAsync(cancellationToken) > 0;

        return saved
            ? Result.Success<int, Error>(brand.Id)
            : Result.Failure<int, Error>(new Error(new ServerEntityError(), ErrorType.ServerError));
    }
}