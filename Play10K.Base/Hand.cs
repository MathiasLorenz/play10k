using System;
using System.Collections.Generic;

namespace Play10K.Base
{
    internal class Hand
    {
        private readonly Random _rand = new Random();
        private int _savedScore = 0;
        private readonly HandValidator _handValidator = new HandValidator();
        public List<int> Dice { get; private set; } = new List<int>(6);
        public CollectedDice SavedDice { get; private set; } = new CollectedDice();
        public int Score => _savedScore + SavedDice.Score;

        // Todo: Unit test
        public void Roll()
        {
            for (int i = 0; i < Dice.Count; i++)
            {
                Dice[i] = _rand.Next(1, 7);
            }
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

        public bool IsAnyCombinationValid()
        {
            return _handValidator.TryValidateAnyDice(this);
        }

        public void Clear()
        {
            _savedScore = 0;
            SavedDice = new CollectedDice();
        }

        private bool CollectAndVerifyDice(ICollection<int> dice)
        {
            return SavedDice.CollectAndVerifyDice(dice);
        }

        //public bool CollectDiceFromHand()
        //{
        //    var collectedDice = GetSpecifiedDiceFromHand(Console.ReadLine());
        //    if (collectedDice == null)
        //    {
        //        Console.WriteLine("I didn't quite get that. Please try to input again. Remember to input numbers [1, 6] seperated by spaces and finish with enter.");
        //    }
        //    else
        //    {
        //        if (CollectAndVerifyDice(collectedDice) == false)
        //        {
        //            Console.WriteLine("Those dice could not be validated, please try again and choose a valid combination.");
        //        }
        //        else
        //        {
        //            // Now collectedDice are in SavedDice.DiceCollectedThisHand.
        //            diceCollected = true;
        //        }
        //    }

        //}

        // I would like to unit test this seperately => must be factored out.
        // Maybe a Dice(Input)Collecter service? Can also handle the input (not super seperation of concerns in that case though..).
        private List<int>? GetSpecifiedDiceFromHand(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }
            var dice = new List<int>();

            var splitInput = input.Split(' ');
            foreach (var item in splitInput)
            {
                if (int.TryParse(item, out int result) && result >= 1 && result <= 6)
                {
                    dice.Add(result);
                }
                else
                {
                    return null;
                }
            }

            return dice;
        }
    }
}