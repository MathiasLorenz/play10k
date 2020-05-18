using System;
using System.Collections.Generic;
using System.Text;

namespace Play10K.Base
{
    public class Player
    {
        private readonly Random _rand = new Random();
        public string Name { get; }
        public int Score { get; } = 0;

        public Player(string name)
        {
            Name = name;
        }

        public void PlayTurn()
        {
            //var hand = new List<int>(6);
            //var turnScore = 0;
            //var savedDice = new CollectedDice();

            return;
        }
    }
}
