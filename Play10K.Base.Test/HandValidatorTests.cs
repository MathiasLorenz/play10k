using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Play10K.Base.Test
{
    [TestClass]
    public class HandValidatorTests
    {
        [TestMethod]
        [DataRow(1)]
        [DataRow(5)]
        public void TryValidateAnyDice_SingleValue_ReturnsTrue(int input)
        {
            var hand = new List<int> { input };
            var result = HandValidator.TryValidateAnyDice(hand);
            var expected = true;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(4)]
        [DataRow(6)]
        public void TryValidateAnyDice_SingleValue_ReturnsFalse(int input)
        {
            var hand = new List<int> { input };
            var result = HandValidator.TryValidateAnyDice(hand);
            var expected = false;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [DataRow(0)]
        [DataRow(8)]
        [DataRow(null)]
        public void TryValidateAnyDice_SingleValue_ThrowsException(int input)
        {
            var hand = new List<int> { input };
            HandValidator.TryValidateAnyDice(hand);
        }

        [TestMethod]
        [DataRow(new int[] { 5, 3, 4, 2, 1 })]
        [DataRow(new int[] { 6, 3, 4, 6, 6, 2 })]
        [DataRow(new int[] { 1, 4 })]
        [DataRow(new int[] { 1, 4, 4, 4 })]
        [DataRow(new int[] { 2, 4, 4, 4 })]
        public void TryValidateAnyDice_MultipleValues_ReturnsTrue(int[] input)
        {
            var result = HandValidator.TryValidateAnyDice(input);
            var expected = true;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [DataRow(new int[] { 2, 3, 4, 4 })]
        [DataRow(new int[] { 2, 3 })]
        [DataRow(new int[] { 2, 3, 3, 4, 4, 6 })]
        public void TryValidateAnyDice_MultipleValues_ReturnsFalse(int[] input)
        {
            var result = HandValidator.TryValidateAnyDice(input);
            var expected = false;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [DataRow(new int[] { })]
        [DataRow(new int[] { 1, 1, 1, 1, 1, 1, 1, 1 })]
        [DataRow(new int[] { 2, 2, 3, 3, 4, 4, 6 })]
        [DataRow(new int[] { 2, 8 })]
        public void TryValidateAnyDice_MultipleValues_ThrowsException(int[] input)
        {
            HandValidator.TryValidateAnyDice(input);
        }

        [TestMethod]
        [DataRow(new int[] { 2 }, new int[] { 2, 3 }, true)]
        [DataRow(new int[] { 1 }, new int[] { 3, 4 }, true)]
        [DataRow(new int[] { 1 }, new int[] { 5, 1 }, true)]
        [DataRow(new int[] { 5 }, new int[] { 5, 1 }, true)]
        [DataRow(new int[] { 5 }, new int[] { 1, 1 }, true)]
        [DataRow(new int[] { 5 }, new int[] { 6, 4 }, true)]
        [DataRow(new int[] { 1 }, null, true)]
        [DataRow(new int[] { 3 }, new int[] { 1, 1 }, false)]
        [DataRow(new int[] { 3 }, null, false)]
        public void TryValidateAnyDice_SingleValueWithCollected(int[] hand, int[]? diceCollection, bool expected)
        {
            var lastCollected = diceCollection != null ? new DiceCollection(diceCollection[0], diceCollection[1]) : null;
            var result = HandValidator.TryValidateAnyDice(hand, lastCollected);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [DataRow(new int[] { 1 })]
        [DataRow(new int[] { 5 })]
        [DataRow(new int[] { 1, 5 })]
        [DataRow(new int[] { 1, 1, 5 })]
        [DataRow(new int[] { 1, 1, 1 })]
        [DataRow(new int[] { 1, 1, 1, 5 })]
        [DataRow(new int[] { 1, 1, 1, 5, 5, 1 })]
        [DataRow(new int[] { 1, 1, 1, 5, 5, 5 })]
        [DataRow(new int[] { 3, 3, 3 })]
        [DataRow(new int[] { 3, 3, 3, 1 })]
        [DataRow(new int[] { 3, 3, 3, 1, 5 })]
        [DataRow(new int[] { 3, 3, 3, 4, 4, 4 })]
        [DataRow(new int[] { 3, 3, 3, 1, 1, 1 })]
        [DataRow(new int[] { 3, 3, 3, 1, 1, 5 })]
        [DataRow(new int[] { 5, 5 })]
        public void TryValidateAllDice_SingleAndMultipleValuesNoCollected_ReturnsTrue(int[] input)
        {
            var result = HandValidator.TryValidateAllDice(input);
            var expected = true;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [DataRow(new int[] { 2, 3 })]
        [DataRow(new int[] { 3 })]
        [DataRow(new int[] { 1, 3 })]
        [DataRow(new int[] { 1, 1, 1, 5, 4 })]
        [DataRow(new int[] { 1, 1, 1, 1, 1, 2 })]
        [DataRow(new int[] { 5, 1, 2 })]
        public void TryValidateAllDice_SingleAndMultipleValuesNoCollected_ReturnsFalse(int[] input)
        {
            var result = HandValidator.TryValidateAllDice(input);
            var expected = false;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [DataRow(new int[] { })]
        [DataRow(new int[] { 1, 1, 1, 1, 1, 1, 1 })]
        [DataRow(new int[] { 1, 8 })]
        public void TryValidateAllDice_SingleAndMultipleValuesNoCollected_ThrowsException(int[] input)
        {
            HandValidator.TryValidateAllDice(input);
        }

        [TestMethod]
        [DataRow(new int[] { 1 }, new int[] { 5, 1 })]
        [DataRow(new int[] { 1 }, null)]
        [DataRow(new int[] { 4 }, new int[] { 4, 3 })]
        [DataRow(new int[] { 5 }, new int[] { 4, 3 })]
        [DataRow(new int[] { 1 }, new int[] { 4, 3 })]
        [DataRow(new int[] { 1 }, new int[] { 5, 5 })]
        [DataRow(new int[] { 5 }, new int[] { 5, 5 })]
        public void TryValidateAllDice_SingleAndMultipleValuesWithCollected_ReturnsTrue(int[] input, int[]? diceCollection)
        {
            var lastCollected = diceCollection != null ? new DiceCollection(diceCollection[0], diceCollection[1]) : null;
            var result = HandValidator.TryValidateAllDice(input, lastCollected);
            var expected = true;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [DataRow(new int[] { 2 }, new int[] { 5, 1 })]
        [DataRow(new int[] { 3 }, null)]
        [DataRow(new int[] { 6 }, new int[] { 4, 3 })]
        [DataRow(new int[] { 3 }, new int[] { 4, 4 })]
        [DataRow(new int[] { 2 }, new int[] { 5, 5 })]
        public void TryValidateAllDice_SingleAndMultipleValuesWithCollected_ReturnsFalse(int[] input, int[]? diceCollection)
        {
            var lastCollected = diceCollection != null ? new DiceCollection(diceCollection[0], diceCollection[1]) : null;
            var result = HandValidator.TryValidateAllDice(input, lastCollected);
            var expected = false;

            Assert.AreEqual(expected, result);
        }
    }
}
