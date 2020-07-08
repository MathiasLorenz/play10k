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

        private Hand _hand = new Hand();

        public int TurnScore => _hand.Score;

        public List<int> Dice => _hand.Dice;

        public Player(string name)
        {
            Name = name;
        }

        public void StartTurn()
        {
            _hand = new Hand();
        }

        public int EndTurn(bool AddTurnScore)
        {
            if (AddTurnScore)
            {
                Score += TurnScore;
            }
            var turnScore = TurnScore;
            _hand.Clear();

            return AddTurnScore ? turnScore : 0;
        }

        public void ReconcileHand()
        {
            _hand.ReconcileHand();
        }

        public void Roll()
            => _hand.Roll();

        public bool CanDoAnything => _hand.IsAnyCombinationValid();

        public bool VerifyDiceToCollect(ICollection<int> dice)
            => _hand.VerifyDiceToCollect(dice);

        public void CollectDice(ICollection<int> dice)
        {
            _hand.SaveDiceToCollected(dice);
            _hand.RemoveDiceFromHand(dice);
        }

        public int TurnScoreWithTempDice(ICollection<int> dice)
        {
            return _hand.ScoreWithTempDice(dice);
        }
    }
}
