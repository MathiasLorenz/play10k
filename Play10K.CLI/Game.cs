using Play10K.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Play10K.CLI
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
                foreach (var player in Players)
                {
                    TakeTurn(player);
                    if (player.Score >= 10000)
                    {
                        isLastRound = true;
                    }
                }
            }
        }

        // Main logic, here the game is actually played.
        public void TakeTurn(Player player)
        {
            Console.WriteLine($"It is now your turn, {player.Name}.");
            Console.WriteLine($"Your score is currently: {player.Score}");

            while (true)
            {
                player.ReconcileHand();
                var canDoAnythingAfterRoll = player.Roll();
                if (canDoAnythingAfterRoll == false)
                {
                    // Todo: Write out: You lost, rip.
                    break;
                }
                
                player.ShowDice();

                hand.CollectDiceFromUser();

                var response = player.ContinueOrEndTurn();
                if (response == 'e')
                {
                    break;
                }
            }

            player.UpdateScore(player.TurnScore);
            Console.WriteLine($"Your turn is now over, {player.Name}.");
            Console.WriteLine($"You collected {player.TurnScore} points this turn and now have {player.Score} points in total.");
        }
    }
}
