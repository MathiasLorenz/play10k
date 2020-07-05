using Play10K.Base.CollectionExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Play10K.Base
{
    internal class DiceCollectionUtils
    {
        // Todo: Unit tests
        public List<DiceCollection> DictionaryToDiceCollections(IDictionary<int, int> dict)
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
