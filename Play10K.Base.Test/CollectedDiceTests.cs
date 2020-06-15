using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Play10K.Base.Test
{
    //[TestClass]
    //public class CollectedDiceTests
    //{
    //    [TestMethod]
    //    [DataRow(new int[] { 5 }, 50)]
    //    [DataRow(new int[] { 1 }, 100)]
    //    [DataRow(new int[] { 3, 3, 3 }, 300)]
    //    [DataRow(new int[] { 4, 4, 4, 4, 4 }, 1600)]
    //    [DataRow(new int[] { 1, 1, 1 }, 1000)]
    //    [DataRow(new int[] { 1, 1, 1, 1 }, 2000)]
    //    [DataRow(new int[] { 6, 6, 6, 6, 6, 6 }, 4800)]
    //    [DataRow(new int[] { 1, 1 }, 200)]
    //    [DataRow(new int[] { 5, 5 }, 100)]
    //    [DataRow(new int[] { 1, 5 }, 150)]
    //    [DataRow(new int[] { 5, 1 }, 150)]
    //    [DataRow(new int[] { 1, 1, 5 }, 250)]
    //    [DataRow(new int[] { 5, 5, 1 }, 200)]
    //    [DataRow(new int[] { 1, 5, 1 }, 250)]
    //    [DataRow(new int[] { 1, 5, 1, 1 }, 1050)]
    //    [DataRow(new int[] { 1, 5, 1, 1, 1 }, 2050)]
    //    [DataRow(new int[] { 1, 5, 1, 1, 1, 1 }, 4050)]
    //    [DataRow(new int[] { 1, 1, 1, 5, 5, 5 }, 1500)]
    //    [DataRow(new int[] { 1, 1, 1, 5, 5 }, 1100)]
    //    [DataRow(new int[] { 1, 1, 1, 1, 5, 5 }, 2100)]
    //    [DataRow(new int[] { 6, 6, 6, 1, 5, 5 }, 800)]
    //    [DataRow(new int[] { 6, 6, 6, 5 }, 650)]
    //    public void SaveCollected_SingleHand_CalculatesCorrectScore(int[] toCollect, int expectedScore)
    //    {
    //        var collectedDice = new CollectedDice();
    //        var result = collectedDice.CollectAndVerifyDice(toCollect);
    //        var expectedCollect = true;
    //        Assert.AreEqual(expectedCollect, result);
            
    //        collectedDice.SaveCollected();
    //        Assert.AreEqual(expectedScore, collectedDice.Score);
    //    }

    //    [TestMethod]
    //    public void CollectAndVerifyDice_Then_SaveCollected_MultipleHands()
    //    {
    //        var collectedDice = new CollectedDice();
    //        var result = collectedDice.CollectAndVerifyDice(new int[] { 3, 3, 3 });
    //        var expected = true;
    //        Assert.AreEqual(expected, result);
    //        collectedDice.SaveCollected();
    //        Assert.AreEqual(300, collectedDice.Score);

    //        result = collectedDice.CollectAndVerifyDice(new int[] { 4 });
    //        expected = false;
    //        Assert.AreEqual(expected, result);

    //        result = collectedDice.CollectAndVerifyDice(new int[] { 3 });
    //        expected = true;
    //        Assert.AreEqual(expected, result);
    //        collectedDice.SaveCollected();
    //        Assert.AreEqual(600, collectedDice.Score);

    //        result = collectedDice.CollectAndVerifyDice(new int[] { 1 });
    //        expected = true;
    //        Assert.AreEqual(expected, result);
    //        collectedDice.SaveCollected();
    //        Assert.AreEqual(700, collectedDice.Score);

    //        result = collectedDice.CollectAndVerifyDice(new int[] { 3 });
    //        expected = false;
    //        Assert.AreEqual(expected, result);

    //        result = collectedDice.CollectAndVerifyDice(new int[] { 5 });
    //        expected = true;
    //        Assert.AreEqual(expected, result);
    //        collectedDice.SaveCollected();
    //        Assert.AreEqual(750, collectedDice.Score);
    //    }
    //}
}
