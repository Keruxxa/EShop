using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Commands.Brands;
using EShop.Application.Interfaces;
using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using EShop.Application.Issues.Errors;
using EShop.Application.Issues.Errors.Base;

namespace EShop.Infrastructure.Handlers.Commands.Brands.Update;

public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, Result<Unit, Error>>
{
    private readonly IEShopDbContext _dbContext;
    private readonly IBrandRepository _brandRepository;

    public UpdateBrandCommandHandler(IEShopDbContext dbContext, IBrandRepository brandRepository)
    {
        _dbContext = dbContext;
        _brandRepository = brandRepository;
    }


    public async Task<Result<Unit, Error>> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
    {
        var brand = await _brandRepository.GetByIdAsync(request.Id, cancellationToken);

        if (brand is null)
        {
            return Result.Failure<Unit, Error>(new Error(new NotFoundEntityError(nameof(Brand), request.Id), ErrorType.NotFound));
        }

        var isNameTaken = await _dbContext.Brands
            .AnyAsync(brand => brand.Name.Equals(request.Name), cancellationToken);

        if (isNameTaken)
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
