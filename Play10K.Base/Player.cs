using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
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
                // Only place hand is rolled is here
                hand.Roll();

                Console.WriteLine($"This turn you have collected {hand.Score} points.");
                Console.WriteLine($"Your dice right now are:");
                hand.Show();

                var diceCollected = false;
                Console.WriteLine("Please input the dice you want to collect with space between. Finish with enter.");
                while (diceCollected == false)
                {
                    var collectedDice = GetSpecifiedDice(Console.ReadLine());
                    if (collectedDice == null)
                    {
                        Console.WriteLine("I didn't quite get that. Please try to input again. Remember to input numbers [1, 6] seperated by spaces and finish with enter.");
                    }
                    else
                    {
                        if (hand.CollectAndVerifyDice(collectedDice) == false)
                        {
                            Console.WriteLine("Those dice could not be validated, please try again and choose a valid combination.");
                        }
                        else
                        {
                            // Now collectedDice are in hand.CollectedThisHand.
                            diceCollected = true;
                        }
                    }
                }

                // Now some dice are collected. Show the score and prompt for saving. If true, save and ask to continue or not
            }

            Console.WriteLine($"Your turn is now over, {Name}.");
            Console.WriteLine($"You collected {hand.Score} points this turn and now have {Score} points in total.");
        }

        // I would like to unit test this seperately => must be factored out.
        // Maybe a Dice(Input)Collecter service? Can also handle the input (not super seperation of concerns in that case though..).
        private List<int>? GetSpecifiedDice(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }
            var dice = new List<int>();

            var splitInput = input.Split(' ');
            foreach (var item in splitInput)
            {
                if (int.TryParse(item, out int result) && result >= 1 && result <= 6)
                {
                    dice.Add(result);
                }
                else
                {
                    return null;
                }
            }

            return dice;
        }
    }
}
