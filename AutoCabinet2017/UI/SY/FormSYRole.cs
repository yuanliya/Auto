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
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraEditors.Controls;

using AutoCabinet2017.Helper;
using AutoCabinet2017.UI.EF;

using NJUST.AUTO06.Utility;

using ZY.EntityFrameWork.Caller;
using ZY.EntityFrameWork.Caller.Facade;
using ZY.EntityFrameWork.Core.DBHelper;
using ZY.EntityFrameWork.Core.Model.Attibutes;
using ZY.EntityFrameWork.Core.Model.Dto;
using DevExpress.XtraTreeList.Nodes.Operations;
using System.Collections;

namespace AutoCabinet2017.UI.SY
{
    public partial class FormSYRole : XtraForm
    {
        public FormSYRole()
        {
            InitializeComponent();
        }

        private List<ModuleDto> roleAllModules;    // 角色的权限表（包含未授权的模块，权限为0）
        private List<ModuleDto> oldRoleAllModules; // 角色的权限表（包含未授权的模块，权限为0）
        private List<UserDto>   users;             // 角色拥有的用户

        /// <summary>
        /// 两个类对象的比较
        /// </summary>
        public class Comparer : IEqualityComparer<ModuleDto>
        {
            public bool Equals(ModuleDto x, ModuleDto y)
            {
                if (Object.ReferenceEquals(x, y)) return true;

                return x != null && y != null && x.ModuleName.Equals(y.ModuleName) && x.Permissions.Equals(y.Permissions) && x.Enabled.Equals(y.Enabled);
            }

            public int GetHashCode(ModuleDto obj)
            {
                int hash1 = obj.ModuleName == null ? 0 : obj.ModuleName.GetHashCode();
                int hash2 = obj.Permissions.GetHashCode();

                // 重新计算
                return hash1 ^ obj.Permissions.GetHashCode()^obj.Enabled.GetHashCode();
            }
        }

        #region 权限下拉框
      
        /// <summary>
        /// 权限表中嵌入下拉框
        /// </summary>
        /// <returns></returns>
        private RepositoryItemCheckedComboBoxEdit InitRepository()
        {
            RepositoryItemCheckedComboBoxEdit ri = new RepositoryItemCheckedComboBoxEdit();
          
            ri.Name = "cbxPermission"; //checkedComboBoxEdit1";
            ri.GetItemEnabled += new GetCheckedComboBoxItemEnabledEventHandler(this.cbxPermission_GetItemEnabled);

            // 生成下拉列表中的所有权限内容
            ri.SetFlags(typeof(PermissionValue));
            removeCombinedFlags(ri);
            
            return ri;
        }

        /// <summary>
        /// 设置下拉框条目的可用性，点击下拉框时调用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxPermission_GetItemEnabled(object sender, GetCheckedComboBoxItemEnabledEventArgs e)
        {
            if (tvRole.FocusedNode.GetDisplayText("RoleName") == "系统管理员")
                return;
            if (tvPermission.Nodes.Contains(tvPermission.FocusedNode) || !((ModuleDto)tvPermission.GetDataRecordByNode(tvPermission.FocusedNode)).Enabled)
            {
                // 父节点（一级功能模块）的权限功能禁用
                // 未启用的模块权限功能禁用
                e.Enabled = false;
            }
            else
            {
                // 设置二级子功能模块的权限
                if((e.Index > 0) && (PropertyHelper.Modules[tvPermission.FocusedNode.Id].Permissions&(0x01<<(e.Index-1)))==0)
                {
                    e.Enabled = false;
                }
            }
        }

        /// <summary>
        /// 移除复合项(保留Add、Edit、Delete、Control，去掉All)
        /// </summary>
        /// <param name="ri"></param>
        private void removeCombinedFlags(RepositoryItemCheckedComboBoxEdit ri)
        {
            for (int i = ri.Items.Count - 1; i > 0; i--)
            {
                Enum val1 = ri.Items[i].Value as Enum;
               
                for (int j = i - 1; j >= 0; j--)
                {
                    Enum val2 = ri.Items[j].Value as Enum;
                    if (val1.HasFlag(val2))
                    {
                        ri.Items.RemoveAt(i);
                        break;
                    }
                }
            }
        }      

        #endregion

        #region 权限树形目录的操作

        /// <summary>
        /// 选中树形目录的CheckBox时进入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvPermission_AfterCheckNode(object sender, NodeEventArgs e)
        {
            // 获得节点对应的模块实体
            ModuleDto item = (ModuleDto)e.Node.TreeList.GetDataRecordByNode(e.Node);
            // 模块是否可用
            item.Enabled = e.Node.Checked;

            // 递归处理该节点下的子节点对应模块
            if (e.Node.TreeList.OptionsBehavior.AllowRecursiveNodeChecking)
            {
                e.Node.TreeList.NodesIterator.DoLocalOperation(new SetBoundFieldTreeListOperation(), e.Node.Nodes);
            }
        }

        /// <summary>
        /// 根据模块是否可用设置对应节点的Checkbox状态
        /// </summary>
        public class InitialStateTreeListOperation : TreeListOperation
        {
            public override void Execute(TreeListNode node)
            {
                // 模块状态设置对应节点Checkbox
                ModuleDto item = (ModuleDto)node.TreeList.GetDataRecordByNode(node);
                node.Checked = item.Enabled;
            }
        }

        /// <summary>
        /// 根据节点状态设置对应模块是否可用
        /// </summary>
        public class SetBoundFieldTreeListOperation : TreeListOperation
        {
            public override void Execute(TreeListNode node)
            {
                // 节点Checkbox状态设置模块状态
                ModuleDto item = (ModuleDto)node.TreeList.GetDataRecordByNode(node);
                item.Enabled = node.Checked;
            }
        }

        #endregion

        private void FormSYRole_Load(object sender, EventArgs e)
        {
            lsbUsers.Text = "包含用户";

            // “角色”树形目录设置

            // 关联数据源
            tvRole.DataSource = CallerFactory.Instance.GetService<IAuthorityService>().GetAllRoles().OrderBy(q=>q.Level).ToList();
            // 角色焦点转移时，更新用户权限和包含用户的表
            tvRole.FocusedNodeChanged += tvRole_FocusedNodeChanged;
            // 默认焦点为第一个节点
            tvRole.FocusedNode = tvRole.Nodes[0];
            tvRole.SelectNode(tvRole.Nodes[0]);
            // 设置控件为焦点
            tvRole.Focus();

            // 根据角色确定对应的权限和用户
            RefreshView();     

            // “模块权限”树形目录设置
            tvPermission.KeyFieldName    = "ID";                                
            tvPermission.ParentFieldName = "ParentID";                  // 设置树形目录的父节点（根据数据源的字段）

            tvPermission.Columns[1].Visible    = false;                 // ”Enabled“列不可见
            
            tvPermission.Columns[2].ColumnEdit = InitRepository();      // ”权限“列嵌入CheckCombobox控件，并设置下拉框内容
            tvPermission.OptionsView.ShowCheckBoxes = true;             // 选项前加入CheckBox
            tvPermission.OptionsBehavior.AllowRecursiveNodeChecking = true; // 允许对节点的递归操作

            lsbUsers.ValueMember = "UserCode";
            // 显示用户信息
            lsbUsers.CustomItemDisplayText += (object sender1, CustomItemDisplayTextEventArgs e1) =>
            {
                // 显示内容：用户名和用户登录名
                e1.DisplayText = String.Format("{0}({1})", ((UserDto)e1.Item).UserName, ((UserDto)e1.Item).UserCode);
            };
        }

        /// <summary>
        /// 角色焦点切换时，更新权限表和包含用户表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvRole_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            RefreshView();         
        }

        /// <summary>
        /// 将模块表中的权限值更新为角色所拥有的权限
        /// </summary>
        private void FillRolePermissions(List<RoleModuleDto> roleModules)
        {
            // 系统具有的所有模块（必须是应用层面的DTO）
            roleAllModules = PropertyHelper.Modules.MapTo<List<ModuleDto>>();
            ModuleDto sys = roleAllModules.Find(q => q.ModuleName == "系统管理");
            //非系统管理员不可使用系统管理
            if (tvRole.FocusedNode.GetDisplayText("RoleName") != "系统管理员" && sys != null)
            {
                roleAllModules.RemoveAll(q => q.ParentID == sys.ID);
                roleAllModules.Remove(sys);
            }
            // 根据角色对模块的使用权限，给模块赋值
            for (int i = roleAllModules.Count - 1; i >= 0; i--)
            {
                RoleModuleDto dto = roleModules.Find(h => h.ModuleName == roleAllModules[i].ModuleName);
                if (dto != null)
                {
                    roleAllModules[i].Permissions = dto.Permissions;  // 模块内具体操作的使用权限
                    roleAllModules[i].Enabled = dto.Enabled;      // 模块是否可用
                }
                else if (PropertyHelper.CurrentUser.RoleName == "超级管理员" || PropertyHelper.RoleModules.Exists(h => h.ModuleName == roleAllModules[i].ModuleName && h.Enabled))
                {
                    roleAllModules[i].Permissions = 0;
                    roleAllModules[i].Enabled = false;
                }
                else if (PropertyHelper.CurrentUser.RoleName != "超级管理员" && roleAllModules[i].ParentID != null)
                {
                    roleAllModules.RemoveAt(i);//当前用户的角色没有的权限不可见
                }
            }

            //儿子不可用，父亲也不可用
            List<ModuleDto> parents = roleAllModules.Where(q => q.ParentID == null).ToList();
            parents.ForEach(q =>
                {
                    if (roleAllModules.Count(t => t.ParentID == q.ID) == 0)
                        roleAllModules.Remove(q);
                });

            // 保存修改前的副本
            oldRoleAllModules = new List<ModuleDto>();
            oldRoleAllModules = roleAllModules.MapTo<List<ModuleDto>>(); 

            // 给模块权限表关联数据源
            tvPermission.DataSource = roleAllModules;
            // 展开树形目录
            tvPermission.ExpandAll();
            if (tvRole.FocusedNode.GetDisplayText("RoleName") == "系统管理员" && PropertyHelper.CurrentUser.RoleName != "超级管理员")
            {
                tvPermission.OptionsView.ShowCheckBoxes = false;//没用？
                tvPermission.OptionsBehavior.ReadOnly = true;
                btnSave.Enabled = false;
            }
            else
            {
                tvPermission.OptionsView.ShowCheckBoxes = true;
                btnSave.Enabled = true;
                tvPermission.OptionsBehavior.ReadOnly = false;
                tvPermission.Columns["ModuleName"].OptionsColumn.AllowEdit = false;
                tvPermission.Columns["ModuleTag"].OptionsColumn.AllowEdit = false;
            }
            // 迭代执行节点对应模块的权限设置
            tvPermission.NodesIterator.DoOperation(new InitialStateTreeListOperation());

            // 根据子模块的权限选择情况确定父模块的选中状态
            foreach (TreeListNode node in tvPermission.Nodes)
            {
                int checkedNum = 0;

                // 遍历该模块下子模块的选中情况
                foreach (TreeListNode no in node.Nodes)
                {
                    if (no.Checked) checkedNum++;
                }

                if (checkedNum == 0)
                {
                    // 子模块都未选中
                    node.CheckState = CheckState.Unchecked;
                }
                else if (checkedNum == node.Nodes.Count)
                {
                    // 子模块全选中
                    node.CheckState = CheckState.Checked;
                }
                else
                {
                    // 子模块部分选中
                    node.CheckState = CheckState.Indeterminate; // 半选状态
                }
            }
        }

        /// <summary>
        /// 根据角色确定对应的权限和用户
        /// </summary>
        private void RefreshView()
        {
            // 角色拥有使用权限的模块
            List<RoleModuleDto> roleModules = CallerFactory.Instance.GetService<IAuthorityService>().GetRolePermissions(tvRole.FocusedNode.GetDisplayText("RoleName"));

            // 角色对所有模块具体的操作权限
            FillRolePermissions(roleModules);

            // 角色包含的用户列表
            users = CallerFactory.Instance.GetService<IAuthorityService>().GetRoleUsers(tvRole.FocusedNode.GetDisplayText("RoleName"));
            if (users == null)
            {
                // 如果该角色没有用户，则新生成空的用户list
                users = new List<UserDto>();
            }
            // 关联数据源   
            lsbUsers.DataSource = users;
        }

        /// <summary>
        /// 添加新角色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            OpenEditForm("Add");
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            // 当前拥有焦点的角色
            RoleDto role = (RoleDto)tvRole.GetDataRecordByNode(tvRole.FocusedNode);

            try 
            {
                // 数据库删除记录
                CallerFactory.Instance.GetService<IAuthorityService>().DeleteRole(role);

                MessageUtil.ShowTips("删除角色成功！");
                // 更新界面
                tvRole.DataSource = CallerFactory.Instance.GetService<IAuthorityService>().GetAllRoles();
                if (tvRole.Nodes.Count > 0)
                {
                    tvRole.SelectNode(tvRole.Nodes[0]);
                    // 更新权限表和用户表
                    RefreshView();
                }
                else
                {
                    tvPermission.DataSource = new List<ModuleDto>();
                    lsbUsers.DataSource = null;
                }
            }
            catch(Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
            }          
        }

        /// <summary>
        /// 编辑角色信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            OpenEditForm("Edit");
        }

        /// <summary>
        /// 为角色添加用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddUser_Click(object sender, EventArgs e)
        {
            FormEFUserList frmEFUser = new FormEFUserList();
            frmEFUser.RoleId = ((RoleDto)tvRole.GetDataRecordByNode(tvRole.FocusedNode)).ID;

            if(DialogResult.OK == frmEFUser.ShowDialog())
            {
                try
                {
                    // 用户表
                    lsbUsers.DataSource = users = CallerFactory.Instance.GetService<IAuthorityService>().GetRoleUsers(tvRole.FocusedNode.GetDisplayText("RoleName"));
                }
                catch (Exception ex)
                {
                    MessageUtil.ShowError(ex.Message);
                }  
            }
        }

        /// <summary>
        /// 保存角色权限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            tvPermission.CloseEditor();

            // 发生变化的模块实体
            List<ModuleDto> toUpdate = roleAllModules.Except(oldRoleAllModules, new Comparer()).ToList();
           
            // 获得拥有焦点的角色对象
            RoleDto selected = (RoleDto)tvRole.GetDataRecordByNode(tvRole.FocusedNode);
            List<RoleModuleDto> dtos = new List<RoleModuleDto>(toUpdate.Select(q =>
            {
                RoleModuleDto dto = new RoleModuleDto
                {
                    // 角色-模块关系实体中的角色信息
                    RoleId      = selected.ID,
                    RoleName    = selected.RoleName,
                    // 角色-模块关系实体中的模块信息
                    ModuleId    = q.ID,
                    Enabled     = q.Enabled,
                    ModuleName  = q.ModuleName,
                    Permissions = q.Permissions
                };

                return dto;
            }));

            try
            {
                if (tvRole.FocusedNode.GetDisplayText("RoleName") == "系统管理员")
                {
                    CallerFactory.Instance.GetService<IAuthorityService>().UpdateModules(toUpdate);
                    PropertyHelper.Modules = CallerFactory.Instance.GetService<IAuthorityService>().GetAllModules();
                }
                // 更新角色-模块关系记录
                CallerFactory.Instance.GetService<IAuthorityService>().UpdateRole(dtos);
                // 更新当前登录用户的权限信息
                PropertyHelper.RoleModules = CallerFactory.Instance.GetService<IAuthorityService>().GetRolePermissions(PropertyHelper.CurrentUser.RoleName);

                MessageUtil.ShowTips("保存成功！");
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 打开角色编辑对话框
        /// </summary>
        /// <param name="tag">对话框标签（“ADD”或“EDIT”）</param>
        private void OpenEditForm(string tag)
        {
            FormEFRole frmEFRole = new FormEFRole();
            frmEFRole.Tag = tag;

            if (tag == "Edit")
            {
                // 从树形目录节点得到角色实体
                frmEFRole.dto = (RoleDto)tvRole.GetDataRecordByNode(tvRole.FocusedNode);
            }
              
            if (DialogResult.OK == frmEFRole.ShowDialog())
            {
                // 树形目录关联所有角色信息
                tvRole.DataSource = CallerFactory.Instance.GetService<IAuthorityService>().GetAllRoles();
                
                // 遍历树形目录所有节点，设置焦点节点
                foreach (TreeListNode node in tvRole.GetNodeList())
                {
                    if (((RoleDto)tvRole.GetDataRecordByNode(node)).ID == frmEFRole.dto.ID)
                    {
                        tvRole.SelectNode(node);
                        break;
                    }
                }

                // 刷新界面显示
                RefreshView();
            }
        }

        /// <summary>
        /// 移除用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemoveUser_Click(object sender, EventArgs e)
        {
            if (lsbUsers.SelectedItems.Count == 0)
            {
                MessageUtil.ShowWarning("请选择待移除的用户!");
                return;
            }
            else
            {
                List<UserDto> users = new List<UserDto>();
                foreach(var obj in lsbUsers.SelectedItems)
                {
                    UserDto user = (UserDto)obj;
                    if(user.UserCode == PropertyHelper.CurrentUser.UserCode)
                    {
                        MessageUtil.ShowWarning("不能删除自身！");
                        return;
                    }
                    user.RoleName = null;
                    user.RoleId = null;
                    users.Add(user);
                }
                try
                {
                    if (CallerFactory.Instance.GetService<IAuthorityService>().Update(users) != 0)
                        MessageUtil.ShowTips("移除用户成功！");
                }
                catch (Exception ex)
                {
                    MessageUtil.ShowWarning("移除用户失败！"+ex.ToString());
                }
            }
        }

        private void tvPermission_BeforeCheckNode(object sender, CheckNodeEventArgs e)
        {
            if (tvRole.FocusedNode.GetDisplayText("RoleName") == "系统管理员" && PropertyHelper.CurrentUser.RoleName != "超级管理员")
            {
                e.CanCheck = false;
            }
        }
    }
}
