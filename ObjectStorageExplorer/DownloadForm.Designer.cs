namespace ObjectStorageExplorer
{
    partial class DownloadForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.SavePathBox = new System.Windows.Forms.TextBox();
            this.SelectButton = new System.Windows.Forms.Button();
            this.PathLab = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.OptionFilenameSetButton = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.OptionRadioButtonB = new System.Windows.Forms.RadioButton();
            this.OptionRadioButtonA = new System.Windows.Forms.RadioButton();
            this.DownloadButton = new System.Windows.Forms.Button();
            this.OptionSaveCheckBox = new System.Windows.Forms.GroupBox();
            this.OptionSaveCheckButton = new System.Windows.Forms.RadioButton();
            this.OptionForceSaveButton = new System.Windows.Forms.RadioButton();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.DownloadNowLab = new System.Windows.Forms.Label();
            this.AutoCloseCheck = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.OptionSaveCheckBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "SavePath";
            // 
            // SavePathBox
            // 
            this.SavePathBox.Location = new System.Drawing.Point(75, 59);
            this.SavePathBox.Name = "SavePathBox";
            this.SavePathBox.Size = new System.Drawing.Size(360, 20);
            this.SavePathBox.TabIndex = 1;
            // 
            // SelectButton
            // 
            this.SelectButton.Location = new System.Drawing.Point(441, 59);
            this.SelectButton.Name = "SelectButton";
            this.SelectButton.Size = new System.Drawing.Size(34, 20);
            this.SelectButton.TabIndex = 2;
            this.SelectButton.Text = "...";
            this.SelectButton.UseVisualStyleBackColor = true;
            this.SelectButton.Click += new System.EventHandler(this.SelectButton_Click);
            // 
            // PathLab
            // 
            this.PathLab.AutoSize = true;
            this.PathLab.Location = new System.Drawing.Point(19, 23);
            this.PathLab.Name = "PathLab";
            this.PathLab.Size = new System.Drawing.Size(89, 13);
            this.PathLab.TabIndex = 3;
            this.PathLab.Text = "Object Download";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.OptionFilenameSetButton);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.OptionRadioButtonB);
            this.groupBox1.Controls.Add(this.OptionRadioButtonA);
            this.groupBox1.Location = new System.Drawing.Point(54, 96);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(360, 117);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "保存方法設定";
            // 
            // OptionFilenameSetButton
            // 
            this.OptionFilenameSetButton.AutoSize = true;
            this.OptionFilenameSetButton.Location = new System.Drawing.Point(21, 81);
            this.OptionFilenameSetButton.Name = "OptionFilenameSetButton";
            this.OptionFilenameSetButton.Size = new System.Drawing.Size(178, 17);
            this.OptionFilenameSetButton.TabIndex = 9;
            this.OptionFilenameSetButton.Text = "指定したファイル名でダウンロード";
            this.OptionFilenameSetButton.UseVisualStyleBackColor = true;
            this.OptionFilenameSetButton.CheckedChanged += new System.EventHandler(this.OptionFilenameSetButton_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(218, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "bucket/dir/object.txt -> bucket_dir_object.txt";
            // 
            // OptionRadioButtonB
            // 
            this.OptionRadioButtonB.AutoSize = true;
            this.OptionRadioButtonB.Location = new System.Drawing.Point(21, 42);
            this.OptionRadioButtonB.Name = "OptionRadioButtonB";
            this.OptionRadioButtonB.Size = new System.Drawing.Size(67, 17);
            this.OptionRadioButtonB.TabIndex = 7;
            this.OptionRadioButtonB.Text = "OCI準拠";
            this.OptionRadioButtonB.UseVisualStyleBackColor = true;
            this.OptionRadioButtonB.CheckedChanged += new System.EventHandler(this.OptionRadioButtonB_CheckedChanged);
            // 
            // OptionRadioButtonA
            // 
            this.OptionRadioButtonA.AutoSize = true;
            this.OptionRadioButtonA.Checked = true;
            this.OptionRadioButtonA.Location = new System.Drawing.Point(21, 19);
            this.OptionRadioButtonA.Name = "OptionRadioButtonA";
            this.OptionRadioButtonA.Size = new System.Drawing.Size(173, 17);
            this.OptionRadioButtonA.TabIndex = 6;
            this.OptionRadioButtonA.TabStop = true;
            this.OptionRadioButtonA.Text = "階層を維持したままダウンロード";
            this.OptionRadioButtonA.UseVisualStyleBackColor = true;
            this.OptionRadioButtonA.CheckedChanged += new System.EventHandler(this.OptionRadioButtonA_CheckedChanged);
            // 
            // DownloadButton
            // 
            this.DownloadButton.Location = new System.Drawing.Point(240, 370);
            this.DownloadButton.Name = "DownloadButton";
            this.DownloadButton.Size = new System.Drawing.Size(116, 40);
            this.DownloadButton.TabIndex = 7;
            this.DownloadButton.Text = "Download";
            this.DownloadButton.UseVisualStyleBackColor = true;
            this.DownloadButton.Click += new System.EventHandler(this.DownloadButton_Click);
            // 
            // OptionSaveCheckBox
            // 
            this.OptionSaveCheckBox.Controls.Add(this.OptionSaveCheckButton);
            this.OptionSaveCheckBox.Controls.Add(this.OptionForceSaveButton);
            this.OptionSaveCheckBox.Location = new System.Drawing.Point(54, 219);
            this.OptionSaveCheckBox.Name = "OptionSaveCheckBox";
            this.OptionSaveCheckBox.Size = new System.Drawing.Size(359, 76);
            this.OptionSaveCheckBox.TabIndex = 8;
            this.OptionSaveCheckBox.TabStop = false;
            this.OptionSaveCheckBox.Text = "確認設定";
            // 
            // OptionSaveCheckButton
            // 
            this.OptionSaveCheckButton.AutoSize = true;
            this.OptionSaveCheckButton.Checked = true;
            this.OptionSaveCheckButton.Location = new System.Drawing.Point(21, 19);
            this.OptionSaveCheckButton.Name = "OptionSaveCheckButton";
            this.OptionSaveCheckButton.Size = new System.Drawing.Size(110, 17);
            this.OptionSaveCheckButton.TabIndex = 15;
            this.OptionSaveCheckButton.TabStop = true;
            this.OptionSaveCheckButton.Text = "上書きを確認する";
            this.OptionSaveCheckButton.UseVisualStyleBackColor = true;
            // 
            // OptionForceSaveButton
            // 
            this.OptionForceSaveButton.AutoSize = true;
            this.OptionForceSaveButton.Location = new System.Drawing.Point(21, 42);
            this.OptionForceSaveButton.Name = "OptionForceSaveButton";
            this.OptionForceSaveButton.Size = new System.Drawing.Size(103, 17);
            this.OptionForceSaveButton.TabIndex = 14;
            this.OptionForceSaveButton.Text = "強制的に上書き";
            this.OptionForceSaveButton.UseVisualStyleBackColor = true;
            // 
            // CancelBtn
            // 
            this.CancelBtn.Location = new System.Drawing.Point(379, 370);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(96, 42);
            this.CancelBtn.TabIndex = 9;
            this.CancelBtn.Text = "CLose";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(54, 310);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(421, 18);
            this.progressBar1.TabIndex = 10;
            // 
            // DownloadNowLab
            // 
            this.DownloadNowLab.AutoSize = true;
            this.DownloadNowLab.Location = new System.Drawing.Point(51, 331);
            this.DownloadNowLab.Name = "DownloadNowLab";
            this.DownloadNowLab.Size = new System.Drawing.Size(0, 13);
            this.DownloadNowLab.TabIndex = 11;
            // 
            // AutoCloseCheck
            // 
            this.AutoCloseCheck.AutoSize = true;
            this.AutoCloseCheck.Checked = true;
            this.AutoCloseCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AutoCloseCheck.Location = new System.Drawing.Point(240, 419);
            this.AutoCloseCheck.Name = "AutoCloseCheck";
            this.AutoCloseCheck.Size = new System.Drawing.Size(140, 17);
            this.AutoCloseCheck.TabIndex = 12;
            this.AutoCloseCheck.Text = "完了したら自動で閉じる";
            this.AutoCloseCheck.UseVisualStyleBackColor = true;
            // 
            // DownloadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 448);
            this.Controls.Add(this.AutoCloseCheck);
            this.Controls.Add(this.DownloadNowLab);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.OptionSaveCheckBox);
            this.Controls.Add(this.DownloadButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.PathLab);
            this.Controls.Add(this.SelectButton);
            this.Controls.Add(this.SavePathBox);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DownloadForm";
            this.Text = "Download";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.OptionSaveCheckBox.ResumeLayout(false);
            this.OptionSaveCheckBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox SavePathBox;
        private System.Windows.Forms.Button SelectButton;
        private System.Windows.Forms.Label PathLab;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton OptionRadioButtonB;
        private System.Windows.Forms.RadioButton OptionRadioButtonA;
        private System.Windows.Forms.Button DownloadButton;
        private System.Windows.Forms.GroupBox OptionSaveCheckBox;
        private System.Windows.Forms.RadioButton OptionSaveCheckButton;
        private System.Windows.Forms.RadioButton OptionForceSaveButton;
        private System.Windows.Forms.RadioButton OptionFilenameSetButton;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label DownloadNowLab;
        private System.Windows.Forms.CheckBox AutoCloseCheck;
    }
}