using System;
using System.Collections.Generic;
using System.Text;

namespace Play10K.Base
{
    internal class ScoreCalculator
    {
        public int CalculateScore(DiceCollection input)
        {
            return CalculateScore(input.Count, input.Value);
        }

        private int CalculateScore(int count, int value)
        {
            return count switch
            {
                1 => value switch
                {
                    1 => 100,
                    5 => 50,
                    _ => throw new ArgumentException("Cannot calculate score for this DiceCollection"),
                },
                3 => ThreeDiceCombinationScores(value),
                4 => 2 * ThreeDiceCombinationScores(value),
                5 => 4 * ThreeDiceCombinationScores(value),
                6 => 8 * ThreeDiceCombinationScores(value),
                _ => throw new ArgumentException("Cannot calculate score for this DiceCollection"),
            };
        }

        private int ThreeDiceCombinationScores(int value)
        {
            return value switch
            {
                1 => 1000,
                2 => 200,
                3 => 300,
                4 => 400,
                5 => 500,
                6 => 600,
                _ => throw new ArgumentException("Cannot calculate score for this DiceCollection"),
            };
        }
    }
}
