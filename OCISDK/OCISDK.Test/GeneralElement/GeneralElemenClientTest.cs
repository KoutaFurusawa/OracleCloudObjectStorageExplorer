using Microsoft.VisualStudio.TestTools.UnitTesting;
using OCISDK.Common;
using OCISDK.GeneralElement;
using OCISDK.ObjectStorage;
using OCISDK.ObjectStorage.Request;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OCISDK.Test.GeneralElement
{
    [TestClass]
    public class GeneralElemenClientTest
    {
        private readonly static string TenantOCID = OCISDK.Test.Properties.Resources.tenancyOCID;

        private readonly static string UserOCID = OCISDK.Test.Properties.Resources.userOCID;

        private readonly static string Fingerprint = OCISDK.Test.Properties.Resources.fingerprint;

        private readonly static string KeyFilePath = OCISDK.Test.Properties.Resources.key_file_path;

        private readonly static string PassPhrase = OCISDK.Test.Properties.Resources.pass_phrase;

        private readonly static string Region = OCISDK.Test.Properties.Resources.region;

        private readonly static string TargetCompartmentOCID = OCISDK.Test.Properties.Resources.targetCompartmentOCID;

        private readonly static string TestBucketNameA = "ObjectStorageDirectoryInfoTestBucketA";

        private readonly static string TestBucketNameB = "ObjectStorageDirectoryInfoTestBucketB";

        private readonly static string TestBucketNameC = "ObjectStorageDirectoryInfoTestBucketC";

        private static IGeneralElemenClient GeneralElemenClient;

        private static string NameSpaceName = "";

        private static IObjectStorageClient ObjectStorageClient;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            ClientConfig clientConfig = new ClientConfig
            {
                TenancyId = TenantOCID,
                UserId = UserOCID,
                Fingerprint = Fingerprint,
                PrivateKey = KeyFilePath,
                Password = PassPhrase,
                PrivateKeyPassphrase = PassPhrase
            };
            GeneralElemenClient = new GeneralElemenClient(clientConfig);

            ObjectStorageClient = new ObjectStorageClient(clientConfig)
            {
                Region = Regions.US_ASHBURN_1
            };

            NameSpaceName = ObjectStorageClient.GetNamespace(new GetNamespaceRequest());

            // テスト用バケットの作成
            // Ashburnのみ
            CreateBucketRequest createBucketRequest = new CreateBucketRequest
            {
                NamespaceName = NameSpaceName,
                CreateBucketDetails = new OCISDK.ObjectStorage.Model.CreateBucketDetails
                {
                    Name = TestBucketNameA,
                    CompartmentId = TargetCompartmentOCID,
                }
            };
            try
            {
                ObjectStorageClient.CreateBucket(createBucketRequest);
            }
            catch
            {
                Trace.WriteLine("create failed test bucket");
            }

            // Tokyo, Osaka複数作成
            createBucketRequest.CreateBucketDetails.Name = TestBucketNameB;
            ObjectStorageClient.SetRegion(Regions.AP_TOKYO_1);
            try
            {
                ObjectStorageClient.CreateBucket(createBucketRequest);
            }
            catch
            {
                Trace.WriteLine("create failed test bucket. region=ap-tokyo-1");
            }

            ObjectStorageClient.SetRegion(Regions.AP_OSAKA_1);
            try
            {
                ObjectStorageClient.CreateBucket(createBucketRequest);
            }
            catch
            {
                Trace.WriteLine("create failed test bucket. region=ap-osaka-1");
            }
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            DeleteBucketRequest deleteBucketRequest = new DeleteBucketRequest
            {
                NamespaceName = NameSpaceName
            };
            try
            {
                // テスト用バケットを削除
                ObjectStorageClient.SetRegion(Regions.US_ASHBURN_1);
                deleteBucketRequest.BucketName = TestBucketNameA;
                var b = ObjectStorageClient.DeleteBucket(deleteBucketRequest);
            }
            catch
            {
                Trace.WriteLine("test bucket not found");
            }

            deleteBucketRequest.BucketName = TestBucketNameB;
            try
            {
                // テスト用バケットを削除
                ObjectStorageClient.SetRegion(Regions.AP_TOKYO_1);
                var b = ObjectStorageClient.DeleteBucket(deleteBucketRequest);
            }
            catch
            {
                Trace.WriteLine("test bucket not found");
            }

            try
            {
                // テスト用バケットを削除
                ObjectStorageClient.SetRegion(Regions.AP_OSAKA_1);
                var b = ObjectStorageClient.DeleteBucket(deleteBucketRequest);
            }
            catch
            {
                Trace.WriteLine("test bucket not found");
            }
        }

        [TestMethod]
        public void 単独のリージョン内にしかないバケットの場所を得る()
        {
            var locations = GeneralElemenClient.GetBucketLocation(new GetBucketLocationRequest { BucketName = TestBucketNameA });

            Assert.IsNotNull(locations);
            Assert.IsTrue(locations.Count == 1);
            Assert.AreEqual(locations[0], Regions.US_ASHBURN_1);
        }

        [TestMethod]
        public void 複数のリージョン内に同一の名前で存在するバケットの場所をすべて得る()
        {
            var locations = GeneralElemenClient.GetBucketLocation(new GetBucketLocationRequest { BucketName = TestBucketNameB });

            Trace.WriteLine(locations.Count);

            Assert.IsNotNull(locations);
            Assert.IsTrue(locations.Count == 2);

            var location = locations.Where(l => l == Regions.AP_TOKYO_1 || l == Regions.AP_OSAKA_1);

            Assert.IsNotNull(location);
        }

        [TestMethod]
        public void どのリージョン内にも存在しないバケットを指定する()
        {
            var e = Assert.ThrowsException<Exception>(() => GeneralElemenClient.GetBucketLocation(new GetBucketLocationRequest { BucketName = TestBucketNameC }));

            Assert.AreEqual(e.Message, "NoSuchBucket");
        }
    }
}
