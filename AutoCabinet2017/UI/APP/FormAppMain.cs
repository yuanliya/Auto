using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevExpress.XtraEditors;
using DevExpress.XtraTabbedMdi;
using DevExpress.XtraNavBar;
using DevExpress.XtraBars;

using AutoCabinet2017.Helper;
using AutoCabinet2017.UI.OP;

using NJUST.AUTO06.Utility;
using ZY.EntityFrameWork.Core.Model.Entity;
using ZY.EntityFrameWork.Core.Model.Dto;
using ZY.EntityFrameWork.Caller;

namespace AutoCabinet2017.UI.APP
{
    public partial class FormAppMain : Form
    {
        // Tab的分页管理
        private PageManager pageManager;
        // 默认主窗口大小
        private int _maxWidth  = 1600;
        private int _maxHeight = 900;

        public FormAppMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 更新导航栏的模块权限
        /// </summary>
        private void AuthorizeModules()
        {
            nbcMainMenu.BeginUpdate();

            // 超级管理员的权限--所有模块均可见
            if (PropertyHelper.CurrentUser.RoleName == "超级管理员")
            {
                // 菜单栏分组标题是否可见
                foreach (NavBarGroup group in nbcMainMenu.Groups)
                {
                    group.Visible = true;
                    // 设置用户导航菜单项使用权限
                    foreach (NavBarItemLink item in group.ItemLinks)
                    {
                        item.Visible = true;
                    }
                }              
            }
            else // 普通用户、管理员和系统管理员的权限
            {
                RoleModuleDto usermodule;
                
                // 菜单栏分组标题是否可见
                foreach (NavBarGroup group in nbcMainMenu.Groups)
                {
                    bool groupVisible = false;

                    // 设置用户导航菜单项使用权限
                    foreach (NavBarItemLink item in group.ItemLinks)
                    {
                        if (!string.IsNullOrEmpty((string)item.Item.Tag))
                        {
                            // 查找模块的权限信息
                            usermodule = PropertyHelper.RoleModules.Find(module => module.ModuleTag == item.Item.Tag.ToString());

                            if (item.Visible = usermodule == null ? false : usermodule.Enabled)
                            {
                                groupVisible = true;
                            }                               
                        }
                    }

                    if (groupVisible)
                    {
                        group.Visible = true;
                    }
                    else
                    {
                        group.Visible = false;
                    }
                }
            }

            nbcMainMenu.EndUpdate();
        }

        private void FormAppMain_Load(object sender, EventArgs e)
        {
            // 获取当前显示器分辨率
            System.Drawing.Rectangle rec = Screen.GetWorkingArea(this);

            if ((rec.Height >= _maxHeight) && (rec.Width >= _maxWidth))
            {
                // 主界面最大设置为1600*900
                this.Size = new Size(_maxWidth, _maxHeight);
                // 启动在屏幕正中
                this.StartPosition = FormStartPosition.WindowsDefaultLocation;
            }
            else
            {
                // 主界面布满显示器屏幕的工作区
                this.Size = SystemInformation.WorkingArea.Size;
                this.Top = 0;
                this.Left = 0;
            }

            // 状态栏信息
            this.toolStatusLabelWelcome.Text  = "当前用户：" + PropertyHelper.CurrentUser;
            this.toolStatusLabelUnit.Text     = System.Configuration.ConfigurationManager.AppSettings["UserUnit"].ToString(); // 从配置文件读取使用单位信息
            this.toolStatusLabelDatetime.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

            // 根据用户权限设置导航栏
            AuthorizeModules();
           
            // 显示封面
            peCover.BringToFront();
            peCover.Visible = true;
            // Tab的Page管理器
            xtraTabbedMdiMgr.Pages.Clear();
            pageManager = new PageManager(this, xtraTabbedMdiMgr);
            xtraTabbedMdiMgr.PageRemoved += xtraTabbedMdiMgr_PageRemoved;          
        }

        /// <summary>
        /// TabPage关闭时的处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void xtraTabbedMdiMgr_PageRemoved(object sender, MdiTabPageEventArgs e)
        {
            if (xtraTabbedMdiMgr.Pages.Count == 0)
            {
                // 显示封面
                peCover.BringToFront();
                peCover.Visible = true;
            }
        }

        /// <summary>
        /// 用户界面关闭后的处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormAppMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 彻底退出应用程序
            Application.Exit();
            // 释放Form资源
            this.Dispose();
        }

        /// <summary>
        /// 用户界面关闭前的处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormAppMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageUtil.ShowYesNoAndTips("确定要退出吗？") == DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 在Tab中显示Form
        /// </summary>
        /// <param name="tag">Form的Tag标识</param>
        /// <param name="caption">Form的Title</param>
        private void ShowForm(string tag, string caption)
        {
            pageManager.ShowForm(tag, caption);

            if (peCover.Visible)
            {
                peCover.SendToBack();    // 使picturebox不可见
                peCover.Visible = false;
            }
        }

        /// <summary>
        /// 导航栏Item的点击事件处理--针对嵌入Tab控件的界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nbiItems_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            // 显示Form
            ShowForm(e.Link.Item.Tag.ToString(), e.Link.Item.Caption);
        }

        /// <summary>
        /// 导航栏Item的点击事件处理--针对弹出式Dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nbiItems1_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            // 显示Dialog
            pageManager.ShowDialogForm(e.Link.Item.Tag.ToString());
        }

        #region  工具栏操作

        /// <summary>
        /// 重新登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolReLogin_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (MessageUtil.ShowYesNoAndWarning("您确定需要重新登录吗？") != DialogResult.Yes)  return;
            
            // 隐藏主界面
            this.Hide();
            
            // 启动登录窗口
            FormLogin loginFrm = new FormLogin();
            if (loginFrm.ShowDialog() == DialogResult.OK)
            {
                // 加载主界面
                FormAppMain_Load(this, e);
            }

            // 显示主界面
            this.Show();
        }

        /// <summary>
        /// 入库管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolInStorage_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowForm(e.Item.Tag.ToString(), e.Item.Caption);
        }

        /// <summary>
        /// 借阅管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolLend_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowForm(e.Item.Tag.ToString(), e.Item.Caption);
        }

        /// <summary>
        /// 归还管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolReturn_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowForm(e.Item.Tag.ToString(), e.Item.Caption);
        }

        /// <summary>
        /// 查询管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolQuery_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowForm(e.Item.Tag.ToString(), e.Item.Caption);
        }

        #endregion
    }
}
