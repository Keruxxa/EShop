using EShop.Application.CQRS.Commands.Brands;
using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Entities;
using MediatR;
using CSharpFunctionalExtensions;
using EShop.Application.Issues.Errors;
using EShop.Application.Issues.Errors.Base;

namespace EShop.Infrastructure.Handlers.Commands.Brands.Delete;

/// <summary>
///     Представляет обработчик команды <see cref="DeleteBrandCommand"/>
/// </summary>
public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, Result<Unit, Error>>
{
    private readonly IBrandRepository _brandRepository;

    public DeleteBrandCommandHandler(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }


    public async Task<Result<Unit, Error>> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
    {
        var brand = await _brandRepository.GetByIdAsync(request.Id, cancellationToken);

        if (brand is null)
        {
            return Result.Failure<Unit, Error>(new Error(new NotFoundEntityError(nameof(Brand), request.Id), ErrorType.NotFound));
        }

        _brandRepository.Delete(brand);

        var saved = await _brandRepository.SaveChangesAsync(cancellationToken) > 0;

        return saved
            ? Result.Success<Unit, Error>(Unit.Value)
            : Result.Failure<Unit, Error>(new Error(new ServerEntityError(), ErrorType.ServerError));
    }
}
