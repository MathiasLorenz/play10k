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

        public bool ValidateAnyDice(ICollection<int> handDice, int? lastCollectedValue = null)
        {
            var handCounter = handDice.DictionaryCounter();
            return Validate(handCounter, lastCollectedValue, false);
        }

        public bool ValidateAllDice(ICollection<int> handDice, ICollection<int> dice, int? lastCollectedValue = null)
        {
            var handCounter = handDice.DictionaryCounter();
            var diceCounter = dice.DictionaryCounter();

            if (_handValidatorInternal.AreDiceContainedInHand(handCounter, diceCounter) == false)
            {
                return false;
            }
            else
            {
                return Validate(diceCounter, lastCollectedValue, true);
            }
        }

        // Public test?
        private bool Validate(IDictionary<int, int> diceCounter, int? lastCollectedValue, bool validateAll)
        {
            var successes = diceCounter
                .Select(x => _handValidatorInternal.ValidateDiceCount(x.Key, x.Value, lastCollectedValue));

            return validateAll ? successes.All(x => x == true) : successes.Any(x => x == true);
        }
    }
}
