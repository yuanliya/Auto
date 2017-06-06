using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraBars;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using AutoCabinet2017.Helper;
using AutoCabinet2017.UI.EF;

using NJUST.AUTO06.Utility;

using ZY.EntityFrameWork.Caller;
using ZY.EntityFrameWork.Caller.Facade;
using ZY.EntityFrameWork.Core.DBHelper;
using ZY.EntityFrameWork.Core.Model.Dto;

namespace AutoCabinet2017.UI.SY
{
    public partial class FormSYUser : XtraForm
    {
        public FormSYUser()
        {
            InitializeComponent();
        }
    
        // 用户信息
        private BindingList<UserDto> userList = null;

        /// <summary>
        /// 配置表格的控制列
        /// 只需要对表格操作一次，以后表格再关联数据源都无需再处理
        /// </summary>
        private void ConfigGridView()
        {
            // 根据列标题设定表格中不可见的列
            string[] cols = new string[] { "RoleId", "ID"};
            GridControlHelper.Instance.SetColumnInvisible(gvUser, cols);

            // 为表格加入控制列
            GridControlHelper.Instance.AddControlButton(gvUser, buttonControl, gvUser.Columns.Count);
            GridControlHelper.Instance.ConfigGridViewStyle(gvUser);
        }

        /// <summary>
        /// 根据登录用户角色加载用户信息
        /// </summary>
        /// <returns></returns>
        private void LoadUserInfo()
        {
            // 从数据库查询所有用户信息
            List<UserDto> userItems = CallerFactory.Instance.GetService<IAuthorityService>().GetAllUsers();
            // 加载所有角色权限低于当前用户角色权限的用户
            gcUser.DataSource = userList = new BindingList<UserDto>(userItems.Where<UserDto>(q => q.RoleLevel > PropertyHelper.CurrentUser.RoleLevel).ToList());

            //// 根据登录用户的权限加载用户信息
            //switch (PropertyHelper.CurrentUser.UserRole.RoleName)
            //{
            //    case "超级管理员":
            //        // 显示所有用户信息,包括系统管理员，管理员和普通用户
            //        gcUser.DataSource = userList = new BindingList<UserDto>(userItems);

            //        break;

            //    case "系统管理员":
            //        // 显示管理员和普通用户信息
            //        gcUser.DataSource = userList = new BindingList<UserDto>(userItems.Where<UserDto>(q => q.UserRole.RoleName == "管理员" || q.UserRole.RoleName == "普通用户").ToList());
 
            //        break;

            //    case "管理员":
            //        // 显示普通用户信息
            //        gcUser.DataSource = userList = new BindingList<UserDto>(userItems.Where<UserDto>(q => q.UserRole.RoleName == "普通用户").ToList());
 
            //        break;

            //    default:
            //        break;
            //}

            // 配置表格格式
            ConfigGridView();
        }

        /// <summary>
        /// 界面加载初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormSYUser_Load(object sender, EventArgs e)
        {
            try
            {
                // 表格加载用户信息
                LoadUserInfo();
            }
            catch(Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 更新表格显示
        /// </summary>
        /// <param name="deviceDto"></param>
        private void UpdateViewShow(UserDto userDto)
        {
            if (toolBar.Tag.ToString() == "ADD")
            {
                // 更新表格关联的实体数据集
                userList.Add(userDto);

                // 更新表格显示
                if (gcUser.DataSource != userList)
                {
                    gcUser.DataSource = userList;
                    // 设置最后一行为焦点行
                    gvUser.FocusedRowHandle = gvUser.RowCount - 1;
                }
            }

            if (toolBar.Tag.ToString() == "EDIT")
            {
                // 更新表格显示
                gvUser.UpdateCurrentRow();
            }
        }

        #region 表格嵌入按钮的操作

        /// <summary>
        /// 删除选中的条目
        /// </summary>
        private void OnDelete()
        {
            if (MessageUtil.ShowYesNoAndWarning("确实要删除这条信息吗？") == DialogResult.No) return;

            try
            {
                // 从数据库中删除多条记录
                CallerFactory.Instance.GetService<IAuthorityService>().Delete((UserDto)gvUser.GetFocusedRow());
                // 从表格中删除记录
                gvUser.DeleteRow(gvUser.FocusedRowHandle);

                MessageUtil.ShowTips("删除成功！");
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.ToString());
            }
        }

        /// <summary>
        /// 密码修改
        /// </summary>
        private void OnPassword()
        {
            FormEFPassWord frmPasswordEdit = new FormEFPassWord();

            // 工具栏为编辑操作
            toolBar.Tag = "EDIT";
            // 获得表格中选中的记录
            frmPasswordEdit.EditedUserDto = gvUser.GetFocusedRow() as UserDto;
            // 订阅表格更新事件
            frmPasswordEdit.OnUpdateView += UpdateViewShow;

            frmPasswordEdit.ShowDialog();
        }

        /// <summary>
        /// 编辑条目
        /// </summary>
        private void OnEdit()
        {
            FormEFUserEdit frmUserEditInfo = new FormEFUserEdit();

            // 工具栏为编辑操作
            toolBar.Tag = "EDIT";
            // 对话框为编辑操作
            frmUserEditInfo.Tag = "EDIT";
            // 获得表格中选中的记录
            frmUserEditInfo.EditedUserDto = gvUser.GetFocusedRow() as UserDto;
            // 订阅表格更新事件
            frmUserEditInfo.OnUpdateView += UpdateViewShow;

            frmUserEditInfo.ShowDialog();
        }

        /// <summary>
        /// 表格中点击按钮操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonControl_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            switch (e.Button.ToolTip)
            {
                case "编辑":
                    OnEdit();
                    break;
                case "修改密码":
                    OnPassword();
                    break;
                case "删除":
                    OnDelete();
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region 工具栏操作

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormEFUserEdit frmUserEditInfo = new FormEFUserEdit();

            // 工具栏为新增操作
            toolBar.Tag = "ADD";
            // 对话框为新增操作
            frmUserEditInfo.Tag = "ADD";
            // 订阅表格更新事件
            frmUserEditInfo.OnUpdateView += UpdateViewShow;

            frmUserEditInfo.ShowDialog();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            int[] selectedIdx = gvUser.GetSelectedRows();
            if (selectedIdx.Length == 0)
            {
                MessageUtil.ShowWarning("请选择要删除的用户信息！");
                return;
            }

            string str = string.Format("确实要删除{0}条用户信息吗？", selectedIdx.Length);
            if (DialogResult.Yes == MessageUtil.ShowYesNoAndTips(str))
            {
                List<UserDto> toDelete = new List<UserDto>();
                foreach (int rowIndex in selectedIdx)
                {
                    // 获得待删除的用户信息
                    toDelete.Add((UserDto)gvUser.GetRow(rowIndex));
                }

                try
                {
                    // 批量删除
                    CallerFactory.Instance.GetService<IAuthorityService>().Delete(toDelete);
                    // 从表格中删除记录
                    gvUser.DeleteSelectedRows();

                    MessageUtil.ShowTips("删除成功！");
                }
                catch (Exception ex)
                {
                    MessageUtil.ShowWarning(ex.Message);
                }
            }
        }

        #endregion

        /// <summary>
        /// 定义单元格显示模式--密码框用“*”
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvUser_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "Password")
            {
                if (e.Value != null && e.Value.ToString().Length > 0)
                {
                    e.DisplayText = new string('*', 6);
                }
            }
        }
    }
}
