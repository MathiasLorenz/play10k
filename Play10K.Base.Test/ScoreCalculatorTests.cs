using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Play10K.Base.Test
{
    [TestClass]
    public class ScoreCalculatorTests
    {
        [TestMethod]
        [DataRow(new [] { 1, 3 }, 1000)]
        [DataRow(new [] { 1, 1 }, 100)]
        [DataRow(new [] { 5, 1 }, 50)]
        [DataRow(new [] { 4, 4 }, 800)]
        [DataRow(new [] { 2, 6 }, 1600)]
        [DataRow(new [] { 6, 3 }, 600)]
        [DataRow(new [] { 2, 5 }, 800)]
        [DataRow(new [] { 1, 6 }, 8000)]
        public void CalculateScore_Calculate_ReturnsCorrect(int[] hand, int expected)
        {
            var scoreCalculator = new ScoreCalculator();
            var diceCollection = new DiceCollection(hand[0], hand[1]);

            var result = scoreCalculator.CalculateScore(diceCollection);

            result.ShouldBe(expected);
        }
    }
}
