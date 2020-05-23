using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace Play10K.Base
{
    internal class DiceCollection
    {
        public int Value { get; } = 0;
        public int Count { get; private set; } = 0;
        public int Score { get => ScoreCalculator.CalculateScore(this); }

        public DiceCollection(int value, int count)
        {
            if (value > 6 || value < 1)
            {
                throw new ArgumentException(message: "Dice value cannot be less than 1 or larger than 6", paramName: "value");
            }
            EnsureCountIsValid(count);

            Count = count;
            Value = value;
        }

        public int AddToCount(int countToAdd)
        {
            if (Count == 1)
            {
                throw new ArgumentException("Can never add to DiceCollection with Count 1.");
            }

            EnsureCountIsValid(Count + countToAdd);
            Count += countToAdd;
            return Count;
        }

        private void EnsureCountIsValid(int count)
        {
            if (count > 6 || count < 1)
            {
                throw new ArgumentException(message: "Cannot add more than 6 or less than 1 dice.", paramName: "count");
            }
            if (count == 2)
            {
                throw new ArgumentException("The count for a DiceCollection can never be two.");
            }
        }
    }
}