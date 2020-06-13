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

        // Todo: Not tested at allllll.
        public void PlayTurn()
        {
            Console.WriteLine($"It is now your turn, {Name}.");
            Console.WriteLine($"Your score is currently: {Score}");
            var doContinue = true;
            var hand = new Hand();

            while (doContinue)
            {
                // If you have collected all six dice, then you need a new set of dice and roll from this fresh set of six.

                // Only place hand is rolled is here
                hand.Roll();
                if (hand.IsAnyCombinationValid() == false)
                {
                    // Clear hand and break out - turn is over
                    hand.Clear();
                    break;
                }

                Console.WriteLine($"This turn you have collected {hand.Score} points.");
                Console.WriteLine($"Your dice right now are:");
                hand.Show();

                var diceCollected = false;
                Console.WriteLine("Please input the dice you want to collect with space between. Finish with enter.");
                while (diceCollected == false)
                {
                    // Collect input from user as to which dice to collect

                    // Verify that the collected dice are valid.
                    // If not -> re-ask for input.
                    // If valid -> continue

                    // Prompt user with the collected dice, score for this hand and total score in turn.
                    // Ask whether or not to save the collection.
                    // If yes -> save, then ask whether to terminate turn or not.
                    // If no -> go back to ask for input.



                }


            }

            Console.WriteLine($"Your turn is now over, {Name}.");
            Console.WriteLine($"You collected {hand.Score} points this turn and now have {Score} points in total.");
        }

    }
}
