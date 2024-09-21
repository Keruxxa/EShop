using EShop.Application.CQRS.Commands.Brands;
using EShop.Application.Interfaces;
using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CSharpFunctionalExtensions;
using EShop.Application.Issues.Errors;
using EShop.Application.Issues.Errors.Base;

namespace EShop.Infrastructure.Handlers.Commands.Brands.Create;

/// <summary>
///     Представляет обработчик команды <see cref="CreateBrandCommand"/>
/// </summary>
public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, Result<int, Error>>
{
    private readonly IEShopDbContext _dbContext;
    private readonly IBrandRepository _brandRepository;

    public CreateBrandCommandHandler(IEShopDbContext dbContext, IBrandRepository brandRepository)
    {
        _dbContext = dbContext;
        _brandRepository = brandRepository;
    }


    public async Task<Result<int, Error>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        var brandExists = await _dbContext.Brands
            .AnyAsync(brand =>
                brand.Name.Equals(request.Name), cancellationToken);

        if (brandExists)
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