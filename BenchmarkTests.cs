using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using DriverSearch.Models;
using DriverSearch.Algorithms;

namespace DriverSearch.Benchmarks
{
    [MemoryDiagnoser]
    public class DriverSearchBenchmarks
    {
        private List<Driver> _drivers;
        private Order _order;
        private readonly IDistanceCalculator _bruteForce = new BruteForceAlgorithm();
        private readonly IDistanceCalculator _sorting = new SortingAlgorithm();
        private readonly IDistanceCalculator _heap = new HeapAlgorithm();

        [Params(100, 1000, 10000)]
        public int DriverCount { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            var random = new Random(42);
            _drivers = new List<Driver>();
            
            for (int i = 0; i < DriverCount; i++)
            {
                _drivers.Add(new Driver(i, random.Next(0, 100), random.Next(0, 100)));
            }
            
            _order = new Order(50, 50);
        }

        [Benchmark]
        public List<Driver> Algorithm1_BruteForce() => _bruteForce.FindNearestDrivers(_drivers, _order, 5);

        [Benchmark]
        public List<Driver> Algorithm2_Sorting() => _sorting.FindNearestDrivers(_drivers, _order, 5);

        [Benchmark]
        public List<Driver> Algorithm3_Heap() => _heap.FindNearestDrivers(_drivers, _order, 5);
    }

    class Program
    {
        static void Main()
        {
            var summary = BenchmarkRunner.Run<DriverSearchBenchmarks>();
        }
    }
}
