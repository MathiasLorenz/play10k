using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Play10K.Base.Extensions
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
    }
}
