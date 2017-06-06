using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;

using AutoCabinet2017.Helper;

using NJUST.AUTO06.Utility;
using DevExpress.XtraEditors;

namespace AutoCabinet2017.UI.PB
{
    public partial class FormPBPrinter : XtraForm
    {
        private DataTable dt = new DataTable();
        // 自定义打印类
        private MyPrinter myPrintHelper = new MyPrinter();

        public FormPBPrinter(DataTable dt)
        {
            InitializeComponent();

            this.dt = dt;
        }

        private void PrintForm_Load(object sender, EventArgs e)
        {
            gcPrint.DataSource = dt;

            txtFooter.Text = "";
            txtTitle.Text  = "档案信息表";
        }

        /// <summary>
        /// Windows下的页面设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPageSet_Click(object sender, EventArgs e)
        {
            myPrintHelper.OpenPageSetupDialog();
        }

        /// <summary>
        /// windows下的打印设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrintSetup_Click(object sender, EventArgs e)
        {
            myPrintHelper.OpenPrintDialog();
        }

        /// <summary>
        /// 启动打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            // 页眉、页脚标注        
            myPrintHelper.PrintTitle     = txtTitle.Text.Trim();
            myPrintHelper.PrintFooter    = txtFooter.Text.Trim();

            DataTable printTable = GridControlHelper.Instance.ConvertToDataTable(gvPrint);
            myPrintHelper.PrintDataTable = printTable;

            myPrintHelper.PrintDocument();
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
