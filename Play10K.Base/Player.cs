using System;
using System.Collections.Generic;
using System.Text;

namespace Play10K.Base
{
    public class Player
    {
        public string Name { get; }
        public int Score { get; } = 0;

        public Player(string name)
        {
            Name = name;
        }

        public void PlayTurn()
        {
            return;
        }
    }
}
