using System;
using System.Collections.Generic;
using System.Linq;
using TheFoundLinq.Classes;

namespace TheFoundLinq.Extensions
{
    public static class LinqExtensions
    {
        public static IEnumerable<TSource> Distinct<TSource, TEquitable>(this IEnumerable<TSource> source, Func<TSource, TEquitable> func)
            where TEquitable : IEquatable<TEquitable>
        {
            var comparer = new FuncComparer<TSource, TEquitable>(func);
            return source.Distinct(comparer);
        }

        public static IEnumerable<TSource> Except<TSource, TEquitable>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource, TEquitable> func)
            where TEquitable : IEquatable<TEquitable>
        {
            var comparer = new FuncComparer<TSource, TEquitable>(func);
            return first.Except(second, comparer);
        }

        public static IEnumerable<TSource> Union<TSource, TEquitable>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource, TEquitable> func)
            where TEquitable : IEquatable<TEquitable>
        {
            var comparer = new FuncComparer<TSource, TEquitable>(func);
            return first.Union(second, comparer);
        }

        public static IEnumerable<TSource> Intersect<TSource, TEquitable>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource, TEquitable> func)
            where TEquitable : IEquatable<TEquitable>
        {
            var comparer = new FuncComparer<TSource, TEquitable>(func);
            return first.Intersect(second, comparer);
        }

        public static bool SequenceEqual<TSource, TEquitable>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource, TEquitable> func)
            where TEquitable : IEquatable<TEquitable>
        {
            var comparer = new FuncComparer<TSource, TEquitable>(func);
            return first.SequenceEqual(second, comparer);
        }

        public static IEnumerable<TSource> ForEachThen<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            foreach (var item in source)
            {
                action(item);
                yield return item;
            }
        }
    }
}
