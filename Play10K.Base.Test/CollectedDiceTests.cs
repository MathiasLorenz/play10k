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
        [DataRow(new int[] { 5 }, 50)]
        [DataRow(new int[] { 1 }, 100)]
        [DataRow(new int[] { 3, 3, 3 }, 300)]
        [DataRow(new int[] { 4, 4, 4, 4, 4 }, 1600)]
        [DataRow(new int[] { 1, 1, 1 }, 1000)]
        [DataRow(new int[] { 1, 1, 1, 1 }, 2000)]
        [DataRow(new int[] { 6, 6, 6, 6, 6, 6 }, 4800)]
        [DataRow(new int[] { 1, 1 }, 200)]
        [DataRow(new int[] { 5, 5 }, 100)]
        [DataRow(new int[] { 1, 5 }, 150)]
        [DataRow(new int[] { 5, 1 }, 150)]
        [DataRow(new int[] { 1, 1, 5 }, 250)]
        [DataRow(new int[] { 5, 5, 1 }, 200)]
        [DataRow(new int[] { 1, 5, 1 }, 250)]
        [DataRow(new int[] { 1, 5, 1, 1 }, 350)]
        [DataRow(new int[] { 1, 5, 1, 1, 1 }, 450)]
        [DataRow(new int[] { 1, 5, 1, 1, 1 }, 450)]
        [DataRow(new int[] { 1, 5, 1, 1, 1, 1 }, 550)]
        public void SaveDice_SingleHand_ReturnsTrue(int[] toCollect, int expectedScore)
        {
            var collectedDice = new CollectedDice();
            var result = collectedDice.CollectAndVerifyDice(toCollect);
            var expectedCollect = true;

            Assert.AreEqual(expectedCollect, result);
            collectedDice.SaveCollectedDice();
            Assert.AreEqual(expectedScore, collectedDice.Score);
        }

        [TestMethod]
        [DataRow(new int[] { 2 })]
        [DataRow(new int[] { 3 })]
        [DataRow(new int[] { 4 })]
        [DataRow(new int[] { 6 })]
        [DataRow(new int[] { 3, 3 })]
        [DataRow(new int[] { 4, 4 })]
        [DataRow(new int[] { 1, 4 })]
        [DataRow(new int[] { 5, 6 })]
        [DataRow(new int[] { 1, 2, 5 })]
        [DataRow(new int[] { 5, 3, 5 })]
        [DataRow(new int[] { 3, 3, 5 })]
        [DataRow(new int[] { 6, 6, 6, 2 })]
        [DataRow(new int[] { 1, 1, 6, 5 })]
        [DataRow(new int[] { 1, 1, 5, 5, 5 })]
        [DataRow(new int[] { 1, 1, 5, 3, 2, 1 })]
        public void SaveDice_SingleHand_ReturnsFalse(int[] toCollect)
        {
            var collectedDice = new CollectedDice();
            var result = collectedDice.CollectAndVerifyDice(toCollect);
            var expected = false;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void SaveDice_MultipleHands()
        {
            var collectedDice = new CollectedDice();
            var result = collectedDice.CollectAndVerifyDice(new int[] { 3, 3, 3 });
            var expected = true;
            Assert.AreEqual(expected, result);

            result = collectedDice.CollectAndVerifyDice(new int[] { 4 });
            expected = false;
            Assert.AreEqual(expected, result);

            result = collectedDice.CollectAndVerifyDice(new int[] { 3 });
            expected = true;
            Assert.AreEqual(expected, result);

            result = collectedDice.CollectAndVerifyDice(new int[] { 1 });
            expected = true;
            Assert.AreEqual(expected, result);

            result = collectedDice.CollectAndVerifyDice(new int[] { 3 });
            expected = false;
            Assert.AreEqual(expected, result);

            result = collectedDice.CollectAndVerifyDice(new int[] { 5 });
            expected = true;
            Assert.AreEqual(expected, result);

            var score = 750;
            Assert.AreEqual(score, collectedDice.Score);
        }
    }
}
