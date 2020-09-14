using OCISDK.ObjectStorage.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ObjectStorageExplorer
{
    public partial class PropartiesForm : Form
    {
        public PropartiesForm()
        {
            InitializeComponent();
        }

        private void PropartiesForm_Load(object sender, EventArgs e)
        {

        }

        public void SetBucket(HeadBucketResponse headBucket, string bucketName, string regionName)
        {
            List<ListViewItem> items = new List<ListViewItem>()
            {
                new ListViewItem(new string[] { "Name", bucketName }),
                new ListViewItem(new string[] { "Region", regionName }),
                new ListViewItem(new string[] { "ETag", headBucket.ETag }),
                new ListViewItem(new string[] { "OpcClientRequestId", headBucket.OpcClientRequestId }),
                new ListViewItem(new string[] { "OpcRequestId", headBucket.OpcRequestId }),
            };
            ObjectPropartiesView.Items.AddRange(items.ToArray());
        }

        public void SetDirectory(string dirName, string path, string regionName)
        {
            List<ListViewItem> items = new List<ListViewItem>()
            {
                new ListViewItem(new string[] { "Name", dirName }),
                new ListViewItem(new string[] { "Region", regionName }),
                new ListViewItem(new string[] { "Path", path }),
            };
            ObjectPropartiesView.Items.AddRange(items.ToArray());
        }

        public void SetObject(HeadObjectResponse headObject, string objName, string regionName)
        {
            List<ListViewItem> items = new List<ListViewItem>()
            {
                new ListViewItem(new string[] { "Name", objName }),
                new ListViewItem(new string[] { "Region", regionName }),
                new ListViewItem(new string[] { "URL", headObject.FileURL }),
                new ListViewItem(new string[] { "ContentLength", (headObject.ContentLength ?? 0) + "" }),
                new ListViewItem(new string[] { "ArchivalState", headObject.ArchivalState }),
                new ListViewItem(new string[] { "CacheControl", headObject.CacheControl }),
                new ListViewItem(new string[] { "ContentDisposition", headObject.ContentDisposition }),
                new ListViewItem(new string[] { "ContentEncoding", headObject.ContentEncoding }),
                new ListViewItem(new string[] { "ContentLanguage", headObject.ContentLanguage }),
                new ListViewItem(new string[] { "ContentMd5", headObject.ContentMd5 }),
                new ListViewItem(new string[] { "ContentType", headObject.ContentType }),
                new ListViewItem(new string[] { "ETag", headObject.ETag }),
                new ListViewItem(new string[] { "LastModified", headObject.LastModified }),
                new ListViewItem(new string[] { "OpcClientRequestId", headObject.OpcClientRequestId }),
                new ListViewItem(new string[] { "OpcMultipartMd5", headObject.OpcMultipartMd5 }),
                new ListViewItem(new string[] { "OpcRequestId", headObject.OpcRequestId }),
                new ListViewItem(new string[] { "TimeOfArchival", headObject.TimeOfArchival })
            };
            ObjectPropartiesView.Items.AddRange(items.ToArray());
        }

        private void PropartiesOkBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
