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
                throw new ArgumentException("You already have dice collected for this hand. You either need to discard these or save before rolling and collecting again.");
            }

            var lastCollected = AllCollectedDice.LastOrDefault();
            var result = HandValidator.TryValidateAllDice(dice, lastCollected, out var diceCounter);
            if (result)
            {
                DiceCollectedThisHand = diceCounter;
            }
            return result;
        }

        // Save to dice collected this hand to AllCollectedDice.
        // The hand is already validated, so we don't have to do this again.
        public void SaveCollected()
        {
            if (DiceCollectedThisHand == null)
            {
                throw new ArgumentException("You have not put any dice aside and thus they cannot be saved.");
            }

            var diceCollection = DictionaryToDiceCollection(DiceCollectedThisHand);
            var lastCollected = AllCollectedDice.LastOrDefault();

            if (lastCollected != null)
            {
                // If last collected is a three-or-more count try to add to this followed by the rest, otherwise just add all
                var dieMatchesLastCollected = diceCollection.FirstOrDefault(x => x.Value == lastCollected.Value);
                if (dieMatchesLastCollected != null && lastCollected.Count >= 3)
                {
                    lastCollected.AddToCount(dieMatchesLastCollected.Count);
                    diceCollection.Remove(dieMatchesLastCollected);
                }
            }

            // The OrderByDescending is to ensure that the following case does not occur:
            // Collecting e.g. { 2, 2, 2, 5 } is valid and gives 250 points, but the 2s are no longer valid for being added to for the next hand!
            // Also putting the 5 aside blocks that. Thus, after rolling the remaining 2 dice, another 2 cannot be added to the already collected.
            // Therefore by ordering the collected after count, we always end AllCollectedDice with the lowest count we can and never
            // accidently gets the chance to add to a non-valid collection.
            diceCollection.OrderByDescending(x => x.Count);
            AllCollectedDice.AddRange(diceCollection);

            DiceCollectedThisHand = null;
        }

        private List<DiceCollection> DictionaryToDiceCollection(Dictionary<int, int> dict)
        {
            if (dict.Count == 0)
            {
                throw new ArgumentException("Cannot parse an empty dictionary of value and count.");
            }
            
            var diceCollection = new List<DiceCollection>();
            foreach (var (value, count) in dict)
            {
                if (count == 2)
                {
                    diceCollection.Add(new DiceCollection(value, 1));
                    diceCollection.Add(new DiceCollection(value, 1));
                }
                else
                {
                    diceCollection.Add(new DiceCollection(value, count));
                }
            }

            return diceCollection;
        }
    }
}