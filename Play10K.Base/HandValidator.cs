using Play10K.Base.CollectionExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("Play10K.Base.Test")]
namespace Play10K.Base
{
    internal class HandValidator
    {
        public bool TryValidateAnyDice(ICollection<int> dice)
        {
            return InternalTryValidate(dice, null, false, out var _);
        }

        public bool TryValidateAnyDice(ICollection<int> dice, DiceCollection? lastCollected)
        {
            return InternalTryValidate(dice, lastCollected, false, out var _);
        }

        //public static bool TryValidateAnyDice(ICollection<int> dice, out Dictionary<int, int>? diceCounter)
        //{
        //    return InternalTryValidate(dice, null, false, out diceCounter);
        //}

        //public static bool TryValidateAnyDice(ICollection<int> dice, DiceCollection? lastCollected, out Dictionary<int, int>? diceCounter)
        //{
        //    return InternalTryValidate(dice, lastCollected, false, out diceCounter);
        //}

        public bool TryValidateAllDice(ICollection<int> dice)
        {
            return InternalTryValidate(dice, null, true, out var _);
        }

        public bool TryValidateAllDice(ICollection<int> dice, DiceCollection? lastCollected)
        {
            return InternalTryValidate(dice, lastCollected, true, out var _);
        }

        //public static bool TryValidateAllDice(ICollection<int> dice, out Dictionary<int, int>? diceCounter)
        //{
        //    return InternalTryValidate(dice, null, true, out diceCounter);
        //}

        // Todo: Get rid of out diceCounter, I don't like this pattern...
        public bool TryValidateAllDice(ICollection<int> dice, DiceCollection? lastCollected, out Dictionary<int, int>? diceCounter)
        {
            return InternalTryValidate(dice, lastCollected, true, out diceCounter);
        }

        private static bool InternalTryValidate(ICollection<int> dice, DiceCollection? lastCollected, bool validateAll, out Dictionary<int, int>? diceCounter)
        {
            if (dice.Count == 0 || dice.Count > 6)
            {
                throw new ArgumentException("Overall dice count cannot be less than one or more than six.");
            }
            diceCounter = dice.DictionaryCounter();
            var successes = diceCounter.Select(x => InternalTryValidateDiceCounter(x.Key, x.Value, lastCollected));
            var result = validateAll ? successes.All(x => x == true) : successes.Any(x => x == true);

            if (result == false)
            {
                diceCounter = null;
            }
            return result;
        }

        private static bool InternalTryValidateDiceCounter(int value, int count, DiceCollection? lastCollected)
        {
            if (count <= 0 || count > 6)
            {
                throw new ArgumentException("You cannot save less than 1 or more than six dice.");
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
