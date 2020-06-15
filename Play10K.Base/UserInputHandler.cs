using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Play10K.Base
{
    public class UserInputHandler
    {
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

        public IEnumerable<int> GetSpecifiedDice()
        {
            var input = Console.ReadLine();
            IEnumerable<int>? result = null;
            while (result == null)
            {
                Console.WriteLine($"Input dice to collect.");
                Console.WriteLine($"Type them without spaces or anything, like 333 or 5.");
                result = GetSpecifiedDiceInternal(input);
                if (result == null)
                {
                    Console.WriteLine($"Seems like your input was invalid. Try again!");
                }
            }
            return result;
        }

        // Todo: Unit test, cannot be right now as it is private :(
        private IEnumerable<int>? GetSpecifiedDiceInternal(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }

            var splitInput = input.Select(x => (int)Char.GetNumericValue(x));
            if (splitInput == null || splitInput.Any(x => x > 1 || x > 6))
            {
                return null;
            }

            return splitInput;
        }
    }
}
