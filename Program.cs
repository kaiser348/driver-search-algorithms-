using DriverSearch.Models;
using DriverSearch.Algorithms;

class Program
{
    static void Main()
    {
        var drivers = new List<Driver>
        {
            new Driver(1, 2, 3),
            new Driver(2, 5, 7),
            new Driver(3, 1, 1),
            new Driver(4, 8, 2),
            new Driver(5, 4, 5),
            new Driver(6, 6, 6),
            new Driver(7, 3, 4),
            new Driver(8, 7, 8),
            new Driver(9, 0, 0),
            new Driver(10, 9, 9)
        };

        var order = new Order(4, 4);

        Console.WriteLine("Тестирование трех алгоритмов поиска ближайших водителей:\n");

        var algorithms = new List<IDistanceCalculator>
        {
            new BruteForceAlgorithm(),
            new SortingAlgorithm(),
            new HeapAlgorithm()
        };

        var names = new[] { "1. Brute Force", "2. Sorting", "3. Heap" };

        for (int i = 0; i < algorithms.Count; i++)
        {
            var result = algorithms[i].FindNearestDrivers(drivers, order, 5);
            Console.WriteLine($"{names[i]} алгоритм:");
            
            foreach (var driver in result)
            {
                var distance = Math.Sqrt(Math.Pow(driver.X - order.X, 2) + Math.Pow(driver.Y - order.Y, 2));
                Console.WriteLine($"  Водитель {driver.Id}: ({driver.X}, {driver.Y}) - расстояние: {distance:F2}");
            }
           
        }
         Console.WriteLine();
         BenchmarkRunner.Run<DriverSearchBenchmarks>();
    }
}
