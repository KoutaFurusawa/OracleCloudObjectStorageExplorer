using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OCISDK.ObjectStorage;
using OCISDK.ObjectStorage.Request;
using OCISDK.ObjectStorage.Response;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace OCISDK.Test.ObjectStorage
{
    [TestClass]
    public class ObjectStorageExecuteTest
    {
        private readonly static string TenantOCID = OCISDK.Test.Properties.Resources.tenancyOCID;

        private readonly static string UserOCID = OCISDK.Test.Properties.Resources.userOCID;

        private readonly static string Fingerprint = OCISDK.Test.Properties.Resources.fingerprint;

        private readonly static string KeyFilePath = OCISDK.Test.Properties.Resources.key_file_path;

        private readonly static string PassPhrase = OCISDK.Test.Properties.Resources.pass_phrase;

        private readonly static string Region = OCISDK.Test.Properties.Resources.region;

        private readonly static string TargetCompartmentOCID = OCISDK.Test.Properties.Resources.targetCompartmentOCID;

        private readonly static string TestBucketName = "ObjectStorageExecuteTestBucket";

        private readonly static string TestUploadImageFile = "ObjectStoragePutTestImage.png";

        private readonly static string[] InitializeTestFiles = new string[] { "testA1.txt", "testB.txt", "testC.txt", "testD.txt" };

        private readonly static string[] DeleteTestFiles = new string[] { "testA2.txt", "testE.txt", "testF.txt" };

        private static string NameSpaceName = "";

        private static IObjectStorageClient ObjectStorageClient;

        public ObjectStorageExecuteTest()
        {
        }

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
            var signer = new ThreadSafeSigner(new OciSigner(clientConfig));

            ObjectStorageClient objectStorageClient = new ObjectStorageClient(clientConfig, signer)
            {
                Region = Region
            };
            ObjectStorageClient = objectStorageClient;

            NameSpaceName = ObjectStorageClient.GetNamespace(new GetNamespaceRequest());
        }

        [TestInitialize]
        public void TestInitialize()
        {
            // テスト用バケットを作成
            CreateBucketRequest createBucketRequest = new CreateBucketRequest
            {
                NamespaceName = NameSpaceName,
                CreateBucketDetails = new OCISDK.ObjectStorage.Model.CreateBucketDetails
                {
                    CompartmentId = TargetCompartmentOCID,
                    Name = TestBucketName
                }
            };

            try
            {
                ObjectStorageClient.CreateBucket(createBucketRequest);
            }
            catch (Exception e)
            {
                Trace.WriteLine($"create bucket failed. message:{e.Message}");
            }

            // あらかじめファイルを登録しておく
            var assembly = Assembly.GetExecutingAssembly();
            PutObjectResponse updateRes;
            PutObjectRequest putObjectRequest = new PutObjectRequest
            {
                NamespaceName = NameSpaceName,
                BucketName = TestBucketName
            };
            for (var i = 0; i < InitializeTestFiles.Length; i++)
            {
                putObjectRequest.ObjectName = InitializeTestFiles[i];
                using (var stream = assembly.GetManifestResourceStream("OCISDK.Test.Properties." + InitializeTestFiles[i]))
                {
                    putObjectRequest.UploadPartBody = stream;
                    updateRes = ObjectStorageClient.PutObject(putObjectRequest);
                }
            }

            // 削除確認用
            for (var i = 0; i < DeleteTestFiles.Length; i++)
            {
                putObjectRequest.ObjectName = DeleteTestFiles[i];
                using (var stream = assembly.GetManifestResourceStream("OCISDK.Test.Properties." + DeleteTestFiles[i]))
                {
                    putObjectRequest.UploadPartBody = stream;
                    updateRes = ObjectStorageClient.PutObject(putObjectRequest);
                }
            }
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            try
            {
                ListObjectsRequest listObjectsRequest = new ListObjectsRequest
                {
                    NamespaceName = NameSpaceName,
                    BucketName = TestBucketName
                };
                var listObj = ObjectStorageClient.ListObjects(listObjectsRequest);

                foreach (var obj in listObj.ListObjects.Objects)
                {
                    // アップしたファイルを削除
                    DeleteObjectRequest deleteObjectRequest = new DeleteObjectRequest
                    {
                        NamespaceName = NameSpaceName,
                        BucketName = TestBucketName,
                        ObjectName = obj.Name
                    };

                    var res = ObjectStorageClient.DeleteObject(deleteObjectRequest);
                }
            }
            catch
            {
                Trace.WriteLine("ClassCleanup. test file not found.");
            }

            try
            {
                // テスト用バケットを削除
                DeleteBucketRequest deleteBucketRequest = new DeleteBucketRequest
                {
                    NamespaceName = NameSpaceName,
                    BucketName = TestBucketName
                };

                var b = ObjectStorageClient.DeleteBucket(deleteBucketRequest);
            }
            catch
            {
                Trace.WriteLine("ClassCleanup. test bucket not found.");
            }
        }

        [TestMethod]
        public void ルートコンパートメント直下バケット一覧取得()
        {
            ListBucketsRequest listBucketsRequest = new ListBucketsRequest
            {
                CompartmentId = TenantOCID,
                NamespaceName = NameSpaceName
            };

            var listBucket = ObjectStorageClient.ListBuckets(listBucketsRequest);

            Assert.IsNotNull(listBucket);
        }

        [TestMethod]
        public void 特定のバケット情報のみ取得()
        {
            GetBucketRequest getBucketRequest = new GetBucketRequest
            {
                NamespaceName = NameSpaceName,
                BucketName = TestBucketName
            };

            var bucketDetails = ObjectStorageClient.GetBucket(getBucketRequest);

            Assert.IsNotNull(bucketDetails.Bucket);
            Assert.IsTrue(bucketDetails.Bucket.Name == TestBucketName);
            Assert.IsNotNull(bucketDetails.Bucket.TimeCreated);
        }

        [TestMethod]
        public void 特定のバケットのオブジェクト一覧を取得し全ファイルが取得できることを確認する()
        {
            // ページングするように設定
            ListObjectsRequest listObjectsRequest = new ListObjectsRequest
            {
                NamespaceName = NameSpaceName,
                BucketName = TestBucketName,
                Limit = 1
            };

            var count = 0;
            while(true)
            { 
                var listObjs = ObjectStorageClient.ListObjects(listObjectsRequest);

                foreach (var obj in listObjs.ListObjects.Objects)
                {
                    if (InitializeTestFiles.Contains(obj.Name))
                    {
                        count++;
                    }
                }

                if (string.IsNullOrEmpty(listObjs.ListObjects.NextStartWith))
                {
                    break;
                }
                else
                {
                    listObjectsRequest.Start = listObjs.ListObjects.NextStartWith;
                }
            }

            Assert.IsTrue(count == 4);
        }

        [TestMethod]
        public void 特定のバケットにオブジェクトをアップする()
        {
            var resourceName = "OCISDK.Test.Properties.mokuroku.csv.gz";
            var assembly = Assembly.GetExecutingAssembly();
            PutObjectResponse updateRes;
            PutObjectRequest putObjectRequest = new PutObjectRequest
            {
                NamespaceName = NameSpaceName,
                BucketName = "bigfile_bucket",
                ObjectName = "test/mokuroku.csv.gz"
            };
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                putObjectRequest.UploadPartBody = stream;
                updateRes = ObjectStorageClient.PutObject(putObjectRequest);
            }
            Assert.IsNotNull(updateRes.LastModified);
        }

        [TestMethod]
        public void 特定のバケットにオブジェクトをダウンロードする()
        {
            var assembly = Assembly.GetExecutingAssembly();
            GetObjectRequest getObjectRequest = new GetObjectRequest
            {
                NamespaceName = NameSpaceName,
                BucketName = "bigfile_bucket",
                ObjectName = "test.csv.gz"
            };
            ObjectStorageClient.DownloadObject(getObjectRequest, @"C:\test.csv.gz");
        }

        [TestMethod]
        public void 特定のバケットに同名のオブジェクトをアップしエラーにならないことを確認する()
        {
            var resourceName = "OCISDK.Test.Properties.ObjectStoragePutTestImage.png";
            var assembly = Assembly.GetExecutingAssembly();
            PutObjectResponse updateRes;
            PutObjectRequest putObjectRequest = new PutObjectRequest
            {
                NamespaceName = NameSpaceName,
                BucketName = TestBucketName,
                ObjectName = TestUploadImageFile
            };

            // first
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                putObjectRequest.UploadPartBody = stream;
                updateRes = ObjectStorageClient.PutObject(putObjectRequest);
            }

            // secound
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                putObjectRequest.UploadPartBody = stream;
                updateRes = ObjectStorageClient.PutObject(putObjectRequest);
            }

            Assert.IsNotNull(updateRes.LastModified);
        }

        [TestMethod]
        public void 特定のバケットに2つオブジェクトをアップする()
        {
            var resourceName = "OCISDK.Test.Properties.ObjectStoragePutTestImage.png";
            var assembly = Assembly.GetExecutingAssembly();
            PutObjectResponse updateRes;
            PutObjectRequest putObjectRequest = new PutObjectRequest
            {
                NamespaceName = NameSpaceName,
                BucketName = TestBucketName
            };

            // first
            putObjectRequest.ObjectName = TestUploadImageFile;
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                putObjectRequest.UploadPartBody = stream;
                updateRes = ObjectStorageClient.PutObject(putObjectRequest);
            }

            Assert.IsNotNull(updateRes.LastModified);

            // secound
            putObjectRequest.ObjectName = TestUploadImageFile + "2";
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                putObjectRequest.UploadPartBody = stream;
                updateRes = ObjectStorageClient.PutObject(putObjectRequest);
            }

            Assert.IsNotNull(updateRes.LastModified);
        }

        [TestMethod]
        public void 特定のバケットの特定のオブジェクトが存在するか確認する()
        {
            GetObjectRequest getObjectRequest = new GetObjectRequest
            { 
                NamespaceName = NameSpaceName,
                BucketName = TestBucketName,
                ObjectName = InitializeTestFiles[0]
            };
            var res = ObjectStorageClient.GetObject(getObjectRequest);

            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void オブジェクトが最低限必要となる情報を持つか確認する()
        {
            GetObjectRequest getObjectRequest = new GetObjectRequest
            {
                NamespaceName = NameSpaceName,
                BucketName = TestBucketName,
                ObjectName = InitializeTestFiles[1]
            };
            var res = ObjectStorageClient.GetObject(getObjectRequest);

            Assert.IsNotNull(res);
            Assert.IsFalse(string.IsNullOrEmpty(res.ContentMd5));
            Assert.IsFalse(string.IsNullOrEmpty(res.ETag));
            Assert.IsFalse(string.IsNullOrEmpty(res.ContentType));
            Assert.IsTrue(res.ContentLength.HasValue);
        }


        [TestMethod]
        public void 特定のバケットの特定のファイルを削除する()
        {
            DeleteObjectRequest deleteObjectRequest = new DeleteObjectRequest
            {
                NamespaceName = NameSpaceName,
                BucketName = TestBucketName,
                ObjectName = InitializeTestFiles[1]
            };

            var res = ObjectStorageClient.DeleteObject(deleteObjectRequest);

            Assert.IsNotNull(res.LastModified);
        }

        [TestMethod]
        public void 特定のバケット内の複数のファイルを削除する()
        {
            DeleteObjectsRequest deleteObjectsRequest = new DeleteObjectsRequest
            {
                NamespaceName = NameSpaceName,
                BucketName = TestBucketName,
                Objects = new List<TargetDelete>()
            };

            for (var i = 0; i < DeleteTestFiles.Length; i++)
            {
                deleteObjectsRequest .Objects.Add(new TargetDelete { ObjectName = DeleteTestFiles[i] });
            }

            var res = ObjectStorageClient.DeleteObjects(deleteObjectsRequest);

            Assert.IsNotNull(res.DeletedObjects);

            foreach (var obj in res.DeletedObjects)
            {
                Assert.AreEqual(obj.Code, 204);
                Assert.IsNotNull(obj.Name);
            }
        }

        [TestMethod]
        public void 特定のバケット内の複数のファイル削除時に不明のファイルを指定し失敗する()
        {
            DeleteObjectsRequest deleteObjectsRequest = new DeleteObjectsRequest
            {
                NamespaceName = NameSpaceName,
                BucketName = TestBucketName,
                Objects = new List<TargetDelete>()
            };

            for (var i = 0; i < DeleteTestFiles.Length; i++)
            {
                deleteObjectsRequest.Objects.Add(new TargetDelete { ObjectName = DeleteTestFiles[i]+".Error" });
            }

            var res = ObjectStorageClient.DeleteObjects(deleteObjectsRequest);

            Assert.IsNotNull(res.DeletedObjects);

            foreach (var obj in res.DeletedObjects)
            {
                Assert.AreEqual(obj.Code, 404);
                Assert.IsNotNull(obj.Name);
            }
        }
    }
}
