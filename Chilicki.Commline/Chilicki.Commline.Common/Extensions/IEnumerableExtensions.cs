using System;
using System.Collections.Generic;
using System.Linq;

namespace Chilicki.Commline.Common.Extensions
{
    public static class IEnumerableExtensions
    {
        public static T FirstOrNull<T>(this IEnumerable<T> sequence) where T : class
        {
            return sequence
                .DefaultIfEmpty(null)
                .FirstOrDefault();
        }

        public static T FirstOrNull<T>(this IEnumerable<T> sequence, Func<T, bool> predicate)
            where T : class
        {
            return sequence
                .DefaultIfEmpty(null)
                .FirstOrDefault(predicate);
        }
    }
}
