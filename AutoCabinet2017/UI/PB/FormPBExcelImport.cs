using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.Data;

using NJUST.AUTO06.Utility;

using AutoCabinet2017.Helper;
using AutoCabinet2017.UI.CustomWaiting;

using ZY.EntityFrameWork.Core.Model.Dto;
using ZY.EntityFrameWork.Caller;
using ZY.EntityFrameWork.Caller.Facade;
using ZY.EntityFrameWork.Core.DBHelper;

namespace AutoCabinet2017.UI.PB
{
    public partial class FormPBExcelImport : XtraForm
    {
        public delegate void GetImportArchivesEventHandler(List<ArchiveInfoDto> arch);
        public event GetImportArchivesEventHandler GetImportArchivesEvent;

        // 记录Excel的数据集
        private DataTable tbImport = new DataTable();
        // 成功导入记录集
        private DataTable successTable = new DataTable();
        // 失败导入记录集
        private DataTable failureTable = new DataTable();
        // 重复记录
        private ArrayList repeatedItems = new ArrayList();

        private DevExpress.XtraSplashScreen.SplashScreenManager loading;

        public FormPBExcelImport()
        {
            InitializeComponent();
        }

        private void FormPBExcelImport_Load(object sender, EventArgs e)
        {
            loading = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(LoadingForm), true, true);
        }

        #region 表格样式设置

        /// <summary>
        /// 显示每一行行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gv_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            this.gv.IndicatorWidth = 50;

            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                // 显示行号
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        /// <summary>
        /// 使表格内的主键数据相同的行颜色为红色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gv_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView View = sender as GridView;

            if (gv.RowCount != 0)
            {
                // 取第一列的值（key）
                object needAlert = View.GetRowCellValue(e.RowHandle, View.Columns[0]);

                if (e.RowHandle >= 0 && repeatedItems.Contains(needAlert))
                {
                    e.Appearance.ForeColor = Color.Red;
                    e.Appearance.BackColor = Color.LightGray;
                }
            }
        }

        #endregion

        #region 工具栏操作

        /// <summary>
        /// 导入文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolFileImport_Click(object sender, EventArgs e)
        {
            // 设置工具栏按钮状态
            SetButtonEnabled();

            // Excel文件导入对话框
            FormPBFileSelect fileSelect = new FormPBFileSelect();

            if (fileSelect.ShowDialog() == DialogResult.OK)
            {
                // 把Excel文件数据导入DataTable
                DataTable dtTemp = ExcelHelper.Instance.OpenExcelAsDataTable(fileSelect.DataFilePath);

                // 空记录集
                if (dtTemp==null||dtTemp.Rows.Count == 0) return;

                gc.DataSource = dtTemp;

                // 筛选出档案编号不为空的记录
                tbImport = ExcelHelper.Instance.FilterNullRecords(dtTemp);
                if (tbImport.Rows.Count == 0)
                {
                    MessageUtil.ShowTips("导入的表格中无有效数据！");
                    return;
                }

                // 复制该 DataTable 的结构和数据
                successTable = tbImport.Copy();
                failureTable = tbImport.Copy();

                // 记录查重
                //repeatedItems = ExcelHelper.Instance.FindRepeatedRecords<string>(tbImport, "档案编号");
                string showName = PropertyHelper.FieldCfgItems.Find(q => q.FieldShowName == "ArvID").FieldShowName;
                try
                {
                    repeatedItems = ExcelHelper.Instance.FindRepeatedRecords(tbImport, showName);
                    // 表格关联数据源
                    gc.DataSource = tbImport;
                    // 显示checkbox
                    gv.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
                }
                catch (Exception ex)
                {
                    MessageUtil.ShowError(ex.ToString());
                }              
            }
        }

        /// <summary>
        /// 保存数据导入到数据库和表格中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolSave_Click(object sender, EventArgs e)
        {
            gv.PostEditor();
            gv.CloseEditor();
            if (gv.SelectedRowsCount == 0)
            {
                MessageUtil.ShowWarning("请选择导入条目！");
                return;
            }
            successTable.Rows.Clear();
            failureTable.Rows.Clear();

            List<ArchiveInfoDto> importedArvList = new List<ArchiveInfoDto>();

            // 将表格中的数据导入数据库中
            if (gv.DataSource != null)
            {
                // 设置列对应的数据库表字段名
                foreach (GridColumn col in gv.Columns)
                {
                    //  从属性类中读取字段对应的显示名
                    List<FieldCfgDto> arvField = PropertyHelper.FieldCfgItems.Where<FieldCfgDto>(item => item.FieldShowName == col.FieldName).ToList<FieldCfgDto>();

                    if (arvField.Count == 1)
                    {
                        col.Name = arvField[0].FieldShowName;
                        col.Tag = true;                    // 指示该列是要写入数据库的信息
                    }
                    else
                    {
                        col.Tag = false;
                    }
                }

                loading.ShowWaitForm();//数据多时界面可能会有卡顿

                // 遍历所有行
                //for (int i = 0; i < gv.RowCount; i++)
                int[] toDelete = gv.GetSelectedRows().Reverse().ToArray();
                //List<ArchiveInfoDto> infos=new List<ArchiveInfoDto>();
                foreach (int i in toDelete)
                {
                    // 当前行对象转化为类实例
                    //ArchiveInfoDto info = GridControlHelper.Instance.ConvertDataRowToObject<ArchiveInfoDto>(gv, i);
                    ArchiveInfoDto info = (ArchiveInfoDto)gv.GetRow(i);//单条保存，如果多条保存出错全部回滚不合理
                    info.ArvStatus = "在档";

                    try
                    {
                        // 记录写入数据库
                        CallerFactory.Instance.GetService<IArvOpService>().InToStorage(info);
                        // 加入写入成功的数据表
                        successTable.Rows.Add(gv.GetDataRow(i).ItemArray);
                        tbImport.Rows.RemoveAt(i);//成功写入的记录从表中移除
                        importedArvList.Add(info);
                    }
                    catch (Exception)
                    {
                        // 加入写入失败的数据表
                        failureTable.Rows.Add(gv.GetDataRow(i).ItemArray);
                    }
                }
                gc.RefreshDataSource();

                OnGetImportArchives(importedArvList);

                // 显示导入结果的对话框
                FormPBExcelShow frmPBExcelShow = new FormPBExcelShow();

                frmPBExcelShow.sCount = successTable.Rows.Count;
                frmPBExcelShow.xCount = failureTable.Rows.Count;
                frmPBExcelShow.Tb_ImportFailure = failureTable;

                frmPBExcelShow.ShowDialog();
            }
            else
            {
                MessageUtil.ShowTips("没有数据可以导入");
            }
        }

        /// <summary>
        /// 清空数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolClear_Click(object sender, EventArgs e)
        {
            // 设置工具栏按钮状态
            SetButtonEnabled();

            if (DialogResult.OK == MessageUtil.ShowTips("确实要删除所有信息吗？"))
            {
                tbImport      = null;
                gc.DataSource = null;

                gv.Columns.Clear();
            }
        }

        /// <summary>
        /// 删除选择的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolDelete_Click(object sender, EventArgs e)
        {
            // 所有选中的记录索引
            int[] selected = gv.GetSelectedRows();

            if (selected.Length == 0)
            {
                MessageUtil.ShowWarning("请选择要删除的档案！");
                return;
            }

            string str = string.Format("确实要删除这{0}条信息吗？", selected.Length);

            if (DialogResult.OK == MessageUtil.ShowTips(str))
            {
                gv.DeleteSelectedRows();
                MessageUtil.ShowTips("删除成功！");
            }
            // 记录查重
            string showName = PropertyHelper.FieldCfgItems.Find(q => q.FieldName == "ArvID").FieldShowName;
            repeatedItems = ExcelHelper.Instance.FindRepeatedRecords(tbImport, showName);
            
            gc.Refresh();
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 设置工具栏按钮可用性
        /// </summary>
         private void SetButtonEnabled()
         {
             toolFileImport.Enabled = !toolFileImport.Enabled;
             toolSave.Enabled       = !toolSave.Enabled;
             toolClear.Enabled      = !toolClear.Enabled;
             toolDelete.Enabled     = !toolDelete.Enabled;
         }

        #endregion


         private void OnGetImportArchives(List<ArchiveInfoDto> arvs)
         {
             if (GetImportArchivesEvent != null)
             {
                 GetImportArchivesEvent(arvs);
             }
         }

        
    }
}
    

