using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OCISDK.ObjectStorage;
using OCISDK.ObjectStorage.Request;
using System.Diagnostics;
using OCISDK.ObjectStorage.Response;
using System.Reflection;
using OCISDK.ObjectStorage.IO;
using OCISDK.ObjectStorage.Model;
using System.Linq;

namespace OCISDK.Test.ObjectStorage.IO
{
    /// <summary>
    /// ObjectStorageDirectoryInfoの機能をテストする
    /// </summary>
    [TestClass]
    public class ObjectStorageDirectoryInfoTest
    {
        private readonly static string TenantOCID = OCISDK.Test.Properties.Resources.tenancyOCID;

        private readonly static string UserOCID = OCISDK.Test.Properties.Resources.userOCID;

        private readonly static string Fingerprint = OCISDK.Test.Properties.Resources.fingerprint;

        private readonly static string KeyFilePath = OCISDK.Test.Properties.Resources.key_file_path;

        private readonly static string PassPhrase = OCISDK.Test.Properties.Resources.pass_phrase;

        private readonly static string Region = OCISDK.Test.Properties.Resources.region;

        private readonly static string TargetCompartmentOCID = OCISDK.Test.Properties.Resources.targetCompartmentOCID;

        private readonly static string TestBucketName = "ObjectStorageDirectoryInfoTestBucket";
        
        private readonly static IDictionary<string, List<string>> TestFileInfos = new Dictionary<string, List<string>> {
            { "",                       new List<string>{ "testA1.txt", "testA2.txt" } }, 
            { "layer1",                 new List<string>{ "testB.txt" } }, 
            { "layer1/layer2-1",        new List<string>{ "testC.txt" } }, 
            { "layer1/layer2-2",        new List<string>{ "testD.txt" } },
            { "layer1/layer2-1/layer3", new List<string>{ "testE.txt" } },
            { "layer1/layer2-2/layer3", new List<string>{ "testF.txt" } },
            { "layer1_test",                 new List<string>{ "testG.txt" } },
            { "layer1_test/layer_test2-1",        new List<string>{ "testH.txt" } },
            { "layer1_test/layer_test2-2",        new List<string>{ "testI.txt" } },
            { "layer1_test/layer_test2-1/layer_test3", new List<string>{ "testJ.txt" } },
            { "layer1_test/layer_test2-2/layer_test3", new List<string>{ "testK.txt" } }
        };

        private static List<string> TestFileRemotePaths = new List<string>();

        private static string NameSpaceName = "";

        private static IObjectStorageClient ObjectStorageClient;
        
        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            foreach (var dir in TestFileInfos.Keys)
            {
                foreach (var fileName in TestFileInfos[dir]) {
                    if (string.IsNullOrEmpty(dir))
                    {
                        TestFileRemotePaths.Add(fileName);
                    }
                    else { 
                        TestFileRemotePaths.Add($"{dir}/{fileName}");
                    }
                }
            }

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
            // テスト用バケットの作成
            CreateBucketRequest createBucketRequest = new CreateBucketRequest
            {
                NamespaceName = NameSpaceName,
                CreateBucketDetails = new OCISDK.ObjectStorage.Model.CreateBucketDetails
                {
                    Name = TestBucketName,
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

            PutObjectRequest putObjectRequest = new PutObjectRequest
            {
                NamespaceName = NameSpaceName,
                BucketName = TestBucketName
            };

            foreach (var testFile in TestFileInfos)
            {
                foreach (var fileName in testFile.Value)
                {
                    if (string.IsNullOrEmpty(testFile.Key))
                    {
                        putObjectRequest.ObjectName = fileName;
                    }
                    else
                    {
                        putObjectRequest.ObjectName = $"{testFile.Key}/{fileName}";
                    }
                    var resourceName = $"OCISDK.Test.Properties.{fileName}";
                    var assembly = Assembly.GetExecutingAssembly();
                    PutObjectResponse updateRes;
                    using (var stream = assembly.GetManifestResourceStream(resourceName))
                    {
                        putObjectRequest.UploadPartBody = stream;
                        updateRes = ObjectStorageClient.PutObject(putObjectRequest);
                    }
                }
            }
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            // アップしたファイルを削除
            foreach (var testFile in TestFileInfos)
            {
                DeleteObjectRequest deleteObjectRequest = new DeleteObjectRequest
                {
                    NamespaceName = NameSpaceName,
                    BucketName = TestBucketName
                };

                foreach (var fileName in testFile.Value)
                {
                    if (string.IsNullOrEmpty(testFile.Key))
                    {
                        deleteObjectRequest.ObjectName = fileName;
                    }
                    else
                    {
                        deleteObjectRequest.ObjectName = $"{testFile.Key}/{fileName}";
                    }

                    try
                    {
                        var res = ObjectStorageClient.DeleteObject(deleteObjectRequest);
                    }
                    catch
                    {
                        Trace.WriteLine("test file not found");
                    }
                }
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
                Trace.WriteLine("test bucket not found");
            }
        }

        [TestMethod]
        public void BucketExists()
        {
            ObjectStorageDirectoryInfo objectStorageDirectoryInfo = new ObjectStorageDirectoryInfo(ObjectStorageClient, NameSpaceName, TestBucketName);

            Assert.IsTrue(objectStorageDirectoryInfo.Exists);
        }

        [TestMethod]
        public void BucketExistsKey()
        {
            ObjectStorageDirectoryInfo objectStorageDirectoryInfo = new ObjectStorageDirectoryInfo(ObjectStorageClient, NameSpaceName, TestBucketName, TestFileRemotePaths[0]);

            Assert.IsTrue(objectStorageDirectoryInfo.Exists);
        }

        [TestMethod]
        public void BucketExistsDir()
        {
            ObjectStorageDirectoryInfo objectStorageDirectoryInfo = new ObjectStorageDirectoryInfo(ObjectStorageClient, NameSpaceName, TestBucketName, "layer1");

            Assert.IsTrue(objectStorageDirectoryInfo.Exists);
        }

        [TestMethod]
        public void BucketExistsDirTopSlash()
        {
            ObjectStorageDirectoryInfo objectStorageDirectoryInfo = new ObjectStorageDirectoryInfo(ObjectStorageClient, NameSpaceName, TestBucketName, "/layer1");

            Assert.IsTrue(objectStorageDirectoryInfo.Exists);
        }

        [TestMethod]
        public void BucketNotExists()
        {
            ObjectStorageDirectoryInfo objectStorageDirectoryInfo = new ObjectStorageDirectoryInfo(ObjectStorageClient, NameSpaceName, "abcdefgTest");

            Assert.IsFalse(objectStorageDirectoryInfo.Exists);
        }

        [TestMethod]
        public void バケットのルート位置にあるファイル情報を取得する()
        {
            ObjectStorageDirectoryInfo objectStorageDirectoryInfo = new ObjectStorageDirectoryInfo(ObjectStorageClient, NameSpaceName, TestBucketName);

            var fileInfos = objectStorageDirectoryInfo.EnumerateFiles("*", System.IO.SearchOption.TopDirectoryOnly);

            Assert.IsNotNull(fileInfos);
            var dir = TestFileInfos[""];
            foreach (var fileInfo in fileInfos)
            {
                var check = dir.SingleOrDefault(f => f == fileInfo.Name);
                Assert.IsNotNull(check);
;           }
        }

        [TestMethod]
        public void バケットの特定のディレクトリ直下にあるファイル情報のみを取得する()
        {
            var targetDir = TestFileInfos.Keys.ToArray()[1];
            ObjectStorageDirectoryInfo objectStorageDirectoryInfo = new ObjectStorageDirectoryInfo(ObjectStorageClient, NameSpaceName, TestBucketName, targetDir);

            var fileInfos = objectStorageDirectoryInfo.EnumerateFiles("*", System.IO.SearchOption.TopDirectoryOnly);

            Assert.IsNotNull(fileInfos);
            var dir = TestFileInfos[targetDir];
            foreach (var fileInfo in fileInfos)
            {
                var check = dir.SingleOrDefault(f => f == fileInfo.Name);
                Assert.IsNotNull(check);
            }
        }

        [TestMethod]
        public void バケットにあるすべてのファイル情報を取得する()
        {
            ObjectStorageDirectoryInfo objectStorageDirectoryInfo = new ObjectStorageDirectoryInfo(ObjectStorageClient, NameSpaceName, TestBucketName);

            var fileInfos = objectStorageDirectoryInfo.EnumerateFiles("*", System.IO.SearchOption.AllDirectories);

            Assert.IsNotNull(fileInfos);

            foreach (var path in TestFileRemotePaths)
            {
                var check = fileInfos.SingleOrDefault(f => f.OriginalKey == path + "/");
            }
        }

        [TestMethod]
        public void バケット内のルート直下のディレクトリを得る()
        {
            ObjectStorageDirectoryInfo objectStorageDirectoryInfo = new ObjectStorageDirectoryInfo(ObjectStorageClient, NameSpaceName, TestBucketName);

            var dirs = objectStorageDirectoryInfo.EnumerateDirectories("*", System.IO.SearchOption.TopDirectoryOnly);

            Assert.IsNotNull(dirs);
            Assert.IsTrue(dirs.Count() == 2);
        }

        [TestMethod]
        public void バケット内のすべてのディレクトリを得る()
        {
            ObjectStorageDirectoryInfo objectStorageDirectoryInfo = new ObjectStorageDirectoryInfo(ObjectStorageClient, NameSpaceName, TestBucketName);

            var dirs = objectStorageDirectoryInfo.EnumerateDirectories("*", System.IO.SearchOption.AllDirectories);

            Assert.IsNotNull(dirs);
            foreach (var checkDir in TestFileInfos.Keys.Where(k => !string.IsNullOrEmpty(k)))
            {
                Assert.IsTrue(dirs.Any(d => d.OriginalKey == checkDir+"/"));
            }
        }

        [TestMethod]
        public void バケット内の第2階層以下のディレクトリをすべて得る()
        {
            ObjectStorageDirectoryInfo objectStorageDirectoryInfo = new ObjectStorageDirectoryInfo(ObjectStorageClient, NameSpaceName, TestBucketName, TestFileInfos.Keys.ToArray()[1]);

            var dirs = objectStorageDirectoryInfo.EnumerateDirectories("*", System.IO.SearchOption.AllDirectories);

            Assert.IsNotNull(dirs);
            foreach (var checkDir in TestFileInfos.Keys.Where(k => !string.IsNullOrEmpty(k) && k.Contains(TestFileInfos.Keys.ToArray()[1]+"/")))
            {
                Assert.IsTrue(dirs.Any(d => d.OriginalKey == checkDir+"/"));
            }
        }

        [TestMethod]
        public void バケット内の第2階層以下のディレクトリをすべて得る_先頭スラッシュ()
        {
            ObjectStorageDirectoryInfo objectStorageDirectoryInfo = new ObjectStorageDirectoryInfo(ObjectStorageClient, NameSpaceName, TestBucketName, "/" + TestFileInfos.Keys.ToArray()[1]);

            var dirs = objectStorageDirectoryInfo.EnumerateDirectories("*", System.IO.SearchOption.AllDirectories);

            Assert.IsNotNull(dirs);
            foreach (var checkDir in TestFileInfos.Keys.Where(k => !string.IsNullOrEmpty(k) && k.Contains(TestFileInfos.Keys.ToArray()[1] + "/")))
            {
                Assert.IsTrue(dirs.Any(d => d.OriginalKey == checkDir + "/"));
            }
        }

        [TestMethod]
        public void バケット内の第2階層以下のファイルをすべて得る()
        {
            ObjectStorageDirectoryInfo objectStorageDirectoryInfo = new ObjectStorageDirectoryInfo(ObjectStorageClient, NameSpaceName, TestBucketName, TestFileInfos.Keys.ToArray()[1]);

            var files = objectStorageDirectoryInfo.EnumerateFiles("*", System.IO.SearchOption.AllDirectories);

            Assert.IsNotNull(files);
        }

        [TestMethod]
        public void バケット内の特定階層のファイルのみ得る()
        {
            ObjectStorageDirectoryInfo objectStorageDirectoryInfo = new ObjectStorageDirectoryInfo(ObjectStorageClient, NameSpaceName, TestBucketName, TestFileInfos.Keys.ToArray()[4]);

            var files = objectStorageDirectoryInfo.EnumerateFiles("*", System.IO.SearchOption.TopDirectoryOnly);

            Assert.IsNotNull(files);
            foreach (var checks in TestFileInfos[TestFileInfos.Keys.ToArray()[4]])
            {
                Assert.IsTrue(files.Any(d => d.Name == checks));
            }
        }

        [TestMethod]
        public void バケット内の特定階層のファイルのみ得る_先頭スッシュ()
        {
            ObjectStorageDirectoryInfo objectStorageDirectoryInfo = new ObjectStorageDirectoryInfo(ObjectStorageClient, NameSpaceName, TestBucketName, "/" + TestFileInfos.Keys.ToArray()[4]);

            var files = objectStorageDirectoryInfo.EnumerateFiles("*", System.IO.SearchOption.TopDirectoryOnly);

            Assert.IsNotNull(files);
            foreach (var checks in TestFileInfos[TestFileInfos.Keys.ToArray()[4]])
            {
                Assert.IsTrue(files.Any(d => d.Name == checks));
            }
        }

        [TestMethod]
        public void バケット内の特定階層のファイルのみ得るケース2()
        {
            ObjectStorageDirectoryInfo objectStorageDirectoryInfo = new ObjectStorageDirectoryInfo(ObjectStorageClient, NameSpaceName, TestBucketName, TestFileInfos.Keys.ToArray()[5]);

            var files = objectStorageDirectoryInfo.EnumerateFiles("*", System.IO.SearchOption.TopDirectoryOnly);

            Assert.IsNotNull(files);
            foreach (var checks in TestFileInfos[TestFileInfos.Keys.ToArray()[5]])
            {
                Assert.IsTrue(files.Any(d => d.Name == checks));
            }
        }

        [TestMethod]
        public void バケット内の特定階層のファイルのみ得るケース2_先頭スラッシュ()
        {
            ObjectStorageDirectoryInfo objectStorageDirectoryInfo = new ObjectStorageDirectoryInfo(ObjectStorageClient, NameSpaceName, TestBucketName, "/" + TestFileInfos.Keys.ToArray()[5]);

            var files = objectStorageDirectoryInfo.EnumerateFiles("*", System.IO.SearchOption.TopDirectoryOnly);

            Assert.IsNotNull(files);
            foreach (var checks in TestFileInfos[TestFileInfos.Keys.ToArray()[5]])
            {
                Assert.IsTrue(files.Any(d => d.Name == checks));
            }
        }
        [TestMethod]
        public void バケット内のファイルをすべて削除する()
        {
            ObjectStorageDirectoryInfo objectStorageDirectoryInfo = new ObjectStorageDirectoryInfo(ObjectStorageClient, NameSpaceName, TestBucketName);

            var fileInfos = objectStorageDirectoryInfo.EnumerateFiles("*", System.IO.SearchOption.AllDirectories);

            Assert.IsNotNull(fileInfos);

            foreach (var fileInfo in fileInfos)
            {
                fileInfo.Delete();
            }

            fileInfos = objectStorageDirectoryInfo.EnumerateFiles("*", System.IO.SearchOption.AllDirectories);
            Assert.IsTrue(fileInfos.Count() == 0);
        }

        [TestMethod]
        public void バケット内の第2階層のファイルのみを削除する()
        {
            ObjectStorageDirectoryInfo objectStorageDirectoryInfo = new ObjectStorageDirectoryInfo(ObjectStorageClient, NameSpaceName, TestBucketName, TestFileInfos.Keys.ToArray()[2]);

            var fileInfos = objectStorageDirectoryInfo.EnumerateFiles("*", System.IO.SearchOption.TopDirectoryOnly);

            Assert.IsNotNull(fileInfos);

            foreach (var fileInfo in fileInfos)
            {
                fileInfo.Delete();
            }

            fileInfos = objectStorageDirectoryInfo.EnumerateFiles("*", System.IO.SearchOption.TopDirectoryOnly);
            Assert.IsTrue(fileInfos.Count() == 0);
        }

        [TestMethod]
        public void バケット内の特定のディレクトリをすべて削除する()
        {
            var targetDir = TestFileInfos.Keys.ToArray()[2];
            ObjectStorageDirectoryInfo objectStorageDirectoryInfo = new ObjectStorageDirectoryInfo(ObjectStorageClient, NameSpaceName, TestBucketName, targetDir);

            objectStorageDirectoryInfo.Delete(true);

            var dirs = objectStorageDirectoryInfo.EnumerateDirectories("*", System.IO.SearchOption.TopDirectoryOnly);

            Assert.IsNotNull(dirs);
            Assert.IsTrue(dirs.Count() == 0);
        }


        [TestMethod]
        public void バケット内の特定のディレクトリを削除時にサブディレクトリがあるため削除しない()
        {
            var targetDir = TestFileInfos.Keys.ToArray()[2];
            ObjectStorageDirectoryInfo objectStorageDirectoryInfo = new ObjectStorageDirectoryInfo(ObjectStorageClient, NameSpaceName, TestBucketName, targetDir);

            objectStorageDirectoryInfo.Delete(false);

            var dirs = objectStorageDirectoryInfo.EnumerateDirectories("*", System.IO.SearchOption.TopDirectoryOnly);

            Assert.IsNotNull(dirs);
            Assert.IsTrue(dirs.Count() != 0);
        }
    }
}
