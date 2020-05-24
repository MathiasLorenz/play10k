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
        private List<DiceCollection> _collectedDice = new List<DiceCollection>();
        public int Score { get => _collectedDice.Sum(x => x.Score); }

        /// <summary>
        /// Tries to add the specified dice in dice to collected dice, i.e. the ones we have already counted in our points.
        /// All validation is done here.
        /// </summary>
        /// <param name="dice"></param>
        // Todo: Unit test
        public bool SaveDice(ICollection<int> dice)
        {
            DiceCollection? lastCollected = _collectedDice.LastOrDefault();
            if (dice.Count <= 0 || dice.Count > 6)
            {
                throw new ArgumentException("Number of dice outside legal range [0, 6]");
            }
            else if (dice.Count == 1)
            {
                var die = dice.Single();
                if (lastCollected?.Count >= 3)
                {
                    if (die == lastCollected.Value)
                    {
                        lastCollected.AddToCount(1);
                    }
                    else if (die == 1 || die == 5)
                    {
                        _collectedDice.Add(new DiceCollection(die, 1));
                    }
                    else
                    {
                        return false;
                    }
                }
                else // => lastCollected?.Count == 1
                {
                    if (die == 1 || die == 5)
                    {
                        _collectedDice.Add(new DiceCollection(die, 1));
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else if (dice.Count == 2)
            {
                if (dice.Any(x => x != 1 && x != 5))
                {
                    return false;
                }
                else if (dice.All(x => x == 1) || dice.All(x => x == 5))
                {
                    var die = dice.First();
                    if (die == lastCollected?.Value && lastCollected?.Count >= 3)
                    {
                        lastCollected.AddToCount(2);
                    }
                    else
                    {
                        _collectedDice.Add(new DiceCollection(die, 1));
                        _collectedDice.Add(new DiceCollection(die, 1));
                    }
                }
                else // => {1, 5} or {5, 1}
                {
                    _collectedDice.Add(new DiceCollection(dice.First(), 1));
                    _collectedDice.Add(new DiceCollection(dice.Last(), 1));
                }
            }
            else // => 3 <= Count <= 6
            {
                if (dice.AllElementsEqual() == false) // If all elements are NOT the same
                {
                    if (dice.Any(x => x != 1 && x != 5))
                    {
                        return false;
                    }
                    else // Consists of only 1 and 5
                    {
                        foreach (var die in dice)
                        {
                            _collectedDice.Add(new DiceCollection(die, 1));
                        }
                    }
                }
                else
                {
                    if (dice.Count == 3)
                    {
                        if (lastCollected?.Count == 3)
                        {
                            if (dice.First() == lastCollected?.Value)
                            {
                                lastCollected.AddToCount(3);
                            }
                            else
                            {
                                _collectedDice.Add(new DiceCollection(dice.First(), 3));
                            }
                        }
                        else // => lastCollected?.Count == 1
                        {
                            _collectedDice.Add(new DiceCollection(dice.First(), 3));
                        }
                    }
                    else // => dice.Count >= 4, thus we cannot have a collection with at least 3 elements already, so we need a new
                    {
                        _collectedDice.Add(new DiceCollection(dice.First(), dice.Count));
                    }
                }
            }

            return true;
        }
    }
}