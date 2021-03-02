using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Core.Extensions
{
    public static class Extensions
    {

        /// <summary>
        /// Wrapper around <see cref="Collection{T}.Add(T)"/> for each in <paramref name="items"/>
        /// </summary>
        public static void AddRange<T>(this ObservableCollection<T> @this, IEnumerable<T> items)
        {
            foreach (var item in items)
                @this.Add(item);
        }


        public static void AddRange<T>(this ISet<T> @this, IEnumerable<T> items)
        {

            foreach (var item in items)
                @this.Add(item);
        }

    }
}
