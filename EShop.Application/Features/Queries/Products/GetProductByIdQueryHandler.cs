using EShop.Application.Dtos.Product;
using EShop.Application.Extensions.DtoMapping;
using EShop.Application.Interfaces;
using EShop.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EShop.Application.Features.Queries.Products
{
    /// <summary>
    ///     Представялет обработчик команды <see cref="GetProductByIdQuery"/>
    /// </summary>
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IEShopDbContext _dbContext;

        public GetProductByIdQueryHandler(IEShopDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _dbContext.Products
                .FirstOrDefaultAsync(product => product.Id == request.Id, cancellationToken);

            if (product == null)
            {
                throw new NotFoundException(nameof(product), request.Id);
            }

            return product.ToDto();
        }
    }
}
