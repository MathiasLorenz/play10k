using Microsoft.VisualStudio.TestTools.UnitTesting;
using Play10K.Base.DiceValidation;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Play10K.Base.Test.DiceValidation
{
    [TestClass]
    public class DiceValidatorInternalTests
    {
        [TestMethod]
        [DataRow(new int[] { 1, 1 })]
        [DataRow(new int[] { 1, 2 })]
        [DataRow(new int[] { 1, 3 })]
        [DataRow(new int[] { 1, 3, 5, 1 })]
        [DataRow(new int[] { 1, 3, 6, 1 })]
        [DataRow(new int[] { 1, 3, 6, 2 })]
        public void AreDiceContainedInHand_DiceAreContained_ReturnsTrue(int[] diceToDictionary)
        {
            var diceValidatorInternal = new DiceValidatorInternal();
            var handDice = new Dictionary<int, int>()
            {
                [1] = 3,
                [5] = 1,
                [6] = 2,
            };
            var dice = ArrayToDictionary(diceToDictionary);

            var result = diceValidatorInternal.AreDiceContainedInHand(handDice, dice);

            result.ShouldBe(true);
        }

        [TestMethod]
        [DataRow(new int[] { 2, 1 })]
        [DataRow(new int[] { 5, 5 })]
        [DataRow(new int[] { 6, 1 })]
        [DataRow(new int[] { 54, 1 })]
        public void AreDiceContainedInHand_DiceNotPresentInHand_ReturnsFalse(int[] diceToDictionary)
        {
            var diceValidatorInternal = new DiceValidatorInternal();
            var handDice = new Dictionary<int, int>()
            {
                [1] = 1,
                [3] = 1,
                [4] = 2,
            };
            var dice = ArrayToDictionary(diceToDictionary);

            var result = diceValidatorInternal.AreDiceContainedInHand(handDice, dice);

            result.ShouldBe(false);
        }

        [TestMethod]
        [DataRow(new int[] { 1, 4 })]
        [DataRow(new int[] { 1, 5 })]
        [DataRow(new int[] { 1, 6 })]
        [DataRow(new int[] { 2, 2 })]
        [DataRow(new int[] { 3, 3 })]
        [DataRow(new int[] { 3, 20 })]
        public void AreDiceContainedInHand_DiceCountExceedsCountInHand_ReturnsFalse(int[] diceToDictionary)
        {
            var diceValidatorInternal = new DiceValidatorInternal();
            var handDice = new Dictionary<int, int>()
            {
                [1] = 3,
                [2] = 1,
                [3] = 2,
            };
            var dice = ArrayToDictionary(diceToDictionary);

            var result = diceValidatorInternal.AreDiceContainedInHand(handDice, dice);

            result.ShouldBe(false);
        }

        private Dictionary<int, int> ArrayToDictionary(int[] input)
        {
            Assert.IsTrue(input.Length % 2 == 0);
            var dict = new Dictionary<int, int>();

            for (int i = 0; i < input.Length; i += 2)
            {
                dict.Add(input[i], input[i+1]);
            }

            return dict;
        }
    }
}
