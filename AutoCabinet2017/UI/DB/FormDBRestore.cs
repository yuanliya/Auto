using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Configuration;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

using NJUST.AUTO06.Utility;
using AutoCabinet2017.Helper;
using DevExpress.XtraEditors;

namespace AutoCabinet2017.UI.DB
{
    public partial class FormDBRestore : XtraForm
    {
        public FormDBRestore()
        {
            InitializeComponent();
        }

        private void Recover(string file)
        {
            if (!File.Exists(file))
            {
                throw new Exception("备份数据库不存在");
            }

            // 数据库类型
            string mode = ConfigurationManager.AppSettings["Connection"].ToString();
            // 连接字符串
            string DBConnectionString = ConfigurationManager.ConnectionStrings[mode].ConnectionString;

            if (mode == "MySQL")
            {
                string[] info = DBConnectionString.Split(';');
                Dictionary<string, object> myParams = new Dictionary<string, object>();
                foreach (string str in info)
                {
                    string[] strs = str.Split('=');
                    myParams.Add(strs[0].Trim(), strs[1].Trim());
                }

                // 构建DOS窗口的命令字
                string cmdStr = "mysql" + " -P" + " " + myParams["Port"] + " -u" + myParams["user id"] + " " + myParams["Initial Catalog"] + " < " + file;

                try
                {
                    // 执行DOS命令行下的数据库备份
                    CommandHelper.Instance.Execute(cmdStr, 0);

                    MessageUtil.ShowTips("数据库还原成功！");

                    // 关闭对话框
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageUtil.ShowError(ex.Message);
                }
            }          
        }

        /// <summary>
        /// 导入数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolRestore_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //// 数据库文件名
            //string DataBaseName = ConfigurationManager.AppSettings["DBName"].ToString();
            //// 源数据库路径需和可执行文件路径保持一致，否则打包程序时会出错
            //string reportPath = Application.StartupPath;
            //reportPath += DataBaseName;

            try
            {
                // 恢复数据库,mdb1为备份数据库绝对路径,mdb2为当前数据库绝对路径
                //Recover(txtPath.Text, reportPath);
                string backPath = txtPath.Text;

                Recover(backPath);
                //MessageUtil.ShowTips("数据库恢复成功！");
            }
            catch (Exception ex)
            {
                MessageUtil.ShowWarning(ex.ToString());
            }

            this.Close();
        }

        /// <summary>
        /// 浏览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolBrower_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = "c:\\"; //注意这里写路径时要用c:\\而不是c:\
            openFileDialog.Filter = "数据库文件|*.sql";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // 取得access文件路径
                txtPath.Text = openFileDialog.FileName;
            }
        }

        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolReset_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtPath.Text = "";
        }    
    }
}
