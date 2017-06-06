using AutoCabinet2017.Helper;
using AutoCabinet2017.UI.OP;
using CustomForm;
using DevExpress.XtraEditors;
using NJUST.AUTO06.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoCabinet2017.UI.APP
{
    public partial class FrmAppMain : XtraForm
    {
        public FrmAppMain()
        {
            InitializeComponent();
        }

        private PageManager pageManager;

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }


        private void hyperlinkLabelControl1_Click(object sender, EventArgs e)
        {//new Point(hyperlinkLabelControl1.Location.X,hyperlinkLabelControl1.Location.Y+hyperlinkLabelControl1.Size.Height)
            //popupMenu1.ShowPopup(Control.MousePosition);
            //这个坐标是以整个屏幕为基准的绝对坐标
            popupMenu1.ShowPopup(new Point(this.Location.X + hyperlinkLabelControl1.Location.X,
                 this.Location.Y + hyperlinkLabelControl1.Location.Y + hyperlinkLabelControl1.Size.Height));
        }

        private void nbiArvIn_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //if (!tileNavPane1.Visible)
            //    tileNavPane1.Visible = true;
            //tileNavPane1.SelectedElement = tileArvIn;
            tileNavPane1.SelectedElement = pageManager.FindElementByName(tileNavPane1, e.Link.Item.Tag.ToString());
            pageManager.ShowForm(e.Link.Item);
            if(pictureEdit1.Visible)
            {
                pictureEdit1.SendToBack();//使picturebox不可见
                pictureEdit1.Visible = false;
                tileNavPane1.Visible = true;
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            // 获取当前显示器分辨率
            System.Drawing.Rectangle rec = Screen.GetWorkingArea(this);

            if ((rec.Height >= 900) && (rec.Width >= 1600))
            {
                // 主界面最大设置为1600*900
                this.Size = new Size(1600, 900);
                // 启动在屏幕正中
                this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            }
            else
            {
                // 主界面布满显示器屏幕的工作区
                this.Size = SystemInformation.WorkingArea.Size;
                this.Top = 0;
                this.Left = 0;
            }

            pageManager = new PageManager(this, xtraTabbedMdiMgr);
            xtraTabbedMdiMgr.PageRemoved += xtraTabbedMdiMgr_PageRemoved;
        }

        void xtraTabbedMdiMgr_PageRemoved(object sender, DevExpress.XtraTabbedMdi.MdiTabPageEventArgs e)
        {
            if(xtraTabbedMdiMgr.Pages.Count==0)
            {
                pictureEdit1.BringToFront();
                pictureEdit1.Visible = true;
                tileNavPane1.Visible = false;
            }
        }

        private void FrmAppMain_FormClosing(object sender, FormClosingEventArgs e)
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

        private void FrmAppMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 彻底退出应用程序
            Application.Exit();
            // 释放Form资源
            this.Dispose();
        }

    }
}
