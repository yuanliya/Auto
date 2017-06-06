using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DevExpress.XtraTab;
using AutoCabinet2017.UI.OP;
using ZY.EntityFrameWork.Core.Model.Dto;
using NJUST.AUTO06.Utility;
using DevExpress.XtraBars.Navigation;

namespace AutoCabinet2017.Helper
{
    public class UIHelper
    {
        #region 单件模式

        UIHelper()
        {
        }

        public static readonly UIHelper Instance = new UIHelper();

        #endregion 

        #region 界面、菜单和工具栏按钮相关

        ///// <summary>
        ///// 显示弹出对话框
        ///// </summary>
        ///// <param name="tag"></param>
        //public void ShowDialogForm(string tag)
        //{
        //    switch (tag)
        //    {
        //        case "301": // “ 数据库备份”
        //            FormDBBackup frmDBBackup = new FormDBBackup();
        //            frmDBBackup.ShowDialog();

        //            break;

        //        case "302": // “ 数据库还原”
        //            FormDBRestore frmDBRestore = new FormDBRestore();
        //            frmDBRestore.ShowDialog();

        //            break;

        //        case "401": // “字段配置”
        //            FormFMFieldConfig frmFieldConfig = new FormFMFieldConfig();
        //            frmFieldConfig.Text = "字段配置";
        //            frmFieldConfig.ShowDialog();

        //            break;

        //        case "402": // “分类管理”
        //            FormFMFieldType frmArvFieldType = new FormFMFieldType();
        //            frmArvFieldType.Text = "分类管理";
        //            frmArvFieldType.ShowDialog();

        //            break;

        //        case "501": // “设备控制”
        //            FormDVDeviceControl frmDevControl = new FormDVDeviceControl();
        //            frmDevControl.ShowDialog();

        //            break;

        //        case "502": // “设备配置”
        //            FormDVSystemConfig frmDevConfig = new FormDVSystemConfig();
        //            frmDevConfig.Text = "设备配置";
        //            frmDevConfig.ShowDialog();

        //            break;

        //        case "503": // “串口配置”
        //            FormDVSerialPortConfig frmDVSpConfig = new FormDVSerialPortConfig();
        //            frmDVSpConfig.Text = "串口配置";
        //            frmDVSpConfig.ShowDialog();

        //            break;

        //        case "602": // 密码修改
        //            FormSYPassWord frmSYPassWord = new FormSYPassWord();
        //            frmSYPassWord.ShowDialog();

        //            break;

        //        case "604": // 模块配置
        //            FormSYModuleCfg frmSYModuleCfg = new FormSYModuleCfg();
        //            frmSYModuleCfg.ShowDialog();

        //            break;
        //    }
        //}

        /// <summary>
        /// 设置工具栏"新增"、"修改"、"删除"、"保存"和"取消"的状态
        /// </summary>
        /// <param name="tsButton"></param>
        public void SetToolButtonStatus(List<ToolStripButton> tsButton)
        {
            // 操作互斥的按钮状态
            foreach (ToolStripButton btn in tsButton)
            {
                btn.Enabled = !btn.Enabled;
            }
        }

        /// <summary>
        /// 设置指定的子菜单项是否可用
        /// </summary>
        /// <param name="menuItem">菜单项控件</param>
        /// <param name="MenuItemName">菜单项名字</param>
        /// <param name="isRight">是否有使用权限</param>
        /// <param name="isVisible">是否可见</param>
        private void CheckSubMenu(ToolStripMenuItem menuItem, string MenuItemName, string isRight, string isVisible)
        {
            // 禁用指定的子菜单选项
            if (menuItem.Text.Equals(MenuItemName))
            {
                menuItem.Enabled = (isRight == "1" ? true : false);
                menuItem.Visible = (isVisible == "1" ? true : false);
            }

            // 遍历子菜单选项
            for (int i = 0; i < menuItem.DropDownItems.Count; i++)
            {
                if (menuItem.DropDownItems[i] is ToolStripSeparator)
                {
                    // 分隔符不处理
                    continue;
                }
                else
                {
                    CheckSubMenu((ToolStripMenuItem)menuItem.DropDownItems[i], MenuItemName, isRight, isVisible);
                }
            }
        }

        /// <summary>
        /// 设置菜单和子菜单是否可用
        /// </summary>
        /// <param name="Menu">菜单控件</param>
        /// <param name="MenuItemName">子菜单名</param>
        /// <param name="isRight">是否有使用权限</param>
        /// <param name="isVisible">是否可见</param>
        public void CheckMenu(MenuStrip Menu, string MenuItemName, string isRight, string isVisible)
        {
            // 遍历主菜单
            foreach (ToolStripMenuItem n in Menu.Items)
            {
                // 遍历子菜单
                CheckSubMenu(n, MenuItemName, isRight, isVisible);
            }
        }

        #endregion

        #region TreeView相关

        /// <summary>
        /// 将treeView绑定到List
        /// </summary>
        /// <param name="tv">TreeView控件</param>
        /// <param name="list">绑定的List数据源</param>
        /// <param name="rootName">根节点的文本属性值</param>
        /// <param name="imgList">ImageList控件</param>
        public void LoadTreeView(TreeView tv, List<DataDictDto> list, string rootName, ImageList imgList)
        {
            TreeNode rootNode = null;
            tv.Nodes.Clear();                   // 清空节点
            tv.ImageList = imgList;             // 设定节点图标

            rootNode = new TreeNode();          // 创建根节点
            rootNode.Tag = null;
            rootNode.Text = rootName;           // 根节点标识
            rootNode.ImageIndex = 1;
            rootNode.SelectedImageIndex = 0;

            try
            {
                InitTree(list, rootNode, rootName);
                tv.Nodes.Add(rootNode);
                tv.ExpandAll();
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 通过递归生成treeview
        /// </summary>
        /// <param name="list">数据源</param>
        /// <param name="Nd">节点</param>
        /// <param name="fieldTypeName">父节点名</param>
        public void InitTree(List<DataDictDto> list, TreeNode Nd, string fieldTypeName)
        {
            TreeNode NewNode;

            for (int i = 0; i < list.Count; i++)
            {
                //寻找list元素中，FieldTypeName属性与父节点名相同的数据行，找到时生成子节点，显示文本为FieldTypeValue属性的值
                if (list[i].FieldTypeName == fieldTypeName)
                {
                    NewNode = new TreeNode(list[i].FieldTypeValue);
                    //NewNode.Tag = list[i].FieldTypeSerialNo;
                    NewNode.Tag = list[i].FieldTypeValue;
                    NewNode.ImageIndex = 1;
                    NewNode.SelectedImageIndex = 0;
                    Nd.Nodes.Add(NewNode);

                    InitTree(list, NewNode, list[i].FieldTypeValue);
                }
            }
        }

        #endregion

        /// <summary>
        /// 配置查询下拉框
        /// </summary>
        /// <param name="fieldCfgItems"></param>
        //public void ConfigQueryComboBox(System.Windows.Forms.ComboBox cbx, List<ArvFieldCfg> fieldCfgItems)
        //{
        //    if (fieldCfgItems.Count == 0) return;

        //    // 清除下拉框
        //    cbx.Items.Clear();

        //    // 配置下拉框
        //    foreach (ArvFieldCfgDto fieldCfg in fieldCfgItems)
        //    {
        //        // 加入下拉列表
        //        if (fieldCfg.IsKeyWord)
        //        {
        //            // 配置下拉列表的显示名和字段名映射
        //            ItemEx item = new ItemEx(fieldCfg.FieldName, fieldCfg.FieldShowName);
        //            cbx.Items.Add(item);
        //            if (item.Tag.ToString() == "ArvID")
        //            {
        //                cbx.SelectedItem = item;//设置默认查询条件为档案编号
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// 配置查询关键字下拉框
        /// </summary>
        /// <param name="cbx"></param>
        /// <param name="fieldCfgItems"></param>
        //public void ConfigKeyWordComboBox(System.Windows.Forms.ComboBox cbx, string queryField)
        //{
        //    // 获取分类信息
        //    List<FieldType> Items = PropertyHelper.FieldTypeItems.Where<FieldType>(item => item.FieldTypeName == queryField)
        //                                                       .OrderBy(item => item.FieldTypeSerialNo)
        //                                                       .ToList<FieldType>();

        //    if (Items.Count != 0)
        //    {
        //        // 若有分类信息，则设置为下拉框选择，不可直接输入
        //        cbx.DropDownStyle = ComboBoxStyle.DropDownList;

        //        foreach (FieldType item in Items)
        //        {
        //            cbx.Items.Add(item.FieldTypeValue);
        //        }
        //    }
        //    else
        //    {
        //        // 若无分类信息，则由用户手动输入
        //        cbx.DropDownStyle = ComboBoxStyle.DropDown;
        //    }
        //}
        #endregion

        
    }
}
