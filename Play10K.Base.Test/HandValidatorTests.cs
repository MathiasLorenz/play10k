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
        public void TestAnyValidCombinationSingleNumber1()
        {
            var hand = new List<int> { 1 };
            var result = HandValidator.AnyValidDiceCombination(hand);
            var expected = true;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestAnyValidCombinationSingleNumber2()
        {
            var hand = new List<int> { 5 };
            var result = HandValidator.AnyValidDiceCombination(hand);
            var expected = true;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestAnyValidCombinationList1()
        {
            var hand = new List<int> { 5, 3, 4, 2, 1 };
            var result = HandValidator.AnyValidDiceCombination(hand);
            var expected = true;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestAnyValidCombinationList2()
        {
            var hand = new List<int> { 4, 3, 4, 2 };
            var result = HandValidator.AnyValidDiceCombination(hand);
            var expected = false;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestAnyValidCombinationList3()
        {
            var hand = new List<int> { 6, 3, 4, 6, 6, 2 };
            var result = HandValidator.AnyValidDiceCombination(hand);
            var expected = true;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestAnyValidCombinationWithCollected1()
        {
            var hand = new List<int> { 2 };
            var lastCollected = new ValueDice(2, 3);
            var result = HandValidator.AnyValidDiceCombination(hand, lastCollected);
            var expected = true;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestAnyValidCombinationWithCollected2()
        {
            var hand = new List<int> { 3 };
            var lastCollected = new ValueDice(2, 3);
            var result = HandValidator.AnyValidDiceCombination(hand, lastCollected);
            var expected = false;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestAnyValidCombinationWithCollected3()
        {
            var hand = new List<int> { 3 };
            var lastCollected = new ValueDice(1, 1);
            var result = HandValidator.AnyValidDiceCombination(hand, lastCollected);
            var expected = false;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestAnyValidCombinationWithCollected4()
        {
            var hand = new List<int> { 1 };
            var lastCollected = new ValueDice(3, 4);
            var result = HandValidator.AnyValidDiceCombination(hand, lastCollected);
            var expected = true;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestAnyValidCombinationWithCollected5()
        {
            var hand = new List<int> { 3 };
            var lastCollected = (ValueDice)null;
            var result = HandValidator.AnyValidDiceCombination(hand, lastCollected);
            var expected = false;

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void TestAnyValidCombinationWithCollected6()
        {
            var hand = new List<int> { 1 };
            var lastCollected = (ValueDice)null;
            var result = HandValidator.AnyValidDiceCombination(hand, lastCollected);
            var expected = true;

            Assert.AreEqual(expected, result);
        }
    }
}
