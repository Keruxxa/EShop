using EShop.Application.Interfaces;
using EShop.Domain.Entities;
using EShop.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EShop.Application.Features.Commands.Brands.Create;

/// <summary>
///     Представляет обработчик команды <see cref="CreateBrandCommand"/>
/// </summary>
public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, int>
{
    private readonly IEShopDbContext _dbContext;

    public CreateBrandCommandHandler(IEShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<int> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        var brandExists = await _dbContext.Brands
            .AnyAsync(brand =>
                brand.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase), cancellationToken);

        if (brandExists)
        {
            throw new DuplicateEntityException(nameof(Brand));
        }

        var brand = new Brand(request.Name);

        _dbContext.Brands.Add(brand);

        var saved = await _dbContext.SaveChangesAsync(cancellationToken) > 0;

        return saved ? brand.Id : 0;
    }
}
