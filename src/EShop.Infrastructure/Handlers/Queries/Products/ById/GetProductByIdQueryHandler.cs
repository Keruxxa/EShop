using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Queries.Products;
using EShop.Application.Dtos.Product;
using EShop.Application.Interfaces;
using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Entities;
using EShop.Domain.Exceptions;
using MapsterMapper;
using MediatR;

namespace EShop.Infrastructure.Handlers.Queries.Products.ById;

/// <summary>
///     Представялет обработчик запроса <see cref="GetProductByIdQuery"/>
/// </summary>
public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<ProductDto>>
{
    private readonly IEShopDbContext _dbContext;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IEShopDbContext dbContext, IProductRepository productRepository, IMapper mapper)
    {
        _dbContext = dbContext;
        _productRepository = productRepository;
        _mapper = mapper;
    }


    public async Task<Result<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);

        if (product is null)
        {
            throw new NotFoundException(nameof(Product), request.Id);
        }

        var productDto = _mapper.From(product).AdaptToType<ProductDto>();

        return Result.Success(productDto);
    }
}

