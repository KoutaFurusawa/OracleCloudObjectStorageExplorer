namespace ObjectStorageExplorer
{
    partial class UserSettingForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.TenantIdBox = new System.Windows.Forms.TextBox();
            this.UserIdBox = new System.Windows.Forms.TextBox();
            this.FingerBox = new System.Windows.Forms.TextBox();
            this.KeyPathBox = new System.Windows.Forms.TextBox();
            this.PassPhraseBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "TenantId";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "UserId";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Fingerprint";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 188);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "KeyFilePath";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 239);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "PassPhrase";
            // 
            // TenantIdBox
            // 
            this.TenantIdBox.Location = new System.Drawing.Point(83, 37);
            this.TenantIdBox.Name = "TenantIdBox";
            this.TenantIdBox.Size = new System.Drawing.Size(558, 20);
            this.TenantIdBox.TabIndex = 5;
            // 
            // UserIdBox
            // 
            this.UserIdBox.Location = new System.Drawing.Point(83, 89);
            this.UserIdBox.Name = "UserIdBox";
            this.UserIdBox.Size = new System.Drawing.Size(558, 20);
            this.UserIdBox.TabIndex = 6;
            // 
            // FingerBox
            // 
            this.FingerBox.Location = new System.Drawing.Point(83, 133);
            this.FingerBox.Name = "FingerBox";
            this.FingerBox.Size = new System.Drawing.Size(378, 20);
            this.FingerBox.TabIndex = 7;
            // 
            // KeyPathBox
            // 
            this.KeyPathBox.Location = new System.Drawing.Point(83, 185);
            this.KeyPathBox.Name = "KeyPathBox";
            this.KeyPathBox.Size = new System.Drawing.Size(558, 20);
            this.KeyPathBox.TabIndex = 8;
            // 
            // PassPhraseBox
            // 
            this.PassPhraseBox.Location = new System.Drawing.Point(83, 236);
            this.PassPhraseBox.Name = "PassPhraseBox";
            this.PassPhraseBox.Size = new System.Drawing.Size(336, 20);
            this.PassPhraseBox.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(33, 346);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 34);
            this.button1.TabIndex = 10;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(165, 346);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(116, 34);
            this.button2.TabIndex = 11;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(33, 283);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(118, 29);
            this.button3.TabIndex = 12;
            this.button3.Text = "ConnectionTest";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // UserSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 402);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.PassPhraseBox);
            this.Controls.Add(this.KeyPathBox);
            this.Controls.Add(this.FingerBox);
            this.Controls.Add(this.UserIdBox);
            this.Controls.Add(this.TenantIdBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "UserSetting";
            this.Text = "UserSetting";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TenantIdBox;
        private System.Windows.Forms.TextBox UserIdBox;
        private System.Windows.Forms.TextBox FingerBox;
        private System.Windows.Forms.TextBox KeyPathBox;
        private System.Windows.Forms.TextBox PassPhraseBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}