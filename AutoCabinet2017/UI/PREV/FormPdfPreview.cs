using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using AutoCabinet2017.Helper;
using NJUST.AUTO06.Utility;
using NJUST.AUTO06.Utility.PreviewUtil;
using DevExpress.XtraEditors;

namespace AutoCabinet2017.UI.PREV
{
    public partial class FormPdfPreview : XtraForm
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
