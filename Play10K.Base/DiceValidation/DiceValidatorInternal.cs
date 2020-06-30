using Play10K.Base.CollectionExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Play10K.Base.Test")]
namespace Play10K.Base.DiceValidation
{
    internal class DiceValidatorInternal
    {
        public bool AreDiceContainedInHand(Dictionary<int, int> handDice, Dictionary<int, int> dice)
        {
            foreach (var (diceValue, diceCount) in dice)
            {
                var hasValue = handDice.TryGetValue(diceValue, out var handCount);
                if (hasValue == false || diceCount > handCount)
                {
                    return false;
                }
            }

            return true;
        }

        public bool TryValidateDiceCounter(int value, int count, DiceCollection? lastCollected)
        {
            if (count <= 0 || count > 6)
            {
                throw new ArgumentException("You cannot save less than one or more than six dice.");
            }
            else if (count >= 3)
            {
                return true;
            }

            if (value <= 0 || value > 6)
            {
                throw new ArgumentException("Dice value is invalid, has to be [1, 6]");
            }
            else if (value == 1 || value == 5)
            {
                return true;
            }
            else // value = { 2, 3, 4, 6 }
            {
                // lastCollected can only have a value in { 2, 3, 4, 6 } if the corresponding count is >= 3, so this is fine.
                return value == lastCollected?.Value;
            }
        }
    }
}
