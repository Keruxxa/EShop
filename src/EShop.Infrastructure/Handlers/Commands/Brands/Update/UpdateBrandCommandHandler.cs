using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Commands.Brands;
using EShop.Application.Interfaces;
using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Entities;
using EShop.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static EShop.Application.Constants;

namespace EShop.Infrastructure.Handlers.Commands.Brands.Update;

public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, Result>
{
    private readonly IEShopDbContext _dbContext;
    private readonly IBrandRepository _brandRepository;

    public UpdateBrandCommandHandler(IEShopDbContext dbContext, IBrandRepository brandRepository)
    {
        _dbContext = dbContext;
        _brandRepository = brandRepository;
    }


    public async Task<Result> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
    {
        var brand = await _brandRepository.GetByIdAsync(request.Id, cancellationToken);

        if (brand is null)
        {
            throw new NotFoundException(nameof(Brand), request.Id);
        }

        var isNameTaken = await _dbContext.Brands
            .AnyAsync(brand => brand.Name.Equals(request.Name), cancellationToken);

        if (isNameTaken)
        {
            return Result.Failure(new DuplicateEntityException(nameof(Brand)).Message);
        }

        brand.UpdateName(request.Name);

        _brandRepository.Update(brand);

        var saved = await _brandRepository.SaveChangesAsync(cancellationToken) > 0;

        return saved
            ? Result.Success()
            : Result.Failure(SERVER_SIDE_ERROR);
    }
}
