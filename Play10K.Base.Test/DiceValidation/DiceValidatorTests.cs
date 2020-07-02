using Microsoft.VisualStudio.TestTools.UnitTesting;
using Play10K.Base.DiceValidation;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;

namespace Play10K.Base.Test.DiceValidation
{
    [TestClass]
    public class DiceValidatorTests
    {
        private readonly DiceValidator _diceValidator = new DiceValidator();

        [TestMethod]
        [DataRow(new int[] { 2, 3, 1 })]
        [DataRow(new int[] { 5 })]
        public void ValidateAnyDice_ContainsValidDie_ReturnsTrue(int[] dice)
        {
            var result = _diceValidator.ValidateAnyDice(dice);

            result.ShouldBeTrue();
        }

        [TestMethod]
        [DataRow(new int[] { 2, 3, 4 })]
        [DataRow(new int[] { 6 })]
        [DataRow(new int[] { 2, 2, 3, 3, 6, 4 })]
        public void ValidateAnyDice_NoValidDice_ReturnsFalse(int[] dice)
        {
            var result = _diceValidator.ValidateAnyDice(dice);

            result.ShouldBeFalse();
        }

        [TestMethod]
        [DataRow(new int[] { 2, 3, 4 }, 2)]
        [DataRow(new int[] { 4 }, 4)]
        public void ValidateAnyDice_MatchesLastCollected_ReturnsTrue(int[] dice, int lastCollectedValue)
        {
            var result = _diceValidator.ValidateAnyDice(dice, lastCollectedValue);

            result.ShouldBeTrue();
        }

        [TestMethod]
        [DataRow(new int[] { 2, 3, 4 }, 6)]
        [DataRow(new int[] { 3 }, 4)]
        [DataRow(new int[] { 4 }, 3)]
        [DataRow(new int[] { 4 }, 1)]
        public void ValidateAnyDice_DoesNotMatchLastCollected_ReturnsFalse(int[] dice, int lastCollectedValue)
        {
            var result = _diceValidator.ValidateAnyDice(dice, lastCollectedValue);

            result.ShouldBeFalse();
        }

        [DataRow(new int[] { 5, 5, 4, 1 }, new int[] { 1, 5 }, null)]
        [DataRow(new int[] { 5, 5, 4, 1 }, new int[] { 5, 5, 1 }, null)]
        [TestMethod]
        public void ValidateAllDice_DiceContainedInHand_ReturnsTrue(int[] handDice, int[] dice, int? lastCollectedValue)
        {
            var result = _diceValidator.ValidateAllDice(handDice, dice, lastCollectedValue);

            result.ShouldBeTrue();
        }

        [TestMethod]
        [DataRow(new int[] { 5, 5, 4, 4, 1 }, new int[] { 5, 5, 4, 1 }, null)]
        [DataRow(new int[] { 5, 5, 4, 4, 1 }, new int[] { 5, 5, 4, 4, 1 }, null)]
        [DataRow(new int[] { 6, 6, 6, 5, 2, 1 }, new int[] { 6 }, null)]
        [DataRow(new int[] { 6, 6, 6, 5, 2, 1 }, new int[] { 6, 6 }, null)]
        [DataRow(new int[] { 6, 6, 6, 5, 2, 1 }, new int[] { 6, 6, 6, 6 }, null)]
        public void ValidateAllDice_DiceContainedInHandButDiceAreInvalid_ReturnsFalse(int[] handDice, int[] dice, int? lastCollectedValue)
        {
            var result = _diceValidator.ValidateAllDice(handDice, dice, lastCollectedValue);

            result.ShouldBeFalse();
        }

        [TestMethod]
        [DataRow(new int[] { 5, 5, 4, 4, 1 }, new int[] { 5, 4, 1 }, 4)]
        [DataRow(new int[] { 5, 5, 4, 4, 1 }, new int[] { 4, 4, 1 }, 4)]
        public void ValidateAllDice_DiceContainedInHandMatchesLastCollected_ReturnsTrue(int[] handDice, int[] dice, int? lastCollectedValue)
        {
            var result = _diceValidator.ValidateAllDice(handDice, dice, lastCollectedValue);

            result.ShouldBeTrue();
        }
    }
}
