using System.Collections.ObjectModel;

namespace Core.Extensions
{
    public static class ObservableCollectionExtensions
    {
        public static void Replace<T>(this ObservableCollection<T> obs, IEnumerable<T> value)
        {
            obs.Clear();

            foreach (var item in value)
            {
                obs.Add(item);
            }
        }
    }
}
