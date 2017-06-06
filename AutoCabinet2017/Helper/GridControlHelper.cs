using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZY.EntityFrameWork.Core.Model.Entity;

namespace AutoCabinet2017.Helper
{
    public class GridControlHelper
    {
        // 私有构造函数
        private GridControlHelper()
        { }

        // 单例模式的类对象
        public static readonly GridControlHelper Instance = new GridControlHelper();

        #region 格式转换
        /// <summary>
        /// 把GridView选中的行转化为DataTable
        /// 适用于数据源为List的表格
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="gv">表格视图</param>
        /// <param name="selected">选中的行</param>
        /// <returns>数据表</returns>
        public DataTable ConvertToDataTable<T>(GridView gv, int[] selectedRows)
        {
            DataTable dt = new DataTable();
            List<string> invisibleCol = new List<string>(); // 未显示的列名

            // 创建列
            foreach (GridColumn col in gv.Columns)
            {
                DataColumn dataCol = new DataColumn(col.Name, typeof(string));
                dataCol.Caption = col.Caption;
                dt.Columns.Add(dataCol);

                // 保存不显示的列
                if (col.Visible == false)
                {
                    invisibleCol.Add(col.Name);
                }
            }

            // 每一行中的列值
            foreach (int idx in selectedRows)
            {
                // 行转换为指定对象
                // GridView关联的数据源必须是List或BindingList
                T item = (T)gv.GetRow(idx);

                // 生成新行
                DataRow dr = dt.NewRow();

                // 利用反射读取泛型类属性值
                int index = 0;
                foreach (System.Reflection.PropertyInfo p in item.GetType().GetProperties())
                {
                    dr[index] = p.GetValue(item, null);
                    index++;
                }

                // 添加新行
                dt.Rows.Add(dr);
            }

            // 删除不显示的列
            foreach (string colName in invisibleCol)
            {
                dt.Columns.Remove(colName);
            }

            return dt;
        }

        /// <summary>
        /// 把GridView的数据导入到DataTable
        /// 适用于数据源为DataTable的表格
        /// </summary>
        /// <param name="gv"></param>
        /// <returns></returns>
        public DataTable ConvertToDataTable(GridView gv)
        {
            DataTable dt = new DataTable();
            List<string> invisibleCol = new List<string>(); // 未显示的列名

            foreach (GridColumn col in gv.Columns)
            {
                // 生成列
                // 数据源为DataTable的列，“Name”为“col”+列对应字段名
                DataColumn dataCol = new DataColumn(col.Name, typeof(string));
                dataCol.Caption = col.Caption;
                dt.Columns.Add(dataCol);

                // 保存不显示的列
                if (col.Visible == false)
                {
                    invisibleCol.Add(col.Name);
                }
            }

            for (int i = 0; i < gv.RowCount; i++)
            {
                // 提取GridView的行记录
                // GridView关联的数据源必须是DataTable
                DataRow row = gv.GetDataRow(i);
                // 行数据加入DataTable
                dt.Rows.Add(row.ItemArray);
            }

            // 删除不显示的列
            foreach (string colName in invisibleCol)
            {
                dt.Columns.Remove(colName);
            }

            return dt;
        }
        #endregion

        #region 表格配置

        /// <summary>
        /// 设定表格列不可见
        /// </summary>
        /// <param name="gv">表格实例</param>
        /// <param name="colNames">不可见的列名集合</param>
        public void SetColumnInvisible(GridView gv, string[] colNames)
        {
            if (colNames == null) return;

            // 遍历表格中每一列
            foreach (GridColumn col in gv.Columns)
            {
                if (colNames.Contains(col.FieldName))
                {
                    col.Visible = false;
                }
                else
                {
                    col.Visible = true;
                }                   
            }
        }

        /// <summary>
        /// 配置表格列标题和是否可见
        /// </summary>
        /// <param name="gv"></param>
        public void ConfigGridViewColumns(GridView gv)
        {
            if (gv.DataSource == null) return;

            if (PropertyHelper.FieldCfgItems == null || PropertyHelper.FieldCfgItems.Count == 0) return;

            // 配置录入界面的字段英文名和中文名映射
            PropertyHelper.FieldCfgItems.ForEach(q =>
            {
                if (gv.Columns[q.FieldName] != null)
                {
                    // 表格列是否可见
                    gv.Columns[q.FieldName].Visible = q.IsFieldVisible;
                    // 表格列标题
                    if (q.IsFieldVisible)
                    {
                        gv.Columns[q.FieldName].Caption = q.FieldShowName;
                        // 字段所在列禁止编辑
                        gv.Columns[q.FieldName].OptionsColumn.AllowEdit = false;
                    }
                }
            });

            // 配置表格样式
            ConfigGridViewStyle(gv);
        }

        /// <summary>
        /// 配置表格样式
        /// </summary>
        /// <param name="gv">表格视图</param>
        public void ConfigGridViewStyle(GridView gv)
        {
            //gv.PopulateColumns();

            //gv.OptionsBehavior.Editable      = false; // 不可编辑  
            gv.OptionsView.ShowDetailButtons = false; // 不显示子内容按钮  
            gv.OptionsView.ShowGroupPanel    = false; // 不显示分组  
            //gv.OptionsView.ShowIndicator     = false;//不显示行头 
            //gv.OptionsView.ShowColumnHeaders = false;//不显示列头 

            // 标题栏字体设置
            //gv.Appearance.HeaderPanel.Font = new Font("新宋体", (float)11, FontStyle.Bold);

            // 标题栏水平竖直均居中
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;
            gv.Appearance.HeaderPanel.TextOptions.VAlignment = VertAlignment.Center;

            // 单元格背景颜色设置
            //gv.Appearance.FocusedRow.BackColor = Color.LightBlue;
            //gv.Appearance.FocusedCell.BackColor = Color.GreenYellow;
            // 控件没被选中时，默认选中的第一行颜色和其他保持一致
            //gv.Appearance.HideSelectionRow.BackColor = Color.PaleGoldenrod;

            // 选中的单元格没有特别的颜色设置
            gv.OptionsSelection.EnableAppearanceFocusedCell = false;
            // 虚线框在整行行周围
            gv.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;

            // 单元格字体设置
            //gv.Appearance.Row.Font = new Font("新宋体", (float)10);

            // 设置单元格对齐方式和背景色
            foreach (GridColumn col in gv.Columns)
            {
                col.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                col.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;

                //col.AppearanceCell.BackColor = Color.PaleGoldenrod;
            }

            // 列宽自适应
            gv.BestFitColumns();
            // 使表格设置生效
            gv.LayoutChanged();
        }
          
        /// <summary>
        /// 在表格中加入Button列
        /// </summary>
        /// <param name="gv"></param>
        /// <param name="item"></param>
        public void AddControlButton(GridView gv, RepositoryItem item, int colIndex)
        {
            GridColumn unbColumn = new GridColumn();

            unbColumn.FieldName  = "Control";
            unbColumn.Caption    = "操作";
            unbColumn.ColumnEdit = item;

            // 加入新列
            gv.Columns.Add(unbColumn);
            // 列位置检索
            unbColumn.VisibleIndex = colIndex; 
        }

        /// <summary>
        /// 设置表格列标题
        /// </summary>
        /// <param name="gv"></param>
        /// <param name="captionList"></param>
        public void AddColumnCaption(GridView gv, List<string> captionList)
        {
            int i = 0;

            foreach (GridColumn col in gv.Columns)
            {
                col.Caption = captionList[i];
                i++;
            }
        }

        #endregion
    }
}
