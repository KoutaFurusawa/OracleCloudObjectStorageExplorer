using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ObjectStorageExplorer.OciResources;

namespace ObjectStorageExplorer
{
    public interface IOciResources
    {
        OCISDK.ObjectStorage.IObjectStorageClient GetObjectStorageClient();

        void SettingReset();

        bool SettingValidation();

        void CreateClient();

        string GetCompartmentName(string compartmentId);

        IEnumerable<OCISDK.Identity.Model.RegionSubscription> GetRegions();

        OCISDK.Identity.Response.GetTenancyResponse GetTenantInfo();

        IEnumerable<OCISDK.Identity.Model.Compartment> GetCompartments(string compartmentId);

        IEnumerable<OCISDK.Identity.Model.Compartment> GetAllCompartments(string tenantId);

        IEnumerable<BukcetInfo> GetAllBuckets(string nameSpaceName, string compartmentId);

        IEnumerable<BukcetInfo> GetBuckets(string nameSpaceName, string compartmentId, string regionName);

        OCISDK.ObjectStorage.Response.HeadBucketResponse GetBucketHead(string nameSpaceName, string bucketName, string regionName);

        OCISDK.ObjectStorage.Response.HeadObjectResponse GetObjectHead(string nameSpaceName, string bucketName, string objectName, string regionName);

        bool CreateBucket(string nameSpaceName, string bucketName, string compartmentId, string regionName);

        bool CreateDirectory(string nameSpaceName, string bucketName, string name, string regionName);

        bool DownloadObject(string nameSpaceName, string bucketName, string name, string regionName, string savePath);

        bool PutObject(string nameSpaceName, string bucketName, string name, string regionName, string filePath);

        bool RenameObject(string nameSpaceName, string bucketName, string fromName, string regionName, string toName);

        bool DeleteObject(string nameSpaceName, string bucketName, string name, string regionName);

        bool DeleteBucket(string nameSpaceName, string bucketName, string regionName);

        bool DeleteDirectory(string nameSpaceName, string bucketName, string dirName, string regionName);
    }
}
