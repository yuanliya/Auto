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
    public partial class FormDBBackup : XtraForm
    {
        public FormDBBackup()
        {
            InitializeComponent();
        }

        private void BaseBackupFrm_Load(object sender, EventArgs e)
        {
            txtPath.ReadOnly = true;
        }

        #region 工具栏操作

        /// <summary>
        /// 浏览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolBrower_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FolderBrowserDialog fbDialog = new FolderBrowserDialog();

            fbDialog.ShowNewFolderButton = false;
            DialogResult dr = fbDialog.ShowDialog();

            if (dr == DialogResult.OK)
            {
                txtPath.Text = fbDialog.SelectedPath;
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPath.Text) || string.IsNullOrEmpty(txtArvTitle.Text))
            {
                MessageUtil.ShowTips("数据库备份文件路径和备份文件名不能为空！");
                return;
            }

            // 获取备份文件的绝对路径(路径+文件名+'_'+时间.sql)
            string backPath = txtPath.Text + "\\" + txtArvTitle.Text + "_" + (System.DateTime.Now.ToString("yyyy.MM.dd")).ToString() + ".sql";

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
                    if (strs.Length == 2)
                        myParams.Add(strs[0].Trim(), strs[1]);
                }

                // 构建DOS窗口的命令字
                string cmdStr = "mysqldump" + " -P" + myParams["port"] + " -u" + myParams["user id"] + " " + myParams["Initial Catalog"] + " > " + backPath;
                
                try
                {
                    // 设定MySQL的安装位置
                    DirectoryInfo dirInfo = new DirectoryInfo(Application.StartupPath);
                    string path = dirInfo.Parent.FullName + "\\MySQL Server 5.7\\bin\\";
                    //string path = "D:\\Program Files\\MySQL\\MySQL Server 5.7\\bin";
                  
                    // 执行DOS命令行下的数据库备份
                    CommandHelper.Instance.Execute(cmdStr, 0, path);
                  
                    MessageUtil.ShowTips("数据库备份成功！");

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
        /// 重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolReset_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtPath.Text     = "";
            txtArvTitle.Text = "";
        }

        #endregion   
    }
}
