using Microsoft.VisualStudio.TestTools.UnitTesting;
using Archspace2.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2
{
    [TestClass]
    [TestCategory("General")]
    public class GeneralTests
    {
        public enum TestEnum
        {
            Test = -3,
            Hello = 2,
            Cypherus = 4,
            Hi = -6
        };

        [TestMethod]
        public void EnumMinMaxGenerateCorrectValues()
        {
            int min = typeof(TestEnum).Min();
            TestEnum minEnum = typeof(TestEnum).Min<TestEnum>();

            Assert.AreEqual(-6, min);
            Assert.AreEqual(TestEnum.Hi, minEnum);

            int max = typeof(TestEnum).Max();
            TestEnum maxEnum = typeof(TestEnum).Max<TestEnum>();

            Assert.AreEqual(4, max);
            Assert.AreEqual(TestEnum.Cypherus, maxEnum);
        }

        [TestMethod]
        public void EnumModifyByIntIsCorrect()
        {
            TestEnum firstTest = TestEnum.Hello.ModifyByInt(2);

            Assert.AreEqual(TestEnum.Cypherus, firstTest);

            TestEnum secondTest = TestEnum.Hi.ModifyByInt(4);

            Assert.AreEqual(TestEnum.Test, secondTest);

            TestEnum thirdTest = TestEnum.Cypherus.ModifyByInt(-8);

            Assert.AreEqual(TestEnum.Test, thirdTest);
        }
    }
}
