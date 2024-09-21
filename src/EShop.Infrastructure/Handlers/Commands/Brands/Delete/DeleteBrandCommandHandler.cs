using EShop.Application.CQRS.Commands.Brands;
using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Entities;
using EShop.Application.Exceptions;
using MediatR;

namespace EShop.Infrastructure.Handlers.Commands.Brands.Delete;

/// <summary>
///     Представляет обработчик команды <see cref="DeleteBrandCommand"/>
/// </summary>
public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, bool>
{
    private readonly IBrandRepository _brandRepository;

    public DeleteBrandCommandHandler(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }


    public async Task<bool> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
    {
        var brand = await _brandRepository.GetByIdAsync(request.Id, cancellationToken);

        if (brand is null)
        {
            throw new NotFoundException(nameof(Brand), request.Id);
        }

        _brandRepository.Delete(brand);

        return await _brandRepository.SaveChangesAsync(cancellationToken) > 0;
    }
}
