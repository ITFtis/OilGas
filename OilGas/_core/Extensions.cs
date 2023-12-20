using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OilGas
{
    public static class Extensions
    {
        public static string Right(this string str, int n)
        {
            return str.Substring(str.Length - n);
        }

        public static IEnumerable<TSource> Distinct<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                var elementValue = keySelector(element);
                if (seenKeys.Add(elementValue))
                {
                    yield return element;
                }
            }
        }

        //Cross Join
        public static IEnumerable<Tuple<T1, T2>> CrossJoin<T1, T2>(this IEnumerable<T1> sequence1, IEnumerable<T2> sequence2)
        {
            return sequence1.SelectMany(t1 => sequence2.Select(t2 => Tuple.Create(t1, t2)));
        }
        ////public static IQueryable<Tuple<T1, T2>> CrossJoin<T1, T2>(this IQueryable<T1> sequence1, IEnumerable<T2> sequence2)
        ////{
        ////    return sequence1.SelectMany(t1 => sequence2.Select(t2 => Tuple.Create(t1, t2)));
        ////}
    }
}