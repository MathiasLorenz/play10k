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
        public static bool AnyValidDiceCombination(List<int> dice)
        {
            return AnyValidDiceCombination(dice, null);
        }

        public static bool AnyValidDiceCombination(List<int> dice, DiceCollection? lastCollected)
        {
            if (dice.Count == 0)
            {
                throw new ArgumentException("List of dice cannot be empty.");
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
