using Play10K.Base.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Play10K.Base
{
    internal static class HandValidator
    {
        public static bool AnyValidDiceCombination(List<int> dice, ValueDice? lastCollected)
        {
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

        public static bool SelectedDiceAreValid(List<int> dice, ValueDice lastCollected)
        {

            return false;
        }
    }
}
