using EShop.Domain.Entities;
using MediatR;

namespace EShop.Application.Features.Queries.Brands.ById
{
    /// <summary>
    ///     Представляет запрос для получения бренда по его Id
    /// </summary>
    public record GetBrandByIdQuery(int Id) : IRequest<Brand>;
}
