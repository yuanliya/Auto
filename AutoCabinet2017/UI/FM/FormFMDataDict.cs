using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;

using AutoCabinet2017.UI.EF;
using AutoCabinet2017.Helper;

using NJUST.AUTO06.Utility;

using ZY.EntityFrameWork.Core.Model.Dto;
using ZY.EntityFrameWork.Caller;
using ZY.EntityFrameWork.Caller.Facade;

namespace AutoCabinet2017.UI.FM
{
    /// <summary>
    /// 数据字典管理UI
    /// </summary>
    public partial class FormFMDataDict : XtraForm
    {
        public FormFMDataDict()
        {
            InitializeComponent();
        }

        private List<DataDictDto> dataDictList      = null;                            // 所有的字典项
        private BindingList<DataDictDto> blDataDict = new BindingList<DataDictDto>();  // 当前绑定的字典项

        /// <summary>
        /// 清除UI显示信息
        /// </summary>
        private void ClearInfo()
        {
            lblDictItem.Text   = "";
            slueditField.Enabled   = false;
            slueditField.EditValue = null;

            // 禁用工具栏操作
            foreach (BarItemLink ctr in toolBar.ItemLinks)
            {
                ctr.Item.Enabled = false;
            }
        }

        /// <summary>
        /// 加载信息
        /// </summary>
        private void LoadInfo()
        {
            // 树形结构选中的条目
            lblDictItem.Text   = tvType.SelectedNode.Text;
            slueditField.Enabled = true;
            slueditField.ReadOnly = true;
            slueditField.EditValue = tvType.SelectedNode.Tag.ToString();

            // 使能工具栏操作
            foreach (BarItemLink ctr in toolBar.ItemLinks)
            {
                ctr.Item.Enabled = true;
            }
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        private void UpdateInfo()
        {
            // 从数据库读取字典全部内容
            dataDictList = PropertyHelper.DataDictItems = CallerFactory.Instance.GetService<ISystemConfigService>().GetAllDataDicts();

            // 如果有选定的字典明细，则显示该明细的内容
            if((tvType.SelectedNode != null) && (tvType.SelectedNode != tvType.TopNode))
            {
                // Linq筛选当前字典大类的项
                blDataDict = new BindingList<DataDictDto>(PropertyHelper.DataDictItems.Where(q => q.FieldTypeName == tvType.SelectedNode.Tag.ToString()).OrderBy(q => q.FieldTypeSerialNo).ToList());
                // 绑定表格显示
                gcUser.DataSource = blDataDict;
                // 加载信息
                LoadInfo();
            }
        }

        /// <summary>
        /// 树形结构的节点显示中文字段名
        /// </summary>
        private void ConvertToShowName()
        {
            // 遍历树形节点
            foreach (TreeNode node in tvType.Nodes[0].Nodes)
            {
                if (node.Tag != null)
                {
                    // 查找字段的中文显示名
                    FieldCfgDto cfg = PropertyHelper.FieldCfgItems.Find(q => q.FieldName == node.Tag.ToString());
                    if (cfg != null)
                    {
                        // 显示中文字段名
                        node.Text = cfg.FieldShowName;
                    }
                }
            }
        }

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
                // 寻找list元素中，FieldTypeName属性与父节点名相同的数据行，找到时生成子节点，显示文本为FieldTypeValue属性的值
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

        private void FormFMDataDict_Load(object sender, EventArgs e)
        {
            // 设置工具栏和表格操作控件的使用权限
            if (PropertyHelper.CurrentUser.RoleName != "超级管理员")
            {
                RoleModuleDto dto = PropertyHelper.RoleModules.Find(q => q.ModuleTag.ToString() == "402");
                if (dto != null)
                {
                    // 工具栏
                    toolBar.Authorize(dto.Permissions);
                    // 表格嵌入控件
                    buttonEdit.Authorize(dto.Permissions);
                }
            }

            // 表格数据绑定
            gcUser.DataSource = blDataDict;

            // 根据列标题设定表格中不可见的列
            string[] cols = new string[] { "ID", "FieldTypeName" };
            GridControlHelper.Instance.SetColumnInvisible(gvDataDict, cols);
            // 为表格加入控制列
            GridControlHelper.Instance.AddControlButton(gvDataDict, buttonEdit, gvDataDict.Columns.Count);
            GridControlHelper.Instance.ConfigGridViewStyle(gvDataDict);

            // 提取数据字典的一级节点--分类管理
            dataDictList = PropertyHelper.DataDictItems.Where(q => q.FieldTypeName == "分类管理").OrderBy(item => item.FieldTypeSerialNo).ToList<DataDictDto>();
            // 加载树形目录
            LoadTreeView(tvType, dataDictList, "分类管理", imageList1);
            // 树形节点显示中文名
            ConvertToShowName();

            ctrlPanel.Tag = "";
            // 清空UI显示
            ClearInfo();

            // 为SearchLookUpEdit控件绑定数据源

            // 下拉框显示字段和相应的中文显示名称
            // 选择只有在界面上配置为Combobox复选框和可用的字段
            slueditField.Properties.DataSource    = PropertyHelper.FieldCfgItems.Where(q => q.FieldType == "复选" && q.IsFieldUsable == true).Select(q => new { q.FieldName, q.FieldShowName }).ToList();
            // 实际显示字段名
            slueditField.Properties.DisplayMember = "FieldName";    
            slueditField.Properties.ValueMember   = "FieldName";

            // 自定义SearchLookUpEdit控件的表格列

            // 映射数据源的"FieldName"字段
            GridColumn gcFieldName = slueditField.Properties.View.Columns.AddField("FieldName");
            gcFieldName.Visible = true;
            gcFieldName.Caption = "字段";

            // 映射数据源的"FieldShowName"字段
            GridColumn gcFieldShowName = slueditField.Properties.View.Columns.AddField("FieldShowName");
            gcFieldShowName.Caption = "名称";
            gcFieldShowName.Visible = true;
        }

        #region 处理树形节点的拖拽事件

        private Point Position = new Point(0, 0);

        /// <summary>
        /// 树形节点被拖拽
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvType_ItemDrag(object sender, ItemDragEventArgs e)
        {
            // 开始拖放操作
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        /// <summary>
        /// 树形节点被拖放进入另外一个节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvType_DragEnter(object sender, DragEventArgs e)
        {
            // 确定当前拖动控件是否为树形节点
            if (e.Data.GetDataPresent(typeof(TreeNode)))
            {
                e.Effect = DragDropEffects.Move;  // 将拖动源的数据移动到放置目标
            }
            else
            {
                e.Effect = DragDropEffects.None;  // 放置目标不接受该数据
            }
        }

        /// <summary>
        /// 拖放操作完成时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvType_DragDrop(object sender, DragEventArgs e)
        {
            TreeNode myNode = null;

            // 判断当前拖动控件是否是树形节点
            if (e.Data.GetDataPresent(typeof(TreeNode)))
            {
                // 读取拖动的树形节点
                myNode = (TreeNode)(e.Data.GetData(typeof(TreeNode)));
            }
            else
            {
                MessageUtil.ShowError("不是有效的树形节点");
            }

            // 屏幕坐标和工作区坐标转换
            Position.X = e.X;
            Position.Y = e.Y;
            Position = tvType.PointToClient(Position);

            // 获取鼠标当前位置的树形节点
            TreeNode DropNode = this.tvType.GetNodeAt(Position);

            // 1.目标节点不是空。2.目标节点不是被拖拽节点本身  
            if (DropNode != null && DropNode != myNode)
            {
                // 在目标节点下增加被拖拽节点  
                TreeNode newNode = (TreeNode)myNode.Clone();      // 克隆选中的节点  

                //if (DropNode.Parent == null)
                if (myNode.Parent == DropNode)
                {
                    // 如果当前位置为父节点，则将节点作为子节点的首位插入
                    DropNode.Nodes.Insert(0, newNode);
                }
                else if (myNode.Parent == DropNode.Parent)
                {
                    // 将节点作为当前子节点的下一位插入
                    DropNode.Parent.Nodes.Insert(DropNode.Index + 1, newNode);
                }
                else
                {
                    MessageUtil.ShowWarning("不可移动到该位置！");
                    return;
                }

                // 删除原节点
                myNode.Remove();

                UpdateNode(newNode.Parent);
            }
        }

        /// <summary>
        /// 选中树形节点，进入编辑状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvArvType_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (tvType.SelectedNode == null || tvType.SelectedNode.Parent == null)
            {
                return;
            }

            if (tvType.SelectedNode.Tag != null)
            {
                // 根据类别绑定条目信息
                blDataDict = new BindingList<DataDictDto>(PropertyHelper.DataDictItems.Where(q => q.FieldTypeName == tvType.SelectedNode.Tag.ToString()).OrderBy(q => q.FieldTypeSerialNo).ToList());//当前字典大类的项
                gcUser.DataSource = blDataDict;
                // 设置按钮和文本
                LoadInfo();
            }
            else
            {
                slueditField.Enabled   = true;
                slueditField.ReadOnly  = false;
                slueditField.EditValue = null;
            }
        }

        /// <summary>
        /// SearchLookUpEdit编辑框选定值发生变化后的操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void slueditField_EditValueChanged(object sender, EventArgs e)
        {
            // tvType.SelectedNode.Tag为null，说明是新增字典条目，slueditField.EditValue不为null，表示选定了其关联的字段
            if (slueditField.EditValue != null && tvType.SelectedNode.Tag == null)
            {
                // 选中字段的中文显示名
                tvType.SelectedNode.Text = lblDictItem.Text = PropertyHelper.FieldCfgItems.Find(q => q.FieldName == slueditField.EditValue.ToString()).FieldShowName;
                // Tag保存字段名
                tvType.SelectedNode.Tag = slueditField.EditValue;

                // 当前操作设置为“ADD”
                ctrlPanel.Tag = "ADD";
                // 使能工具栏操作
                foreach (BarItemLink ctr in toolBar.ItemLinks)
                {
                    ctr.Item.Enabled = true;
                }
            }
        }

        /// <summary>
        /// SearchLookUpEdit编辑框选择新的值时的操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void slueditField_EditValueChanging(object sender, ChangingEventArgs e)
        {
            // tvType.SelectedNode.Tag为null，说明是新增字典条目，要确定其关联的字段e.NewValue不能被别的条目关联
            if (tvType.SelectedNode.Tag == null && e.NewValue != null)
            {
                // 选中的字段已经被别的条目关联
                if (PropertyHelper.DataDictItems.Exists(q => q.FieldTypeName == "分类管理" && q.FieldTypeValue == e.NewValue.ToString()) == true)
                {
                    MessageUtil.ShowError("该字段已有字典关联！");
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// 删除或移动节点后更新字段
        /// </summary>
        /// <param name="node">节点</param>
        void UpdateNode(TreeNode node)
        {
            DataDictDto fieldInfo = new DataDictDto();

            foreach (TreeNode tn in node.Nodes)
            {
                // 更新移动树形节点的数据库记录
                if (tn.Parent.Tag != null)
                {
                    fieldInfo.FieldTypeName = tn.Parent.Tag.ToString();
                }
                else
                {
                    fieldInfo.FieldTypeName = tn.Parent.Text;
                }

                fieldInfo.FieldTypeValue = tn.Text;
                fieldInfo.FieldTypeSerialNo = tn.Index;

                CallerFactory.Instance.GetService<ISystemConfigService>().UpdateDataDict(fieldInfo);
            }

            PropertyHelper.DataDictItems = CallerFactory.Instance.GetService<ISystemConfigService>().GetAllDataDicts();
        }

        #endregion

        #region 数据库操作
       
        /// <summary>
        /// 加入树形结构的根节点
        /// </summary>
        /// <returns></returns>
        private bool AddParent()
        {
            if (dataDictList.Exists(q => q.FieldTypeValue == slueditField.EditValue.ToString()))   return true;

            DataDictDto dto = new DataDictDto();

            dto.FieldTypeName = "分类管理";
            dto.FieldTypeValue = slueditField.EditValue.ToString();
            dto.FieldTypeSerialNo = tvType.TopNode.GetNodeCount(false) - 1;

            try
            {
                // 记录写入数据库
                CallerFactory.Instance.GetService<ISystemConfigService>().Add(dto);
                return true;
            }
            catch (Exception ex)
            {
                MessageUtil.ShowWarning(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 添加纪录
        /// </summary>
        /// <param name="dto">实体对象</param>
        private void Add(DataDictDto dto)
        {
            if (PropertyHelper.DataDictItems.Count<DataDictDto>(q => q.FieldTypeName == dto.FieldTypeName
                           && q.FieldTypeValue == dto.FieldTypeValue) > 0)
            {
                MessageUtil.ShowWarning("重复的条目值，请重新输入!");
                return;
            }

            try
            {
                // 记录写入数据库
                CallerFactory.Instance.GetService<ISystemConfigService>().Add(dto);
                MessageUtil.ShowTips("添加新条目成功!");
            }
            catch (Exception ex)
            {
                MessageUtil.ShowWarning(ex.Message);
            }
        }

        /// <summary>
        /// 更新纪录
        /// </summary>
        /// <param name="fieldInfo">实体对象</param>
        private void Update(DataDictDto fieldInfo)
        {
            try
            {
                // 更新作为子节点的记录
                CallerFactory.Instance.GetService<ISystemConfigService>().UpdateDataDict(fieldInfo);
                // 禁止编辑标签文本
                tvType.LabelEdit = false;

                MessageUtil.ShowTips("更新条目成功!");
            }
            catch (Exception ex)
            {
                MessageUtil.ShowWarning(ex.Message);
            }
        }


        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="subNodes">树的子节点集合</param>
        /// <param name="tn">待删除节点</param>
        public void Delete(List<TreeNode> subNodes, TreeNode tn)
        {
            // 节点实体对象
            DataDictDto fieldInfo = new DataDictDto();

            try
            {
                foreach (TreeNode node in subNodes)
                {
                    // 确定子节点的父节点
                    if (node.Parent.Tag != null)
                    {
                        fieldInfo.FieldTypeName = node.Parent.Tag.ToString();
                    }
                    else
                    {
                        fieldInfo.FieldTypeName = node.Parent.Text;
                    }

                    fieldInfo.FieldTypeValue = node.Text;    // 节点值
                    fieldInfo.FieldTypeSerialNo = node.Index;   // 节点序号

                    // 从数据库中删除节点
                    CallerFactory.Instance.GetService<ISystemConfigService>().Delete(fieldInfo);
                }

                // 删除视图上指定节点
                tvType.Nodes.Remove(tn);

                MessageUtil.ShowTips("条目已删除!");
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
            }
        }

        #endregion

        /// <summary>
        /// “添加字典类别”--右键操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 创建子节点
            TreeNode childNode = new TreeNode();

            childNode.Text = "请选择关联字段";
            childNode.ImageIndex = 1;
            childNode.SelectedImageIndex = 0;

            // 加入树形目录
            tvType.TopNode.Nodes.Add(childNode);
            tvType.SelectedNode = childNode;

            lblDictItem.Text = "";
            slueditField.EditValue = null;
            slueditField.Enabled = true;
            
            // 表格关联数据源
            blDataDict = new BindingList<DataDictDto>();
            gcUser.DataSource = blDataDict;
        }

        /// <summary>
        /// “删除字典类别”--右键操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 不能删除根节点
            if (tvType.SelectedNode == tvType.TopNode)
            {
                MessageUtil.ShowWarning("无法删除该节点！");
                return;
            }

            if(tvType.SelectedNode == null)
            {
                MessageUtil.ShowWarning("请选择要删除的节点！");
                return;
            }
            
            try
            {
                // 如果字典条目还没有关联字段，直接移除树形节点
                if(tvType.SelectedNode.Tag == null)
                {
                    // 删除视图上指定节点
                    tvType.Nodes.Remove(tvType.SelectedNode);
                    return;
                }

                List<DataDictDto> delList = new List<DataDictDto>(); // 待删除字典项

                if (dataDictList.Count(q => q.FieldTypeValue == tvType.SelectedNode.Tag.ToString()) != 0)
                {
                    delList.Add(dataDictList.Find(q => q.FieldTypeValue == tvType.SelectedNode.Tag.ToString()));      // 根
                    delList.AddRange(dataDictList.Where(q => q.FieldTypeName == tvType.SelectedNode.Tag.ToString())); // 字典的子项
                    // 删除数据库记录
                    CallerFactory.Instance.GetService<ISystemConfigService>().Delete(delList);
                }

                // 删除视图上指定节点
                tvType.Nodes.Remove(tvType.SelectedNode);
                // 更新属性集中的字典记录
                PropertyHelper.DataDictItems = CallerFactory.Instance.GetService<ISystemConfigService>().GetAllDataDicts();

                MessageUtil.ShowTips("删除成功！");     
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.ToString());
            }          
        }

        /// <summary>
        /// 表格中的编辑按钮操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            FrmDataDictInfo frmInfo = new FrmDataDictInfo();

            frmInfo.Tag         = "EDIT";
            frmInfo.dataDictDto = (DataDictDto)gvDataDict.GetFocusedRow();
            
            if(frmInfo.ShowDialog()==DialogResult.OK)
            {
                // 更新信息
                UpdateInfo();
            }
        }

        #region 工具栏操作

        private void toolAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (AddParent())
            {
                // 字典的实体类
                DataDictDto dto = new DataDictDto();
                dto.FieldTypeName = slueditField.EditValue.ToString();
                dto.FieldTypeSerialNo = gvDataDict.RowCount;

                // 字典编辑对话框
                FrmDataDictInfo frmInfo = new FrmDataDictInfo();
                frmInfo.Tag = "ADD";
                frmInfo.dataDictDto = dto;

                if (frmInfo.ShowDialog() == DialogResult.OK)
                {
                    // 更新数据库
                    UpdateInfo();

                    if (slueditField.Enabled)
                    {
                        slueditField.Enabled = false;
                    }
                }
            }

            // 设置当前状态为“新增”
            ctrlPanel.Tag = "ADD";
        }

        private void toolDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // 是否选中条目
            int[] selected = gvDataDict.GetSelectedRows();
            if (selected.Length == 0)
            {
                MessageUtil.ShowWarning("请选择要删除的信息！");
                return;
            }

            // 表格选中的记录
            DataDictDto item = (DataDictDto)gvDataDict.GetFocusedRow();

            string str = string.Format("确实要删除该用户信息吗？");
            if (DialogResult.Yes == MessageUtil.ShowYesNoAndTips(str))
            {
                int[] idx = selected.Reverse().ToArray();

                try
                {
                    foreach (int rowIndex in idx)
                    {
                        // 表格中删除选中的列
                        DataDictDto dataDict = (DataDictDto)gvDataDict.GetRow(rowIndex);
                        gvDataDict.DeleteRow(rowIndex);

                        // 在数据库中删除信息
                        CallerFactory.Instance.GetService<ISystemConfigService>().Delete(dataDict);
                    }

                    MessageUtil.ShowTips("删除成功！");
                }
                catch (Exception ex)
                {
                    MessageUtil.ShowWarning(ex.Message);
                }
            }
        }

        #endregion
    }
}
