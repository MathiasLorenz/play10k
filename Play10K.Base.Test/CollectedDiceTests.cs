using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Play10K.Base.Test
{
    [TestClass]
    public class CollectedDiceTests
    {
        [TestMethod]
        [DataRow(new int[] { 1, 1, 1 })]
        [DataRow(new int[] { 1, 5, 1 })]
        [DataRow(new int[] { 4, 4, 4, 4 })]
        [DataRow(new int[] { 5 })]
        [DataRow(new int[] { 5, 5, 5, 5, 1 })]
        public void CollectDice_Collects(int[] toCollect)
        {
            var collectedDice = new CollectedDice();
            collectedDice.CollectDice(toCollect);
            Assert.IsNotNull(collectedDice.DiceCollectedThisHand);
        }

        [TestMethod]
        [DataRow(new int[] { 4 })]
        [DataRow(new int[] { 5, 5, 5, 5, 2 })]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CollectDice_IncorrectInput_ThrowsException(int[] toCollect)
        {
            var collectedDice = new CollectedDice();
            collectedDice.CollectDice(toCollect);
        }

        [TestMethod]
        [DataRow(new int[] { 5 }, new int[] { 5, 1 })]
        [DataRow(new int[] { 1, 5 }, new int[] { 1, 1, 5, 1 })]
        [DataRow(new int[] { 3, 3, 3 }, new int[] { 3, 3 })]
        [DataRow(new int[] { 3, 3, 3, 3 }, new int[] { 3, 4 })]
        [DataRow(new int[] { 3, 3, 3, 3, 1 }, new int[] { 3, 4, 1, 1 })] // This together with the below are the important, shows the dice ordering is not taking into account.
        [DataRow(new int[] { 1, 3, 3, 3, 3 }, new int[] { 3, 4, 1, 1 })]
        public void SaveCollectedThisHand_SingleHandWithNoLastCollected_SavesCorrectly(int[] toCollect, int[] expectedDiceCollection)
        {
            var collectedDice = new CollectedDice();
            collectedDice.CollectDice(toCollect);
            collectedDice.SaveCollectedThisHand();

            Assert.IsNull(collectedDice.DiceCollectedThisHand);

            VerifyAllCollectedDice(collectedDice, expectedDiceCollection);
        }

        [TestMethod]
        [DataRow(new int[] { 1 }, new int[] { 5 }, new int[] { 1, 1, 5, 1 })]
        [DataRow(new int[] { 1 }, new int[] { 1 }, new int[] { 1, 1, 1, 1 })]
        [DataRow(new int[] { 4, 4, 4 }, new int[] { 4 }, new int[] { 4, 4 })]
        [DataRow(new int[] { 4, 4, 4 }, new int[] { 1 }, new int[] { 4, 3, 1, 1 })]
        public void SaveCollectedThisHand_MultipleHands_SavesCorrectly(int[] collectFirst, int[] thenCollect, int[] expectedDiceCollection)
        {
            var collectedDice = new CollectedDice();
            collectedDice.CollectDice(collectFirst);
            collectedDice.SaveCollectedThisHand();
            collectedDice.CollectDice(thenCollect);
            collectedDice.SaveCollectedThisHand();

            Assert.IsNull(collectedDice.DiceCollectedThisHand);

            VerifyAllCollectedDice(collectedDice, expectedDiceCollection);
        }

        private void VerifyAllCollectedDice(CollectedDice collectedDice, int[] expectedDiceCollection)
        {
            int index = 0;
            foreach (var diceCollection in collectedDice.AllCollectedDice)
            {
                var expected = new DiceCollection(expectedDiceCollection[index++], expectedDiceCollection[index++]);
                Assert.AreEqual(expected.Count, diceCollection.Count);
                Assert.AreEqual(expected.Value, diceCollection.Value);
            }
        }

    }
}
