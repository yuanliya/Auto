using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Windows.Forms;

using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors;
using DevExpress.XtraBars;

using ZY.EntityFrameWork.Core.Model.Dto;
using ZY.EntityFrameWork.Caller;
using ZY.EntityFrameWork.Caller.Facade;

using AutoCabinet2017.Helper;
using NJUST.AUTO06.Utility;

namespace AutoCabinet2017.UI.FM
{
    public partial class FormFMFieldCfg : XtraForm
    {
        public FormFMFieldCfg()
        {
            InitializeComponent();
        }
        
        private List<FieldCfgDto> cfgList = new List<FieldCfgDto>();
        private List<string> initialNames = new List<string>();//修改前的显示名称  

        private void FormViewConfig_Load(object sender, EventArgs e)
        {
            // 根据用户角色设置工具栏和嵌入式按钮的使用权限
            if (PropertyHelper.CurrentUser.RoleName != "超级管理员")
            {
                RoleModuleDto dto = PropertyHelper.RoleModules.Find(q => q.ModuleTag.ToString() == "401");
                if (dto != null)
                {
                    toolBar.Authorize(dto.Permissions);
                }
            }

            // 读取字段信息
            cfgList = PropertyHelper.FieldCfgItems;
            // 关联数据源
            gcField.DataSource = cfgList;
            // 保存初始的字段显示名称
            initialNames = cfgList.Select(q => q.FieldShowName).ToList();

            // 不可见列
            string[] cols = new string[] { "ID", "FieldName", "SerialNo" };
            GridControlHelper.Instance.SetColumnInvisible(gvField, cols);
        }

        /// <summary>
        /// 显示序号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvField_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            GridView view = (GridView)sender;
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        /// <summary>
        /// 表格Cell值改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvField_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            // 若设置字段不可用，则自动修改为不可见且不可作为搜索条件
            if (e.Column == gvField.Columns.ColumnByFieldName("IsFieldUsable") && Convert.ToBoolean(e.Value) == false)
            {
                gvField.SetRowCellValue(e.RowHandle, "IsFieldVisible", e.Value);
                gvField.SetRowCellValue(e.RowHandle, "IsKeyWord", e.Value);
            }
        }

        /// <summary>
        /// 点击表格Cell时，是否允许编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvField_ShowingEditor(object sender, CancelEventArgs e)
        {
            // 若字段不可用，则字段显示名不能编辑
            if (gvField.FocusedColumn != gvField.Columns[2] && !Convert.ToBoolean(gvField.GetRowCellValue(gvField.FocusedRowHandle, gvField.Columns[2])))
            {
                e.Cancel = true;
            }
        }

        #region 工具栏操作

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            // 根据表格的设置更新数据源
            for (int i = 0; i < gvField.RowCount; i++)
            {
                cfgList[i].IsFieldUsable = Convert.ToBoolean(gvField.GetRowCellValue(i, "IsFieldUsable"));
                cfgList[i].IsFieldVisible = Convert.ToBoolean(gvField.GetRowCellValue(i, "IsFieldVisible"));
            }

            try
            {
                CallerFactory.Instance.GetService<ISystemConfigService>().UpdateFieldCfgs(cfgList);
                PropertyHelper.FieldCfgItems = cfgList;

                MessageUtil.ShowTips("字段配置更新成功！");
            }
            catch(Exception ex)
            {
                MessageUtil.ShowTips(ex.Message);
            }
        }

        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolReset_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // 利用Linq to XML从XNL文件中读取字段配置信息
            XElement root = XElement.Load(Application.StartupPath + "\\XML\\FieldToShowConfig.xml");

            // 读取XML树中所有的“Field”属性及其子元素的值
            var fieldList = from ele in root.Elements("Field") select ele;

            List<FieldCfgDto> items = new List<FieldCfgDto>();
            // 从XML文件中读取字段信息
            foreach (var item in fieldList)
            {
                // 提取字段信息
                string[] arr = item.Value.Split(',');
                FieldCfgDto dictItem = new FieldCfgDto();

                dictItem.FieldName      = item.Attribute("Name").Value.ToString(); // 字段名（对应数据库）
                dictItem.FieldShowName  = arr[0];                                  // 字段UI显示名
                dictItem.IsFieldVisible = arr[2] == "1" ? true : false;            // 是否显示  
                dictItem.IsFieldUsable  = arr[1] == "1" ? true : false;            // 是否能用
                dictItem.IsKeyWord      = arr[3] == "1" ? true : false;            // 是否主键
                dictItem.FieldType      = arr[4];                                  // 字段类型  
                dictItem.Remark         = arr[5];                                  // 备注
                dictItem.SerialNo       = Convert.ToInt32(arr[6]);                 // 序号
                
                items.Add(dictItem);
            }

            try
            {
                // 更新数据库
                CallerFactory.Instance.GetService<ISystemConfigService>().Add(items);
                // 更新当前字段配置
                PropertyHelper.FieldCfgItems.AddRange(items);

                MessageUtil.ShowError("字段配置信息恢复到默认值");
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
            }
        }

        #endregion
    }
}
