using System;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using NearestDrivers;

namespace NearestDrivers.Benchmarks;

[MemoryDiagnoser]
public class NearestDriverBenchmarks
{
    private DriverStorage _storage = null!;
    private const int N = 1000;
    private const int M = 1000;
    private const int OrderX = 500;
    private const int OrderY = 500;

    [GlobalSetup]
    public void Setup()
    {
        _storage = new DriverStorage();
        var random = new Random(42);
        for (int i = 0; i < 10_000; i++)
        {
            _storage.AddOrUpdateDriver(i, random.Next(N), random.Next(M));
        }
    }

    [Benchmark]
    public object[] Linq() => DistanceAlgorithms.NearestLinq(_storage, OrderX, OrderY).ToArray();

    [Benchmark]
    public object[] SortedSet() => DistanceAlgorithms.NearestSortedSet(_storage, OrderX, OrderY).ToArray();

    [Benchmark]
    public object[] PartialSort() => DistanceAlgorithms.NearestPartialSort(_storage, OrderX, OrderY).ToArray();
}

public class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<NearestDriverBenchmarks>();
    }
}
