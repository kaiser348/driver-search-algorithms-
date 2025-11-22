namespace NearestDrivers;

public interface IDriverStorage
{
    void AddOrUpdateDriver(int id, int x, int y);
    IEnumerable<Driver> GetAllDrivers();
}
