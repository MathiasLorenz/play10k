using System;
using System.Collections.Generic;
using System.Text;

namespace Play10K.Base
{
    public class Game
    {
        public List<Player> Players { get; } = new List<Player>();

        public Game(List<Player> players)
        {
            Players = players;
        }

        public void Play()
        {
            bool isLastRound = false;
            while (isLastRound == false)
            {
                foreach(var player in Players)
                {
                    player.PlayTurn();
                    if (player.Score >= 10000)
                    {
                        isLastRound = true;
                    }
                }
            }
        }
    }
}
