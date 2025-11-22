using DriverSearch.Models;

namespace DriverSearch.Algorithms
{
    public interface IDistanceCalculator
    {
        List<Driver> FindNearestDrivers(List<Driver> drivers, Order order, int count);
    }
}
