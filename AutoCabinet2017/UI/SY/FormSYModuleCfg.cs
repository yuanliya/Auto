using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Collections;

using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using ZY.EntityFrameWork.Core.Model.Dto;
using AutoCabinet2017.Helper;
using NJUST.AUTO06.Utility;
using ZY.EntityFrameWork.Core.DBHelper;
using ZY.EntityFrameWork.Caller;
using ZY.EntityFrameWork.Caller.Facade;
using DevExpress.XtraTreeList.Nodes.Operations;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;
using DevExpress.XtraEditors.Repository;
using ZY.EntityFrameWork.Core.Model.Attibutes;


namespace AutoCabinet2017.UI.SY
{
    public partial class FormSYModuleCfg : Form
    {
        public FormSYModuleCfg()
        {
            InitializeComponent();
        }


        private List<ModuleDto> curModules;    // 角色的权限表（包含未授权的模块，权限为0）

        #region 权限下拉框

        /// <summary>
        /// 权限表中嵌入下拉框
        /// </summary>
        /// <returns></returns>
        private RepositoryItemCheckedComboBoxEdit InitRepository()
        {
            RepositoryItemCheckedComboBoxEdit ri = new RepositoryItemCheckedComboBoxEdit();

            ri.Name = "cbxPermission"; //checkedComboBoxEdit1";

            // 生成下拉列表中的所有权限内容
            ri.SetFlags(typeof(PermissionValue));
            removeCombinedFlags(ri);

            return ri;
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
        private void tvModule_AfterCheckNode(object sender, NodeEventArgs e)
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
                return hash1 ^ obj.Permissions.GetHashCode() ^ obj.Enabled.GetHashCode();
            }
        }

        private void FormSYModuleCfg_Load(object sender, EventArgs e)
        {
            tvModule.DataSource=curModules = PropertyHelper.Modules.MapTo<List<ModuleDto>>();//建立副本

            // “模块权限”树形目录设置
            tvModule.KeyFieldName = "ID";
            tvModule.ParentFieldName = "ParentID";                  // 设置树形目录的父节点（根据数据源的字段）

            tvModule.Columns[1].Visible = false;                 // ”Enabled“列不可见
            tvModule.Columns["ModuleName"].OptionsColumn.AllowEdit = false;
            //tvModule.Columns["ModuleTag"].OptionsColumn.AllowEdit = false;
            tvModule.Columns[2].ColumnEdit = InitRepository();      // ”权限“列嵌入CheckCombobox控件，并设置下拉框内容
            tvModule.OptionsView.ShowCheckBoxes = true;             // 选项前加入CheckBox
            tvModule.OptionsBehavior.AllowRecursiveNodeChecking = true; // 允许对节点的递归操作

            // 展开树形目录
            tvModule.ExpandAll();
            // 迭代执行节点对应模块的权限设置
            tvModule.NodesIterator.DoOperation(new InitialStateTreeListOperation());

            // 根据子模块的权限选择情况确定父模块的选中状态
            foreach (TreeListNode node in tvModule.Nodes)
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            tvModule.CloseEditor();
            try
            {
                // 发生变化的模块实体
                List<ModuleDto> toUpdate = curModules.Except(PropertyHelper.Modules, new Comparer()).ToList();
                // 更新角色-模块关系记录
                CallerFactory.Instance.GetService<IAuthorityService>().UpdateModules(toUpdate);

                //若数据库中无系统管理员身份，则添加该身份
                RoleDto sysRole = CallerFactory.Instance.GetService<IAuthorityService>().GetAllRoles().Find(q => q.RoleName == "系统管理员");
                if(sysRole==null)
                {
                    //初始化
                    sysRole = new RoleDto { ID = Guid.NewGuid().ToString("N"), RoleName = "系统管理员", SerialNo = 0 };
                    CallerFactory.Instance.GetService<IAuthorityService>().AddRole(sysRole);

                    List<RoleModuleDto> dtos = new List<RoleModuleDto>(curModules.Select(q =>
                    {
                        RoleModuleDto dto = new RoleModuleDto
                        {
                            // 角色-模块关系实体中的角色信息
                            RoleId = sysRole.ID,
                            RoleName = sysRole.RoleName,
                            // 角色-模块关系实体中的模块信息
                            ModuleId = q.ID,
                            Enabled = q.Enabled,
                            ModuleName = q.ModuleName,
                            Permissions = q.Permissions
                        };

                        return dto;
                    }));

                    // 更新系统管理员角色-模块关系记录
                    CallerFactory.Instance.GetService<IAuthorityService>().UpdateRole(dtos);
                }

                // 更新当前登录用户的权限信息
                PropertyHelper.Modules = CallerFactory.Instance.GetService<IAuthorityService>().GetAllModules();

                ////若无系统管理员用户，则增加系统管理员用户
                List<UserDto> users=CallerFactory.Instance.GetService<IAuthorityService>().GetRoleUsers("系统管理员");
                if (CallerFactory.Instance.GetService<IAuthorityService>().GetRoleUsers("系统管理员").Count ==0)
                {
                    UserDto user = new UserDto
                    {
                        ID = Guid.NewGuid().ToString("N"),
                        UserCode = "sysadmin",
                        Password = "sysadmin123",
                        UserName = "系统管理员",
                        RoleId = sysRole.ID
                    };
                    CallerFactory.Instance.GetService<IAuthorityService>().Add(user);
                }

                MessageUtil.ShowTips("保存成功！");

                
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
            }
        }


       
    }
}
