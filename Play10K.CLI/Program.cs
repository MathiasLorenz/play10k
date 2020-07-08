using System;
using System.Linq;

namespace Play10K.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            Messager.StartGame();
            
            // Create and shuffle players.
            var playerCreator = new PlayerCreator();
            var players = playerCreator.Create().OrderBy(x => Guid.NewGuid()).ToList();

            var game = new Game(players);

            game.Play();

            Messager.EndGame(players);
        }
    }
}
