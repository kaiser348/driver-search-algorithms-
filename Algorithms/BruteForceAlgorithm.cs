using DriverSearch.Models;

namespace DriverSearch.Algorithms
{
    // АЛГОРИТМ 1: Полный перебор (Brute Force)
    public class BruteForceAlgorithm : IDistanceCalculator
    {
        public List<Driver> FindNearestDrivers(List<Driver> drivers, Order order, int count)
        {
            if (drivers.Count <= count)
                return new List<Driver>(drivers);

            var driverDistances = drivers.Select(driver => new
            {
                Driver = driver,
                Distance = CalculateDistanceSquared(driver, order)
            }).ToList();

            return driverDistances
                .OrderBy(dd => dd.Distance)
                .Take(count)
                .Select(dd => dd.Driver)
                .ToList();
        }

        private double CalculateDistanceSquared(Driver driver, Order order)
        {
            return Math.Pow(driver.X - order.X, 2) + Math.Pow(driver.Y - order.Y, 2);
        }
    }
}
