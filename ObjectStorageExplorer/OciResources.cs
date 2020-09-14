using OCISDK;
using OCISDK.Identity;
using OCISDK.Identity.Request;
using OCISDK.Identity.Response;
using OCISDK.ObjectStorage;
using OCISDK.ObjectStorage.IO;
using OCISDK.ObjectStorage.Request;
using OCISDK.ObjectStorage.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectStorageExplorer
{
    public class OciResources : IOciResources
    {
        private OciSetting OciSetting;
        private IIdentityClient IdentityClient;
        private IObjectStorageClient ObjectStorageClient { get; set; }

        public OciResources()
        {
            SettingReset();
        }

        public IObjectStorageClient GetObjectStorageClient()
        {
            return ObjectStorageClient;
        }

        /// <summary>
        /// 接続設定リセット
        /// </summary>
        public void SettingReset()
        {
            OciSetting = new OciSetting
            {
                TenancyId = Properties.Settings.Default.TenancyId,
                UserId = Properties.Settings.Default.UserId,
                Fingerprint = Properties.Settings.Default.Fingerprint,
                KeyFilePath = Properties.Settings.Default.KeyFilePath,
                PassPhrase = Properties.Settings.Default.PassPhrase
            };
        }

        /// <summary>
        /// 設定バリデーション
        /// </summary>
        /// <returns></returns>
        public bool SettingValidation()
        {
            return OciSetting.Validation();
        }

        ClientConfig ClientConfig;
        /// <summary>
        /// クライアント作成
        /// </summary>
        public void CreateClient()
        {
            ClientConfig = new ClientConfig
            {
                TenancyId = OciSetting.TenancyId,
                UserId = OciSetting.UserId,
                Fingerprint = OciSetting.Fingerprint,
                PrivateKey = OciSetting.KeyFilePath,
                PrivateKeyPassphrase = OciSetting.PassPhrase
            };
            ThreadSafeSigner threadSafeSigner = new ThreadSafeSigner(new OciSigner(ClientConfig));

            IdentityClient = new IdentityClient(ClientConfig);
            ObjectStorageClient = new ObjectStorageClient(ClientConfig, threadSafeSigner);
        }

        public string GetCompartmentName(string compartmentId)
        {
            GetCompartmentRequest getCompartmentRequest = new GetCompartmentRequest()
            {
                CompartmentId = compartmentId
            };

            return IdentityClient.GetCompartment(getCompartmentRequest).Compartment.Name;
        }

        /// <summary>
        /// サブスクリプション済み全リージョン取得
        /// </summary>
        /// <returns></returns>
        public IEnumerable<OCISDK.Identity.Model.RegionSubscription> GetRegions()
        {
            return IdentityClient.ListRegionSubscriptions(new OCISDK.Identity.Request.ListRegionSubscriptionsRequest { TenancyId = OciSetting.TenancyId }).Items;
        }

        /// <summary>
        /// テナント情報取得
        /// </summary>
        /// <returns></returns>
        public GetTenancyResponse GetTenantInfo()
        {
            return IdentityClient.GetTenancy(new OCISDK.Identity.Request.GetTenancyRequest { TenancyId = OciSetting.TenancyId });
        }

        /// <summary>
        /// 指定されたコンパートメントに属するすべての子コンパートメント取得
        /// </summary>
        /// <param name="compartmentId"></param>
        /// <returns></returns>
        public IEnumerable<OCISDK.Identity.Model.Compartment> GetCompartments(string compartmentId)
        {
            List<OCISDK.Identity.Model.Compartment> res = new List<OCISDK.Identity.Model.Compartment>();

            var request = new OCISDK.Identity.Request.ListCompartmentRequest
            {
                CompartmentId = compartmentId,
                CompartmentIdInSubtree = false,
                AccessLevel = OCISDK.Identity.Request.ListCompartmentRequest.AccessLevels.ACCESSIBLE,
                Limit = 100
            };

            while (true)
            {
                var compartments = IdentityClient.ListCompartment(request);

                res.AddRange(compartments.Items);

                if (string.IsNullOrEmpty(compartments.OpcNextPage))
                    break;

                request.Page = compartments.OpcNextPage;
            }

            return res;
        }

        /// <summary>
        ///  階層指定を無視して全コンパートメントを得る
        /// </summary>
        /// <param name="compartmentId"></param>
        /// <returns></returns>
        public IEnumerable<OCISDK.Identity.Model.Compartment> GetAllCompartments(string tenantId)
        {
            List<OCISDK.Identity.Model.Compartment> res = new List<OCISDK.Identity.Model.Compartment>();

            var request = new OCISDK.Identity.Request.ListCompartmentRequest
            {
                CompartmentId = tenantId,
                CompartmentIdInSubtree = true,
                AccessLevel = OCISDK.Identity.Request.ListCompartmentRequest.AccessLevels.ACCESSIBLE,
                Limit = 100
            };

            while (true)
            {
                var compartments = IdentityClient.ListCompartment(request);

                res.AddRange(compartments.Items);

                if (string.IsNullOrEmpty(compartments.OpcNextPage))
                    break;

                request.Page = compartments.OpcNextPage;
            }

            return res;
        }

        public class BukcetInfo
        {
            public string Name { get; set; }

            public string Region { get; set; }

            public string Id { get; set; }

            public string ModifiedTime { get; set; }
        }

        /// <summary>
        /// すべてのバケットを取得する
        /// </summary>
        /// <param name="nameSpaceName"></param>
        /// <param name="compartmentId"></param>
        /// <returns></returns>
        public IEnumerable<BukcetInfo> GetAllBuckets(string nameSpaceName, string compartmentId)
        {
            List<BukcetInfo> res = new List<BukcetInfo>();

            var regions = GetRegions();
            var request = new ListBucketsRequest { NamespaceName = nameSpaceName, CompartmentId = compartmentId, Limit = 10 };
            Parallel.ForEach(regions, region => {
                ObjectStorageClient objectStorageClient = new ObjectStorageClient(ClientConfig);
                objectStorageClient.SetRegion(region.RegionName);
                while (true)
                {
                    var buckets = objectStorageClient.ListBuckets(request);

                    foreach (var item in buckets.Items)
                    {
                        res.Add(new BukcetInfo() { Name = item.Name, Region = region.RegionName, Id = item.Id, ModifiedTime=item.TimeCreated });
                    }

                    if (string.IsNullOrEmpty(buckets.OpcNextPage))
                    {
                        break;
                    }

                    request.Page = buckets.OpcNextPage;
                }
            });

            return res;
        }

        /// <summary>
        /// バケットを取得する
        /// </summary>
        /// <param name="nameSpaceName"></param>
        /// <param name="compartmentId"></param>
        /// <param name="regionName"></param>
        /// <returns></returns>
        public IEnumerable<BukcetInfo> GetBuckets(string nameSpaceName, string compartmentId, string regionName)
        {
            List<BukcetInfo> res = new List<BukcetInfo>();
            var request = new ListBucketsRequest { NamespaceName = nameSpaceName, CompartmentId = compartmentId, Limit = 10 };
            ObjectStorageClient.SetRegion(regionName);
            while (true)
            {
                var buckets = ObjectStorageClient.ListBuckets(request);

                foreach (var item in buckets.Items)
                {
                    res.Add(new BukcetInfo() { Name = item.Name, Region = regionName, Id = item.Id });
                }

                if (string.IsNullOrEmpty(buckets.OpcNextPage))
                    break;

                request.Page = buckets.OpcNextPage;
            }

            return res;
        }

        /// <summary>
        /// バケットヘッダを取得する
        /// </summary>
        /// <param name="nameSpaceName"></param>
        /// <param name="bucketName"></param>
        /// <param name="regionName"></param>
        /// <returns></returns>
        public HeadBucketResponse GetBucketHead(string nameSpaceName, string bucketName, string regionName)
        {
            ObjectStorageClient.SetRegion(regionName);
            HeadBucketRequest headObjectRequest = new HeadBucketRequest
            {
                NamespaceName = nameSpaceName,
                BucketName = bucketName,
            };
            return ObjectStorageClient.HeadBucket(headObjectRequest);
        }

        /// <summary>
        /// オブジェクトヘッダを取得する
        /// </summary>
        /// <param name="nameSpaceName"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="regionName"></param>
        /// <returns></returns>
        public HeadObjectResponse GetObjectHead(string nameSpaceName, string bucketName, string objectName, string regionName)
        {
            ObjectStorageClient.SetRegion(regionName);
            HeadObjectRequest headObjectRequest = new HeadObjectRequest
            {
                NamespaceName = nameSpaceName,
                BucketName = bucketName,
                ObjectName = objectName
            };
            return ObjectStorageClient.HeadObject(headObjectRequest);
        }

        /// <summary>
        /// オブジェクトをアップロードする
        /// </summary>
        /// <param name="nameSpaceName"></param>
        /// <param name="bucketName"></param>
        /// <param name="name"></param>
        /// <param name="regionName"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool PutObject(string nameSpaceName, string bucketName, string name, string regionName, string filePath)
        {
            try
            {
                ObjectStorageClient.SetRegion(regionName);
                PutObjectRequest putObjectRequest = new PutObjectRequest
                {
                    NamespaceName = nameSpaceName,
                    BucketName = bucketName,
                    ObjectName = name
                };
                PutObjectResponse updateRes;
                using (FileStream stream = new FileStream(filePath, FileMode.Open))
                {
                    putObjectRequest.UploadPartBody = stream;

                    updateRes = ObjectStorageClient.PutObject(putObjectRequest);
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// オブジェクトの名前変更
        /// </summary>
        /// <param name="nameSpaceName"></param>
        /// <param name="bucketName"></param>
        /// <param name="fromName"></param>
        /// <param name="regionName"></param>
        /// <param name="toName"></param>
        /// <returns></returns>
        public bool RenameObject(string nameSpaceName, string bucketName, string fromName, string regionName, string toName)
        {
            try
            {
                ObjectStorageClient.SetRegion(regionName);
                RenameObjectRequest renameObjectRequest = new RenameObjectRequest
                {
                    NamespaceName = nameSpaceName,
                    BucketName = bucketName,
                    RenameObjectDetails = new OCISDK.ObjectStorage.Model.RenameObjectDetails
                    {
                        NewName = toName,
                        SourceName = fromName
                    }
                };
                ObjectStorageClient.RenameObject(renameObjectRequest);
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// オブジェクトをダウンロードする
        /// </summary>
        /// <param name="nameSpaceName"></param>
        /// <param name="bucketName"></param>
        /// <param name="name"></param>
        /// <param name="regionName"></param>
        /// <param name="savePath"></param>
        /// <returns></returns>
        public bool DownloadObject(string nameSpaceName, string bucketName, string name, string regionName, string savePath)
        {
            ObjectStorageClient.SetRegion(regionName);
            GetObjectRequest getObjectRequest = new GetObjectRequest
            {
                NamespaceName = nameSpaceName,
                BucketName = bucketName,
                ObjectName = name
            };

            var result = ObjectStorageClient.DownloadObject(getObjectRequest, savePath);

            return result ?? false;
        }

        /// <summary>
        /// バケットを作成する
        /// </summary>
        /// <param name="nameSpaceName"></param>
        /// <param name="bucketName"></param>
        /// <param name="regionName"></param>
        /// <returns></returns>
        public bool CreateBucket(string nameSpaceName, string bucketName, string compartmentId, string regionName)
        {
            try
            {
                ObjectStorageClient.SetRegion(regionName);
                CreateBucketRequest createBucketRequest = new CreateBucketRequest
                {
                    NamespaceName = nameSpaceName,
                    CreateBucketDetails = new OCISDK.ObjectStorage.Model.CreateBucketDetails
                    {
                        CompartmentId = compartmentId,
                        Name = bucketName
                    }
                };
                ObjectStorageClient.CreateBucket(createBucketRequest);
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// ディレクトリを作成する
        /// </summary>
        /// <param name="nameSpaceName"></param>
        /// <param name="bucketName"></param>
        /// <param name="name"></param>
        /// <param name="regionName"></param>
        /// <returns></returns>
        public bool CreateDirectory(string nameSpaceName, string bucketName, string name, string regionName)
        {
            if (!name.EndsWith("/"))
            {
                return false;
            }
            try
            {
                PutObjectRequest putObjectRequest = new PutObjectRequest
                {
                    NamespaceName = nameSpaceName,
                    BucketName = bucketName,
                    ObjectName = name,
                    ContentDisposition = "tool_created_directory"
                };
                putObjectRequest.UploadPartBody = Stream.Null;

                ObjectStorageClient.PutObject(putObjectRequest);
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// オブジェクトを削除する
        /// </summary>
        /// <param name="nameSpaceName"></param>
        /// <param name="bucketName"></param>
        /// <param name="name"></param>
        /// <param name="regionName"></param>
        /// <returns></returns>
        public bool DeleteObject(string nameSpaceName, string bucketName, string name, string regionName)
        {
            try
            {
                ObjectStorageClient.SetRegion(regionName);
                DeleteObjectRequest deleteObjectRequest = new DeleteObjectRequest
                {
                    NamespaceName = nameSpaceName,
                    BucketName = bucketName,
                    ObjectName = name
                };
                ObjectStorageClient.DeleteObject(deleteObjectRequest);
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        ///バケットを削除する
        /// </summary>
        /// <param name="nameSpaceName"></param>
        /// <param name="bucketName"></param>
        /// <param name="regionName"></param>
        /// <returns></returns>
        public bool DeleteBucket(string nameSpaceName, string bucketName, string regionName)
        {
            try
            {
                ObjectStorageClient.SetRegion(regionName);
                ListObjectsRequest listObjectsRequest = new ListObjectsRequest
                {
                    NamespaceName = nameSpaceName,
                    BucketName = bucketName,
                    Limit = 100
                };
                while (true)
                {
                    var objects = ObjectStorageClient.ListObjects(listObjectsRequest);

                    foreach (var obj in objects.ListObjects.Objects)
                    {
                        DeleteObject(nameSpaceName, bucketName, obj.Name, regionName);
                    }

                    if (string.IsNullOrEmpty(objects.ListObjects.NextStartWith))
                    {
                        break;
                    }

                    listObjectsRequest.Start = objects.OpcRequestId;
                }

                DeleteBucketRequest deleteBucketRequest = new DeleteBucketRequest
                {
                    NamespaceName = nameSpaceName,
                    BucketName = bucketName
                };
                ObjectStorageClient.DeleteBucket(deleteBucketRequest);
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// ディレクトリを削除する
        /// </summary>
        /// <param name="nameSpaceName"></param>
        /// <param name="bucketName"></param>
        /// <param name="dirName"></param>
        /// <param name="regionName"></param>
        /// <returns></returns>
        public bool DeleteDirectory(string nameSpaceName, string bucketName, string dirName, string regionName)
        {
            try
            {
                ObjectStorageClient.SetRegion(regionName);
                var dirInfo = new ObjectStorageDirectoryInfo(ObjectStorageClient, nameSpaceName, bucketName, dirName + "/");
                dirInfo.Delete(true);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
