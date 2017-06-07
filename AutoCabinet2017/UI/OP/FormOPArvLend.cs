using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevExpress.XtraGrid.Columns;

using ZY.EntityFrameWork.Caller;
using ZY.EntityFrameWork.Caller.Facade;

using ZY.EntityFrameWork.Core.Model.Dto;
using ZY.EntityFrameWork.Core.DBHelper;

using AutoCabinet2017.Helper;

using NJUST.AUTO06.Utility;
using NJUST.AUTO06.Utility.PreviewUtil;

namespace AutoCabinet2017.UI.OP
{
    public partial class FormOPArvLend : Form
    {
        // 查询表格关联的数据集
        private BindingList<ArchiveInfoDto> arvList = new BindingList<ArchiveInfoDto>();
        // 借阅表格关联的数据集
        private BindingList<ArchiveInfoDto> arvToLendList = new BindingList<ArchiveInfoDto>();

        public FormOPArvLend()
        {
            InitializeComponent();
        }

        private void FormOPArvLend_Load(object sender, EventArgs e)
        {
            // 工具栏默认无操作
            toolBar.Tag = "";

            // 根据用户角色设置工具栏和嵌入式按钮的使用权限
            if (PropertyHelper.CurrentUser.RoleName != "超级管理员")
            {
                RoleModuleDto dto = PropertyHelper.RoleModules.Find(q => q.ModuleTag.ToString() == "105");
                if (dto != null)
                {
                    toolBar.Authorize(dto.Permissions);
                }
            }

            // 默认隐藏“高级搜索”
            panelSearch.Visible = false;

            // 表格关联数据源
            gcArvInfo.DataSource = arvList;
            gcArvToLend.DataSource = arvToLendList;

            // ID列不可见
            string[] cols = new string[] { "ID" };
            GridControlHelper.Instance.SetColumnInvisible(gvArvInfo, cols);
            GridControlHelper.Instance.SetColumnInvisible(gvArvToLend, cols);

            // 为表格加入控制列
            GridControlHelper.Instance.AddControlButton(gvArvInfo, btnPreview, gvArvInfo.Columns.Count);  // 档案信息表的预览列
            GridControlHelper.Instance.AddControlButton(gvArvToLend, btnRemove, 0);                       // 借阅信息表的移除列
            // 配置表格列标题和可见性
            GridControlHelper.Instance.ConfigGridViewColumns(gvArvInfo);
            GridControlHelper.Instance.ConfigGridViewColumns(gvArvToLend);
            // 表格宽度自适应
            gvArvInfo.BestFitColumns();
            gvArvToLend.BestFitColumns();
        }

        /// <summary>
        /// 工具栏操作--高级检索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolAdvancedQuery_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
        /// 工具栏操作--导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // 获取所有需要导出记录
            int[] allSelected = gvArvToLend.GetSelectedRows();

            if (allSelected.Length == 0)
            {
                MessageUtil.ShowWarning("请选择需要导出到Excel文件的记录!");
                return;
            }

            // 把表格记录转换为数据表格
            DataTable exportTable = GridControlHelper.Instance.ConvertToDataTable<ArchiveInfoDto>(gvArvInfo, allSelected);
            // 数据表格写入Excel文件
            ExcelHelper.Instance.DataTableExportToExcel(exportTable, "借出档案信息");
        }

        /// <summary>
        /// 工具栏操作-执行借阅
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolLend_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //测试
            foreach (ArchiveInfoDto dto in arvToLendList)
            {
                dto.ArvStatus = "借出";
            }

            ArvLendInfoDto lendInfo = new ArvLendInfoDto 
            {
                LendDate = dtLendDate.DateTime, 
                Lender = txtLender.Text,
                ReturnDeadline = dtReturnDate.DateTime,
                LenderDept = cbxArvUnit.Text, 
                LendExecuter = txtExcuter.Text 
            };

            CallerFactory.Instance.GetService<IArvOpService>().ArvLend(lendInfo,arvToLendList.ToList());
        }

        /// <summary>
        /// 工具栏操作--检索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolQuery_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<QueryCondition> conditions = new List<QueryCondition>();

            try
            {
                 // 根据常规搜索panel的内容构建搜索条件
                 QueryCondition condition = new QueryCondition(edtArvID.Tag.ToString(), CompareType.Include, edtArvID.EditValue == null ? "" : edtArvID.EditValue.ToString());
                 conditions.Add(condition);
                 // 执行搜索操作
                 arvList = new BindingList<ArchiveInfoDto>(CallerFactory.Instance.GetService<IArvOpService>().FindArvs(conditions));
                 // 关联到表格
                 gcArvInfo.DataSource = arvList.Where<ArchiveInfoDto>(q => q.ArvStatus == "在库");
            }
            catch(Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 执行扩展检索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdvanced_Click(object sender, EventArgs e)
        {
            List<QueryCondition> conditions = new List<QueryCondition>();

            try
            {
                // 根据常规搜索panel的内容构建搜索条件
                QueryCondition condition = new QueryCondition(edtArvID.Tag.ToString(), CompareType.Include, edtArvID.EditValue == null ? "" : edtArvID.EditValue.ToString());
                conditions.Add(condition);

                // 根据高级搜索panel的内容构建搜索条件
                conditions.AddRange(QueryHelper.GetIntegrativeCondition(panelSearch));

                // 执行搜索操作
                arvList = new BindingList<ArchiveInfoDto>(CallerFactory.Instance.GetService<IArvOpService>().FindArvs(conditions));
                // 关联到表格
                gcArvInfo.DataSource = arvList.Where<ArchiveInfoDto>(q => q.ArvStatus == "在库");
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 档案信息表选中记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvArvInfo_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            // 清空借阅信息表
            arvToLendList.Clear();

            // 选中的行
            int[] numSelected = gvArvInfo.GetSelectedRows();

            foreach (int index in numSelected)
            {
                // 提取选中记录
                ArchiveInfoDto info = gvArvInfo.GetRow(index) as ArchiveInfoDto;
                // 加入借阅明细表
                arvToLendList.Add(info);
            }

            // 更新借阅明细数据源
            gcArvToLend.RefreshDataSource();
            gvArvToLend.BestFitColumns();
        }

        /// <summary>
        /// 借阅信息表的移除操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemove_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            // 提取当前记录
            ArchiveInfoDto info = gvArvToLend.GetRow(gvArvToLend.FocusedRowHandle) as ArchiveInfoDto;

            // 取消该条记录在借阅明细表中的选中状态
            for (int i = 0; i < gvArvInfo.RowCount; i++)
            {
                if (gvArvInfo.GetRowCellValue(i, gvArvInfo.Columns[0]).ToString() == info.ID)//ArvID)
                {
                    // 档案信息表中取消“CheckBox”选中状态，同时引起表格的“SelectionChanged”事件
                    gvArvInfo.UnselectRow(i);
                    break;
                }
            }
        }
    }
}
