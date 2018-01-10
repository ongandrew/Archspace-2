using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2
{
    [TestClass]
    [TestCategory("Admiral")]
    public class AdmiralTests
    {
        [TestMethod]
        public void AdmiralNamesAreCorrect()
        {
            Admiral admiral = new Admiral();

            Console.WriteLine(admiral.Name);
        }
    }
}
