using BenchmarkDotNet.Attributes;
using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Commands.Brands;
using EShop.Application.Exceptions;
using EShop.Application.Issues.Errors;
using EShop.Domain.Entities;
using EShop.Infrastructure.Data;
using EShop.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Benchmarks.Brands;

#pragma warning disable CS8618
[MemoryDiagnoser]
public class DeleteBrandBenchmark
{
    private BrandRepository _brandRepository;
    private EShopDbContext _dbContext;
    private int wrongId = 50;

    [GlobalSetup]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<EShopDbContext>()
            .UseNpgsql("Host=localhost; Port=5432; Database=EShop; Username=postgres; Password=superuser").Options;
        _dbContext = new EShopDbContext(options);
        _brandRepository = new BrandRepository(_dbContext);
    }

    [Benchmark]
    public async Task HandleException()
    {
        var request = new DeleteBrandCommand(wrongId);
        var cancellationToken = CancellationToken.None;

        Brand brand = null;

        try
        {
            brand = await _brandRepository.GetByIdAsync(request.Id, cancellationToken);

            if (brand is null)
            {
                throw new NotFoundException(nameof(Brand), request.Id);
            }

            _brandRepository.Delete(brand);

            await _brandRepository.SaveChangesAsync(cancellationToken);
        }
        catch (NotFoundException exception)
        {
            string exceptionMessage = exception.Message;
        }
    }

    [Benchmark]
    public async Task<Result> HandleMessage()
    {
        var request = new DeleteBrandCommand(wrongId);
        var cancellationToken = CancellationToken.None;

        var brand = await _brandRepository.GetByIdAsync(request.Id, cancellationToken);

        if (brand is null)
        {
            return Result.Failure(new NotFoundEntityError(nameof(Brand), request.Id).Message);
        }

        _brandRepository.Delete(brand);

        var saved = await _brandRepository.SaveChangesAsync(cancellationToken) > 0;

        return saved
            ? Result.Success()
            : Result.Failure("Error");
    }


    [GlobalCleanup]
    public void GlobalCleanup()
    {
        _dbContext.Dispose();
    }
}
