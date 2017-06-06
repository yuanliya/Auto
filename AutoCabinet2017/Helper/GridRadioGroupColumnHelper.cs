using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Controls;

namespace AutoCabinet2017.Helper
{
    /// <summary>
    /// 处理表格控件中的RadioGroup控件
    /// </summary>
    public class GridRadioGroupColumnHelper
    {
        private GridView gridView;        

        private RepositoryItemCheckEdit repositoryItem = new RepositoryItemCheckEdit();
        public RepositoryItemCheckEdit RadioRepositoryItem
        {
            get { return repositoryItem; }
            set { repositoryItem = value; }
        }

        // 表格中的RadioGroup列
        private GridColumn radioGroupColumn = new GridColumn();
        public GridColumn RadioGroupColumn
        {
            get { return radioGroupColumn; }
            set { radioGroupColumn = value; }
        }

        // 表格中选中的行检索
        private int selectedDataSourceRowIndex;
        public int SelectedDataSourceRowIndex
        {
            get { return selectedDataSourceRowIndex; }
            set
            {
                if (selectedDataSourceRowIndex != value)
                {
                    // 保存老值
                    int oldRowIndex = selectedDataSourceRowIndex;
                    // 设置新值
                    selectedDataSourceRowIndex = value;
                    // 设置表格中上一个选中行对应的值
                    gridView.SetRowCellValue(gridView.GetDataSourceRowIndex(oldRowIndex), RadioGroupColumn, false);
                    // 通知界面显示
                    OnSelectedRowChanged();
                }
            }
        }

        // 选择变化的事件
        public event EventHandler SelectedRowChanged;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_gridView"></param>
        public GridRadioGroupColumnHelper(GridView _gridView)
        {
            gridView = _gridView;

            gridView.BeginUpdate();
            
            InitGridView();
            InitColumn();
            InitRepositoryItem();
            
            gridView.EndUpdate();
        }

        /// <summary>
        /// 初始化表格
        /// </summary>
        private void InitGridView()
        {
            // 为表格中可视的、未绑定字段的列提供数据处理方法
            gridView.CustomUnboundColumnData += gridView_CustomUnboundColumnData;
        }

        /// <summary>
        /// 非绑定列的数据处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void gridView_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            // 处理RadioGroup列
            if (e.Column == RadioGroupColumn)
            {
                if (e.IsGetData)
                {
                    // 从数据源获得当前cell的值
                    e.Value = e.ListSourceRowIndex == SelectedDataSourceRowIndex;
                }
                    
                if (e.IsSetData)
                {
                    // 是否保存当前cell的值到数据源
                    if (e.Value.Equals(true))
                    {
                        // 激发SelectedDataSourceRowIndex属性的set处理
                        SelectedDataSourceRowIndex = e.ListSourceRowIndex;
                    }
                }                    
            }
        }

        /// <summary>
        /// 表格增加Radio复选框列
        /// </summary>
        private void InitColumn()
        {
            RadioGroupColumn.FieldName = "RadioGroupColumn";

            gridView.Columns.Add(RadioGroupColumn);

            RadioGroupColumn.Visible    = true;
            // 列里嵌入RadioGroup控件
            RadioGroupColumn.ColumnEdit = RadioRepositoryItem;
            RadioGroupColumn.Caption    = "选择";
            RadioGroupColumn.OptionsColumn.AllowEdit = true;
            // 列绑定控件值的数据类型
            RadioGroupColumn.UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
            RadioGroupColumn.MaxWidth    = 50;
        }

        /// <summary>
        /// 初始化RadioGroup控件属性
        /// </summary>
        private void InitRepositoryItem()
        {
            RadioRepositoryItem.CheckStyle = CheckStyles.Radio;
            RadioRepositoryItem.EditValueChanged += new EventHandler(RadioRepositoryItem_EditValueChanged);
        }

        /// <summary>
        /// RadioGroup控件的选定值发生变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void RadioRepositoryItem_EditValueChanged(object sender, EventArgs e)
        {
            // 提交变化的表格编辑值到数据源
            // 必须有这句！否则选中新的radio，老的不会立即清掉，会同时有两个Radio选中
            gridView.PostEditor();
        }

        /// <summary>
        /// 处理RadioGroup控件选择变化的事件
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        private void OnSelectedRowChanged()//(int oldValue, int newValue)
        {
            if (SelectedRowChanged != null)
            {
                SelectedRowChanged(gridView, EventArgs.Empty);
            }              
        }
    }
}
