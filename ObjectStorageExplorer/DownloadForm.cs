using OCISDK.ObjectStorage;
using OCISDK.ObjectStorage.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ObjectStorageExplorer
{
    public partial class DownloadForm : Form
    {
        private IOciResources OciResources;
        private string NamespaceName;
        private string BucketName;
        private string TargetName;
        private string RegionName;
        private bool IsBucket = false;
        private bool IsFile = false;

        public DownloadForm(IOciResources ociResources, string namespaceName, string bucketName, string targetName, string regionName)
        {
            InitializeComponent();

            OciResources = ociResources;
            NamespaceName = namespaceName;
            BucketName = bucketName;
            TargetName = targetName;
            RegionName = regionName;

            this.PathLab.Text = bucketName + "://" + targetName + "(" + regionName + ")";

            IsBucket = string.IsNullOrEmpty(TargetName);

            if (!IsBucket)
            {
                IsFile = !TargetName.EndsWith("/") && !string.IsNullOrEmpty(TargetName);

                if (IsFile)
                {
                    OptionFilenameSetButton.Checked = true;
                    OptionSaveCheckBox.Enabled = false;
                }
                else
                {
                    OptionRadioButtonA.Checked = true;
                    OptionFilenameSetButton.Enabled = false;
                }
            }
            else
            {
                OptionRadioButtonA.Checked = true;
                OptionFilenameSetButton.Enabled = false;
            }
        }

        private void SelectButton_Click(object sender, EventArgs e)
        {
            if (IsFile && OptionFilenameSetButton.Checked)
            {
                var defaultPath = @"C:\";
                if (!string.IsNullOrEmpty(SavePathBox.Text))
                {
                    defaultPath = SavePathBox.Text;
                }
                SavePathBox.Text = DialogManagement.SaveDialogShow(defaultPath);
            }
            else
            {
                SavePathBox.Text = DialogManagement.FolderSelectDialogShow();
            }
        }

        private void DownloadButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SavePathBox.Text))
            {
                DialogManagement.ShowMessageDialog("エラー", "保存先を指定してください", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            CancelBtn.Enabled = false;

            bool res;
            if (IsFile)
            {
                res = OneDownloadFile();
            }
            else
            {
                res = DirectoryDownloadFile();
            }

            CancelBtn.Enabled = true;

            if (res)
            {
                DialogManagement.ShowMessageDialog("完了", "ダウンロードが完了しました", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (AutoCloseCheck.Checked)
                {
                    this.Close();
                }
            }
        }

        private bool OneDownloadFile()
        {
            var isDirMode = OptionRadioButtonA.Checked;
            var isForceSave = OptionForceSaveButton.Checked;

            var savePath = SavePathBox.Text;
            if (!OptionFilenameSetButton.Checked)
            {
                if (!SavePathBox.Text.EndsWith("\\"))
                {
                    SavePathBox.Text += "\\";
                }
                savePath = GetSavePath(TargetName, isDirMode, isForceSave);
                if (string.IsNullOrEmpty(savePath))
                {
                    return false;
                }
            }
            return OciResources.DownloadObject(NamespaceName, BucketName, TargetName, RegionName, savePath);
        }

        private bool DirectoryDownloadFile()
        {
            var isDirMode = OptionRadioButtonA.Checked;
            var isForceSave = OptionForceSaveButton.Checked;
            if (!SavePathBox.Text.EndsWith("\\"))
            {
                SavePathBox.Text += "\\";
            }

            var files = GetDireftoryInfo(TargetName);
            progressBar1.Maximum = files.Count();
            List<string> failedNames = new List<string>();
            foreach (var file in files)
            {
                DownloadNowLab.Text = "download...[" + file.OriginalKey + "]";
                var savePath = GetSavePath(file.OriginalKey, isDirMode, isForceSave);
                var res  = OciResources.DownloadObject(NamespaceName, BucketName, file.OriginalKey, RegionName, savePath);
                if (!res)
                {
                    failedNames.Add(file.OriginalKey);
                }
                progressBar1.Value++;
                DownloadNowLab.Refresh();
                progressBar1.Refresh();
            };

            if (failedNames.Count() > 0)
            {
                var message = "";
                foreach (var name in failedNames)
                {
                    message += $"{name}\n";
                }
                DialogManagement.ShowMessageDialog("エラー", message + "のダウンロードができませんでした", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            progressBar1.Value = 0;
            DownloadNowLab.Text = "";
            return true;
        }

        private IEnumerable<ObjectStorageFileInfo> GetDireftoryInfo(string dirPath)
        {
            List<ObjectStorageFileInfo> files = new List<ObjectStorageFileInfo>();
            OciResources.GetObjectStorageClient().SetRegion(RegionName);
            var directoryInfo = new ObjectStorageDirectoryInfo(OciResources.GetObjectStorageClient(), NamespaceName, BucketName, dirPath);

            while (true)
            {
                var fileInfos = directoryInfo.EnumerateNextFiles("*", System.IO.SearchOption.TopDirectoryOnly, 10);
                files.AddRange(fileInfos);

                if (files.Count() <= 0 || !directoryInfo.CheckNextPage())
                {
                    break;
                }
            }

            var topDirs = directoryInfo.EnumerateDirectories("*", System.IO.SearchOption.TopDirectoryOnly);
            foreach (var dir in topDirs)
            {
                files.AddRange(GetDireftoryInfo(dir.OriginalKey));
            }

            return files;
        }

        private string GetSavePath(string originName, bool isDirMode, bool isForceSave)
        {
            var newName = originName;
            if (!isDirMode)
            {
                newName = newName.Replace("/", "_");
            }
            var pathBoxText = SavePathBox.Text;
            if (IsBucket)
            {
                pathBoxText += BucketName + "\\";
            }
            var savePath = DecodeKey(pathBoxText + newName);

            if (!isForceSave)
            {
                FileInfo fileInfo = new FileInfo(savePath);
                if (fileInfo.Exists)
                {
                    DialogResult resulet = DialogManagement.ShowMessageDialog("確認", $"{savePath}を上書きしますか？", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    if (resulet == DialogResult.No)
                    {
                        return "";
                    }
                }
            }

            return savePath;
        }

        private string EncodeKey(string key)
        {
            return key.Replace('\\', '/');
        }

        private string DecodeKey(string key)
        {
            return key.Replace('/', '\\');
        }

        private void OptionRadioButtonA_CheckedChanged(object sender, EventArgs e)
        {
            if (!SavePathBox.Text.EndsWith("\\"))
            {
                var names = SavePathBox.Text.Split('\\');
                SavePathBox.Text = "";
                for (var i = 0; i < names.Length - 1; ++i)
                {
                    SavePathBox.Text += names[i] + "\\";
                }
            }
        }

        private void OptionRadioButtonB_CheckedChanged(object sender, EventArgs e)
        {
            if (!SavePathBox.Text.EndsWith("\\"))
            {
                var names = SavePathBox.Text.Split('\\');
                SavePathBox.Text = "";
                for (var i = 0; i < names.Length - 1; ++i)
                {
                    SavePathBox.Text += names[i] + "\\";
                }
            }
        }

        private void OptionFilenameSetButton_CheckedChanged(object sender, EventArgs e)
        {
            if (SavePathBox.Text.EndsWith("\\"))
            {
                var names = TargetName.Split('/');
                SavePathBox.Text += names[names.Length - 1];
            }
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
