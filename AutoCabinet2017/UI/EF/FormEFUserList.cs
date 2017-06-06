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
using DevExpress.XtraBars;

using ZY.EntityFrameWork.Caller;
using ZY.EntityFrameWork.Caller.Facade;
using ZY.EntityFrameWork.Core.DBHelper;
using ZY.EntityFrameWork.Core.Model.Dto;

using AutoCabinet2017.Helper;

using NJUST.AUTO06.Utility;

namespace AutoCabinet2017.UI.EF
{
    public partial class FormEFUserList : Form
    {
        public FormEFUserList()
        {
            InitializeComponent();
        }

        private BindingList<UserDto> userList = new BindingList<UserDto>();
        private BindingList<UserDto> selectedUserList = new BindingList<UserDto>();

        // 角色编号
        public string RoleId { get; set; }

        // <summary>
        /// 配置表格的控制列
        /// 只需要对表格操作一次，以后表格再关联数据源都无需再处理
        /// </summary>
        private void ConfigGridView()
        {
            // 根据列标题设定表格中不可见的列
            string[] cols = new string[] { "ID", "RoleId" };
            GridControlHelper.Instance.SetColumnInvisible(gvUser, cols);
        }

        private void FormEFUser_Load(object sender, EventArgs e)
        {
            // 加载用户信息
            LoadUserInfo();
            // 配置表格格式
            ConfigGridView();
        }

        /// <summary>
        /// 根据登录用户角色加载用户信息
        /// </summary>
        /// <returns></returns>
        private void LoadUserInfo()
        {
            try
            {
                // 查询所有用户角色信息
                List<RoleDto> roleItems = CallerFactory.Instance.GetService<IAuthorityService>().GetAllRoles();
                foreach (RoleDto item in roleItems)
                {
                    cbxUserRole.Properties.Items.Add(item.RoleName);
                }
            }
            catch(Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
            }

            try
            {
                // 部门分类信息-控件扩展方法
                cbxUserDept.AddItems("ArvUnit");
            }
            catch (Exception ex)
            {
                MessageUtil.ShowWarning(ex.Message);
            }

            try 
            {
                // 查询所有用户信息
                List<UserDto> userItems = CallerFactory.Instance.GetService<IAuthorityService>().GetAllUsers();
                // 关联数据源表格(只列出所有不是当前角色的用户)
                gcUser.DataSource = userList = new BindingList<UserDto>(userItems.Where<UserDto>(q => q.RoleId != RoleId).ToList());
            }
            catch (Exception ex)
            {
                MessageUtil.ShowWarning(ex.Message);
            }
        }

        /// <summary>
        /// 检索操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolQuery_ItemClick(object sender, ItemClickEventArgs e)
        {
            List<QueryCondition> conditions = new List<QueryCondition>();

            // 根据常规搜索panel的内容构建搜索条件
            QueryCondition condition = new QueryCondition(edtUserCode.Tag.ToString(), CompareType.Include, edtUserCode.EditValue == null ? "" : edtUserCode.EditValue.ToString());
            conditions.Add(condition);
            // 执行搜索操作
            userList = new BindingList<UserDto>(CallerFactory.Instance.GetService<IAuthorityService>().FindUsers(conditions));
            // 关联到表格
            gcUser.DataSource = userList;
        }

        /// <summary>
        /// 展开高级搜索面板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolAdvancedQuery_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (panelSearch.Visible)
            {
                panelSearch.Visible = false;
            }
            else
            {
                panelSearch.Visible = true;
            }              
        }

        /// <summary>
        /// 高级检索的搜索操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdvanced_Click(object sender, EventArgs e)
        {
            // 根据常规搜索panel的内容构建搜索条件
            List<QueryCondition> conditions = new List<QueryCondition>();

            // 常规搜索的查询条件
            conditions.Add(QueryHelper.GetConditionModel(edtUserCode.Tag.ToString(), CompareType.Include, edtUserCode.EditValue == null ? "" : edtUserCode.EditValue.ToString()));
            if (conditions != null)
            {
                // 根据高级搜索panel的内容构建搜索条件
                conditions.AddRange(QueryHelper.GetIntegrativeCondition(panelSearch));
                // 执行搜索操作
                userList = new BindingList<UserDto>(CallerFactory.Instance.GetService<IAuthorityService>().FindUsers(conditions));
                // 关联到表格
                gcUser.DataSource = userList;
            }
        }

        /// <summary>
        /// 定义单元格显示模式--密码框用“*”
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvUser_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "Password")
            {
                if (e.Value != null && e.Value.ToString().Length > 0)
                {
                    e.DisplayText = new string('*', 6);
                }
            }
        }

        /// <summary>
        /// 添加操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            // 表格中选中的行
            int[] selectedIdx = gvUser.GetSelectedRows();
            if (selectedIdx.Length == 0)
            {
                MessageUtil.ShowWarning("请选择要加入的用户！");
                return;
            }

            string msg = string.Format("确实要添加这{0}位用户吗？", selectedIdx.Length);
            if (MessageUtil.ShowYesNoAndWarning(msg) == DialogResult.Yes)
            {
                // 选中的用户
                foreach(int idx in selectedIdx)
                {
                    selectedUserList.Add((UserDto)gvUser.GetRow(idx));
                }
            }

            try
            {
                // 给选中用户关联新的角色ID
                selectedUserList.ToList().ForEach(q => q.RoleId = RoleId);
                // 更新用户信息
                CallerFactory.Instance.GetService<IAuthorityService>().Update(selectedUserList.ToList());
                // 更新成功
                this.DialogResult = DialogResult.OK;

                this.Close();
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
            }
        }
    }
}
