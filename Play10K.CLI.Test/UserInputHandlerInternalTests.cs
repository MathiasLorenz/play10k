using Microsoft.VisualStudio.TestTools.UnitTesting;
using Play10K.CLI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Play10K.Base.Test
{
    [TestClass]
    public class UserInputInternalTests
    {
        [TestMethod]
        [DataRow("535", new int[] { 5, 3, 5 })]
        [DataRow("4", new int[] { 4 })]
        [DataRow("98765644321", new int[] { 9, 8, 7, 6, 5, 6, 4, 4, 3, 2, 1 })]
        public void GetSpecifiedDiceInternal_CorrectInput_ReturnsValues(string input, int[] expected)
        {
            var userInputHandlerInternal = new UserInputInternal();
            var result = userInputHandlerInternal.GetSpecifiedDiceInternal(input);
            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(expected.ToList(), result.ToList());
        }

        [TestMethod]
        [DataRow("ab345")]
        [DataRow("3)4")]
        [DataRow("╩")]
        public void GetSpecifiedDiceInternal_InvalidInput_ReturnsNull(string input)
        {
            var userInputHandlerInternal = new UserInputInternal();
            var result = userInputHandlerInternal.GetSpecifiedDiceInternal(input);
            Assert.IsNull(result);
        }
    }
}
