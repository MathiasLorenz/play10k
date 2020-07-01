using Microsoft.VisualStudio.TestTools.UnitTesting;
using Play10K.Base.DiceValidation;
using Shouldly;
using System;
using System.Collections.Generic;

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

            result.ShouldBeFalse();
        }

        [TestMethod]
        [DataRow(2, -1, null)]
        [DataRow(2, 0, null)]
        [DataRow(2, 7, null)]
        [DataRow(2, 65, null)]
        public void ValidateDiceCounter_CountOutsideValidRange_ThrowsException(int value, int count, int? lastCollectedValue)
        {
            var diceValidatorInternal = new DiceValidatorInternal();
            Should.Throw<ArgumentException>(() => {
                diceValidatorInternal.ValidateDiceCounter(value, count, lastCollectedValue);
            });
        }

        [TestMethod]
        [DataRow(-1, 1, null)]
        [DataRow(0, 2, null)]
        [DataRow(7, 6, null)]
        [DataRow(65, 3, null)]
        public void ValidateDiceCounter_ValueOutsideValidRange_ThrowsException(int value, int count, int? lastCollectedValue)
        {
            var diceValidatorInternal = new DiceValidatorInternal();
            Should.Throw<ArgumentException>(() => {
                diceValidatorInternal.ValidateDiceCounter(value, count, lastCollectedValue);
            });
        }

        [TestMethod]
        [DataRow(2, 3, null)]
        [DataRow(1, 1, null)]
        [DataRow(1, 5, null)]
        [DataRow(4, 3, null)]
        [DataRow(5, 1, null)]
        [DataRow(6, 6, null)]
        [DataRow(2, 1, 2)]
        [DataRow(2, 3, 2)]
        [DataRow(3, 3, 3)]
        [DataRow(1, 2, 1)]
        public void ValidateDiceCounter_VariousValidInputs_ReturnsTrue(int value, int count, int? lastCollectedValue)
        {
            var diceValidatorInternal = new DiceValidatorInternal();
            var result = diceValidatorInternal.ValidateDiceCounter(value, count, lastCollectedValue);

            result.ShouldBeTrue();
        }

        [TestMethod]
        [DataRow(4, 2, null)]
        [DataRow(3, 1, null)]
        [DataRow(2, 1, 3)]
        [DataRow(6, 2, 1)]
        public void ValidateDiceCounter_VariousInvalidInputs_ReturnsFalse(int value, int count, int? lastCollectedValue)
        {
            var diceValidatorInternal = new DiceValidatorInternal();
            var result = diceValidatorInternal.ValidateDiceCounter(value, count, lastCollectedValue);

            result.ShouldBeFalse();
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
