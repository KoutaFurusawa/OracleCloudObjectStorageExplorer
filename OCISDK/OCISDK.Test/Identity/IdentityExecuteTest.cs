using System;
using OCISDK.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OCISDK.Identity.Request;

namespace OCISDK.Identity.Test
{
    [TestClass]
    public class IdentityExecuteTest
    {
        private readonly string TenantOCID = OCISDK.Test.Properties.Resources.tenancyOCID;

        private readonly string UserOCID = OCISDK.Test.Properties.Resources.userOCID;

        private readonly string Fingerprint = OCISDK.Test.Properties.Resources.fingerprint;

        private readonly string KeyFilePath = OCISDK.Test.Properties.Resources.key_file_path;

        private readonly string PassPhrase = OCISDK.Test.Properties.Resources.pass_phrase;

        private readonly string Region = OCISDK.Test.Properties.Resources.region;

        private IdentityClient CreateClient()
        {
            ClientConfig clientConfig = new ClientConfig
            {
                TenancyId = TenantOCID,
                UserId = UserOCID,
                Fingerprint = Fingerprint,
                PrivateKey = KeyFilePath,
                PrivateKeyPassphrase = PassPhrase
            };
            IdentityClient identityClient = new IdentityClient(clientConfig)
            {
                Region = Region
            };

            return identityClient;
        }

        [TestMethod]
        public void テナント取得()
        {
            var client = CreateClient();

            GetTenancyRequest getTenancyRequest = new GetTenancyRequest
            { 
                 TenancyId = TenantOCID
            };
            var tenant = client.GetTenancy(getTenancyRequest);

            Assert.AreEqual(tenant.Tenancy.Id, TenantOCID);
        }

        [TestMethod]
        public void コンパートメント一覧取得()
        {
            var client = CreateClient();

            ListCompartmentRequest listCompartmentRequest = new ListCompartmentRequest
            {
                CompartmentId = TenantOCID,
                AccessLevel = ListCompartmentRequest.AccessLevels.ACCESSIBLE,
                CompartmentIdInSubtree = true
            };
            var tenant = client.ListCompartment(listCompartmentRequest);

            Assert.IsTrue(tenant.Items.Count > 0);
        }
    }
}
