using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Play10K.Base
{
    internal class ValueDice
    {
        public int Value { get; } = 0;
        public int Count { get; private set; } = 0;
        public int Score { get => ScoreCalculator.CalculateScore(this); }

        public ValueDice(int value, int count)
        {
            if (count > 6 || count < 1)
            {
                throw new ArgumentException(message: "Cannot add more than 6 or less than 1 dice.", paramName: "count");
            }
            if (value > 6 || value < 1)
            {
                throw new ArgumentException(message: "Dice value cannot be less than 1 or larger than 6", paramName: "value");
            }

            Count = count;
            Value = value;
        }
    }
}