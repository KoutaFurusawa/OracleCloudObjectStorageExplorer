using OCISDK.Common;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OCISDK.Test.Common
{
    [TestClass]
    public class EnumParamTest
    {
        [TestMethod]
        public void OrderByDisplaynameEqualASC()
        {
            var getDisplayname = SortOrder.ASC.Value;

            Assert.AreEqual("ASC", getDisplayname);
        }

        [TestMethod]
        public void OrderByDisplaynameEqualDESC()
        {
            var getDisplayname = SortOrder.DESC.Value;

            Assert.AreEqual("DESC", getDisplayname);
        }
    }
}
