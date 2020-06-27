using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Play10K.Base
{
    public class Player
    {
        public string Name { get; }
        public int Score { get; private set; } = 0;
        public int TurnScore => _hand.Score;

        private readonly Hand _hand = new Hand();

        public Player(string name)
        {
            Name = name;
        }

        public void ReconcileHand()
        {
            _hand.ReconcileHand();
        }

        public bool Roll()
        {
            _hand.Roll();
            if (_hand.IsAnyCombinationValid() == false)
            {
                _hand.Clear();
                return false;
            }
            return true;
        }

        public void ShowDice()
        {
            Console.WriteLine($"This turn you have collected {TurnScore} points.");
            Console.WriteLine($"Your dice right now are:");
            var s = "";
            foreach (var die in _hand.Dice)
            {
                s += $" {die}";
            }
            Console.WriteLine(s);
        }

        public char ContinueOrEndTurn()
        {
            Console.WriteLine($"This turn you have collected {Score} points.");
            Console.WriteLine($"You have {_hand.Dice.Count} dice left.");
            Console.WriteLine($"Do you want to continue your turn by rolling again, or do you want to stop now and collect the points?");
            Console.WriteLine($"For continue/end, enter: c/e");
            return _userInput.GetCharResponse(new List<char> { 'c', 'e' });
        }

        public void UpdateScore(int scoreToAdd)
        {
            if (scoreToAdd < 0)
            {
                throw new ArgumentException("Cannot add a negative score to overall score.");
            }

            Score += scoreToAdd;
        }
    }
}
