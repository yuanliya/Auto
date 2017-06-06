using AutoCabinet2017.Helper;
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
using ZY.EntityFrameWork.Caller;
using ZY.EntityFrameWork.Caller.Facade;
using ZY.EntityFrameWork.Core.Model.Dto;

namespace AutoCabinet2017.UI.EF
{
    public partial class FormEFUser : Form
    {
        public FormEFUser()
        {
            InitializeComponent();
        }

        private BindingList<UserDto> userList = new BindingList<UserDto>();
        private BindingList<UserDto> selectedList = new BindingList<UserDto>();

        public string RoleId { get; set; }//角色编号

        // <summary>
        /// 配置表格的控制列
        /// 只需要对表格操作一次，以后表格再关联数据源都无需再处理
        /// </summary>
        private void ConfigGridView()
        {
            // 根据列标题设定表格中不可见的列
            string[] cols = new string[] { "Id" };//其实用户编号作Id就够的
            GridControlHelper.Instance.SetColumnInvisible(gvSelected, cols);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (ctrlPanel.Tag != null && ctrlPanel.Tag.ToString() == "Add")
            {
                ctrlPanel.Tag = "Search";
            }

            switch ((sender as SimpleButton).Name)
            {
                case "btnSearch":    // 搜索
                    //OnSearch();
                    break;

                case "btnExpand":    // 展开下拉菜单
                    //OnExpand();
                    break;

                case "btnAdvanced":  // 高级搜索
                    //OnAdvanced();
                    break;

                default:
                    break;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                selectedList.ToList().ForEach(q => q.RoleId = RoleId);
                CallerFactory.Instance.GetService<IAuthorityService>().Update(selectedList.ToList());
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.ToString());
            }
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void FormEFUser_Load(object sender, EventArgs e)
        {
            LoadUserInfo();
        }

        /// <summary>
        /// 根据登录用户角色加载用户信息
        /// </summary>
        /// <returns></returns>
        private void LoadUserInfo()
        {
            // 从数据库查询所有用户信息
            List<UserDto> userItems = CallerFactory.Instance.GetService<IAuthorityService>().GetAllUsers();

            // 根据登录用户的权限加载用户信息
            switch (PropertyHelper.CurrentUser.UserRole)
            {
                case "超级管理员":
                    // 显示所有用户信息,包括系统管理员，管理员和普通用户
                    gcUser.DataSource = userList = new BindingList<UserDto>(userItems);
                    // 设置角色复选框
                    //要从数据库中选择！
                    cbxUserRole.Properties.Items.AddRange(new string[] { "普通用户", "管理员", "系统管理员" });

                    break;

                case "系统管理员":
                    // 显示管理员和普通用户信息
                    gcUser.DataSource = userList = new BindingList<UserDto>(userItems.Where<UserDto>(q => q.UserRoleName == "管理员" || q.UserRoleName == "普通用户").ToList());
                    // 设置角色复选框
                    cbxUserRole.Properties.Items.AddRange(new string[] { "普通用户", "管理员" });

                    break;

                case "管理员":
                    // 显示普通用户信息
                    gcUser.DataSource = userList = new BindingList<UserDto>(userItems.Where<UserDto>(q => q.UserRoleName == "普通用户").ToList());
                    // 设置角色复选框
                    cbxUserRole.Properties.Items.Add("普通用户");

                    break;

                default:
                    break;
            }

            // 配置表格格式
            ConfigGridView();
        }

        private void gvUser_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            if (e.ControllerRow < 0)
                return;
            if (selectedList.Contains(userList[e.ControllerRow]))
            {
                selectedList.Remove(userList[e.ControllerRow]);
            }
            else
                selectedList.Add(userList[e.ControllerRow]);
            gcSelect.DataSource = selectedList;
        }


    }
}
