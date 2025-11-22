using System;
using System.Collections.Generic;
using System.Linq;

namespace NearestDrivers;

public static class DistanceAlgorithms
{
    // Euclidean distance squared (avoid sqrt for performance)
    private static long DistanceSquared(Driver d, int x, int y) =>
        (long)(d.X - x) * (d.X - x) + (long)(d.Y - y) * (d.Y - y);

    // Algorithm 1: LINQ OrderBy + Take (simple & readable)
    public static IEnumerable<Driver> NearestLinq(IDriverStorage storage, int orderX, int orderY, int count = 5)
    {
        return storage.GetAllDrivers()
            .OrderBy(d => DistanceSquared(d, orderX, orderY))
            .Take(count);
    }

    // Algorithm 2: Manual min-heap simulation using SortedSet
    public static IEnumerable<Driver> NearestSortedSet(IDriverStorage storage, int orderX, int orderY, int count = 5)
    {
        var comparer = Comparer<(long dist, int id)>.Create((a, b) =>
        {
            int cmp = a.dist.CompareTo(b.dist);
            return cmp != 0 ? cmp : a.id.CompareTo(b.id); // stabilize
        });

        var set = new SortedSet<(long dist, int id, Driver driver)>(Comparer<(long, int, Driver)>.Create((a, b) =>
        {
            int cmp = comparer.Compare((a.Item1, a.Item2), (b.Item1, b.Item2));
            return cmp != 0 ? cmp : a.Item2.CompareTo(b.Item2);
        }));

        foreach (var driver in storage.GetAllDrivers())
        {
            var dist = DistanceSquared(driver, orderX, orderY);
            set.Add((dist, driver.Id, driver));
            if (set.Count > count)
                set.Remove(set.Max); // remove farthest
        }

        return set.Select(x => x.driver);
    }

    // Algorithm 3: Partial selection sort (in-place top-k)
    public static IEnumerable<Driver> NearestPartialSort(IDriverStorage storage, int orderX, int orderY, int count = 5)
    {
        var drivers = storage.GetAllDrivers().ToArray();
        int n = drivers.Length;
        count = Math.Min(count, n);

        for (int i = 0; i < count; i++)
        {
            int minIndex = i;
            for (int j = i + 1; j < n; j++)
            {
                if (DistanceSquared(drivers[j], orderX, orderY) < DistanceSquared(drivers[minIndex], orderX, orderY))
                    minIndex = j;
            }
            (drivers[i], drivers[minIndex]) = (drivers[minIndex], drivers[i]);
        }

        return drivers.Take(count);
    }
}
