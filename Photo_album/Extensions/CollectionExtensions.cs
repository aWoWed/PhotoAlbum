using System;
using System.Collections.Generic;
using System.Linq;

namespace Photo_album.Extensions
{
    public static class CollectionExtensions
    {
        public static T RandomElement<T>(this IEnumerable<T> enumerable) => enumerable.RandomElementUsing(new Random());

        public static IEnumerable<T> RandomElements<T>(this IEnumerable<T> enumerable, int count) =>
            enumerable.RandomElementsUsing(new Random(), count);

        private static IEnumerable<T> RandomElementsUsing<T>(this IEnumerable<T> enumerable, Random rand, int count)
        {
            var array = enumerable as T[] ?? enumerable.ToArray();
            for (var i = 0; i < count; i++)
            {
                var index = rand.Next(0, array.Length);
                yield return array.ElementAt(index);
            }
        }

        private static T RandomElementUsing<T>(this IEnumerable<T> enumerable, Random rand) =>
            enumerable.RandomElementsUsing(rand, 1).SingleOrDefault();
    }
}
