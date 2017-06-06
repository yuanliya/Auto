using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using AutoCabinet2017.Helper;
using DevExpress.XtraEditors;

namespace AutoCabinet2017.UI.PB
{
    public partial class FormPBExcelShow : XtraForm
    {
        /// <summary>
        /// 导入失败的记录明细表
        /// </summary>
        public DataTable Tb_ImportFailure { get; set; }

        // 成功的记录数
        public int sCount { set; get; }
        // 未成功的记录数
        public int xCount { set; get; }

        delegate void processor();
        processor p1;

        public FormPBExcelShow()
        {
            InitializeComponent();
        }

        public void SetFormSize()
        {
            SuccessLabel.Text = sCount.ToString();
            FalseLabel.Text = xCount.ToString();

            // 设置窗体的尺寸
            //this.Height -= (Table.Height+60);
            this.Height -= (Table.Height);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {  
            // 关闭窗体
            this.Close();
        }

        private void btnDetail_Click(object sender, EventArgs e)
        { 
            // 更新显示数据
            gcImportFailure.DataSource = Tb_ImportFailure;

            GridControlHelper.Instance.ConfigGridViewStyle(gvImportFailure);
            Table.Visible = true;

            this.Height += Table.Height + 60;
        }


        private void FormPBExcelShow_Load(object sender, EventArgs e)
        {
            p1 = new processor(SetFormSize);

            // 设置窗体的尺寸
            p1.Invoke();
        }
    }
}
