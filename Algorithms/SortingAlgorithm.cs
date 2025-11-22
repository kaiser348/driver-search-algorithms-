using DriverSearch.Models;

namespace DriverSearch.Algorithms
{
    // АЛГОРИТМ 2: Сортировка всего массива
    public class SortingAlgorithm : IDistanceCalculator
    {
        public List<Driver> FindNearestDrivers(List<Driver> drivers, Order order, int count)
        {
            if (drivers.Count <= count)
                return new List<Driver>(drivers);

            var sortedDrivers = new List<Driver>(drivers);
            sortedDrivers.Sort((a, b) => 
            {
                var distA = Math.Pow(a.X - order.X, 2) + Math.Pow(a.Y - order.Y, 2);
                var distB = Math.Pow(b.X - order.X, 2) + Math.Pow(b.Y - order.Y, 2);
                return distA.CompareTo(distB);
            });

            return sortedDrivers.Take(count).ToList();
        }
    }
}
