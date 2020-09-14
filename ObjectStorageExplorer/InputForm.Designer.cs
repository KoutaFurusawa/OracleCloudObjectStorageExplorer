namespace ObjectStorageExplorer
{
    partial class InputForm
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
            this.TextLab = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.OkBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TextLab
            // 
            this.TextLab.AutoSize = true;
            this.TextLab.Location = new System.Drawing.Point(21, 9);
            this.TextLab.Name = "TextLab";
            this.TextLab.Size = new System.Drawing.Size(24, 13);
            this.TextLab.TabIndex = 0;
            this.TextLab.Text = "text";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(24, 36);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(507, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // OkBtn
            // 
            this.OkBtn.Location = new System.Drawing.Point(313, 82);
            this.OkBtn.Name = "OkBtn";
            this.OkBtn.Size = new System.Drawing.Size(99, 34);
            this.OkBtn.TabIndex = 2;
            this.OkBtn.Text = "OK";
            this.OkBtn.UseVisualStyleBackColor = true;
            this.OkBtn.Click += new System.EventHandler(this.OkBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.Location = new System.Drawing.Point(432, 82);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(99, 34);
            this.CancelBtn.TabIndex = 3;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // InputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 128);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.OkBtn);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.TextLab);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InputForm";
            this.Text = "InputForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TextLab;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button OkBtn;
        private System.Windows.Forms.Button CancelBtn;
    }
}