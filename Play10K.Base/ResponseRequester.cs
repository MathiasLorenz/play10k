using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Play10K.Base
{
    public class ResponseRequester
    {
        public char GetResponse(ICollection<char> validResponses)
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
                    Console.WriteLine($"Any of these inputs are valid: " + ListValidInputs(validResponses));
                }
            }
        }

        private string ListValidInputs(ICollection<char> responses)
        {
            return String.Join('/', responses);
        }
    }
}
