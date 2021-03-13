using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

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

        public static bool Replace<T>(this ISet<T> @this, T newObj) where T : IComparable<T>
        {
            if(@this.Remove(newObj))
            {
                return @this.Add(newObj);
            }
            return false;
        }

        public static IEnumerable<IEnumerable<TSource>> Batch<TSource>(
                  this IEnumerable<TSource> source, int size)
        {
            /* https://github.com/morelinq/MoreLINQ/blob/master/MoreLinq/Batch.cs */
            TSource[] bucket = null;
            var count = 0;

            foreach (var item in source)
            {
                if (bucket == null)
                    bucket = new TSource[size];

                bucket[count++] = item;
                if (count != size)
                    continue;

                yield return bucket;

                bucket = null;
                count = 0;
            }

            if (bucket != null && count > 0)
                yield return bucket.Take(count).ToArray();
        }

    }
}
