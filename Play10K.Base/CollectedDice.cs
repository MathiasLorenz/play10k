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
        private readonly HandValidator _handValidator = new HandValidator();
        public List<DiceCollection> AllCollectedDice { get; private set; } = new List<DiceCollection>();
        public DiceCollection? LastCollected => AllCollectedDice.LastOrDefault();
        public Dictionary<int, int>? DiceCollectedThisHand { get; private set; } = null;
        public int Score { get => AllCollectedDice.Sum(x => x.Score); }

        // Collects the specified dice into DiceCollectedThisHand.
        public void CollectDice(ICollection<int> dice)
        {
            var validation = _handValidator.TryValidateAllDice(dice, LastCollected, out var diceCounter);
            if (validation == false)
            {
                throw new InvalidOperationException("Dice should already be validated here, the dice have been changed, which should not be possible.");
            }

            DiceCollectedThisHand = diceCounter;
        }

        // Todo: Unit test and add to larger tests of all functionality
        public void DiscardDiceCollectedThisHand()
        {
            if (DiceCollectedThisHand == null)
            {
                throw new ArgumentNullException("No last collected to discard.");
            }
            DiceCollectedThisHand = null;
        }

        // Save dice collected this hand (DiceCollectedThisHand) to AllCollectedDice.
        // The hand is already validated, so we don't have to do this again.
        public void SaveCollectedThisHand()
        {
            if (DiceCollectedThisHand == null)
            {
                throw new ArgumentException("You have not put any dice aside and thus they cannot be saved.");
            }

            var diceCollection = DictionaryToDiceCollection(DiceCollectedThisHand);

            if (LastCollected != null)
            {
                // If last collected is a three-or-more count try to add to this followed by the rest, otherwise just add all
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
            diceCollection.OrderByDescending(x => x.Count);
            AllCollectedDice.AddRange(diceCollection);

            DiceCollectedThisHand = null;
        }

        // Should maybe be a Dictionary extension?
        // Or at least public and unit tested!
        // Todo: Refactor to a public place and unit test
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