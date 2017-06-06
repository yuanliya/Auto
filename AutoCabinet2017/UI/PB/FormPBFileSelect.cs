using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoCabinet2017.Helper;
using System.IO;

using NJUST.AUTO06.Utility;
using DevExpress.XtraEditors;

namespace AutoCabinet2017.UI.PB
{
    public partial class FormPBFileSelect : XtraForm
    {
        public string DataFilePath { get; set; }

        public FormPBFileSelect()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 打开Excel或Txt文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrower_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (rbtnExcel.Checked)
            {
                openFileDialog.InitialDirectory = "d:\\"; //注意这里写路径时要用c:\\而不是c:\
                openFileDialog.Filter = "Excel文件(*.xls)|*.xls;*.xlsx";
                openFileDialog.RestoreDirectory = true;
                openFileDialog.FilterIndex = 1;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // 取得Excel文件路径
                    txtFilePath.Text = openFileDialog.FileName;
                }
            }
        }
        /// <summary>
        /// 判断文件路径是否为空，并导入数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilePath.Text))
            {
                MessageUtil.ShowWarning("没有选择文件,无法进行数据导入!");
                return;
            }

            if (!File.Exists(txtFilePath.Text))
            {
                MessageUtil.ShowWarning("数据文件不存在,无法进行数据导入!");
                return;
            }

            // 设置文件路径
            DataFilePath = txtFilePath.Text;

            // 关闭对话框
            this.Close();
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DataFilePath = null;

            this.Close();
        }

        private void FormPBFileSelect_Load(object sender, EventArgs e)
        {

        }
    }
}
