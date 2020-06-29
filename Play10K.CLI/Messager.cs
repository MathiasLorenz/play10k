using Play10K.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Play10K.CLI
{
    public static class Messager
    {
        public static void StartGame()
        {
            Console.WriteLine("Welcome to playing 10k!");
            Console.WriteLine();
        }

        public static void EndGame(List<Player> players)
        {
            Console.WriteLine("Game is finised! Thanks for playing.");
            Console.WriteLine("The leaderboard is as follows:");
            var leaderboard = players.OrderByDescending(x => x.Score);
            int place = 1;
            foreach (var player in leaderboard)
            {
                Console.WriteLine($"In place: {place} is {player.Name} with {player.Score} points!");
                place++;
            }
        }
    }
}
