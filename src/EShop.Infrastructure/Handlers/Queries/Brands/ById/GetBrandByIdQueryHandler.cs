using EShop.Application.CQRS.Queries.Brands;
using EShop.Application.Interfaces;
using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Entities;
using EShop.Application.Exceptions;
using MediatR;

namespace EShop.Infrastructure.Handlers.Queries.Brands.ById;

/// <summary>
///     Представляет обработчик запроса <see cref="GetBrandByIdQuery"/>
/// </summary>
public class GetBrandByIdQueryHandler : IRequestHandler<GetBrandByIdQuery, Brand>
{
    private readonly IEShopDbContext _dbContext;
    private readonly IBrandRepository _brandRepository;

    public GetBrandByIdQueryHandler(IEShopDbContext dbContext, IBrandRepository brandRepository)
    {
        _dbContext = dbContext;
        _brandRepository = brandRepository;
    }


    public async Task<Brand> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
    {
        var brand = await _brandRepository.GetByIdAsync(request.Id, cancellationToken);

        if (brand is null)
        {
            throw new NotFoundException(nameof(Brand), request.Id);
        }

        return brand;
    }
}
