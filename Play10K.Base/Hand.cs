using Play10K.Base.DiceValidation;
using System;
using System.Collections.Generic;

namespace Play10K.Base
{
    internal class Hand
    {
        private readonly Random _rand = new Random();
        private readonly DiceValidator _diceValidator = new DiceValidator();
        private int _savedScore = 0;
        private CollectedDice _collectedDice = new CollectedDice();
        // The private setter makes it impossible to mock this and test properly :(
        // I feel like this should be organized in a different way in order to test more easily.
        public List<int> Dice { get; private set; } = new List<int> { 0, 0, 0, 0, 0, 0 };
        public int Score => _savedScore + _collectedDice.Score;

        /// <summary>
        /// If all dice have been used, make a new set of dice and save score in from this hand to the total.
        /// </summary>
        // Todo: Unit test
        public void ReconcileHand()
        {
            if (Dice.Count > 0)
            {
                return;
            }
            else if (Dice.Count < 0)
            {
                throw new InvalidOperationException("Negative amount of dice, this should not be possible.");
            }
            else
            {
                Dice = new List<int> { 0, 0, 0, 0, 0, 0 };
                _savedScore += _collectedDice.Score;
                _collectedDice = new CollectedDice();
            }
        }

        public void Roll()
        {
            for (int i = 0; i < Dice.Count; i++)
            {
                Dice[i] = _rand.Next(1, 7);
            }
        }

        public bool IsAnyCombinationValid()
            => _diceValidator.ValidateAnyDice(Dice, _collectedDice.LastCollected?.Value);

        public void Clear()
        {
            _savedScore = 0;
            _collectedDice = new CollectedDice();
        }

        public void SaveDiceToCollected(ICollection<int> dice)
            => _collectedDice.Collect(dice);

        // Todo: Unit test!!!
        public void RemoveDiceFromHand(ICollection<int> dice)
        {
            foreach (var die in dice)
            {
                var removed = Dice.Remove(die);
                if (removed == false)
                {
                    throw new InvalidOperationException("Dice not present in the hand cannot be removed.");
                }
            }
        }

        // Verifies that the selected list of dice to collect is valid.
        internal bool VerifyDiceToCollect(ICollection<int> diceToVerify)
            => _diceValidator.ValidateAllDice(Dice, diceToVerify, _collectedDice.LastCollected?.Value);
    }
}