using Microsoft.VisualStudio.TestTools.UnitTesting;
using Play10K.Base.CollectionExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Play10K.Base.Test.CollectionExtensions.IEnumerableExtensions
{
    [TestClass]
    public class DictionaryCounterTests
    {
        [TestMethod]
        public void TestEmptyList()
        {
            var enumerable = new List<int>();
            var dict = enumerable.DictionaryCounter();
            var expected = new Dictionary<int, int>();

            Assert.IsTrue(DictionaryEquals(expected, dict));
        }

        [TestMethod]
        public void TestEmptyString()
        {
            var enumerable = "";
            var dict = enumerable.DictionaryCounter();
            var expected = new Dictionary<char, int>();

            Assert.IsTrue(DictionaryEquals(expected, dict));
        }

        [TestMethod]
        public void TestIntList()
        {
            var enumerable = new List<int> { 2, 3, 4, 3, 2, 2 };
            var dict = enumerable.DictionaryCounter();
            var expected = new Dictionary<int, int>()
            {
                { 2, 3 },
                { 3, 2 },
                { 4, 1 },
            };

            Assert.IsTrue(DictionaryEquals(expected, dict));
        }

        [TestMethod]
        public void TestString()
        {
            var enumerable = "abcabaa";
            var dict = enumerable.DictionaryCounter();
            var expected = new Dictionary<char, int>()
            {
                { 'a', 4 },
                { 'b', 2 },
                { 'c', 1 },
            };

            Assert.IsTrue(DictionaryEquals(expected, dict));
        }

        [TestMethod]
        public void TestDoubleStack()
        {
            var enumerable = new Stack<double>();
            enumerable.Push(2.3);
            enumerable.Push(1.3);
            enumerable.Push(3.3);
            enumerable.Push(3.3);
            enumerable.Push(3.3);
            var dict = enumerable.DictionaryCounter();
            var expected = new Dictionary<double, int>()
            {
                { 1.3, 1 },
                { 2.3, 1 },
                { 3.3, 3 },
            };

            Assert.IsTrue(DictionaryEquals(expected, dict));
        }

        private bool DictionaryEquals<TKey, TValue>(Dictionary<TKey, TValue> dic1, Dictionary<TKey, TValue> dic2)
        {
            if (dic1 == dic2)
            {
                throw new ArgumentException("Compared dictionaries cannot be the same reference.");
            }

            return dic1.Count == dic2.Count && !dic1.Except(dic2).Any();
        }
    }
}
