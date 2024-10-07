using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Queries.Products;
using EShop.Application.Dtos.Product;
using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Entities;
using MapsterMapper;
using MediatR;
using EShop.Application.Issues.Errors;
using EShop.Application.Issues.Errors.Base;

namespace EShop.Infrastructure.Handlers.Queries.Products.ById;

/// <summary>
///     Представялет обработчик запроса <see cref="GetProductByIdQuery"/>
/// </summary>
public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<ProductDto, Error>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }


    public async Task<Result<ProductDto, Error>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);

        if (product is null)
        {
            return Result.Failure<ProductDto, Error>(new Error(new NotFoundEntityError(nameof(Product), request.Id), ErrorType.NotFound));
        }

        var productDto = _mapper.From(product).AdaptToType<ProductDto>();

        return Result.Success<ProductDto, Error>(productDto);
    }
}

