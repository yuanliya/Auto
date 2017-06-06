using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.Spreadsheet;
using System.IO;

using AutoCabinet2017.Helper;
using NJUST.AUTO06.Utility;
using NJUST.AUTO06.Utility.PreviewUtil;
using DevExpress.XtraEditors;

namespace AutoCabinet2017.UI.PREV
{
    public partial class FormExcelPreview : XtraForm
    {
        public FormExcelPreview()
        {
            InitializeComponent();
        }

        private void FormExcelPreview_Load(object sender, EventArgs e)
        {
            
            IWorkbook workbook = spreadsheetControl1.Document;
            workbook.LoadDocument(this.Tag.ToString());
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = PreviewHelper.Instance.ShowSaveFileDialog(this.Tag.ToString());
                if (fileName == null)
                {
                    return;
                }

                IWorkbook workbook = spreadsheetControl1.Document;
                workbook.SaveDocument(fileName);
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            this.spreadsheetControl1.ShowPrintPreview();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
