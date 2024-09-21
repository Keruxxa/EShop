using EShop.Application.CQRS.Commands.Brands;
using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Entities;
using EShop.Application.Exceptions;
using MediatR;
using CSharpFunctionalExtensions;
using static EShop.Application.Constants;

namespace EShop.Infrastructure.Handlers.Commands.Brands.Delete;

/// <summary>
///     Представляет обработчик команды <see cref="DeleteBrandCommand"/>
/// </summary>
public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, Result>
{
    private readonly IBrandRepository _brandRepository;

    public DeleteBrandCommandHandler(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }


    public async Task<Result> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
    {
        var brand = await _brandRepository.GetByIdAsync(request.Id, cancellationToken);

        if (brand is null)
        {
            return Result.Failure(new NotFoundEntity(nameof(Brand), request.Id).Message);
        }

        _brandRepository.Delete(brand);

        var saved = await _brandRepository.SaveChangesAsync(cancellationToken) > 0;

        return saved
            ? Result.Success()
            : Result.Failure(SERVER_SIDE_ERROR);
    }
}
