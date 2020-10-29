using Common.Unknown;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Extentions
{
    public static class EnumrableExtensions
    {
        public static TResult[] ConvertArray<TSource, TResult>(this IEnumerable<TSource> items, Func<TSource, TResult> toResult)
        {
            return items == null ? EmptyArray<TResult>.Instance : items.Select(toResult).ToArray();
        }
    }
}
