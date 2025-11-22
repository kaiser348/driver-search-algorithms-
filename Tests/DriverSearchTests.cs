using NUnit.Framework;
using DriverSearch.Models;
using DriverSearch.Algorithms;

namespace DriverSearch.Tests
{
    [TestFixture]
    public class DriverSearchTests
    {
        private List<Driver> _testDrivers = new List<Driver>();
        private Order _testOrder = new Order(0, 0);

        [SetUp]
        public void Setup()
        {
            _testDrivers = new List<Driver>
            {
                new Driver(1, 1, 1),
                new Driver(2, 4, 4),
                new Driver(3, 5, 5),
                new Driver(4, 3, 3),
                new Driver(5, 6, 6),
                new Driver(6, 2, 2),
                new Driver(7, 7, 7),
                new Driver(8, 0, 0),
                new Driver(9, 8, 8),
                new Driver(10, 4, 3)
            };

            _testOrder = new Order(4, 4);
        }

        [Test]
        public void AllAlgorithms_ReturnSameResults()
        {
            var algorithms = new IDistanceCalculator[]
            {
                new BruteForceAlgorithm(),
                new SortingAlgorithm(),
                new HeapAlgorithm()
            };

            var results = algorithms.Select(algo => algo.FindNearestDrivers(_testDrivers, _testOrder, 5)).ToList();

            for (int i = 1; i < results.Count; i++)
            {
                CollectionAssert.AreEqual(results[0], results[i]);
            }
        }
    }
}
