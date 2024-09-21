using BenchmarkDotNet.Running;
using Benchmarks.Brands;

namespace Benchmarks;

internal class Program
{
    static void Main(string[] args)
    {
        BenchmarkRunner.Run<GetBrandByIdBenchmark>();
    }
}
