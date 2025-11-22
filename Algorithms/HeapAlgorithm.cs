using DriverSearch.Models;

namespace DriverSearch.Algorithms
{
    // АЛГОРИТМ 3: Алгоритм с кучей (самый эффективный)
    public class HeapAlgorithm : IDistanceCalculator
    {
        public List<Driver> FindNearestDrivers(List<Driver> drivers, Order order, int count)
        {
            if (drivers.Count <= count)
                return new List<Driver>(drivers);

            var queue = new PriorityQueue<Driver, double>(Comparer<double>.Create((a, b) => b.CompareTo(a)));

            foreach (var driver in drivers)
            {
                var distance = CalculateDistanceSquared(driver, order);
                
                if (queue.Count < count)
                {
                    queue.Enqueue(driver, distance);
                }
                else if (distance < queue.Peek().Priority)
                {
                    queue.Dequeue();
                    queue.Enqueue(driver, distance);
                }
            }

            var result = new List<Driver>();
            while (queue.Count > 0)
            {
                result.Add(queue.Dequeue().Element);
            }

            result.Reverse();
            return result;
        }

        private double CalculateDistanceSquared(Driver driver, Order order)
        {
            return Math.Pow(driver.X - order.X, 2) + Math.Pow(driver.Y - order.Y, 2);
        }
    }
}
