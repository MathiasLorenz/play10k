using Play10K.Base.CollectionExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("Play10K.Base.Test")]
namespace Play10K.Base.DiceValidation
{
    internal class DiceValidator
    {
        private readonly DiceValidatorInternal _handValidatorInternal = new DiceValidatorInternal();

        public bool TryValidateAnyDice(ICollection<int> dice)
        {
            return Validate(dice, null, false);
        }

        public bool TryValidateAnyDice(ICollection<int> dice, DiceCollection? lastCollected)
        {
            return Validate(dice, lastCollected, false);
        }

        public bool TryValidateAllDice(ICollection<int> dice)
        {
            return Validate(dice, null, true);
        }

        public bool TryValidateAllDice(ICollection<int> dice, DiceCollection? lastCollected)
        {
            return Validate(dice, lastCollected, true);
        }

        private bool Validate(ICollection<int> dice, DiceCollection? lastCollected, bool validateAll)
        {
            if (dice.Count == 0 || dice.Count > 6)
            {
                throw new ArgumentException("Overall dice count cannot be less than one or more than six.");
            }
            //if (_handValidatorInternal.AreDiceContainedInHand(handDice, dice) == false)
            //{
            //    return false;
            //}

            var diceCounter = dice.DictionaryCounter();
            var successes = diceCounter.Select(x => _handValidatorInternal.TryValidateDiceCounter(x.Key, x.Value, lastCollected));

            return validateAll ? successes.All(x => x == true) : successes.Any(x => x == true);
        }
    }
}
