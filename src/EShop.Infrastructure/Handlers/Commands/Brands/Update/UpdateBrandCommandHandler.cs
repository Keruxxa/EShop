using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Commands.Brands;
using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Entities;
using MediatR;
using EShop.Application.Issues.Errors;
using EShop.Application.Issues.Errors.Base;
using EShop.Application.Interfaces.Services;

namespace EShop.Infrastructure.Handlers.Commands.Brands.Update;

public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, Result<Unit, Error>>
{
    private readonly IBrandRepository _brandRepository;
    private readonly IBrandService _brandService;

    public UpdateBrandCommandHandler(IBrandRepository brandRepository, IBrandService brandService)
    {
        _brandRepository = brandRepository;
        _brandService = brandService;
    }


    public async Task<Result<Unit, Error>> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
    {
        var brand = await _brandRepository.GetByIdAsync(request.Id, cancellationToken);

        if (brand is null)
        {
            return Result.Failure<Unit, Error>(new Error(new NotFoundEntityError(nameof(Brand), request.Id), ErrorType.NotFound));
        }

        if (!await _brandService.IsNameUniqueAsync(request.Name, cancellationToken))
        {
            return Result.Failure<Unit, Error>(new Error(new DuplicateEntityError(nameof(Brand)), ErrorType.Duplicate));
        }

        brand.UpdateName(request.Name);

        _brandRepository.Update(brand);

        var saved = await _brandRepository.SaveChangesAsync(cancellationToken) > 0;

        return saved
            ? Result.Success<Unit, Error>(Unit.Value)
            : Result.Failure<Unit, Error>(new Error(new ServerEntityError(), ErrorType.ServerError));
    }
}
