using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OCISDK.Common;

namespace OCISDK.Test.Common
{
    [TestClass]
    public class ExpandableEnumTest
    {
        /// <summary>
        /// Test ExpandableEnum
        /// </summary>
        public class TestEnum : ExpandableEnum<TestEnum>
        {
            public TestEnum(string value) : base(value) { }

            public static implicit operator TestEnum(string value)
            {
                return Parse(value);
            }

            public static readonly TestEnum ParamA = new TestEnum("ParamA");

            public static readonly TestEnum ParamB = new TestEnum("ParamB");

            public static readonly TestEnum ParamC = new TestEnum("ParamC");
        }

        [TestMethod]
        public void Equal_ParamA()
        {
            var param = TestEnum.ParamA;

            Assert.AreEqual("ParamA", param.Value);
        }

        [TestMethod]
        public void Equal_ParamB()
        {
            var param = TestEnum.ParamB;

            Assert.AreEqual("ParamB", param.Value);
        }

        [TestMethod]
        public void NotEqual()
        {
            var param = TestEnum.ParamC;

            Assert.AreNotEqual("ParamA", param.Value);
        }

        [TestMethod]
        public void Equal_ToString()
        {
            var param = TestEnum.ParamA;

            Assert.AreEqual("ParamA", param.ToString());
        }

        [TestMethod]
        public void CompareTo_Equivalent()
        {
            var param = TestEnum.ParamA;

            var res = param.CompareTo(TestEnum.ParamA);

            Assert.AreEqual(0, res);
        }

        [TestMethod]
        public void CompareTo_TargetToFront()
        {
            var param = TestEnum.ParamB;

            var res = param.CompareTo(TestEnum.ParamA);

            Assert.AreEqual(1, res);
        }

        [TestMethod]
        public void CompareTo_TargetToBack()
        {
            var param = TestEnum.ParamB;

            var res = param.CompareTo(TestEnum.ParamC);

            Assert.AreEqual(-1, res);
        }

        [TestMethod]
        public void Equals_True()
        {
            var param = TestEnum.ParamA;

            var res = param.Equals(TestEnum.ParamA);

            Assert.IsTrue(res);
        }

        [TestMethod]
        public void Equals_False()
        {
            var param = TestEnum.ParamA;

            var res = param.Equals(TestEnum.ParamB);

            Assert.IsFalse(res);
        }

        [TestMethod]
        public void Equal_Null()
        {
            var param = TestEnum.ParamA;

            var res = param.Equals(null);

            Assert.IsFalse(res);
        }

        [TestMethod]
        public void OperatorEqual_True()
        {
            var param = TestEnum.ParamA;

            var res = TestEnum.ParamA == param.Value;

            Assert.IsTrue(res);
        }

        [TestMethod]
        public void OperatorNotEqual_True()
        {
            var param = TestEnum.ParamA;

            var res = TestEnum.ParamB != param.Value;

            Assert.IsTrue(res);
        }

        [TestMethod]
        public void OperatorNotEqual_False()
        {
            var param = TestEnum.ParamA;

            var res = TestEnum.ParamA != param.Value;

            Assert.IsFalse(res);
        }

        [TestMethod]
        public void OperatorEqual_ValueToParam_True()
        {
            var param = TestEnum.ParamA;

            var res = param.Value == TestEnum.ParamA;

            Assert.IsTrue(res);
        }

        [TestMethod]
        public void OperatorNotEqual_ValueToParam_True()
        {
            var param = TestEnum.ParamA;

            var res = param.Value != TestEnum.ParamB;

            Assert.IsTrue(res);
        }

        [TestMethod]
        public void OperatorNotEqual_ValueToParam_False()
        {
            var param = TestEnum.ParamA;

            var res = param.Value != TestEnum.ParamA;

            Assert.IsFalse(res);
        }

        [TestMethod]
        public void Parse_True()
        {
            var res = TestEnum.Parse("ParamA");

            Assert.AreEqual(TestEnum.ParamA, res);
        }

        [TestMethod]
        public void Parse_Exception()
        {
            Assert.ThrowsException<InvalidCastException>(()=> TestEnum.Parse("ParamD"));
        }
    }
}