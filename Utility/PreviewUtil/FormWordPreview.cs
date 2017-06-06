using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace NJUST.AUTO06.Utility.PreviewUtil
{
    public partial class FormWordPreview : Form
    {
        public FormWordPreview()
        {
            InitializeComponent();
        }

        private void FormWordPreview_Load(object sender, EventArgs e)
        {
            richEditControl1.LoadDocument(this.Tag.ToString());
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            try
            {
                richEditControl1.SaveDocumentAs();
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            this.richEditControl1.ShowPrintPreview();
        }
    }
}
