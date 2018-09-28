using Archspace2.Battle;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Archspace2
{
    [TestClass]
    [TestCategory("Unit")]
    public class UnitTests
    {
        [TestMethod]
        public void MovingRightWorks()
        {
            Unit unit = new Unit(5000, 5000, 0);
            unit.Move(1);

            Assert.AreEqual(5000, unit.Y);
            Assert.IsTrue(unit.X > 5000);
        }

        [TestMethod]
        public void MovingLeftWorks()
        {
            Unit unit = new Unit(5000, 5000, 180);
            unit.Move(1);

            Assert.AreEqual(5000, unit.Y);
            Assert.IsTrue(unit.X < 5000);
        }

        [TestMethod]
        public void MovingUpWorks()
        {
            Unit unit = new Unit(5000, 5000, 90);
            unit.Move(1);

            Assert.AreEqual(5000, unit.X);
            Assert.IsTrue(unit.Y > 5000);
        }

        [TestMethod]
        public void MovingDownWorks()
        {
            Unit unit = new Unit(5000, 5000, 270);
            unit.Move(1);

            Assert.AreEqual(5000, unit.X);
            Assert.IsTrue(unit.Y < 5000);
        }

        [TestMethod]
        public void CanTurnTo()
        {
            Unit unit = new Unit(5000, 5000, 0);
            unit.TurnTo(new Unit(7500, 7500), 1);

            Assert.AreEqual(5000, unit.X);
            Assert.AreEqual(5000, unit.Y);
            Assert.IsTrue(unit.Direction > 0);
        }
    }
}
