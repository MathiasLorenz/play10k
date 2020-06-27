using System;
using System.Collections.Generic;
using System.Linq;

namespace Play10K.Base
{
    internal class Hand
    {
        private readonly Random _rand = new Random();
        private readonly HandValidator _handValidator = new HandValidator();
        private int _savedScore = 0;
        private CollectedDice _savedDice = new CollectedDice();
        public List<int> Dice { get; private set; } = new List<int> { 0, 0, 0, 0, 0, 0 };
        public int Score => _savedScore + _savedDice.Score;

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
            if (Dice.Count < 0)
            {
                throw new InvalidOperationException("Negative amount of dice, this should not be possible.");
            }

            Dice = new List<int> { 0, 0, 0, 0, 0, 0 };
            _savedScore += _savedDice.Score;
            _savedDice = new CollectedDice();
        }

        public void Roll()
        {
            for (int i = 0; i < Dice.Count; i++)
            {
                Dice[i] = _rand.Next(1, 7);
            }
        }

        public bool IsAnyCombinationValid()
            => _handValidator.TryValidateAnyDice(Dice, _savedDice.LastCollected);

        public void Clear()
        {
            _savedScore = 0;
            _savedDice = new CollectedDice();
        }

        // Collect input from user as to which dice to collect.
        // Todo: Unit test. Test that correct is saved and removed from the current dice.
        public void CollectDiceFromUser()
        {
            while (true)
            {
                // Keep asking until a valid hand has been supplied.
                var dice = _userInput.GetSpecifiedDice().ToList();
                if (VerifyDiceToCollect(dice) == false)
                {
                    // Todo: Implement a way to tell which dice were wrong.
                    Console.WriteLine($"The specified dice were not valid. Try again :)");
                    dice = _userInput.GetSpecifiedDice().ToList();
                }

                // Collect the chosen. Ask the user whether or not to keep this choice.
                _savedDice.CollectDice(dice);
                // Todo: Fix this write out to the user
                Console.WriteLine($"You have put XXXX aside right now.");
                Console.WriteLine($"Together with the already collected, this amounts to YYYY points.");
                Console.WriteLine($"Would you like to save these dice and move on with your turn? Note that this cannot be undone.");
                Console.WriteLine($"Save these dice or cancel and choose again? Enter s/c:");
                var response = _userInput.GetCharResponse(new List<char> { 's', 'c' });
                if (response == 's')
                {
                    _savedDice.SaveCollectedThisHand();
                    foreach (var die in dice)
                    {
                        Dice.Remove(die);
                    }
                    break;
                }
                else // response == 'c'
                {
                    _savedDice.DiscardDiceCollectedThisHand();
                }
            }
        }

        // Verifies that the selected list of dice to collect is valid.
        private bool VerifyDiceToCollect(ICollection<int> dice)
        {
            if (_savedDice.DiceCollectedThisHand != null)
            {
                // Consider the ability to add to this already collected hand. Maybe keep record of hands put into this class?
                // Or make a converter from DiceCollection -> ICollection<int>, so that the extra dice can be added and recalculated.
                throw new InvalidOperationException("You already have dice collected for this hand. You either need to discard these or save before rolling and collecting again.");
            }
            return _handValidator.TryValidateAllDice(dice, _savedDice.LastCollected);
        }
    }
}