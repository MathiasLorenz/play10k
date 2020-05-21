using Play10K.Base.CollectionExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("Play10K.Base.Test")]
namespace Play10K.Base
{
    internal static class HandValidator
    {
        public static bool IsAnyDiceCombinationValid(ICollection<int> dice)
        {
            return IsAnyDiceCombinationValid(dice, null);
        }

        public static bool IsAnyDiceCombinationValid(ICollection<int> dice, DiceCollection? lastCollected)
        {
            if (dice.Count == 0)
            {
                throw new ArgumentException("List of dice cannot be empty.");
            }
            else if (dice.Count > 6)
            {
                throw new ArgumentException("Cannot check for more than six dice.");
            }
            else if (dice.Any(x => x < 1) || dice.Any(x => x > 6))
            {
                throw new ArgumentException("Dice value has to be in range [1, 6]");
            }

            var counter = dice.DictionaryCounter();

            if (counter.TryGetValue(1, out var _) || counter.TryGetValue(5, out var _))
            {
                return true;
            }
            else if (counter.Any(x => x.Value >= 3))
            {
                return true;
            }
            else
            {
                if (lastCollected == null)
                {
                    return false;
                }
                else
                {
                    if (counter.Any(x => x.Key == lastCollected.Value))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
    }
}
