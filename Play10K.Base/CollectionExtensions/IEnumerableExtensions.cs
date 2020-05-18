using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Play10K.Base.CollectionExtensions
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        ///  Mimics collections.Counter from Python.
        ///  Returns a dictionary of all distinct values from source together with a counter of how many times it is present in the input.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Dictionary<T, int> DictionaryCounter<T>(this IEnumerable<T> source)
        {
            var dict = new Dictionary<T, int>();

            foreach(var elem in source)
            {
                if (dict.TryGetValue(elem, out var value))
                {
                    dict[elem] += 1;
                }
                else
                {
                    dict[elem] = 1;
                }
            }

            return dict;
        }

        /// <summary>
        /// Returns boolean describing if the array consists of only the same element.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        // Todo: Unit test
        public static bool AllElementsEqual<T>(this IEnumerable<T> items)
        {
            return items.Distinct().Take(2).Count() == 1;
        }
    }
}
