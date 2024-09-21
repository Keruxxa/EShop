using EShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using EShop.Application.CQRS.Queries.Brands;
using EShop.Domain.Entities;
using EShop.Infrastructure.Repositories;
using CSharpFunctionalExtensions;
using BenchmarkDotNet.Attributes;
using EShop.Application.Exceptions;

namespace Benchmarks.Brands;

#pragma warning disable CS8618
[MemoryDiagnoser]
public class GetBrandByIdBenchmark
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
    public async Task<Brand> HandleException()
    {
        var request = new GetBrandByIdQuery(wrongId);
        var cancellationToken = CancellationToken.None;

        Brand brand = null;

        try
        {
            brand = await _brandRepository.GetByIdAsync(request.Id, cancellationToken);

            if (brand is null)
            {
                throw new NotFoundException(nameof(Brand), request.Id);
            }

            return brand;
        }
        catch (NotFoundException exception)
        {
            string exceptionMessage = exception.Message;
        }

        return brand;
    }

    [Benchmark]
    public async Task<Result<Brand>> HandleResult()
    {
        var request = new GetBrandByIdQuery(wrongId);
        var cancellationToken = CancellationToken.None;

        var brand = await _brandRepository.GetByIdAsync(request.Id, cancellationToken);

        if (brand is null)
        {
            return Result.Failure<Brand>(new NotFoundException(nameof(Brand), request.Id).Message);
        }

        return brand;
    }

    [GlobalCleanup]
    public void GlobalCleanup()
    {
        _dbContext.Dispose();
    }
}
