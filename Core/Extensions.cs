using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;

namespace Core
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


        /// <summary>
        /// Compare all rows between the two tables. Returning the number of rows where at least one cell differs.
        /// </summary>
        public static IEnumerable<int> CompareTables(this DataTable left, DataTable right)
        {
            for (int i = 0; i < Math.Min(left.Rows.Count, right.Rows.Count); i++)
            {
                for(int c = 0; c < Math.Min(left.Rows[i].ItemArray.Length,
                                            right.Rows[i].ItemArray.Length); c++)
                {
                    object leftCell = left.Rows[i].ItemArray[c];
                    object rightCell = right.Rows[i].ItemArray[c];

                    if(!leftCell.Equals(rightCell))
                    {
                        yield return i;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(
            this IEnumerable<TSource> source, 
            Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

    }
}
