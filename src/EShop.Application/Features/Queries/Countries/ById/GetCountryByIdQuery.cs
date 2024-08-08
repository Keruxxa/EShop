using EShop.Domain.Entities;
using MediatR;

namespace EShop.Application.Features.Queries.Countries.ById;

/// <summary>
///     Представляет запрос для получения страны по ее Id
/// </summary>
public record GetCountryByIdQuery(int Id) : IRequest<Country>;
