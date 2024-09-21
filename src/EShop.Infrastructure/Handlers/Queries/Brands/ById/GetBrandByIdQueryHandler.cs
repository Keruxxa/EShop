﻿using EShop.Application.CQRS.Queries.Brands;
using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Entities;
using MediatR;
using CSharpFunctionalExtensions;
using EShop.Application.Issues.Errors;

namespace EShop.Infrastructure.Handlers.Queries.Brands.ById;

/// <summary>
///     Представляет обработчик запроса <see cref="GetBrandByIdQuery"/>
/// </summary>
public class GetBrandByIdQueryHandler : IRequestHandler<GetBrandByIdQuery, Result<Brand>>
{
    private readonly IBrandRepository _brandRepository;

    public GetBrandByIdQueryHandler(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }


    public async Task<Result<Brand>> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
    {
        var brand = await _brandRepository.GetByIdAsync(request.Id, cancellationToken);

        if (brand is null)
        {
            return Result.Failure<Brand>(new NotFoundEntityError(nameof(Brand), request.Id).Message);
        }

        return brand;
    }
}
