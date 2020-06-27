using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("Play10K.CLI.Test")]
namespace Play10K.CLI
{
    internal class UserInputInternal
    {
        public IEnumerable<int>? GetSpecifiedDiceInternal(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }

            var splitInput = input.Select(x => (int)Char.GetNumericValue(x));

            // Char.GetNumericValue() return -1 if the char does not represent a number, thus we check for x < 0.
            // Any input < 0 would be illegal anyway, so we do not lose any with this check.
            var anyIllegalInput = splitInput.Any(x => x < 0);

            if (splitInput == null || anyIllegalInput)
            {
                return null;
            }
            else
            {
                return splitInput;
            }
        }
    }
}
