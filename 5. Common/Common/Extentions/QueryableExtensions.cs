using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Extentions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> PageBy<T>(this IQueryable<T> query, int skipCount, int maxResultCount)
        {
            if(query == null)
                throw new ArgumentNullException(nameof(query));

            return query.Skip(skipCount).Take(maxResultCount);
        }
    }
}
