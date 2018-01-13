using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Archspace2
{
    [TestClass]
    [TestCategory("Admiral")]
    public class AdmiralTests
    {
        [TestMethod]
        public void CreatingAdmiralsGeneratesRandomProperties()
        {
            List<Admiral> admirals = new List<Admiral>();

            for (int i = 0; i < 10; i++)
            {
                admirals.Add(new Admiral(Game.Universe));
            }

            Assert.AreNotEqual(0, admirals.Count);

            Assert.AreNotEqual(1, admirals.GroupBy(x => x.Name).Count());
            Assert.AreNotEqual(1, admirals.GroupBy(x => x.RacialAbility).Count());
            Assert.AreNotEqual(1, admirals.GroupBy(x => x.SpecialAbility).Count());
        }
    }
}
