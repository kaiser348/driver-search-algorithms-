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
            // Подготовка тестовых данных
            _testDrivers = new List<Driver>
            {
                new Driver(1, 1, 1),  // расстояние: √((1-4)² + (1-4)²) = √18 ≈ 4.24
                new Driver(2, 4, 4),  // расстояние: 0 (ближайший)
                new Driver(3, 5, 5),  // расстояние: √2 ≈ 1.41
                new Driver(4, 3, 3),  // расстояние: √2 ≈ 1.41
                new Driver(5, 6, 6),  // расстояние: √8 ≈ 2.83
                new Driver(6, 2, 2),  // расстояние: √8 ≈ 2.83
                new Driver(7, 7, 7),  // расстояние: √18 ≈ 4.24
                new Driver(8, 0, 0),  // расстояние: √32 ≈ 5.66
                new Driver(9, 8, 8),  // расстояние: √32 ≈ 5.66
                new Driver(10, 4, 3)  // расстояние: 1
            };

            _testOrder = new Order(4, 4);
        }

        // ТЕСТ 1: Проверка что все три алгоритма возвращают одинаковые результаты
        [Test]
        public void AllAlgorithms_ReturnSameResults()
        {
            // Arrange
            var algorithms = new IDistanceCalculator[]
            {
                new BruteForceAlgorithm(),  // АЛГОРИТМ 1
                new SortingAlgorithm(),     // АЛГОРИТМ 2
                new HeapAlgorithm()         // АЛГОРИТМ 3
            };

            // Act
            var results = algorithms.Select(algo => algo.FindNearestDrivers(_testDrivers, _testOrder, 5)).ToList();

            // Assert
            for (int i = 1; i < results.Count; i++)
            {
                CollectionAssert.AreEqual(results[0], results[i], 
                    $"Алгоритм {i+1} должен возвращать те же результаты что и алгоритм 1");
            }
        }

        // ТЕСТ 2: Проверка АЛГОРИТМА 1 (BruteForce)
        [Test]
        public void Algorithm1_BruteForce_ReturnsCorrectDrivers()
        {
            // Arrange
            var algorithm = new BruteForceAlgorithm();

            // Act
            var result = algorithm.FindNearestDrivers(_testDrivers, _testOrder, 3);

            // Assert
            Assert.AreEqual(3, result.Count);
            // Ближайшие водители: 2 (расстояние 0), 10 (расстояние 1), 3 и 4 (расстояние √2)
            Assert.IsTrue(result.Any(d => d.Id == 2)); // Самый близкий
            Assert.IsTrue(result.Any(d => d.Id == 10)); // Второй по близости
        }

        // ТЕСТ 3: Проверка АЛГОРИТМА 2 (Sorting)
        [Test]
        public void Algorithm2_Sorting_ReturnsCorrectDrivers()
        {
            // Arrange
            var algorithm = new SortingAlgorithm();

            // Act
            var result = algorithm.FindNearestDrivers(_testDrivers, _testOrder, 4);

            // Assert
            Assert.AreEqual(4, result.Count);
            var expectedIds = new[] { 2, 10, 3, 4 }; // Ожидаемые ближайшие водители
            CollectionAssert.AreEquivalent(expectedIds, result.Select(d => d.Id));
        }

        // ТЕСТ 4: Проверка АЛГОРИТМА 3 (Heap)
        [Test]
        public void Algorithm3_Heap_ReturnsCorrectDrivers()
        {
            // Arrange
            var algorithm = new HeapAlgorithm();

            // Act
            var result = algorithm.FindNearestDrivers(_testDrivers, _testOrder, 2);

            // Assert
            Assert.AreEqual(2, result.Count);
            // Должны быть 2 ближайших водителя
            Assert.AreEqual(2, result[0].Id); // Самый близкий
            Assert.AreEqual(10, result[1].Id); // Второй по близости
        }

        // ТЕСТ 5: Проверка когда водителей меньше чем запрашиваемое количество
        [Test]
        public void AllAlgorithms_WhenFewerDriversThanRequested_ReturnAllDrivers()
        {
            // Arrange
            var fewDrivers = new List<Driver>
            {
                new Driver(1, 1, 1),
                new Driver(2, 2, 2)
            };
            var algorithms = new IDistanceCalculator[]
            {
                new BruteForceAlgorithm(),  // АЛГОРИТМ 1
                new SortingAlgorithm(),     // АЛГОРИТМ 2
                new HeapAlgorithm()         // АЛГОРИТМ 3
            };

            foreach (var algorithm in algorithms)
            {
                // Act
                var result = algorithm.FindNearestDrivers(fewDrivers, _testOrder, 5);

                // Assert
                Assert.AreEqual(2, result.Count, $"{algorithm.GetType().Name} должен вернуть всех водителей");
                CollectionAssert.AreEquivalent(fewDrivers.Select(d => d.Id), result.Select(d => d.Id));
            }
        }

        // ТЕСТ 6: Проверка пустого списка водителей
        [Test]
        public void AllAlgorithms_WhenNoDrivers_ReturnEmptyList()
        {
            // Arrange
            var emptyDrivers = new List<Driver>();
            var algorithms = new IDistanceCalculator[]
            {
                new BruteForceAlgorithm(),  // АЛГОРИТМ 1
                new SortingAlgorithm(),     // АЛГОРИТМ 2
                new HeapAlgorithm()         // АЛГОРИТМ 3
            };

            foreach (var algorithm in algorithms)
            {
                // Act
                var result = algorithm.FindNearestDrivers(emptyDrivers, _testOrder, 5);

                // Assert
                Assert.IsEmpty(result, $"{algorithm.GetType().Name} должен вернуть пустой список");
            }
        }

        // ТЕСТ 7: Проверка корректности сортировки по расстоянию
        [Test]
        public void AllAlgorithms_ReturnDriversInDistanceOrder()
        {
            // Arrange
            var algorithms = new IDistanceCalculator[]
            {
                new BruteForceAlgorithm(),  // АЛГОРИТМ 1
                new SortingAlgorithm(),     // АЛГОРИТМ 2
                new HeapAlgorithm()         // АЛГОРИТМ 3
            };

            foreach (var algorithm in algorithms)
            {
                // Act
                var result = algorithm.FindNearestDrivers(_testDrivers, _testOrder, 5);

                // Assert - проверяем что водители отсортированы по возрастанию расстояния
                for (int i = 0; i < result.Count - 1; i++)
                {
                    var dist1 = CalculateDistance(result[i], _testOrder);
                    var dist2 = CalculateDistance(result[i + 1], _testOrder);
                    Assert.LessOrEqual(dist1, dist2, 
                        $"{algorithm.GetType().Name}: Водители должны быть отсортированы по расстоянию");
                }
            }
        }

        // ТЕСТ 8: Проверка граничного случая - 0 ближайших водителей
        [Test]
        public void AllAlgorithms_WhenCountIsZero_ReturnEmptyList()
        {
            // Arrange
            var algorithms = new IDistanceCalculator[]
            {
                new BruteForceAlgorithm(),  // АЛГОРИТМ 1
                new SortingAlgorithm(),     // АЛГОРИТМ 2
                new HeapAlgorithm()         // АЛГОРИТМ 3
            };

            foreach (var algorithm in algorithms)
            {
                // Act
                var result = algorithm.FindNearestDrivers(_testDrivers, _testOrder, 0);

                // Assert
                Assert.IsEmpty(result, $"{algorithm.GetType().Name} должен вернуть пустой список при count=0");
            }
        }

        // Вспомогательный метод для расчета расстояния
        private double CalculateDistance(Driver driver, Order order)
        {
            return Math.Sqrt(Math.Pow(driver.X - order.X, 2) + Math.Pow(driver.Y - order.Y, 2));
        }
    }
}
