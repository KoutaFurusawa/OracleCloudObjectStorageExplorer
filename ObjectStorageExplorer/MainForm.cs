using ObjectStorageExplorer.Properties;
using OCISDK;
using OCISDK.Identity;
using OCISDK.Identity.Model;
using OCISDK.ObjectStorage;
using OCISDK.ObjectStorage.IO;
using OCISDK.ObjectStorage.Request;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace ObjectStorageExplorer
{
    public partial class MainForm : Form
    {
        private string NameSpaceName = "";
        private IOciResources OciResources;
        private List<RegionSubscription> Regions;

        public MainForm()
        {
            DoubleBuffered = true;

            InitializeComponent();

            OciResources = new OciResources();

            ObjectList.ContextMenuStrip = NormalMenuStrip;

        }

        /// <summary>
        /// OnShow
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            if (!OciResources.SettingValidation())
            {
                SettingFormShow();
            }
            base.OnShown(e);
        }

        /// <summary>
        /// 読み込み中パネル（※間に合わせ）
        /// </summary>
        /// <param name="visible"></param>
        private void LoadingPanelVisible(bool visible)
        {
            ProgressPanel.Visible = visible;
            ProgressLab.Refresh();
        }

        /// <summary>
        /// 接続ボタン
        /// </summary>
        private bool IsConnection = false;
        private void ConnectionBtn_Click(object sender, EventArgs e)
        {
            ExplorerInitialize();
        }

        /// <summary>
        /// 初期化
        /// </summary>
        private void ExplorerInitialize()
        {
            ObjectGUIEnable(false);

            DirTree.Nodes.Clear();
            ObjectList.Items.Clear();

            LoadingPanelVisible(true);

            OciResources.CreateClient();

            Regions = OciResources.GetRegions().ToList();

            CreateCompartmentNodeTree();

            string[] item = { "select compartment", "", "" };
            ObjectList.Items.Add(new ListViewItem(item));

            LoadingPanelVisible(false);
            ConnectionBtn.Text = "ReConnect";
            IsConnection = true;
        }


        /// <summary>
        /// 全コンパートメントをノード追加
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="parentNode"></param>
        private void CreateCompartmentNodeTree()
        {
            var root = OciResources.GetTenantInfo();
            NameSpaceName = root.Tenancy.Name;
            var rootNode = DirTree.Nodes.Add(root.Tenancy.Name + "(root)");
            rootNode.Name = root.Tenancy.Name + "(root)";
            rootNode.Tag = "Compartment," + root.Tenancy.Id;

            var allCompartments = OciResources.GetAllCompartments(root.Tenancy.Id);

            List<string> addedCom = new List<string>();

            while (true)
            {
                foreach (var com in allCompartments.ToList())
                {
                    if (addedCom.Contains(com.Id))
                    {
                        continue;
                    }
                    if (com.CompartmentId == root.Tenancy.Id)
                    {
                        AddCompartmentNode(com, rootNode);
                        addedCom.Add(com.Id);
                    }
                    else
                    {
                        var parentName = allCompartments.FirstOrDefault(c => c.Id == com.CompartmentId);
                        if (parentName == null)
                        {
                            continue;
                        }
                        var search = rootNode.Nodes.Find(parentName.Name, true);
                        if (search.Count() <= 0)
                        {
                            continue;
                        }
                        if (search.Count() > 1)
                        {
                            throw new Exception("node multiple!");
                        }
                        AddCompartmentNode(com, search.First());
                        addedCom.Add(com.Id);
                    }
                }
                if (addedCom.Count() == allCompartments.Count())
                    break;
            }
        }

        private void AddCompartmentNode(Compartment compartment, TreeNode parentNode)
        {
            if (compartment.LifecycleState != "ACTIVE")
                return;
            var node = parentNode.Nodes.Add(compartment.Name);
            node.Name = compartment.Name;
            node.Tag = "Compartment," + compartment.Id;

            DirTree.Refresh();
        }

        string NowCompartmentId;
        string NowBucket;
        string NowRegion;
        string FullPath;
        ObjectStorageDirectoryInfo DirectoryInfo;

        /// <summary>
        /// 全バケットを追加
        /// </summary>
        /// <param name="compartmentId"></param>
        public void SetAllBucket(string compartmentId)
        {
            ObjectList.Items.Clear();

            List<string[]> viewItems = new List<string[]>();
            var buckets = OciResources.GetAllBuckets(NameSpaceName, compartmentId);
            foreach (var bucket in buckets)
            {
                string[] item = { bucket.Name, bucket.Region, "", bucket.ModifiedTime };
                viewItems.Add(item);
            }
            foreach (var item in viewItems)
            {
                var bb = ObjectList.Items.Add(new ListViewItem(item));
                bb.Tag = "Bucket";
                bb.ImageIndex = 2;
                bb.Name = item[0];
            }
        }

        /// <summary>
        /// 指定されたディレクトリ内容をセットする
        /// </summary>
        /// <param name="dir"></param>
        private void SetDir(string dir)
        {
            SelectObject = null;
            LoadingPanelVisible(true);

            OciResources.GetObjectStorageClient().SetRegion(NowRegion);
            DirectoryInfo = new ObjectStorageDirectoryInfo(OciResources.GetObjectStorageClient(), NameSpaceName, NowBucket, dir);
            GetDireftoryInfo();

            LoadingPanelVisible(false);
        }

        /// <summary>
        /// データダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ObjectList_DoubleClick(object sender, EventArgs e)
        {
            var view = sender as ListView;

            if (view.SelectedItems.Count == 0)
            {
                return;
            }

            var st = view.SelectedItems[0];

            if ((st.Tag as string) != "Object")
            {
                NowRegion = st.SubItems[1].Text;

                string dir = "";
                if (string.IsNullOrEmpty(NowBucket))
                {
                    NowBucket = st.Text;
                    SetPath("");
                }
                else
                {
                    SetPath(FullPath + (st.Text + "/"));
                    dir = FullPath;
                }

                SetDir(dir);
                SelectObject = null;
            }
        }

        /// <summary>
        /// カラムヘッダをクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ObjectList_ColumnClick(object o, ColumnClickEventArgs e)
        {
            
        }

        /// <summary>
        /// 選択物が変更された
        /// </summary>
        ListViewItem SelectObject;
        private void ObjectList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var view = sender as ListView;

            if (view.SelectedItems.Count == 0)
            {
                SelectObject = null;
                ObjectList.ContextMenuStrip = NormalMenuStrip;
                return;
            }

            SelectObject = view.SelectedItems[0];
        }

        /// <summary>
        /// 選択が外れた
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DirTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag == null)
                return;

            if ((e.Node.Tag as string).Contains("Compartment"))
            {
                LoadingPanelVisible(true);

                SetPath("");
                NowBucket = "";
                var tags = (e.Node.Tag as string).Split(',');
                NowCompartmentId = tags[1];
                SetAllBucket(NowCompartmentId);

                ObjectGUIEnable(true);
                LoadingPanelVisible(false);
            }
        }

        /// <summary>
        /// ディレクトリ情報を取得
        /// </summary>
        private void GetDireftoryInfo()
        {
            ObjectList.Items.Clear();

            List<ListViewItem> items = new List<ListViewItem>();
            while (true)
            {
                var files = DirectoryInfo.EnumerateNextFiles("*", System.IO.SearchOption.TopDirectoryOnly, 1000);
                foreach (var file in files)
                {
                    string[] item = { file.Name, NowRegion, file.Length + "", file.LastWriteTime.ToString() };
                    var viewItem = new ListViewItem(item);
                    viewItem.Tag = "Object";
                    viewItem.ImageIndex = 1;
                    viewItem.Name = file.Name;
                    items.Add(viewItem);
                    if (items.Count > 10)
                    {
                        ObjectList.Invoke((MethodInvoker)delegate
                        {
                            ObjectList.Items.AddRange(items.ToArray());
                            ObjectList.Refresh();
                        });
                        items.Clear();
                    }
                }

                if (files.Count() <= 0 || !DirectoryInfo.CheckNextPage())
                {
                    break;
                }
            }

            if (items.Count > 0)
            {
                ObjectList.Invoke((MethodInvoker)delegate
                {
                    ObjectList.Items.AddRange(items.ToArray());
                    ObjectList.Refresh();
                });
                items.Clear();
            }

            var topDirs = DirectoryInfo.EnumerateDirectories("*", System.IO.SearchOption.TopDirectoryOnly);
            foreach (var dir in topDirs)
            {
                if (!string.IsNullOrEmpty(FullPath))
                {
                    var t = dir.OriginalKey.Replace(FullPath, "").Split('/').Length;
                    if (t > 2)
                    {
                        continue;
                    }
                }
                string[] item = { dir.Name, NowRegion, "", "" };
                ObjectList.Invoke((MethodInvoker)delegate {
                    var obj = ObjectList.Items.Add(new ListViewItem(item));
                    obj.Tag = "Directory";
                    obj.ImageIndex = 0;
                    obj.Name = dir.Name;
                    ObjectList.Refresh();
                });
            }
        }

        /// <summary>
        /// 一階層上へボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrevBtn_Click(object sender, EventArgs e)
        {
            if (DirectoryInfo == null)
                return;

            if (string.IsNullOrEmpty(DirectoryInfo.OriginalKey))
            {
                NowBucket = "";
                SetPath("");
                SetAllBucket(NowCompartmentId);
            }
            else
            {
                var parent = DirectoryInfo.Parent;
                SetPath(parent.FullName.Replace(NowBucket, "").Replace(":\\", ""));
                if (!string.IsNullOrEmpty(FullPath))
                {
                    SetPath(FullPath + "/");
                }
                SetDir(FullPath);
            }
        }

        /// <summary>
        /// パスを設定する
        /// </summary>
        /// <param name="path"></param>
        private void SetPath(string path)
        {
            FullPath = path;
            BucketNameLab.Text = NowBucket + ":/";
            PathBox.Text =  FullPath;
        }

        private void PathBox_Leave(object sender, System.EventArgs e)
        {
            if (!IsConnection || string.IsNullOrEmpty(NowBucket))
            {
                return;
            }

            PathBox.Text = FullPath;
        }

        private void PathBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (!IsConnection || string.IsNullOrEmpty(NowBucket))
            {
                return;
            }

            if (e.KeyCode == Keys.Return)
            {
                var path = PathBox.Text;
                if (!path.EndsWith("/"))
                {
                    path += "/";
                }
                var checkDir = new ObjectStorageDirectoryInfo(OciResources.GetObjectStorageClient(), NameSpaceName, NowBucket, path);
                if (checkDir.Exists)
                {
                    SetPath(path);
                    SetDir(path);
                }
                else
                {
                    DialogManagement.ShowMessageDialog("エラー", $"{path} が見つかりませんでした", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    PathBox.SelectAll();
                }
            }
        }

        /// <summary>
        /// 接続設定ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingBtn_Click(object sender, EventArgs e)
        {
            SettingFormShow();
        }

        UserSettingForm UserSettingForm;
        private bool UserSetting = false;

        /// <summary>
        /// 設定フォーム表示
        /// </summary>
        private void SettingFormShow()
        {
            if (UserSetting)
                return;
            UserSetting = true;
            UserSettingForm = new UserSettingForm();
            UserSettingForm.Show();
            UserSettingForm.FormClosed += new FormClosedEventHandler(SettingForm_FormClosed);
        }

        /// <summary>
        /// 設定フォームが閉じられた
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            UserSettingForm.Dispose();
            UserSetting = false;
            OciResources.SettingReset();
        }

        /// <summary>
        /// アップロードボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Upload_Click(object sender, EventArgs e)
        {
            if (!IsConnection)
            {
                return;
            }

            if (string.IsNullOrEmpty(NowBucket))
            {
                DialogManagement.ShowMessageDialog("警告", "バケットを選択してください", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            UploadForm uploadForm = new UploadForm(OciResources, NameSpaceName, NowBucket, FullPath, NowRegion);
            if (uploadForm.ShowDialog() == DialogResult.OK)
            {
                SetDir(FullPath);
            }
        }

        /// <summary>
        /// プロパティボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Proparties_Click(object sender, EventArgs e)
        {
            if (!CheckSelectItem())
            {
                DialogManagement.ShowMessageDialog("警告", "アイテムを指定してください", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if ((SelectObject.Tag as string) == "Object")
            {
                var head = OciResources.GetObjectHead(NameSpaceName, NowBucket, FullPath + SelectObject.Text, SelectObject.SubItems[1].Text);
                PropartiesForm propartiesForm = new PropartiesForm();
                propartiesForm.SetObject(head, SelectObject.Text, SelectObject.SubItems[1].Text);
                propartiesForm.Show();
            }
            else if ((SelectObject.Tag as string) == "Bucket")
            {
                var head = OciResources.GetBucketHead(NameSpaceName, SelectObject.Text, SelectObject.SubItems[1].Text);
                PropartiesForm propartiesForm = new PropartiesForm();
                propartiesForm.SetBucket(head, SelectObject.Text, SelectObject.SubItems[1].Text);
                propartiesForm.Show();
            }
            else if ((SelectObject.Tag as string) == "Directory")
            {
                PropartiesForm propartiesForm = new PropartiesForm();
                propartiesForm.SetDirectory(SelectObject.Text, FullPath, SelectObject.SubItems[1].Text);
                propartiesForm.Show();
            }
        }

        /// <summary>
        /// 削除ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void Delete_Click(object sender, EventArgs e)
        {
            if (!CheckSelectItem())
            {
                DialogManagement.ShowMessageDialog("警告", "アイテムを指定してください", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            bool res = true;
            if (ObjectList.SelectedItems.Count > 1)
            {
                DialogResult resulet = DialogManagement.ShowMessageDialog("確認", $"選択されたすべてを削除します。よろしいですか？", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                if (resulet != DialogResult.Yes)
                {
                    return;
                }

                var targets = ObjectList.SelectedItems.Cast<ListViewItem>();
                foreach (var item in targets)
                {
                    DeleteItem(item, true);
                }

                if (string.IsNullOrEmpty(NowBucket))
                {
                    SetAllBucket(NowCompartmentId);
                }
                else
                {
                    SetDir(FullPath);
                }
            }
            else if (ObjectList.SelectedItems.Count == 1)
            {
                string target = (SelectObject.Tag as string);

                DialogResult resulet = DialogManagement.ShowMessageDialog("確認", $"{target} [ {SelectObject.Text} ] を削除しますか？", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                if (resulet != DialogResult.Yes)
                {
                    return;
                }

                res = DeleteItem(SelectObject, false);
            }

            if(res)
            {
                if (string.IsNullOrEmpty(NowBucket))
                {
                    SetAllBucket(NowCompartmentId);
                }
                else
                {
                    SetDir(FullPath);
                }
            }
        }

        private bool DeleteItem(ListViewItem targetItem, bool force)
        {
            bool res = false;
            var name = targetItem.Text;
            if ((targetItem.Tag as string) == "Object")
            {
                res = OciResources.DeleteObject(NameSpaceName, NowBucket, FullPath + name, NowRegion);
            }
            else if ((targetItem.Tag as string) == "Bucket")
            {
                if (!force)
                {
                    DialogResult resulet = DialogManagement.ShowMessageDialog("確認", $"バケット {name} 内すべてのオブジェクトが削除されます。よろしいですか？", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                    if (resulet != DialogResult.Yes)
                    {
                        return false;
                    }
                }

                var region = targetItem.SubItems[1].Text;
                res = OciResources.DeleteBucket(NameSpaceName, name, region);
                if (res)
                {
                    SetAllBucket(NowCompartmentId);
                }
            }
            else if ((targetItem.Tag as string) == "Directory")
            {
                if (!force)
                {
                    DialogResult resulet = DialogManagement.ShowMessageDialog("確認", $"ディレクトリ {name} 内すべてのオブジェクトが削除されます。よろしいですか？", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                    if (resulet != DialogResult.Yes)
                    {
                        return false;
                    }
                }

                res = OciResources.DeleteDirectory(NameSpaceName, NowBucket, FullPath + name, NowRegion);
            }

            if (res)
            {
                return true;
            }
            else
            {
                DialogManagement.ShowMessageDialog("エラー", $"{name} の削除に失敗しました", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// ダウンロードボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuDownload_Click(object sender, EventArgs e)
        {
            if (!CheckSelectItem())
            {
                DialogManagement.ShowMessageDialog("警告", "アイテムを指定してください", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if ((SelectObject.Tag as string) == "Object")
            {
                DownloadForm downloadForm = new DownloadForm(OciResources, NameSpaceName, NowBucket, FullPath + SelectObject.Text, SelectObject.SubItems[1].Text);
                downloadForm.Show();
            }
            else if ((SelectObject.Tag as string) == "Bucket")
            {
                DownloadForm downloadForm = new DownloadForm(OciResources, NameSpaceName, SelectObject.Text, "", SelectObject.SubItems[1].Text);
                downloadForm.Show();
            }
            else if ((SelectObject.Tag as string) == "Directory")
            {
                DownloadForm downloadForm = new DownloadForm(OciResources, NameSpaceName, NowBucket, FullPath + SelectObject.Text + "/", SelectObject.SubItems[1].Text);
                downloadForm.Show();
            }
        }

        /// <summary>
        /// 作成ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuNew_Click(object sender, EventArgs e)
        {
            if (!IsConnection || string.IsNullOrEmpty(NowCompartmentId))
            {
                return;
            }

            if (string.IsNullOrEmpty(NowBucket))
            {
                // new bucket
                NewAddForm newAddForm = new NewAddForm(OciResources, NameSpaceName, "", "", "", NowCompartmentId);
                if (newAddForm.ShowDialog() == DialogResult.OK)
                {
                    SetAllBucket(NowCompartmentId);
                }
            }
            else
            {
                // new Directory
                NewAddForm newAddForm = new NewAddForm(OciResources, NameSpaceName, NowBucket, FullPath, NowRegion, NowCompartmentId);
                if (newAddForm.ShowDialog() == DialogResult.OK)
                {
                    SetDir(FullPath);
                }
            }
        }

        /// <summary>
        /// 選択したアイテムが操作可能かチェックする
        /// </summary>
        /// <returns></returns>
        private bool CheckSelectItem()
        {
            if (SelectObject == null ||
                !((SelectObject.Tag as string) == "Object" || (SelectObject.Tag as string) == "Bucket" || (SelectObject.Tag as string) == "Directory"))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 名前変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuRename_Click(object sender, EventArgs e)
        {
            if((SelectObject.Tag as string) == "Bucket")
            {
                DialogManagement.ShowMessageDialog("警告", "バケットの名称は変更できません", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            if ((SelectObject.Tag as string) == "Object")
            {
                InputForm inputForm = new InputForm("オブジェクト名を入力してください", SelectObject.Text);
                inputForm.ShowDialog(this);
                if (inputForm.DialogResult == DialogResult.OK)
                {
                    var text = inputForm.TextValue;
                    if (text != SelectObject.Text)
                    {
                        string now = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                        if (OciResources.RenameObject(NameSpaceName, NowBucket, FullPath + SelectObject.Text, NowRegion, FullPath + text))
                        {
                            if (text.Contains("/"))
                            {
                                SetDir(FullPath);
                            }
                            else
                            {
                                SelectObject.Text = text;
                                SelectObject.SubItems[3].Text = now;
                            }
                        }
                        else
                        {
                            DialogManagement.ShowMessageDialog("エラー", "名前の変更に失敗しました", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else if ((SelectObject.Tag as string) == "Directory")
            {
                InputForm inputForm = new InputForm("ディレクトリ名を入力してください", SelectObject.Text);
                inputForm.ShowDialog(this);
                if (inputForm.DialogResult == DialogResult.OK)
                {
                    var text = inputForm.TextValue;
                    if (text == SelectObject.Text)
                    {
                        return;
                    }
                    if (text.StartsWith("/"))
                    {
                        text = text.Remove(0, 1);
                    }
                    if (!text.EndsWith("/"))
                    {
                        text += "/";
                    }
                    List<string> failedFiles = new List<string>();
                    OciResources.GetObjectStorageClient().SetRegion(NowRegion);
                    var directoryInfo = new ObjectStorageDirectoryInfo(OciResources.GetObjectStorageClient(), NameSpaceName, NowBucket, FullPath + SelectObject.Text);
                    var files = directoryInfo.EnumerateFiles("*", System.IO.SearchOption.AllDirectories);
                    var result = DialogManagement.ShowMessageDialog("警告", "ディレクトリ下のすべてのファイルとディレクトリが変更されます。\nこの処理にはしばらくかかります。\n実行しますか？", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    if (result != DialogResult.Yes)
                    {
                        return;
                    }
                    LoadingPanelVisible(true);
                    foreach (var file in files)
                    {
                        var sourceName = file.OriginalKey;
                        var newName = text + file.OriginalKey.Replace(FullPath + SelectObject.Text + "/", "");
                        if (!OciResources.RenameObject(NameSpaceName, NowBucket, sourceName, NowRegion, newName))
                        {
                            failedFiles.Add(file.FullName);
                        }
                    }
                    LoadingPanelVisible(false);
                    if (failedFiles.Count > 0)
                    {
                        var message = "";
                        foreach (var name in failedFiles)
                        {
                            message += $"{name}\n";
                        }
                        DialogManagement.ShowMessageDialog("エラー", message + "の名前変更ができませんでした", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    if (ObjectList.Items.Find(inputForm.TextValue, false).Length > 0)
                    {
                        ObjectList.Items.Remove(SelectObject);
                    }
                    else
                    {
                        SelectObject.Text = inputForm.TextValue;
                    }
                    inputForm.Dispose();
                }
            }
        }

        private void NormalMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            if (!CheckSelectItem())
            {
                MenuDelete.Visible = false;
                MenuProparties.Visible = false;
                MenuNew.Visible = true;
                MenuRename.Visible = false;

                return;
            }

            MenuRename.Visible = true;
            MenuNew.Visible = false;
            MenuDelete.Visible = true;
            MenuProparties.Visible = true;

        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            if (!IsConnection)
            {
                return;
            }

            if (string.IsNullOrEmpty(NowBucket))
            {
                SetAllBucket(NowCompartmentId);
            }
            else
            {
                SetDir(FullPath);
            }
        }

        private void ObjectGUIEnable(bool enable)
        {
            UpdateBtn.Enabled = enable;
            PathBox.Enabled = enable;
            PrevBtn.Enabled = enable;
            ObjectList.Enabled = enable;
        }
    }
}
