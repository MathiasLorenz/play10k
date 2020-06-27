using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Play10K.CLI
{
    public class UserInput
    {
        private readonly UserInputInternal _userInputInternal = new UserInputInternal();
        /// <summary>
        /// Collects a char response from the user.
        /// </summary>
        /// <param name="validResponses">List of valid response chars.</param>
        /// <returns>The char supplied from the user. Is in the input list <paramref name="validResponses"/>.</returns>
        public char GetCharResponse(ICollection<char> validResponses)
        {
            while (true)
            {
                var response = Console.ReadLine().Trim().ToLower().ToCharArray();
                if (response.Length == 1 && validResponses.Contains(response.First()))
                {
                    return response.First();
                }
                else
                {
                    Console.WriteLine("That was not a valid input.");
                    Console.WriteLine($"Any of these inputs are valid: {String.Join('/', validResponses)}");
                }
            }
        }

        /// <summary>
        /// Collects the dice from the user to be saved in this roll.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<int> GetSpecifiedDice()
        {
            while (true)
            {
                Console.WriteLine($"Input dice to collect.");
                Console.WriteLine($"Type them without spaces or anything, like 333 or 5.");
                var input = Console.ReadLine();
                var dice = _userInputInternal.GetSpecifiedDiceInternal(input);
                if (dice != null)
                {
                    return dice;
                }
                else
                {
                    Console.WriteLine($"Seems like your input was invalid. Try again!");
                }
            }
        }
    }
}
