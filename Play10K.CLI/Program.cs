using System;

namespace Play10K.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            Messager.StartGame();

            var playerCreator = new PlayerCreator();
            var players = playerCreator.Create();
            var game = new Game(players);

            game.Play();

            Messager.EndGame(players);
        }
    }
}
