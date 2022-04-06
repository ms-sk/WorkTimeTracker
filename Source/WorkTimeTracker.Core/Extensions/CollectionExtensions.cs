using System.Collections.ObjectModel;

namespace WorkTimeTracker.Core.Extensions;

public static class CollectionExtensions
{
    public static void Replace<T>(this Collection<T> obs, IEnumerable<T> value)
    {
        obs.Clear();

        foreach (var item in value)
        {
            obs.Add(item);
        }
    }

    public static void Replace<T>(this List<T> obs, IEnumerable<T> value)
    {
        obs.Clear();

        foreach (var item in value)
        {
            obs.Add(item);
        }
    }
}