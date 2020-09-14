namespace ObjectStorageExplorer
{
    partial class NewAddForm
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
            this.NameLab = new System.Windows.Forms.Label();
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.PathLab = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.RegionsBox = new System.Windows.Forms.ComboBox();
            this.MakeBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // NameLab
            // 
            this.NameLab.AutoSize = true;
            this.NameLab.Location = new System.Drawing.Point(32, 50);
            this.NameLab.Name = "NameLab";
            this.NameLab.Size = new System.Drawing.Size(35, 13);
            this.NameLab.TabIndex = 0;
            this.NameLab.Text = "Name";
            // 
            // NameTextBox
            // 
            this.NameTextBox.Location = new System.Drawing.Point(73, 47);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.Size = new System.Drawing.Size(499, 20);
            this.NameTextBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Path";
            // 
            // PathLab
            // 
            this.PathLab.AutoSize = true;
            this.PathLab.Location = new System.Drawing.Point(70, 20);
            this.PathLab.Name = "PathLab";
            this.PathLab.Size = new System.Drawing.Size(35, 13);
            this.PathLab.TabIndex = 3;
            this.PathLab.Text = "label3";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Region";
            // 
            // RegionsBox
            // 
            this.RegionsBox.FormattingEnabled = true;
            this.RegionsBox.Location = new System.Drawing.Point(73, 77);
            this.RegionsBox.Name = "RegionsBox";
            this.RegionsBox.Size = new System.Drawing.Size(246, 21);
            this.RegionsBox.TabIndex = 5;
            // 
            // MakeBtn
            // 
            this.MakeBtn.Location = new System.Drawing.Point(322, 117);
            this.MakeBtn.Name = "MakeBtn";
            this.MakeBtn.Size = new System.Drawing.Size(134, 44);
            this.MakeBtn.TabIndex = 6;
            this.MakeBtn.Text = "OK";
            this.MakeBtn.UseVisualStyleBackColor = true;
            this.MakeBtn.Click += new System.EventHandler(this.MakeBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.Location = new System.Drawing.Point(481, 117);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(108, 44);
            this.CancelBtn.TabIndex = 7;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // NewAddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 173);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.MakeBtn);
            this.Controls.Add(this.RegionsBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.PathLab);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.NameTextBox);
            this.Controls.Add(this.NameLab);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewAddForm";
            this.Text = "NewAddForm";
            this.Load += new System.EventHandler(this.NewAddForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label NameLab;
        private System.Windows.Forms.TextBox NameTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label PathLab;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox RegionsBox;
        private System.Windows.Forms.Button MakeBtn;
        private System.Windows.Forms.Button CancelBtn;
    }
}