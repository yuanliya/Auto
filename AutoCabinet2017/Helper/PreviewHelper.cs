using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace NJUST.AUTO06.Utility.PreviewUtil
{
    sealed class PreviewHelper
    {
        private PreviewHelper()
        { }

        // 单例模式的类对象
        public static readonly PreviewHelper Instance = new PreviewHelper();

        /// <summary>
        /// 打开预览相关对话框
        /// </summary>
        /// <param name="path">文档路径</param>
        public void OpenPreview(object path)
        {
            if (path==null||!File.Exists(path.ToString()))
            {
                NJUST.AUTO06.Utility.MessageUtil.ShowError("该电子档案不存在！");
                return;
            }

            string ext = Path.GetExtension(path.ToString());

            if ((ext == ".doc") || (ext == ".docx"))
            {
                FormWordPreview wordPreview = new FormWordPreview();
                wordPreview.Tag = path;
                wordPreview.ShowDialog();

                return;
            }

            switch(ext)
            {
                case ".pdf":
                    FormPdfPreview pdfPreview = new FormPdfPreview();
                    pdfPreview.Tag = path;
                    pdfPreview.ShowDialog();

                break;

                case ".xls":
                    FormExcelPreview excelPreview = new FormExcelPreview();
                    excelPreview.Tag = path;
                    excelPreview.ShowDialog();

                break;

                default:
                    MessageUtil.ShowWarning("不支持该格式的预览！");
                break;
            }
        }

        /// <summary>
        /// 打开保存对话框
        /// </summary>
        /// <param name="name">文件名</param>
        /// <returns></returns>
        public string ShowSaveFileDialog(string name)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            //设置文件类型
            string ext = Path.GetExtension(name.ToString());

            sfd.Filter = string.Format("|*{0}", ext);
            sfd.FileName = Path.GetFileName(name.ToString());

            // 设置默认文件类型显示顺序 
            sfd.FilterIndex = 1;

            // 保存对话框是否记忆上次打开的目录 
            sfd.RestoreDirectory = true;

            // 点了保存按钮进入 
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                // 获得文件路径 
                return sfd.FileName.ToString();  
            }

            return null;
        }
    }
}
