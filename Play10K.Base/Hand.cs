using System;
using System.Collections.Generic;
using System.Linq;

namespace Play10K.Base
{
    internal class Hand
    {
        private readonly Random _rand = new Random();
        private readonly HandValidator _handValidator = new HandValidator();
        private readonly UserInputHandler _userInputHandler = new UserInputHandler();
        private int _savedScore = 0;
        private List<int> _dice = new List<int>(6);
        private CollectedDice _savedDice = new CollectedDice();
        public int Score => _savedScore + _savedDice.Score;

        // If all dice have been used, then make a new set of dice and save score in from this hand to the total.
        // Todo: Unit test
        public void ReconcileHand()
        {
            if (_dice.Count > 0)
            {
                return;
            }
            if (_dice.Count < 0)
            {
                throw new InvalidOperationException("Negative amount of dice, this should not be possible.");
            }

            _dice = new List<int>(6);
            _savedScore += _savedDice.Score;
            _savedDice = new CollectedDice();
        }

        public void Roll()
        {
            for (int i = 0; i < _dice.Count; i++)
            {
                _dice[i] = _rand.Next(1, 7);
            }
        }

        public void Show()
        {
            var s = "";
            foreach (var die in _dice)
            {
                s += $" {die}";
            }
            Console.WriteLine(s);
        }

        public bool IsAnyCombinationValid()
            => _handValidator.TryValidateAnyDice(_dice, _savedDice.LastCollected);

        public void Clear()
        {
            _savedScore = 0;
            _savedDice = new CollectedDice();
        }

        // Collect input from user as to which dice to collect.
        public void CollectDiceFromUser()
        {
            while (true)
            {
                // Keep asking until a valid hand has been supplied.
                var dice = _userInputHandler.GetSpecifiedDice().ToList();
                if (VerifyDiceToCollect(dice) == false)
                {
                    // Todo: Implement a way to tell which dice were wrong.
                    Console.WriteLine($"The specified dice were not valid. Try again :)");
                    dice = _userInputHandler.GetSpecifiedDice().ToList();
                }

                // Collect the chosen. Ask the user whether or not to keep this choice.
                _savedDice.CollectDice(dice);
                // Todo: Fix this write out to the user
                Console.WriteLine($"You have put XXXX aside right now.");
                Console.WriteLine($"Together with the already collected, this amounts to YYYY points.");
                Console.WriteLine($"Would you like to save these dice and move on with your turn? Note that this cannot be undone.");
                Console.WriteLine($"Save these dice or cancel and choose again? Enter s/c:");
                var response = _userInputHandler.GetCharResponse(new List<char> { 's', 'c' });
                if (response == 's')
                {
                    _savedDice.SaveCollectedThisHand();
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

        public char ContinueOrEndTurn()
        {
            Console.WriteLine($"This turn you have collected {Score} points.");
            Console.WriteLine($"You have {_dice.Count} dice left.");
            Console.WriteLine($"Do you want to continue your turn by rolling again, or do you want to stop now and collect the points?");
            Console.WriteLine($"For continue/end, enter: c/e");
            return _userInputHandler.GetCharResponse(new List<char> { 'c', 'e' });
        }
    }
}