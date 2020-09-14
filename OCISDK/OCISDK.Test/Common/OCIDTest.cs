using Microsoft.VisualStudio.TestTools.UnitTesting;
using OCISDK.Common;

namespace OCISDK.Test.Common
{
    [TestClass]
    public class OCIDTest
    {
        [TestMethod]
        public void ValidOcid()
        {
            Assert.IsTrue(OCID.IsValid(
                ""));
        }

        [TestMethod]
        public void ValidLegacyOcid()
        {
            Assert.IsTrue(OCID.IsValid(
                ""));
        }

        [TestMethod]
        [DataRow("ocid1.user.oc1.")]
        [DataRow("ocid1.user.oc1.adsfasfsafdf")]
        [DataRow("ocid1.user.oc1.1354aasdf.")]
        public void InvalidOcid(string ocid)
        {
            Assert.IsFalse(OCID.IsValid(ocid));
        }
    }
}
