using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;

using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraEditors.Drawing;

namespace AutoCabinet2017.Helper
{
    /// <summary>
    /// 为BandGridView表格增加CheckBox首列
    /// </summary>
    public class GridCheckMarksSelection 
    {
        protected BandedGridView _view;
        protected ArrayList selection;
        GridColumn column;
        RepositoryItemCheckEdit edit;
        const int CheckboxIndent = 4;

        public GridCheckMarksSelection()          
        {
            selection = new ArrayList();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="view">需要增加CheckBox列的BandGridView表格</param>
        public GridCheckMarksSelection(BandedGridView view) : this() 
        {
            View = view;
        }
        
        public BandedGridView View 
        {
            get { return _view; }
            set {
                if (_view != value) 
                {
                    Detach();
                    Attach(value);
                }
            }
        }

        public GridColumn CheckMarkColumn { get { return column; } }

        /// <summary>
        /// 获得选中的CheckBox的数量
        /// </summary>
        public int SelectedCount { get { return selection.Count; } }
        
        /// <summary>
        /// 获取选中的行
        /// </summary>
        /// <param name="index">行检索</param>
        /// <returns></returns>
        public object GetSelectedRow(int index) 
        {
            return selection[index];
        }

        /// <summary>
        /// 获得行检索
        /// </summary>
        /// <param name="row">表格的行的实例</param>
        /// <returns></returns>
        public int GetSelectedIndex(object row) 
        {
            return selection.IndexOf(row);
        }

        /// <summary>
        /// 清除选中的行
        /// </summary>
        public void ClearSelection()
        {
            selection.Clear();
            Invalidate();
        }

        /// <summary>
        /// 选中所有行
        /// </summary>
        public void SelectAll() 
        {
            selection.Clear();
            // fast (won't work if the grid is filtered)
            //if(_view.DataSource is ICollection)
            //	selection.AddRange(((ICollection)_view.DataSource));
            //else
            // slow:
            for (int i = 0; i < _view.DataRowCount; i++)
            {
                selection.Add(_view.GetRow(i));
            }

            Invalidate();
        }

        public void SelectGroup(int rowHandle, bool select) 
        {
            if (IsGroupRowSelected(rowHandle) && select) return;
        
            for (int i = 0; i < _view.GetChildRowCount(rowHandle); i++)
            {
                int childRowHandle = _view.GetChildRowHandle(rowHandle, i);
                if (_view.IsGroupRow(childRowHandle))
                    SelectGroup(childRowHandle, select);
                else
                    SelectRow(childRowHandle, select, false);
            }

            Invalidate();
        }

        /// <summary>
        /// 设置行选中状态
        /// </summary>
        /// <param name="rowHandle"></param>
        /// <param name="select"></param>
        public void SelectRow(int rowHandle, bool select)
        {
            SelectRow(rowHandle, select, true);
        }

        public void InvertRowSelection(int rowHandle) 
        {
            if (View.IsDataRow(rowHandle)) 
            {
                SelectRow(rowHandle, !IsRowSelected(rowHandle));
            }
            
            if (View.IsGroupRow(rowHandle)) 
            {
                SelectGroup(rowHandle, !IsGroupRowSelected(rowHandle));
            }
        }
        public bool IsGroupRowSelected(int rowHandle) 
        {
            for (int i = 0; i < _view.GetChildRowCount(rowHandle); i++)
            {
                int row = _view.GetChildRowHandle(rowHandle, i);
                if (_view.IsGroupRow(row)) 
                {
                    if (!IsGroupRowSelected(row)) return false;
                } 
                else
                    if (!IsRowSelected(row)) return false;
            }
            return true;
        }

        /// <summary>
        /// 行是否选中
        /// </summary>
        /// <param name="rowHandle"></param>
        /// <returns></returns>
        public bool IsRowSelected(int rowHandle) 
        {
            if (_view.IsGroupRow(rowHandle))
                return IsGroupRowSelected(rowHandle);

            object row = _view.GetRow(rowHandle);
            return GetSelectedIndex(row) != -1;
        }

        /// <summary>
        /// 为BandGridView增加一个CheckBox列
        /// </summary>
        /// <param name="view"></param>
        protected virtual void Attach(BandedGridView view) 
        {
            if (view == null) return;
            selection.Clear();
            this._view = view;
            
            view.BeginUpdate();
        
            try 
            {
                edit = view.GridControl.RepositoryItems.Add("CheckEdit") as RepositoryItemCheckEdit;

                column = view.Columns.Add();
                column.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
                column.Visible = true;
                column.VisibleIndex = 0;
                column.FieldName = "CheckMarkSelection";
                column.Caption = "Mark";
                column.OptionsColumn.ShowCaption = false;
                column.OptionsColumn.AllowEdit = false;
                column.OptionsColumn.AllowSize = false;
                column.UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
                column.Width = 80;// GetCheckBoxWidth();
                column.ColumnEdit = edit;

                // 添加Band列，设置字体和对齐方式
                GridBand band = view.Bands.AddBand("选择");
                band.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                band.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                band.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                view.Bands.MoveTo(0, band);
                view.Columns["CheckMarkSelection"].OwnerBand = band;
                view.Click += new EventHandler(View_Click);
                view.CustomDrawColumnHeader  += new ColumnHeaderCustomDrawEventHandler(View_CustomDrawColumnHeader);
                view.CustomDrawGroupRow      += new RowObjectCustomDrawEventHandler(View_CustomDrawGroupRow);
                view.CustomUnboundColumnData += new CustomColumnDataEventHandler(view_CustomUnboundColumnData);
                view.KeyDown  += new KeyEventHandler(view_KeyDown);
                view.RowStyle += new RowStyleEventHandler(view_RowStyle);
            } 
            finally 
            {
                view.EndUpdate();
            }
        }

        /// <summary>
        /// BandGridView删除一个CheckBox列
        /// </summary>
        /// <param name="view"></param>
        protected virtual void Detach() 
        {
            if (_view == null) return;
            
            if (column != null)
                column.Dispose();
            
            if (edit != null) 
            {
                _view.GridControl.RepositoryItems.Remove(edit);
                edit.Dispose();
            }

            _view.Click -= new EventHandler(View_Click);
            _view.CustomDrawColumnHeader -= new ColumnHeaderCustomDrawEventHandler(View_CustomDrawColumnHeader);
            _view.CustomDrawGroupRow -= new RowObjectCustomDrawEventHandler(View_CustomDrawGroupRow);
            _view.CustomUnboundColumnData -= new CustomColumnDataEventHandler(view_CustomUnboundColumnData);
            _view.KeyDown -= new KeyEventHandler(view_KeyDown);
            _view.RowStyle -= new RowStyleEventHandler(view_RowStyle);

            _view = null;
        }
        
        protected int GetCheckBoxWidth() 
        {
            CheckEditViewInfo info = edit.CreateViewInfo() as CheckEditViewInfo;
            int width = 0;
            GraphicsInfo.Default.AddGraphics(null);
         
            try 
            {
                width = info.CalcBestFit(GraphicsInfo.Default.Graphics).Width;
            } 
            finally 
            {
                GraphicsInfo.Default.ReleaseGraphics();
            }

            return width + CheckboxIndent * 2;
        }
        
        protected void DrawCheckBox(Graphics g, Rectangle r, bool Checked) 
        {
            CheckEditViewInfo info;
            CheckEditPainter painter;
            ControlGraphicsInfoArgs args;

            info    = edit.CreateViewInfo() as CheckEditViewInfo;
            painter = edit.CreatePainter() as CheckEditPainter;
            info.EditValue = Checked;
            info.Bounds = r;
            info.CalcViewInfo(g);
            
            args = new ControlGraphicsInfoArgs(info, new DevExpress.Utils.Drawing.GraphicsCache(g), r);
            
            painter.Draw(args);
            args.Cache.Dispose();
        }

        void Invalidate() 
        {
            _view.CloseEditor();
            _view.BeginUpdate();
            _view.EndUpdate();
        }

        void SelectRow(int rowHandle, bool select, bool invalidate)
        {
            if (IsRowSelected(rowHandle) == select) return;
            object row = _view.GetRow(rowHandle);
           
            if (select)
                selection.Add(row);
            else
                selection.Remove(row);
            
            if (invalidate) 
            {
               Invalidate();
            }
        }

        void view_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e) 
        {
            if (e.Column == CheckMarkColumn) 
            {
                if (e.IsGetData)
                    e.Value = IsRowSelected(View.GetRowHandle(e.ListSourceRowIndex));
                else
                    SelectRow(View.GetRowHandle(e.ListSourceRowIndex), (bool)e.Value);
            }
        }

        void view_KeyDown(object sender, KeyEventArgs e) 
        {
            if (View.FocusedColumn != column || e.KeyCode != Keys.Space) return;
            InvertRowSelection(View.FocusedRowHandle);
        }

        /// <summary>
        /// 处理表格点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void View_Click(object sender, EventArgs e) 
        {
            GridHitInfo info;
            Point pt = _view.GridControl.PointToClient(Control.MousePosition);
            info = _view.CalcHitInfo(pt);
           
            if (info.Column == column) 
            {
                if (info.InColumn) 
                {
                    if (SelectedCount == _view.DataRowCount)
                        ClearSelection();
                    else
                        SelectAll();
                }
                
                if (info.InRowCell) 
                {
                    InvertRowSelection(info.RowHandle);
                }
            }

            if (info.InRow && _view.IsGroupRow(info.RowHandle) && info.HitTest != GridHitTest.RowGroupButton) 
            {
                InvertRowSelection(info.RowHandle);
            }
        }

        void View_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            if (e.Column == column)
            {
                e.Info.InnerElements.Clear();
                e.Painter.DrawObject(e.Info);
                DrawCheckBox(e.Graphics, e.Bounds, SelectedCount == _view.DataRowCount);
                e.Handled = true;
            }
        }

        void View_CustomDrawGroupRow(object sender, RowObjectCustomDrawEventArgs e)
        {
            GridGroupRowInfo info;
            info = e.Info as GridGroupRowInfo;

            info.GroupText = "         " + info.GroupText.TrimStart();
            e.Info.Paint.FillRectangle(e.Graphics, e.Appearance.GetBackBrush(e.Cache), e.Bounds);
            e.Painter.DrawObject(e.Info);

            Rectangle r = info.ButtonBounds;
            r.Offset(r.Width + CheckboxIndent * 2 - 1, 0);
            DrawCheckBox(e.Graphics, r, IsGroupRowSelected(e.RowHandle));
            e.Handled = true;
        }

        void view_RowStyle(object sender, RowStyleEventArgs e) 
        {
            if (IsRowSelected(e.RowHandle))
            {
                e.Appearance.BackColor = SystemColors.Highlight;
                e.Appearance.ForeColor = SystemColors.HighlightText;
            }
        }
    }
}
