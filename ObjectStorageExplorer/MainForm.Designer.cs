namespace ObjectStorageExplorer
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("NoConnecton");
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.NormalMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuUpload = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuNew = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuDownload = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuProparties = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SettingBtn = new System.Windows.Forms.Button();
            this.ConnectionBtn = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.BucketNameLab = new System.Windows.Forms.Label();
            this.ProgressPanel = new System.Windows.Forms.Panel();
            this.ProgressLab = new System.Windows.Forms.Label();
            this.UpdateBtn = new System.Windows.Forms.Button();
            this.ObjectList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DirTree = new System.Windows.Forms.TreeView();
            this.PathBox = new System.Windows.Forms.TextBox();
            this.PrevBtn = new System.Windows.Forms.Button();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MenuRename = new System.Windows.Forms.ToolStripMenuItem();
            this.NormalMenuStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.ProgressPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "folder.ico");
            this.imageList1.Images.SetKeyName(1, "text_icon.ico");
            this.imageList1.Images.SetKeyName(2, "bucket.ico");
            // 
            // NormalMenuStrip
            // 
            this.NormalMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuUpload,
            this.MenuNew,
            this.MenuDownload,
            this.MenuDelete,
            this.MenuRename,
            this.MenuProparties});
            this.NormalMenuStrip.Name = "contextMenuStrip1";
            this.NormalMenuStrip.Size = new System.Drawing.Size(129, 136);
            this.NormalMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.NormalMenuStrip_Opening);
            // 
            // MenuUpload
            // 
            this.MenuUpload.Name = "MenuUpload";
            this.MenuUpload.Size = new System.Drawing.Size(180, 22);
            this.MenuUpload.Text = "Upload";
            this.MenuUpload.Click += new System.EventHandler(this.Upload_Click);
            // 
            // MenuNew
            // 
            this.MenuNew.Name = "MenuNew";
            this.MenuNew.Size = new System.Drawing.Size(180, 22);
            this.MenuNew.Text = "New";
            this.MenuNew.Click += new System.EventHandler(this.MenuNew_Click);
            // 
            // MenuDownload
            // 
            this.MenuDownload.Name = "MenuDownload";
            this.MenuDownload.Size = new System.Drawing.Size(180, 22);
            this.MenuDownload.Text = "Download";
            this.MenuDownload.Click += new System.EventHandler(this.MenuDownload_Click);
            // 
            // MenuDelete
            // 
            this.MenuDelete.Name = "MenuDelete";
            this.MenuDelete.Size = new System.Drawing.Size(180, 22);
            this.MenuDelete.Text = "Delete";
            this.MenuDelete.Click += new System.EventHandler(this.Delete_Click);
            // 
            // MenuProparties
            // 
            this.MenuProparties.Name = "MenuProparties";
            this.MenuProparties.Size = new System.Drawing.Size(180, 22);
            this.MenuProparties.Text = "Proparties";
            this.MenuProparties.Click += new System.EventHandler(this.Proparties_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.SettingBtn);
            this.panel1.Controls.Add(this.ConnectionBtn);
            this.panel1.Location = new System.Drawing.Point(4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(916, 31);
            this.panel1.TabIndex = 8;
            // 
            // SettingBtn
            // 
            this.SettingBtn.Location = new System.Drawing.Point(96, 3);
            this.SettingBtn.Name = "SettingBtn";
            this.SettingBtn.Size = new System.Drawing.Size(75, 25);
            this.SettingBtn.TabIndex = 8;
            this.SettingBtn.Text = "Setting";
            this.SettingBtn.UseVisualStyleBackColor = true;
            this.SettingBtn.Click += new System.EventHandler(this.SettingBtn_Click);
            // 
            // ConnectionBtn
            // 
            this.ConnectionBtn.Location = new System.Drawing.Point(5, 3);
            this.ConnectionBtn.Name = "ConnectionBtn";
            this.ConnectionBtn.Size = new System.Drawing.Size(85, 25);
            this.ConnectionBtn.TabIndex = 7;
            this.ConnectionBtn.Text = "Connection";
            this.ConnectionBtn.UseVisualStyleBackColor = true;
            this.ConnectionBtn.Click += new System.EventHandler(this.ConnectionBtn_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.BucketNameLab);
            this.panel2.Controls.Add(this.ProgressPanel);
            this.panel2.Controls.Add(this.UpdateBtn);
            this.panel2.Controls.Add(this.ObjectList);
            this.panel2.Controls.Add(this.DirTree);
            this.panel2.Controls.Add(this.PathBox);
            this.panel2.Controls.Add(this.PrevBtn);
            this.panel2.Location = new System.Drawing.Point(7, 38);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(912, 545);
            this.panel2.TabIndex = 9;
            // 
            // BucketNameLab
            // 
            this.BucketNameLab.AutoSize = true;
            this.BucketNameLab.Location = new System.Drawing.Point(362, 9);
            this.BucketNameLab.Name = "BucketNameLab";
            this.BucketNameLab.Size = new System.Drawing.Size(55, 13);
            this.BucketNameLab.TabIndex = 13;
            this.BucketNameLab.Text = "NoBucket";
            // 
            // ProgressPanel
            // 
            this.ProgressPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ProgressPanel.Controls.Add(this.ProgressLab);
            this.ProgressPanel.Location = new System.Drawing.Point(241, 231);
            this.ProgressPanel.Name = "ProgressPanel";
            this.ProgressPanel.Size = new System.Drawing.Size(441, 69);
            this.ProgressPanel.TabIndex = 10;
            this.ProgressPanel.Visible = false;
            // 
            // ProgressLab
            // 
            this.ProgressLab.AutoSize = true;
            this.ProgressLab.Location = new System.Drawing.Point(193, 29);
            this.ProgressLab.Name = "ProgressLab";
            this.ProgressLab.Size = new System.Drawing.Size(54, 13);
            this.ProgressLab.TabIndex = 0;
            this.ProgressLab.Text = "Loading...";
            // 
            // UpdateBtn
            // 
            this.UpdateBtn.Enabled = false;
            this.UpdateBtn.Image = global::ObjectStorageExplorer.Properties.Resources.update;
            this.UpdateBtn.Location = new System.Drawing.Point(860, 13);
            this.UpdateBtn.Name = "UpdateBtn";
            this.UpdateBtn.Size = new System.Drawing.Size(40, 26);
            this.UpdateBtn.TabIndex = 12;
            this.UpdateBtn.UseVisualStyleBackColor = true;
            this.UpdateBtn.Click += new System.EventHandler(this.UpdateBtn_Click);
            // 
            // ObjectList
            // 
            this.ObjectList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader3,
            this.columnHeader2,
            this.columnHeader4});
            this.ObjectList.Enabled = false;
            this.ObjectList.FullRowSelect = true;
            this.ObjectList.GridLines = true;
            this.ObjectList.HideSelection = false;
            this.ObjectList.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.ObjectList.LargeImageList = this.imageList1;
            this.ObjectList.Location = new System.Drawing.Point(315, 56);
            this.ObjectList.Name = "ObjectList";
            this.ObjectList.ShowItemToolTips = true;
            this.ObjectList.Size = new System.Drawing.Size(587, 477);
            this.ObjectList.SmallImageList = this.imageList1;
            this.ObjectList.TabIndex = 9;
            this.ObjectList.UseCompatibleStateImageBehavior = false;
            this.ObjectList.View = System.Windows.Forms.View.Details;
            this.ObjectList.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ObjectList_ColumnClick);
            this.ObjectList.SelectedIndexChanged += new System.EventHandler(this.ObjectList_SelectedIndexChanged);
            this.ObjectList.DoubleClick += new System.EventHandler(this.ObjectList_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 200;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Region";
            this.columnHeader3.Width = 80;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Size";
            this.columnHeader2.Width = 100;
            // 
            // DirTree
            // 
            this.DirTree.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DirTree.Location = new System.Drawing.Point(12, 13);
            this.DirTree.Name = "DirTree";
            this.DirTree.Size = new System.Drawing.Size(269, 520);
            this.DirTree.TabIndex = 8;
            this.DirTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.DirTree_AfterSelect);
            // 
            // PathBox
            // 
            this.PathBox.Enabled = false;
            this.PathBox.Location = new System.Drawing.Point(365, 30);
            this.PathBox.Name = "PathBox";
            this.PathBox.Size = new System.Drawing.Size(422, 20);
            this.PathBox.TabIndex = 11;
            this.PathBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PathBox_KeyDown);
            this.PathBox.Leave += new System.EventHandler(this.PathBox_Leave);
            // 
            // PrevBtn
            // 
            this.PrevBtn.Enabled = false;
            this.PrevBtn.Location = new System.Drawing.Point(313, 11);
            this.PrevBtn.Name = "PrevBtn";
            this.PrevBtn.Size = new System.Drawing.Size(43, 30);
            this.PrevBtn.TabIndex = 10;
            this.PrevBtn.Text = "prev";
            this.PrevBtn.UseVisualStyleBackColor = true;
            this.PrevBtn.Click += new System.EventHandler(this.PrevBtn_Click);
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "LastModified";
            this.columnHeader4.Width = 200;
            // 
            // MenuRename
            // 
            this.MenuRename.Name = "MenuRename";
            this.MenuRename.Size = new System.Drawing.Size(180, 22);
            this.MenuRename.Text = "Rename";
            this.MenuRename.Click += new System.EventHandler(this.MenuRename_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(921, 588);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "MainForm";
            this.Text = "ObjectStorageExplorer";
            this.NormalMenuStrip.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ProgressPanel.ResumeLayout(false);
            this.ProgressPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ContextMenuStrip NormalMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem MenuUpload;
        private System.Windows.Forms.ToolStripMenuItem MenuDelete;
        private System.Windows.Forms.ToolStripMenuItem MenuProparties;
        private System.Windows.Forms.ToolStripMenuItem MenuDownload;
        private System.Windows.Forms.ToolStripMenuItem MenuNew;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button SettingBtn;
        private System.Windows.Forms.Button ConnectionBtn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListView ObjectList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.TreeView DirTree;
        private System.Windows.Forms.TextBox PathBox;
        private System.Windows.Forms.Button PrevBtn;
        private System.Windows.Forms.Panel ProgressPanel;
        private System.Windows.Forms.Label ProgressLab;
        private System.Windows.Forms.Button UpdateBtn;
        private System.Windows.Forms.Label BucketNameLab;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ToolStripMenuItem MenuRename;
    }
}

