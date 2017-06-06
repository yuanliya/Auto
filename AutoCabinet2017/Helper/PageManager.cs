using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevExpress.XtraBars;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.XtraNavBar;
using DevExpress.XtraTabbedMdi;
using DevExpress.XtraSplashScreen;

using AutoCabinet2017.UI.FM;
using AutoCabinet2017.UI.DB;
using AutoCabinet2017.UI.OP;
using AutoCabinet2017.UI.SY;
using AutoCabinet2017.UI.RP;
using AutoCabinet2017.UI.DV;
using AutoCabinet2017.UI.CustomWaiting;

namespace AutoCabinet2017.Helper
{
    public class PageManager
    {
        private Form parent;
        private XtraTabbedMdiManager xtraTabbedMdiMgr;  // tab界面管理器
        private SplashScreenManager  loading;           // 加载等待处理     

        public PageManager(Form parent, XtraTabbedMdiManager xtraTabbedMdiMgr)
        {
            this.parent           = parent;
            this.xtraTabbedMdiMgr = xtraTabbedMdiMgr;
            // 初始化界面加载的进度指示
            loading = new SplashScreenManager(parent, typeof(LoadingForm), true, true);
        }

        #region XtraTabbedMdi--多Tab界面的管理

        /// <summary>
        /// 显示打开的界面
        /// </summary>
        /// <param name="fName">界面Title</param>
        /// <returns></returns>
        private bool ShowOpenedPage(string fName)
        {
            // 遍历Tab管理器中所有的Pages，显示和当前Title符合的Page
            foreach (XtraMdiTabPage page in xtraTabbedMdiMgr.Pages)
            {
                if (page.Text.Trim().ToUpper() == fName.Trim().ToUpper())
                {
                    xtraTabbedMdiMgr.SelectedPage = page;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 生成要显示在Tab中的Form
        /// </summary>
        /// <param name="strTitle"></param>
        /// <param name="strTag"></param>
        /// <param name="frmBase"></param>
        /// <param name="image"></param>
        private void CreateMDIControl(string strTitle, string strTag, Form frmBase)
        {
            try
            {
                DevExpress.XtraTabbedMdi.XtraMdiTabPage currentPage = null;
             
                if (currentPage == null)
                {
                    // 等待加载窗口
                    loading.ShowWaitForm();

                    // 设置Form属性
                    frmBase.Dock = DockStyle.Fill;
                    frmBase.Text = strTitle;
                    frmBase.Tag  = strTag;
                    frmBase.Location = new Point(100, 100);
                    frmBase.MdiParent = this.parent;
    
                    frmBase.Show();

                    // 记录当前Tab中的Page
                    currentPage = xtraTabbedMdiMgr.SelectedPage;
                }
                else
                {
                    xtraTabbedMdiMgr.SelectedPage = currentPage;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
            finally
            {
                // 关闭加载窗口
                loading.CloseWaitForm();
            }
        }

        #endregion

        #region 界面显示

        /// <summary>
        /// 显示弹出对话框
        /// </summary>
        /// <param name="tag"></param>
        public void ShowDialogForm(string tag)
        {
            switch (tag)
            {
                case "301": // “ 数据库备份”
                    FormDBBackup frmDBBackup = new FormDBBackup();
                    frmDBBackup.ShowDialog();

                    break;

                case "302": // “ 数据库还原”
                    FormDBRestore frmDBRestore = new FormDBRestore();
                    frmDBRestore.ShowDialog();

                    break;

                //case "501": // “设备控制”
                //    FormDVDeviceControl frmDevControl = new FormDVDeviceControl();
                //    frmDevControl.ShowDialog();

                //    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// 根据导航栏指令打开相应Form窗体并加入TabControl
        /// </summary>
        /// <param name="xPage">窗体的TabControl载体</param>
        public void ShowForm(string tag, string caption)
        {
            if ((tag == null) || string.IsNullOrEmpty(tag.ToString())) return;
            if (ShowOpenedPage(caption) == true) return;
            
            switch (tag as string)
            {
                #region "档案操作"菜单的操作

                case "101": // “入库管理”
                    CreateMDIControl(caption, "", new FormOPArvIn());
                    break;
                case "102": // “出库管理”
                    CreateMDIControl(caption, "", new FormOPArvOut());
                    break;
                case "103": // “移库管理”
                    // 生成Form
                    CreateMDIControl(caption, "", new FormOPArvMove());
                    break;
                case "104": // “盘库管理”
                    CreateMDIControl(caption, "", new FormOPArvCheck());
                    break;
                case "105": // “借阅管理”
                    CreateMDIControl(caption, "", new FormOPArvLend());
                    break;
                case "106": // “归还管理”
                    CreateMDIControl(caption, "", new FormOPArvReturn());
                    break;
                default:
                    break;

                #endregion

                #region 报表管理

                case "201": // “查询管理”
                    CreateMDIControl(caption, "", new FormRPQuery());
                    break;

                case "202": // “统计管理”
                    CreateMDIControl(caption, "", new FormRPStatics());
                    break;

                #endregion

                #region "基本资料"菜单的操作

                case "401": // “字段配置”
                    CreateMDIControl(caption, "", new FormFMFieldCfg());
                    break;

                case "402": // “数据字典”
                    CreateMDIControl(caption, "", new FormFMDataDict());
                    break;

                #endregion

                #region "设备管理"菜单的操作

                case "502": // 打开“设备配置”窗口
                    CreateMDIControl(caption, "", new FormDVDeviceConfig());
                    break;

                #endregion

                #region "系统管理"菜单的操作

                case "601": // “用户管理”
                    CreateMDIControl(caption, "", new FormSYUser());
                    break;

                case "602": // “角色管理”
                    CreateMDIControl(caption, "", new FormSYRole());
                    break;

                //case "603":// “模块管理”
                //    CreateMDIControl(caption, "", new FormSYModuleCfg());
                //    break;

                case "603": // “日志管理”
                    CreateMDIControl(caption, "", new FormSYLog());
                    break;

                #endregion

                case "501": // “设备控制”
                    CreateMDIControl(caption, "", new FormDVControlPanel());

                    break;
            }
            
        }

        #endregion

        #region TileNavPane
        public TileNavElement FindElementByName(TileNavPane control, string tag)
        {
            if (tag == null)
                return null;

            TileNavElement result = null;
            foreach (TileNavCategory category in control.Categories)
            {
                if (FindInCategory(category, tag, out result))
                    return result;
            }
            if (FindInCategory(control.DefaultCategory, tag, out result))
                return result;
            return result;
        }

        bool FindInCategory(TileNavCategory category, string tag, out TileNavElement found)
        {
            TileNavElement result = null;
            //第一级
            if (category.Tag!=null&&string.Equals(category.Tag.ToString(), tag))
            {
                found = category;
                return true;
            }
            //第二级
            foreach (TileNavItem item in category.Items)
            {
                if (FindInItem(item, tag, out result))
                {
                    found = result;
                    return true;
                }
            }
            found = null;
            return false;
        }
        bool FindInItem(TileNavItem item, string tag, out TileNavElement found)
        {
            if (item.Tag!=null&&string.Equals(item.Tag.ToString(), tag))
            {
                found = item;
                return true;
            }
            //第三级
            foreach (TileNavSubItem subitem in item.SubItems)
            {
                if (string.Equals(subitem.Tag.ToString(), tag))
                {
                    found = subitem;
                    return true;
                }
            }
            found = null;
            return false;
        }
        #endregion
    }
}