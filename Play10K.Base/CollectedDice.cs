using Play10K.Base.CollectionExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Play10K.Base.Test")]
namespace Play10K.Base
{
    internal class CollectedDice
    {
        public List<DiceCollection> AllCollectedDice { get; private set; } = new List<DiceCollection>();
        public Dictionary<int, int>? DiceCollectedThisHand { get; private set; } = null;
        public int Score { get => AllCollectedDice.Sum(x => x.Score); }

        public bool CollectAndVerifyDice(ICollection<int> dice)
        {
            if (DiceCollectedThisHand != null)
            {
                // Consider the ability to add to this already collected hand. Maybe keep record of hands put into this class?
                // Or make a converter from DiceCollection -> ICollection<int>, so that the extra dice can be added and recalculated.
                throw new ArgumentException("You already have dice collected for this hand, you need to discard these.");
            }

            var lastCollected = AllCollectedDice.LastOrDefault();
            var result = HandValidator.TryValidateAllDice(dice, lastCollected, out var diceCounter);
            if (result)
            {
                DiceCollectedThisHand = diceCounter;
            }
            return result;
        }

        public void SaveCollectedDice()
        {
            if (DiceCollectedThisHand == null)
            {
                throw new ArgumentException("You have not put any dice aside and thus they cannot be saved.");
            }

            // Todo: Implement the save. Call DictionaryToDiceCollection and combine with lastCollected if present.
            

            DiceCollectedThisHand = null;
        }

        private List<DiceCollection> DictionaryToDiceCollection(Dictionary<int, int> dict)
        {
            return dict.Select(x => new DiceCollection(x.Key, x.Value)).ToList();
        }
    }
}