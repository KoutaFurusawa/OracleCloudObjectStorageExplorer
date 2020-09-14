using OCISDK.ObjectStorage.IO;
using OCISDK.ObjectStorage.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ObjectStorageExplorer
{
    public partial class UploadForm : Form
    {
        private IOciResources OciResources;
        private string NamespaceName;
        private string BucketName;
        private string TargetPath;
        private string RegionName;

        public UploadForm(IOciResources ociResources, string namespaceName, string bucketName, string targetPath, string regionName)
        {
            InitializeComponent();

            OciResources = ociResources;
            NamespaceName = namespaceName;
            BucketName = bucketName;
            TargetPath = targetPath;
            RegionName = regionName;

            PathLab.Text = bucketName + "://" + targetPath + "(" + regionName + ")";

            FileListView.ContextMenuStrip = MenuStrip;
        }

        private void FolderSelectBtn_Click(object sender, EventArgs e)
        {
            FileListView.Items.Clear();
            var path = DialogManagement.FolderSelectDialogShow();

            SetListFiles(path);
        }

        private void FileSelectBtn_Click(object sender, EventArgs e)
        {
            var paths = DialogManagement.MultipleFileOpenDialogShow(@"C:\");

            foreach (var path in paths)
            {
                if(string.IsNullOrEmpty(path))
                {
                    continue;
                }
                FileInfo fileInfo = new FileInfo(path);
                if (fileInfo.Exists)
                {
                    string[] itemView = new string[] { fileInfo.FullName, "waiting" };
                    FileListView.Items.Add(new ListViewItem(itemView));
                }
            }
        }

        private void SetListFiles(string path)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            if (CheckFolder(path))
            {
                SearchOption searchOption = SearchOption.AllDirectories;
                if (TopDirectoryCheck.Checked)
                {
                    searchOption = SearchOption.TopDirectoryOnly;
                }
                try
                {
                    var files = directoryInfo.EnumerateFiles("*", searchOption);

                    foreach (var file in files)
                    {
                        string[] itemView = new string[] { file.FullName, "waiting" };
                        FileListView.Items.Add(new ListViewItem(itemView));
                    }
                    FileListView.Refresh();
                }
                catch (Exception e)
                {
                    DialogManagement.ShowMessageDialog("エラー", e.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                DialogManagement.ShowMessageDialog("エラー", "フォルダーが見つかりませんでした", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CheckFolder(string path)
        {
            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                if (directoryInfo.Exists)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        private void AllClearBtn_Click(object sender, EventArgs e)
        {
            FileListView.Items.Clear();
        }

        private void DeleteMenuItem_Click(object sender, EventArgs e)
        {
            if (FileListView.SelectedItems.Count <= 0)
            {
                return;
            }

            foreach (var item in FileListView.SelectedItems)
            {
                FileListView.Items.Remove((item as ListViewItem));
            }
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            if (IsUploading)
            {
                CancellationTokenSource.Cancel();

                UploadMode(false);
            }
            else
            {
                if (IsChanged)
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                else
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                }
                this.Close();
            }
        }

        private void UploadMode(bool enable)
        {
            if (enable)
            {
                IsUploading = true;
                AllButtonEnable(false);
                CancelBtn.Text = "Cancel";

            }
            else
            {
                IsUploading = false;
                AllButtonEnable(true);
                CancelBtn.Invoke((MethodInvoker)delegate {
                    CancelBtn.Text = "Close";
                });
            }
        }

        private bool IsChanged = false;
        private bool IsUploading = false;
        CancellationTokenSource CancellationTokenSource;
        private void UploadBtn_Click(object sender, EventArgs e)
        {
            if (FileListView.Items.Count <= 0 || IsUploading)
            {
                return;
            }

            IsChanged = true;

            UploadMode(true);

            var isForce = OptionForceSaveButton.Checked;

            List<ListViewItem> targets = new List<ListViewItem>();
            foreach (var item in FileListView.Items)
            {
                targets.Add(item as ListViewItem);
            }
            ProgressBar.Maximum = targets.Count();

            Task.Run(() => { UploadFiles(targets, isForce); });

        }

        private void UploadFiles(List<ListViewItem> targets, bool isForce)
        {
            ParallelOptions po = new ParallelOptions();
            CancellationTokenSource = new CancellationTokenSource();
            po.CancellationToken = CancellationTokenSource.Token;
            po.MaxDegreeOfParallelism = System.Environment.ProcessorCount;
            try
            {
                Parallel.ForEach(targets, po, item =>
                {
                    var paths = item.Text.Split('\\');
                    var filename = paths[paths.Length - 1];
                    DialogResult resulet = DialogResult.Yes;
                    HeadObjectResponse head = null;
                    if (!isForce)
                    {
                        try
                        {
                            head = OciResources.GetObjectHead(NamespaceName, BucketName, TargetPath + filename, RegionName);
                        }
                        catch (WebException we)
                        {
                            if (we.Status.Equals(WebExceptionStatus.ProtocolError) && ((HttpWebResponse)we.Response).StatusCode == HttpStatusCode.NotFound)
                            {
                                head = null;
                            }
                            else
                            {
                                throw;
                            }
                        }
                        if (head != null)
                        {
                            resulet = DialogManagement.ShowMessageDialog("確認", "オブジェクト [" + TargetPath + filename + "] を上書きしますか？", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                        }
                    }
                    if (resulet == DialogResult.Yes)
                    {
                        if (!OciResources.PutObject(NamespaceName, BucketName, TargetPath + filename, RegionName, item.Text))
                        {
                            DialogManagement.ShowMessageDialog("エラー", "ファイル [" + item.Text + "] のアップロードに失敗しました", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            FileListView.Invoke((MethodInvoker)delegate {
                                item.SubItems[1].Text = "Failed";
                            });
                        }
                        else 
                        {
                            FileListView.Invoke((MethodInvoker)delegate {
                                item.SubItems[1].Text = "Successed";
                            });
                        }

                        ProgressBar.Invoke((MethodInvoker)delegate {
                            ProgressBar.Value++;
                        });
                    }
                });
            }
            catch (OperationCanceledException oce)
            {
                DialogManagement.ShowMessageDialog("警告", "アップロードはキャンセルされました。\nMessage:" + oce.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                CancellationTokenSource.Dispose();

                ProgressBar.Invoke((MethodInvoker)delegate {
                    ProgressBar.Value = 0;
                });

                UploadMode(false);

                DialogManagement.ShowMessageDialog("完了", "アップロードが完了しました", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void AllButtonEnable(bool enable)
        {
            FolderSelectBtn.Invoke((MethodInvoker)delegate {
                FolderSelectBtn.Enabled = enable;
            });
            FileSelectBtn.Invoke((MethodInvoker)delegate {
                FileSelectBtn.Enabled = enable;
            });
            AllClearBtn.Invoke((MethodInvoker)delegate {
                AllClearBtn.Enabled = enable;
            });
            UploadBtn.Invoke((MethodInvoker)delegate {
                UploadBtn.Enabled = enable;
            });
        }
    }
}
