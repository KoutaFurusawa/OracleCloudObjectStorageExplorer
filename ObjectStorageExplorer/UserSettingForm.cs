using OCISDK;
using OCISDK.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace ObjectStorageExplorer
{
    public partial class UserSettingForm : Form
    {
        public UserSettingForm()
        {
            InitializeComponent();

            TenantIdBox.Text = Properties.Settings.Default.TenancyId;
            UserIdBox.Text = Properties.Settings.Default.UserId;
            FingerBox.Text = Properties.Settings.Default.Fingerprint;
            KeyPathBox.Text = Properties.Settings.Default.KeyFilePath;
            PassPhraseBox.Text = Properties.Settings.Default.PassPhrase;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.TenancyId = TenantIdBox.Text;
            Properties.Settings.Default.UserId = UserIdBox.Text;
            Properties.Settings.Default.Fingerprint = FingerBox.Text;
            Properties.Settings.Default.KeyFilePath = KeyPathBox.Text;
            Properties.Settings.Default.PassPhrase = PassPhraseBox.Text;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ClientConfig clientConfig = new ClientConfig
            {
                TenancyId = TenantIdBox.Text,
                UserId = UserIdBox.Text,
                Fingerprint = FingerBox.Text,
                PrivateKey = KeyPathBox.Text,
                PrivateKeyPassphrase = PassPhraseBox.Text
            };

            string caption = "SuccessFull";
            string messageBoxText = "OCI Connected.";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Information;

            var identityClient = new IdentityClient(clientConfig);
            try
            {
                var root = identityClient.GetTenancy(new OCISDK.Identity.Request.GetTenancyRequest { TenancyId = clientConfig.TenancyId });
            }
            catch(Exception ex)
            {
                messageBoxText = ex.Message;
                caption = "failed.";
                button = MessageBoxButton.OK;
                icon = MessageBoxImage.Error;
            }

            System.Windows.MessageBox.Show(messageBoxText, caption, button, icon);
        }
    }
}
