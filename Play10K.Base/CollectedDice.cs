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
        private readonly DiceCollectionUtils _diceCollectionUtils = new DiceCollectionUtils();

        public List<DiceCollection> AllCollectedDice { get; private set; } = new List<DiceCollection>();
        public DiceCollection? LastCollected => AllCollectedDice.LastOrDefault();
        public int Score { get => AllCollectedDice.Sum(x => x.Score); }

        // This method depends on the input to already be validated. Maybe shouldn't be like that.
        public void Collect(ICollection<int> dice)
        {
            // Doing dice -> DictionaryCounter -> DiceCollection could skip one step.
            var diceCollection = _diceCollectionUtils.DictionaryToDiceCollections(dice.DictionaryCounter());

            if (LastCollected != null)
            {
                // If last collected is a three-or-more count try to add to this followed by the rest, otherwise just add all.
                var dieMatchesLastCollected = diceCollection.FirstOrDefault(x => x.Value == LastCollected.Value);
                if (dieMatchesLastCollected != null && LastCollected.Count >= 3)
                {
                    LastCollected.AddToCount(dieMatchesLastCollected.Count);
                    diceCollection.Remove(dieMatchesLastCollected);
                }
            }

            // The OrderByDescending is to ensure that the following case does not occur:
            // Collecting e.g. { 2, 2, 2, 5 } is valid and gives 250 points, but the 2s are no longer valid for being added to for the next hand!
            // Also putting the 5 aside blocks that. Thus, after rolling the remaining 2 dice, another 2 cannot be added to the already collected.
            // Therefore by ordering the collected after count, we always end AllCollectedDice with the lowest count, thus blocking the above mentioned
            // illegal situation.
            diceCollection = diceCollection.OrderByDescending(x => x.Count).ToList();
            AllCollectedDice.AddRange(diceCollection);
        }
    }
}