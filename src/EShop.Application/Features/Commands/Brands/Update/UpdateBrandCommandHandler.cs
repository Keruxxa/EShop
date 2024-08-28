using CSharpFunctionalExtensions;
using EShop.Application.Interfaces;
using EShop.Domain.Entities;
using EShop.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

using static EShop.Application.Constants;

namespace EShop.Application.Features.Commands.Brands.Update;

public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, Result<int>>
{
    private readonly IEShopDbContext _dbContext;

    public UpdateBrandCommandHandler(IEShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<Result<int>> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
    {
        var brand = await _dbContext.Brands
            .FirstOrDefaultAsync(brand => brand.Id == request.Id, cancellationToken);

        if (brand is null)
        {
            throw new NotFoundException(nameof(Brand), request.Id);
        }

        var isNameTaken = await _dbContext.Brands
            .AnyAsync(brand => brand.Name.Equals(request.Name), cancellationToken);

        if (isNameTaken)
        {
            throw new DuplicateEntityException(nameof(Brand));
        }

        brand.UpdateName(request.Name);

        _dbContext.Brands.Update(brand);

        var saved = await _dbContext.SaveChangesAsync(cancellationToken) > 0;

        return saved
            ? Result.Success(brand.Id)
            : Result.Failure<int>(SERVER_SIDE_ERROR);
    }
}
