using EShop.Application.CQRS.Commands.Brands;
using EShop.Application.Interfaces;
using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Entities;
using EShop.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.Handlers.Commands.Brands.Create;

/// <summary>
///     Представляет обработчик команды <see cref="CreateBrandCommand"/>
/// </summary>
public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, int>
{
    private readonly IEShopDbContext _dbContext;
    private readonly IBrandRepository _brandRepository;

    public CreateBrandCommandHandler(IEShopDbContext dbContext, IBrandRepository brandRepository)
    {
        _dbContext = dbContext;
        _brandRepository = brandRepository;
    }


    public async Task<int> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        var brandExists = await _dbContext.Brands
            .AnyAsync(brand =>
                brand.Name.Equals(request.Name), cancellationToken);

        if (brandExists)
        {
            throw new DuplicateEntityException(nameof(Brand));
        }

        var brand = new Brand(request.Name);

        _brandRepository.Create(brand);

        var saved = await _brandRepository.SaveChangesAsync(cancellationToken) > 0;

        return saved ? brand.Id : 0;
    }
}