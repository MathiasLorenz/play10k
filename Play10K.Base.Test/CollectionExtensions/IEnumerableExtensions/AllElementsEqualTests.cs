using Microsoft.VisualStudio.TestTools.UnitTesting;
using Play10K.Base.CollectionExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Play10K.Base.Test.CollectionExtensions.IEnumerableExtensions
{
    [TestClass]
    public class AllElementsEqualTests
    {
        [TestMethod]
        public void ListOfIntAllEqual()
        {
            var enumerable = new List<int> { 7, 7, 7 };
            var actual = enumerable.AllElementsEqual();
            var expected = true;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ListOfIntNotEqual()
        {
            var enumerable = new List<int> { 7, 6, 7 };
            var actual = enumerable.AllElementsEqual();
            var expected = false;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DictionaryOfStringValuesAllEqual()
        {
            var enumerable = new Dictionary<int, string>
            {
                { 2, "sup" },
                { 3, "sup" },
                { 4, "sup" },
            };
            var actual = enumerable.Values.AllElementsEqual();
            var expected = true;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DictionaryOfStringValuesNotEqual()
        {
            var enumerable = new Dictionary<int, string>
            {
                { 2, "sup" },
                { 3, "sup" },
                { 4, "næ" },
                { 6, "slet ikke" },
            };
            var actual = enumerable.Values.AllElementsEqual();
            var expected = false;

            Assert.AreEqual(expected, actual);
        }
    }
}
