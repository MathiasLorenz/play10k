using Play10K.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Play10K.CLI
{
    public class GameDriver
    {
        private readonly UserInput _userInput = new UserInput();

        // Todo: Split into two functions - one for getting dice input and one asking to keep or not.
        public List<int> GetSpecifiedDice(Player player)
        {
            List<int> dice;
            while (true)
            {
                dice = _userInput.GetSpecifiedDice().ToList();
                while (player.VerifyDiceToCollect(dice) == false)
                {
                    // Todo: Implement a way to tell which dice were wrong.
                    Console.WriteLine($"The specified dice were not valid. Try again :)");
                    dice = _userInput.GetSpecifiedDice().ToList();
                }

                // Ask the user whether or not to keep this choice.
                // Todo: Fix this write out to the user
                Console.WriteLine($"You have put XXXX aside right now.");
                Console.WriteLine($"Together with the already collected, this amounts to {player.TurnScoreWithTempDice(dice)} points.");
                Console.WriteLine($"Would you like to save these dice and move on with your turn? Note that this cannot be undone.");
                Console.WriteLine($"Save these dice or cancel and choose again? Enter s/c:");
                var response = _userInput.GetCharResponse(new List<char> { 's', 'c' });
                if (response == 's')
                {
                    break;
                }
            }

            return dice;
        }

        internal void ShowScoreAfterRound(List<Player> players)
        {
            Console.WriteLine();
            Console.WriteLine("The round is over. The scores (in order) are:");
            foreach (var player in players.OrderByDescending(x => x.Score))
            {
                Console.WriteLine($"{player.Name} with score: {player.Score}.");
            }
        }

        internal void ShowPlayerOrder(List<Player> players)
        {
            Console.WriteLine();
            Console.WriteLine("The order of players has been shuffled. The order is:");
            Console.WriteLine(string.Join(", ", players.Select(x => x.Name)));
        }

        public void ShowHand(Player player)
        {
            Console.WriteLine();
            Console.WriteLine($"This turn you have collected {player.TurnScore} points.");
            Console.WriteLine($"Your dice right now are:");
            var sortedList = new List<int>(player.Dice);
            sortedList.Sort();

            Console.WriteLine(string.Join(" ", sortedList));
        }

        // Think pure messages should be in their own class.
        public void MessageNoValidDice()
        {
            Console.WriteLine();
            Console.WriteLine("Unfortunately with your roll you cannot do anything. Your turn is up!");
        }

        public void MessageStartOfTurn(Player player)
        {
            Console.WriteLine();
            Console.WriteLine($"It is now your turn, {player.Name}.");
            Console.WriteLine($"Your score is currently: {player.Score}");
        }

        public void MessageEndOfTurn(Player player, int turnScore)
        {
            Console.WriteLine();
            Console.WriteLine($"Your turn is now over, {player.Name}.");
            Console.WriteLine($"You collected {turnScore} points this turn and have {player.Score} points in total.");
        }

        public char ContinueOrEndTurn(Player player)
        {
            Console.WriteLine();
            Console.WriteLine($"This turn you have collected {player.TurnScore} points.");
            Console.WriteLine($"You have {player.Dice.Count} dice left.");
            Console.WriteLine($"Do you want to continue your turn by rolling again, or do you want to stop now and collect the points?");
            Console.WriteLine($"For roll/end, enter: r/e");
            return _userInput.GetCharResponse(new List<char> { 'r', 'e' });
        }

        public void WaitForAnyKeyInput()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to start the next players turn...");
            _userInput.GetAnyInput();
            Console.WriteLine();
        }
    }
}
