using EShop.Domain.Entities;
using MediatR;

namespace EShop.Application.Features.Queries.Brands
{
    /// <summary>
    ///     Представляет запрос для получения бренда по его Id
    /// </summary>
    public class GetBrandByIdQuery : EntityBase<int>, IRequest<Brand>
    {
    }
}
