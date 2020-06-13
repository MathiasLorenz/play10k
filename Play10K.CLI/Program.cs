using Play10K.Base;
using System;

namespace Play10K.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to playing 10k!");
            Console.WriteLine();

            var playerCreator = new PlayerCreator();
            var players = playerCreator.Create();
            var game = new Game(players);

            game.Play();

            Congratulator.EndGame(players);
        }
    }
}
