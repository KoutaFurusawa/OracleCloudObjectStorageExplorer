using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ObjectStorageExplorer
{
    public partial class InputForm : Form
    {
        public string TextValue;

        public InputForm(string text, string defaultValue)
        {
            InitializeComponent();

            TextLab.Text = text;
            textBox1.Text = defaultValue;
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void OkBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TextValue = textBox1.Text;
        }
    }
}
