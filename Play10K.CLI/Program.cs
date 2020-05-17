using Play10K.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Play10K.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var players = PlayerCreator.Create();
            var game = new Game(players);

            game.Play();

            Congratulator.EndGame(players);
        }
    }
}
