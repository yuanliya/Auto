using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NJUST.AUTO06.Utility;
using System.IO;

namespace NJUST.AUTO06.Utility
{
    public sealed class FileDialogHelper
    {
        #region 构造函数和实例

        // 私有构造函数
        FileDialogHelper()
        {
        }

        // 利用单件模式构造线程安全的独立类实例
        public static readonly FileDialogHelper Instance = new FileDialogHelper();
        
        #endregion

        /// <summary>
        /// 打开PDF文件对话框
        /// </summary>
        /// <returns>文件名</returns>
        public string OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            //openFileDialog.Filter = "PDF文件|*.pdf";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog.FileName;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 打开另存为对话框
        /// </summary>
        /// <returns></returns>
        public string SaveAs()
        {
            FolderBrowserDialog fbDialog = new FolderBrowserDialog();

            fbDialog.ShowNewFolderButton = true;

            if (fbDialog.ShowDialog() == DialogResult.OK)
            {
                return fbDialog.SelectedPath;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 打开WORD文件对话框
        /// </summary>
        /// <returns>文件名</returns>
        public string OpenWord()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Word文档|*.doc;*.docx";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog.FileName;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 打开EXCEL文件对话框
        /// </summary>
        /// <returns>文件名</returns>
        public string OpenExcel()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Excel表格|*.xls";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog.FileName;
            }
            else
            {
                return null;
            }
        }

        
    }
}
