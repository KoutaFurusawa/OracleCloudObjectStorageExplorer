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
    public partial class NewAddForm : Form
    {
        private IOciResources OciResources;
        private string NamespaceName;
        private string BucketName;
        private string DirectoryName;
        private string RegionName;
        private string CompartmentId;
        private bool IsBucket = false;

        public NewAddForm(IOciResources ociResources, string namespaceName, string bucketName, string directoryName, string regionName, string compartmentId="")
        {
            InitializeComponent();

            OciResources = ociResources;
            NamespaceName = namespaceName;
            BucketName = bucketName;
            DirectoryName = directoryName;
            RegionName = regionName;
            CompartmentId = compartmentId;
            string compartmentName = "";

            if (!string.IsNullOrEmpty(compartmentId))
            {
                compartmentName = OciResources.GetCompartmentName(compartmentId);
            }

            IsBucket = string.IsNullOrEmpty(bucketName);

            if (IsBucket)
            {
                this.PathLab.Text = compartmentName + "/";

                var regions = ociResources.GetRegions();
                foreach (var region in regions)
                {
                    RegionsBox.Items.Add(region.RegionName);
                }
            }
            else
            {
                this.PathLab.Text = compartmentName + "/" + bucketName + "://" + directoryName;

                RegionsBox.Items.Add(regionName);
                RegionsBox.Enabled = false;
            }
        }

        private void NewAddForm_Load(object sender, EventArgs e)
        {

        }

        private void MakeBtn_Click(object sender, EventArgs e)
        {
            bool res;
            if (IsBucket)
            {
                if (RegionsBox.SelectedItem == null || string.IsNullOrEmpty(RegionsBox.SelectedItem.ToString()))
                {
                    DialogManagement.ShowMessageDialog("エラー", "リージョンを指定してください", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                res = OciResources.CreateBucket(NamespaceName, NameTextBox.Text, CompartmentId, RegionsBox.SelectedItem.ToString());
            }
            else
            {
                if (NameTextBox.Text.StartsWith("/"))
                {
                    DialogManagement.ShowMessageDialog("エラー", "名称の先頭に / を入れないでください", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!NameTextBox.Text.EndsWith("/"))
                {
                    NameTextBox.Text += "/";
                }

                if (NameTextBox.Text.Contains("//"))
                {
                    DialogManagement.ShowMessageDialog("エラー", $"不正なパスを指定しています。\n[{NameTextBox.Text}]", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                res = OciResources.CreateDirectory(NamespaceName, BucketName, DirectoryName + NameTextBox.Text, RegionName);
            }

            if (!res)
            {
                DialogManagement.ShowMessageDialog("エラー", $"{(IsBucket ? "バケット" : "ディレクトリ")}作成に失敗しました", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
