using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Play10K.Base.CollectionExtensions;

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
        public void CollectDice_SingleHand_CollectsCorrectly(int[] toCollect)
        {
            var collectedDice = new CollectedDice();
            collectedDice.Collect(toCollect);
            Assert.IsNotNull(collectedDice.LastCollected);
        }

        //[TestMethod]
        //[DataRow(new int[] { 4 })]
        //[DataRow(new int[] { 5, 5, 5, 5, 2 })]
        //[ExpectedException(typeof(InvalidOperationException))]
        //public void CollectDice_IncorrectInput_ThrowsException(int[] toCollect)
        //{
        //    var collectedDice = new CollectedDice();
        //    collectedDice.CollectDice(toCollect);
        //}

        //[TestMethod]
        //[DataRow(new int[] { 5 }, new int[] { 5, 1 })]
        //[DataRow(new int[] { 1, 5 }, new int[] { 1, 1, 5, 1 })]
        //[DataRow(new int[] { 3, 3, 3 }, new int[] { 3, 3 })]
        //[DataRow(new int[] { 3, 3, 3, 3 }, new int[] { 3, 4 })]
        //[DataRow(new int[] { 3, 3, 3, 3, 1 }, new int[] { 3, 4, 1, 1 })] // This together with the below are the important, shows the dice ordering is not taking into account.
        //[DataRow(new int[] { 1, 3, 3, 3, 3 }, new int[] { 3, 4, 1, 1 })]
        //[DataRow(new int[] { 3, 3, 1, 3, 3 }, new int[] { 3, 4, 1, 1 })]
        //public void SaveCollectedThisHand_SingleHandWithNoLastCollected_SavesCorrectly(int[] toCollect, int[] toExpectedDiceCollections)
        //{
        //    var collectedDice = new CollectedDice();
        //    collectedDice.CollectDice(toCollect);
        //    collectedDice.SaveCollectedThisHand();

        //    Assert.IsNull(collectedDice.DiceCollectedThisHand);

        //    VerifyAllCollectedDice(collectedDice, toExpectedDiceCollections);
        //}

        //[TestMethod]
        //[DataRow(new int[] { 1 }, new int[] { 5 }, new int[] { 1, 1, 5, 1 })]
        //[DataRow(new int[] { 1 }, new int[] { 1 }, new int[] { 1, 1, 1, 1 })]
        //[DataRow(new int[] { 4, 4, 4 }, new int[] { 4 }, new int[] { 4, 4 })]
        //[DataRow(new int[] { 4, 4, 4 }, new int[] { 1 }, new int[] { 4, 3, 1, 1 })]
        //public void SaveCollectedThisHand_MultipleHands_SavesCorrectly(int[] collectFirst, int[] thenCollect, int[] toExpectedDiceCollections)
        //{
        //    var collectedDice = new CollectedDice();
        //    collectedDice.CollectDice(collectFirst);
        //    collectedDice.SaveCollectedThisHand();
        //    collectedDice.CollectDice(thenCollect);
        //    collectedDice.SaveCollectedThisHand();

        //    Assert.IsNull(collectedDice.DiceCollectedThisHand);

        //    VerifyAllCollectedDice(collectedDice, toExpectedDiceCollections);
        //}

        private List<DiceCollection> ListToDiceCollection(int[] list)
        {
            Assert.IsTrue(list.Length % 2 == 0);

            var diceCollections = new List<DiceCollection>();
            var index = 0;
            for (int i = 0; i < list.Length / 2; i++)
            {
                diceCollections.Add(new DiceCollection(list[index++], list[index++]));
            }
            return diceCollections;
        }

        private void VerifyAllCollectedDice(CollectedDice collectedDice, int[] toExptecedDiceCollections)
        {
            var expectedDiceCollections = ListToDiceCollection(toExptecedDiceCollections);
            CollectionAssert.AreEqual(expectedDiceCollections, collectedDice.AllCollectedDice);
        }
    }
}
