using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using NJUST.AUTO06.Utility;

namespace NJUST.AUTO06.Utility.PreviewUtil
{
    public partial class FormPdfPreview : Form
    {
        public FormPdfPreview()
        {
            InitializeComponent();
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            // 获得另存为的文件名
            string fileName = PreviewHelper.Instance.ShowSaveFileDialog(this.Tag.ToString());
            if (fileName == null)
            {
                return;
            }
            // 保存文档
            this.pdfViewer1.SaveDocument(fileName);
        }

        private void FormPdfPreview_Load(object sender, EventArgs e)
        {
            this.pdfViewer1.LoadDocument(this.Tag.ToString());
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            this.pdfViewer1.Print();
        }
    }
}
