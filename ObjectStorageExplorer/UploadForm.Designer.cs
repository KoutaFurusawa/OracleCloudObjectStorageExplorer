namespace ObjectStorageExplorer
{
    partial class UploadForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.FileListView = new System.Windows.Forms.ListView();
            this.FileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FolderSelectBtn = new System.Windows.Forms.Button();
            this.UploadBtn = new System.Windows.Forms.Button();
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.FileSelectBtn = new System.Windows.Forms.Button();
            this.TopDirectoryCheck = new System.Windows.Forms.CheckBox();
            this.AllClearBtn = new System.Windows.Forms.Button();
            this.MenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DeleteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PathLab = new System.Windows.Forms.Label();
            this.OptionSaveCheckBox = new System.Windows.Forms.GroupBox();
            this.OptionSaveCheckButton = new System.Windows.Forms.RadioButton();
            this.OptionForceSaveButton = new System.Windows.Forms.RadioButton();
            this.MenuStrip.SuspendLayout();
            this.OptionSaveCheckBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // FileListView
            // 
            this.FileListView.BackColor = System.Drawing.Color.White;
            this.FileListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.FileName,
            this.Status});
            this.FileListView.GridLines = true;
            this.FileListView.HideSelection = false;
            this.FileListView.Location = new System.Drawing.Point(17, 38);
            this.FileListView.Name = "FileListView";
            this.FileListView.Size = new System.Drawing.Size(541, 387);
            this.FileListView.TabIndex = 0;
            this.FileListView.UseCompatibleStateImageBehavior = false;
            this.FileListView.View = System.Windows.Forms.View.Details;
            // 
            // FileName
            // 
            this.FileName.Text = "FileName";
            this.FileName.Width = 400;
            // 
            // Status
            // 
            this.Status.Text = "Status";
            this.Status.Width = 80;
            // 
            // FolderSelectBtn
            // 
            this.FolderSelectBtn.Location = new System.Drawing.Point(17, 431);
            this.FolderSelectBtn.Name = "FolderSelectBtn";
            this.FolderSelectBtn.Size = new System.Drawing.Size(95, 32);
            this.FolderSelectBtn.TabIndex = 3;
            this.FolderSelectBtn.Text = "フォルダから追加";
            this.FolderSelectBtn.UseVisualStyleBackColor = true;
            this.FolderSelectBtn.Click += new System.EventHandler(this.FolderSelectBtn_Click);
            // 
            // UploadBtn
            // 
            this.UploadBtn.Location = new System.Drawing.Point(363, 619);
            this.UploadBtn.Name = "UploadBtn";
            this.UploadBtn.Size = new System.Drawing.Size(89, 41);
            this.UploadBtn.TabIndex = 4;
            this.UploadBtn.Text = "Upload";
            this.UploadBtn.UseVisualStyleBackColor = true;
            this.UploadBtn.Click += new System.EventHandler(this.UploadBtn_Click);
            // 
            // ProgressBar
            // 
            this.ProgressBar.Location = new System.Drawing.Point(51, 584);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(494, 29);
            this.ProgressBar.TabIndex = 5;
            // 
            // CancelBtn
            // 
            this.CancelBtn.Location = new System.Drawing.Point(470, 619);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(83, 41);
            this.CancelBtn.TabIndex = 6;
            this.CancelBtn.Text = "Close";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // FileSelectBtn
            // 
            this.FileSelectBtn.Location = new System.Drawing.Point(129, 431);
            this.FileSelectBtn.Name = "FileSelectBtn";
            this.FileSelectBtn.Size = new System.Drawing.Size(85, 32);
            this.FileSelectBtn.TabIndex = 7;
            this.FileSelectBtn.Text = "ファイルを追加";
            this.FileSelectBtn.UseVisualStyleBackColor = true;
            this.FileSelectBtn.Click += new System.EventHandler(this.FileSelectBtn_Click);
            // 
            // TopDirectoryCheck
            // 
            this.TopDirectoryCheck.AutoSize = true;
            this.TopDirectoryCheck.Location = new System.Drawing.Point(25, 469);
            this.TopDirectoryCheck.Name = "TopDirectoryCheck";
            this.TopDirectoryCheck.Size = new System.Drawing.Size(120, 17);
            this.TopDirectoryCheck.TabIndex = 8;
            this.TopDirectoryCheck.Text = "ディレクトリ直下のみ";
            this.TopDirectoryCheck.UseVisualStyleBackColor = true;
            // 
            // AllClearBtn
            // 
            this.AllClearBtn.Location = new System.Drawing.Point(481, 431);
            this.AllClearBtn.Name = "AllClearBtn";
            this.AllClearBtn.Size = new System.Drawing.Size(77, 32);
            this.AllClearBtn.TabIndex = 9;
            this.AllClearBtn.Text = "AllClear";
            this.AllClearBtn.UseVisualStyleBackColor = true;
            this.AllClearBtn.Click += new System.EventHandler(this.AllClearBtn_Click);
            // 
            // MenuStrip
            // 
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteMenuItem});
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(108, 26);
            // 
            // DeleteMenuItem
            // 
            this.DeleteMenuItem.Name = "DeleteMenuItem";
            this.DeleteMenuItem.Size = new System.Drawing.Size(107, 22);
            this.DeleteMenuItem.Text = "Delete";
            this.DeleteMenuItem.Click += new System.EventHandler(this.DeleteMenuItem_Click);
            // 
            // PathLab
            // 
            this.PathLab.AutoSize = true;
            this.PathLab.Location = new System.Drawing.Point(22, 14);
            this.PathLab.Name = "PathLab";
            this.PathLab.Size = new System.Drawing.Size(35, 13);
            this.PathLab.TabIndex = 11;
            this.PathLab.Text = "label2";
            // 
            // OptionSaveCheckBox
            // 
            this.OptionSaveCheckBox.Controls.Add(this.OptionSaveCheckButton);
            this.OptionSaveCheckBox.Controls.Add(this.OptionForceSaveButton);
            this.OptionSaveCheckBox.Location = new System.Drawing.Point(17, 502);
            this.OptionSaveCheckBox.Name = "OptionSaveCheckBox";
            this.OptionSaveCheckBox.Size = new System.Drawing.Size(197, 76);
            this.OptionSaveCheckBox.TabIndex = 12;
            this.OptionSaveCheckBox.TabStop = false;
            this.OptionSaveCheckBox.Text = "確認設定";
            // 
            // OptionSaveCheckButton
            // 
            this.OptionSaveCheckButton.AutoSize = true;
            this.OptionSaveCheckButton.Location = new System.Drawing.Point(21, 19);
            this.OptionSaveCheckButton.Name = "OptionSaveCheckButton";
            this.OptionSaveCheckButton.Size = new System.Drawing.Size(110, 17);
            this.OptionSaveCheckButton.TabIndex = 15;
            this.OptionSaveCheckButton.Text = "上書きを確認する";
            this.OptionSaveCheckButton.UseVisualStyleBackColor = true;
            // 
            // OptionForceSaveButton
            // 
            this.OptionForceSaveButton.AutoSize = true;
            this.OptionForceSaveButton.Checked = true;
            this.OptionForceSaveButton.Location = new System.Drawing.Point(21, 42);
            this.OptionForceSaveButton.Name = "OptionForceSaveButton";
            this.OptionForceSaveButton.Size = new System.Drawing.Size(103, 17);
            this.OptionForceSaveButton.TabIndex = 14;
            this.OptionForceSaveButton.TabStop = true;
            this.OptionForceSaveButton.Text = "強制的に上書き";
            this.OptionForceSaveButton.UseVisualStyleBackColor = true;
            // 
            // UploadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 673);
            this.Controls.Add(this.OptionSaveCheckBox);
            this.Controls.Add(this.PathLab);
            this.Controls.Add(this.AllClearBtn);
            this.Controls.Add(this.TopDirectoryCheck);
            this.Controls.Add(this.FileSelectBtn);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.UploadBtn);
            this.Controls.Add(this.FolderSelectBtn);
            this.Controls.Add(this.FileListView);
            this.Name = "UploadForm";
            this.Text = "UploadFiles";
            this.MenuStrip.ResumeLayout(false);
            this.OptionSaveCheckBox.ResumeLayout(false);
            this.OptionSaveCheckBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView FileListView;
        private System.Windows.Forms.ColumnHeader FileName;
        private System.Windows.Forms.ColumnHeader Status;
        private System.Windows.Forms.Button FolderSelectBtn;
        private System.Windows.Forms.Button UploadBtn;
        private System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.Button FileSelectBtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.CheckBox TopDirectoryCheck;
        private System.Windows.Forms.Button AllClearBtn;
        private System.Windows.Forms.ContextMenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem DeleteMenuItem;
        private System.Windows.Forms.Label PathLab;
        private System.Windows.Forms.GroupBox OptionSaveCheckBox;
        private System.Windows.Forms.RadioButton OptionSaveCheckButton;
        private System.Windows.Forms.RadioButton OptionForceSaveButton;
    }
}