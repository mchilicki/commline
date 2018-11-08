using System.Collections.Generic;

namespace Chilicki.Commline.Common.Extensions
{
    public static class IEnumerableExtensions
    {
        public static T FirstOrNull<T>(this IEnumerable<T> sequence) where T : class
        {
            foreach (T item in sequence)
                return item;
            return null;
        }
    }
}
