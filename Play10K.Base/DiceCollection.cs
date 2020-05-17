using System.Collections.Generic;
using System.Linq;

namespace Play10K.Base
{
    internal class DiceCollection
    {
        public Stack<ValueDice> collectedDice { get; } = new Stack<ValueDice>();
        public int Score { get => collectedDice.Sum(x => x.Score); }

        /// <summary>
        /// Tries to add the specified dice in dice to collected dice, i.e. the ones we have already counted in our points.
        /// All validation is done in this step.
        /// </summary>
        /// <param name="dice"></param>
        public bool Add(List<int> dice)
        {
            if (HandValidator.SelectedDiceAreValid(dice, collectedDice.Peek()))
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