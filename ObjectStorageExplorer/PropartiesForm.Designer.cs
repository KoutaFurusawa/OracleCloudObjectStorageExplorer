namespace ObjectStorageExplorer
{
    partial class PropartiesForm
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
            this.ObjectPropartiesView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PropartiesOkBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ObjectPropartiesView
            // 
            this.ObjectPropartiesView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ObjectPropartiesView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.ObjectPropartiesView.FullRowSelect = true;
            this.ObjectPropartiesView.GridLines = true;
            this.ObjectPropartiesView.HideSelection = false;
            this.ObjectPropartiesView.Location = new System.Drawing.Point(12, 12);
            this.ObjectPropartiesView.MultiSelect = false;
            this.ObjectPropartiesView.Name = "ObjectPropartiesView";
            this.ObjectPropartiesView.Size = new System.Drawing.Size(660, 403);
            this.ObjectPropartiesView.TabIndex = 0;
            this.ObjectPropartiesView.UseCompatibleStateImageBehavior = false;
            this.ObjectPropartiesView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Key";
            this.columnHeader1.Width = 100;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Value";
            this.columnHeader2.Width = 550;
            // 
            // PropartiesOkBtn
            // 
            this.PropartiesOkBtn.Location = new System.Drawing.Point(23, 429);
            this.PropartiesOkBtn.Name = "PropartiesOkBtn";
            this.PropartiesOkBtn.Size = new System.Drawing.Size(81, 26);
            this.PropartiesOkBtn.TabIndex = 1;
            this.PropartiesOkBtn.Text = "OK";
            this.PropartiesOkBtn.UseVisualStyleBackColor = true;
            this.PropartiesOkBtn.Click += new System.EventHandler(this.PropartiesOkBtn_Click);
            // 
            // PropartiesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 463);
            this.Controls.Add(this.PropartiesOkBtn);
            this.Controls.Add(this.ObjectPropartiesView);
            this.Name = "PropartiesForm";
            this.Text = "Proparties";
            this.Load += new System.EventHandler(this.PropartiesForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView ObjectPropartiesView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button PropartiesOkBtn;
    }
}