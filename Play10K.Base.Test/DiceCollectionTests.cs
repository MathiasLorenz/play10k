using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Play10K.Base.Test
{
    [TestClass]
    public class DiceCollectionTests
    {
        [TestMethod]
        [DataRow(1, 3)]
        [DataRow(3, 4)]
        public void DiceCollection_ConstructorValid(int value, int count)
        {
            var diceCollection = new DiceCollection(value, count);

            Assert.AreEqual(value, diceCollection.Value);
            Assert.AreEqual(count, diceCollection.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [DataRow(1, 2)]
        [DataRow(0, 4)]
        [DataRow(7, 6)]
        [DataRow(22, 6)]
        public void DiceCollection_ConstructorInvalid_ThrowsException(int value, int count)
        {
            _ = new DiceCollection(value, count);
        }

        [TestMethod]
        [DataRow(1, 3, 1, 4)]
        [DataRow(2, 3, 2, 5)]
        [DataRow(5, 3, 1, 4)]
        [DataRow(3, 4, 2, 6)]
        public void AddToCount_AddValid(int value, int count, int countToAdd, int expected)
        {
            var diceCollection = new DiceCollection(value, count);
            diceCollection.AddToCount(countToAdd);

            Assert.AreEqual(expected, diceCollection.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [DataRow(1, 1, 2, 4)]
        [DataRow(2, 2, 1, 5)]
        [DataRow(3, 1, 4, 5)]
        [DataRow(4, 5, 2, 7)]
        public void AddToCount_AddInvalid_ThrowsException(int value, int count, int countToAdd, int expected)
        {
            var diceCollection = new DiceCollection(value, count);
            diceCollection.AddToCount(countToAdd);
        }
    }
}
