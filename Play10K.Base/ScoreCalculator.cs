using System;
using System.Collections.Generic;
using System.Text;

namespace Play10K.Base
{
    internal static class ScoreCalculator
    {
        public static int CalculateScore(DiceCollection input)
        {
            return input.Count switch
            {
                1 => input.Value switch
                {
                    1 => 100,
                    5 => 50,
                    _ => throw new ArgumentException("Cannot calculate score for this DiceCollection"),
                },
                3 => ThreeDiceCombinationScores(input.Value),
                4 => 2*ThreeDiceCombinationScores(input.Value),
                5 => 4*ThreeDiceCombinationScores(input.Value),
                6 => 8*ThreeDiceCombinationScores(input.Value),
                _ => throw new ArgumentException("Cannot calculate score for this DiceCollection"),
            };
        }

        private static int ThreeDiceCombinationScores(int value)
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
