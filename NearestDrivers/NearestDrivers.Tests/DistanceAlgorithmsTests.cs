using System.Linq;
using NUnit.Framework;
using NearestDrivers;

namespace NearestDrivers.Tests;

public class DistanceAlgorithmsTests
{
    [Test]
    public void All_Algorithms_Return_Same_Results()
    {
        var storage = new DriverStorage();
        storage.AddOrUpdateDriver(1, 0, 0);
        storage.AddOrUpdateDriver(2, 1, 1);
        storage.AddOrUpdateDriver(3, 2, 2);
        storage.AddOrUpdateDriver(4, 10, 10);
        storage.AddOrUpdateDriver(5, 3, 3);
        storage.AddOrUpdateDriver(6, 0, 1);

        int orderX = 0, orderY = 0;
        var expected = new[] { 1, 6, 2, 3, 5 }; // IDs in order of distance

        var linq = DistanceAlgorithms.NearestLinq(storage, orderX, orderY).Select(d => d.Id).ToArray();
        var sortedSet = DistanceAlgorithms.NearestSortedSet(storage, orderX, orderY).Select(d => d.Id).ToArray();
        var partial = DistanceAlgorithms.NearestPartialSort(storage, orderX, orderY).Select(d => d.Id).ToArray();

        Assert.That(linq, Is.EqualTo(expected));
        Assert.That(sortedSet, Is.EqualTo(expected));
        Assert.That(partial, Is.EqualTo(expected));
    }

    [Test]
    public void Handles_Empty_Storage()
    {
        var storage = new DriverStorage();
        var result = DistanceAlgorithms.NearestLinq(storage, 0, 0);
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void Returns_All_When_Less_Than_Five()
    {
        var storage = new DriverStorage();
        storage.AddOrUpdateDriver(1, 0, 0);
        storage.AddOrUpdateDriver(2, 1, 1);

        var result = DistanceAlgorithms.NearestLinq(storage, 0, 0).Count();
        Assert.That(result, Is.EqualTo(2));
    }
}
