using System;
using System.Collections.Generic;

namespace Play10K.Base
{
    internal class Hand
    {
        private readonly Random _rand = new Random();
        private int _savedScore = 0;
        public List<int> Dice { get; private set; } = new List<int>(6);
        public int Score => _savedScore + SavedDice.Score;
        public CollectedDice SavedDice { get; private set; } = new CollectedDice();

        // Todo: Unit test
        public void Roll()
        {
            for (int i = 0; i < Dice.Count; i++)
            {
                Dice[i] = _rand.Next(1, 7);
            }
        }

        public bool CollectAndVerifyDice(ICollection<int> dice)
        {
            return SavedDice.CollectAndVerifyDice(dice);
        }

        public void Show()
        {
            var s = "";
            foreach (var die in Dice)
            {
                s += $" {die}";
            }
            Console.WriteLine(s);
        }
    }
}