using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System.Collections.Generic;
using System.Linq;

namespace Play10K.Base.Test
{
    [TestClass]
    public class CollectedDiceTests
    {
        [TestMethod]
        [DataRow(new int[] { 1, 1, 1 }, new int[] { 1, 3 })]
        [DataRow(new int[] { 1, 5, 1 }, new int[] { 1, 1, 1, 1, 5, 1 })]
        [DataRow(new int[] { 4, 4, 4, 4 }, new int[] { 4, 4 })]
        [DataRow(new int[] { 5 }, new int[] { 5, 1 })]
        [DataRow(new int[] { 5, 5, 5, 5, 1 }, new int[] { 5, 4, 1, 1 })]
        public void Collect_CheckPropertiesLastCollectedAndAllCollectedDice(int[] toCollect, int[] toExpectedDiceCollections)
        {
            var collectedDice = new CollectedDice();
            var expectedDiceCollections = ListToDiceCollection(toExpectedDiceCollections);

            collectedDice.Collect(toCollect);

            // Checks for last collected.
            collectedDice.ShouldSatisfyAllConditions(
                () => collectedDice.LastCollected.ShouldNotBeNull(),
                () => collectedDice.LastCollected.ShouldBe(expectedDiceCollections.Last())
            );
            
            // Check for all collected dice.
            CollectionAssert.AreEqual(expectedDiceCollections, collectedDice.AllCollectedDice);
        }

        [TestMethod]
        [DataRow(new int[] { 1, 1, 1 }, new int[] { 1 }, new int[] { 1, 4 })]
        [DataRow(new int[] { 1, 1, 1 }, new int[] { 5, 1 }, new int[] { 1, 4, 5, 1 })]
        [DataRow(new int[] { 1, 1, 1 }, new int[] { 1, 5 }, new int[] { 1, 4, 5, 1 })]
        [DataRow(new int[] { 4, 4, 4 }, new int[] { 5, 4 }, new int[] { 4, 4, 5, 1 })]
        [DataRow(new int[] { 6, 6, 6, 6 }, new int[] { 6 }, new int[] { 6, 5 })]
        public void Collect_CollectTwoHands_CorrectlyAddsToLastCollected(int[] firstCollect, int[] thenCollect, int[] toExpectedDiceCollections)
        {
            var collectedDice = new CollectedDice();
            var expectedDiceCollections = ListToDiceCollection(toExpectedDiceCollections);

            collectedDice.Collect(firstCollect);
            collectedDice.Collect(thenCollect);

            // Checks for last collected.
            collectedDice.ShouldSatisfyAllConditions(
                () => collectedDice.LastCollected.ShouldNotBeNull(),
                () => collectedDice.LastCollected.ShouldBe(expectedDiceCollections.Last())
            );

            // Check for all collected dice.
            CollectionAssert.AreEqual(expectedDiceCollections, collectedDice.AllCollectedDice);
        }

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
    }
}
