using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ObjectStorageExplorer
{
    public static class DialogManagement
    {
        /// <summary>
        /// メッセージボックスを開く
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="buttons"></param>
        /// <returns></returns>
        public static DialogResult ShowMessageDialog(string title, string message, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            DialogResult result = MessageBox.Show(message,
            title,
            buttons,
            icon,
            MessageBoxDefaultButton.Button2);

            return result;
        }

        /// <summary>
        /// ファイルを開くダイアログ
        /// </summary>
        /// <returns></returns>
        public static string FileOpenDialogShow(string defaultDir)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.FileName = "";
                ofd.InitialDirectory = defaultDir;
                ofd.Title = "ファイルを選択";
                ofd.RestoreDirectory = true;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    return ofd.FileName;
                }
            }

            return "";
        }

        /// <summary>
        /// ファイルを開くダイアログ
        /// </summary>
        /// <returns></returns>
        public static string[] MultipleFileOpenDialogShow(string defaultDir)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.FileName = "";
                ofd.InitialDirectory = defaultDir;
                ofd.Title = "ファイルを選択";
                ofd.RestoreDirectory = true;
                ofd.Multiselect = true;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    return ofd.FileNames;
                }
            }

            return new string[] { "" };
        }

        /// <summary>
        /// フォルダー選択ダイアログ
        /// </summary>
        /// <returns></returns>
        public static string FolderSelectDialogShow()
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "フォルダーを選択";
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    return folderBrowserDialog.SelectedPath;
                }
            }

            return "";
        }

        public static string SaveDialogShow(string fileName)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                if (!string.IsNullOrEmpty(fileName))
                {
                    FileInfo fileInfo = new FileInfo(fileName);
                    string directoryName = fileInfo.DirectoryName;
                    if (Directory.Exists(directoryName))
                    {
                        saveFileDialog.InitialDirectory = directoryName;
                    }
                }
                DialogResult dialogResult = saveFileDialog.ShowDialog();
                if (dialogResult == DialogResult.Cancel)
                {
                    return "";
                }

                return saveFileDialog.FileName;
            }
        }
    }
}
