using AutoCabinet2017.Helper;
using AutoCabinet2017.UI.DV;
using AutoCabinet2017.UI.EditFrm;
using AutoCabinet2017.UI.PB;
using DevExpress.XtraBars.Docking2010.Views.NativeMdi;
using DevExpress.XtraBars.Docking2010.Views.Tabbed;
using DevExpress.XtraEditors;
using NJUST.AUTO06.Utility;
using NJUST.AUTO06.Utility.PreviewUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZY.EntityFrameWork.Caller;
using ZY.EntityFrameWork.Caller.Facade;
using ZY.EntityFrameWork.Core.DBHelper;
using ZY.EntityFrameWork.Core.Model.Dto;
using ZY.EntityFrameWork.Core.Model.Dto.Archive;
using ZY.EntityFrameWork.Core.Model.Dto.SysSetting;

namespace AutoCabinet2017.UI.OP
{
    public partial class FrmOPArvInput : XtraForm
    {
        public FrmOPArvInput()
        {
            InitializeComponent();
        }

        // 表格关联的数据集
        private BindingList<ArchiveInfoDto> arvList = new BindingList<ArchiveInfoDto>();
        // 查询得到的表
        private BindingList<ArchiveInfoDto> queryList = new BindingList<ArchiveInfoDto>();

        #region 编辑框

        /// <summary>
        /// 打开档案编辑对话框
        /// </summary>
        /// <param name="operation">命令类型</param>
        /// <param name="scopeList">档案信息集合</param>
        void OpenFormInfo(string cmdType, BindingList<ArchiveInfoDto> scopeList)
        {
            FormArvInfo frmOPArvInfo = new FormArvInfo();

            // 设置对话框参数
            frmOPArvInfo.Tag = cmdType;  // 当前窗口的Tag
            if(scopeList==null)
                frmOPArvInfo.ArvInfoList = (BindingList<ArchiveInfoDto>)gvArvInfo.DataSource;
            else
                frmOPArvInfo.ArvInfoList = scopeList;

            // 订阅事件
            frmOPArvInfo.UpdateGViewDataListEvent += UpdateGridView;
            // 档案编辑对话框
            frmOPArvInfo.ShowDialog();
        }

        private void OnSetLocation()
        {
            if(gvArvInfo.SelectedRowsCount==0)
            {
                MessageUtil.ShowWarning("请选择需要选库的档案条目！");
                return;
            }
            FrmArvBox frmArvBox = new FrmArvBox();
            if (frmArvBox.ShowDialog() == DialogResult.OK)
            {
                LoadArvBox(frmArvBox.ArvBoxDto);
            }
        }

        private void LoadArvBox(ArvBoxDto arvBoxDto)
        {
            List<ArchiveInfoDto> arvs = new List<ArchiveInfoDto>();
            foreach(int index in gvArvInfo.GetSelectedRows())
            {
                ArchiveInfoDto dto=(ArchiveInfoDto)gvArvInfo.GetRow(index);
                dto.ArvBoxID = arvBoxDto.ArvBoxID;
                dto.ArvBoxTitle = arvBoxDto.ArvBoxTitle;
                dto.GroupNo = arvBoxDto.GroupNo;
                dto.LayerNo = arvBoxDto.LayerNo;
                dto.CellNo = arvBoxDto.CellNo;
                arvs.Add(dto);
            }
            try
            {
                CallerFactory.Instance.GetService<IArvOpService>().UpdateArvInfos(arvs);//更新数据库
                BindingList<ArchiveInfoDto> bindingList = (BindingList<ArchiveInfoDto>)gcArvInfo.DataSource;//当前的绑定
                foreach (int index in gvArvInfo.GetSelectedRows())
                {
                    //更新表格显示
                    gvArvInfo.SetRowCellValue(index, "ArvBoxID", arvBoxDto.ArvBoxID);
                    gvArvInfo.SetRowCellValue(index, "ArvBoxTitle", arvBoxDto.ArvBoxTitle);
                    gvArvInfo.SetRowCellValue(index, "GroupNo", arvBoxDto.GroupNo);
                    gvArvInfo.SetRowCellValue(index, "LayerNo", arvBoxDto.LayerNo);
                    gvArvInfo.SetRowCellValue(index, "CellNo", arvBoxDto.CellNo);
                }
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.ToString());
            }
        }

        /// <summary>
        /// 更新表格显示
        /// </summary>
        /// <param name="arch">编辑记录信息</param>
        private void UpdateGridView(ArchiveInfoDto arch)
        {
            //string curOpera = this.toolStrip1.Tag.ToString();
            string curOpera = ctrlPanel.Tag.ToString();

            switch (curOpera)
            {
                case "ADD":  // “新增”操作时对表格的更新
                    // 更新记录集
                    arvList.Add(arch);

                    if (gcArvInfo.DataSource != arvList)
                    {
                        // 表格关联数据集发生变化，更新数据集
                        gcArvInfo.DataSource = arvList;

                        ClearSearch();  // 清除搜索结果
                    }

                    break;

                case "EDIT": // “修改”操作时对表格的更新
                    // 获取当前表格中记录
                    BindingList<ArchiveInfoDto> bindingList = (BindingList<ArchiveInfoDto>)gcArvInfo.DataSource;
                    // 查找被修改的记录
                    ArchiveInfoDto arv = bindingList.First(p => p.ArvID == arch.ArvID);
                    // 用新的记录替代原纪录，表格自动更新
                    bindingList[bindingList.IndexOf(arv)] = arch;

                    break;

                default:
                    break;
            }
        }

        #endregion

        #region 档案操作
        private void OnEdit()
        {
            if (gvArvInfo.GetSelectedRows().Length == 0)
            {
                MessageUtil.ShowWarning("请至少选择一条记录进行编辑！");
                return;
            }

            // 记录选中记录的索引
            int[] selectedIdx = gvArvInfo.GetSelectedRows();

            // 建立所有选中档案的记录集
            BindingList<ArchiveInfoDto> selectedArvItems = new BindingList<ArchiveInfoDto>();

            foreach (int rowIndex in selectedIdx)
            {
                // 已选择的档案信息
                ArchiveInfoDto info = (ArchiveInfoDto)gvArvInfo.GetRow(rowIndex);

                selectedArvItems.Add(info);
            }

            // 进入编辑对话框
            ctrlPanel.Tag = "EDIT";
            OpenFormInfo("EDIT", selectedArvItems);

            // 刷新表格关联数据集
            gcArvInfo.RefreshDataSource();
        }

        private void OnEditOne()
        {
            // 进入编辑对话框
            ctrlPanel.Tag = "EDIT";
            // 已选择的档案信息
            ArchiveInfoDto info = (ArchiveInfoDto)gvArvInfo.GetFocusedRow();
            OpenFormInfo("EDIT", null);
        }

        /// <summary>
        /// 多条删除
        /// </summary>
        private void OnDelete()
        {
            if (gvArvInfo.GetSelectedRows().Length == 0)
            {
                MessageUtil.ShowWarning("请至少选择一条待删除记录！");
                return;
            }

            // 记录选中记录的索引
            int[] selectedIdx = gvArvInfo.GetSelectedRows();
            string msg = string.Format("确实要删除这{0}条信息吗？", selectedIdx.Length);
            if (MessageUtil.ShowYesNoAndWarning(msg) == DialogResult.Yes)
            {
                // 反序排列
                int[] idx = selectedIdx.Reverse().ToArray();

                List<ArchiveInfoDto> toDelete = new List<ArchiveInfoDto>();
                foreach (int rowIndex in idx)
                {
                    // 获得待删除的档案信息
                    toDelete.Add((ArchiveInfoDto)gvArvInfo.GetRow(rowIndex));
                }
                try
                {
                    CallerFactory.Instance.GetService<IArvOpService>().DeleteArvs(toDelete);
                    gvArvInfo.DeleteSelectedRows();
                    MessageUtil.ShowTips("删除成功！");
                }
                catch (Exception ex)
                {
                    MessageUtil.ShowError(ex.ToString());
                }
            }
        }

        /// <summary>
        /// 单条删除
        /// </summary>
        private void OnDeleteOne()
        {
            string msg = string.Format("确实要删除这条信息吗？");
            if (MessageUtil.ShowYesNoAndWarning(msg) == DialogResult.Yes)
            {
                try
                {
                    CallerFactory.Instance.GetService<IArvOpService>().DeleteArv((ArchiveInfoDto)gvArvInfo.GetFocusedRow());
                    gvArvInfo.DeleteRow(gvArvInfo.FocusedRowHandle);
                    MessageUtil.ShowTips("删除成功！");
                }
                catch (Exception ex)
                {
                    MessageUtil.ShowError(ex.ToString());
                }
            }
           
        }

        /// <summary>
        /// EXCEL导入后更新界面显示
        /// </summary>
        /// <param name="arvs"></param>
        private void UpdateGridView(List<ArchiveInfoDto> arvs)
        {
            foreach (ArchiveInfoDto arv in arvs)
            {
                arvList.Add(arv);
            }
        }

        private void OnImport()
        {
            FormPBExcelImport frmImport = new FormPBExcelImport();
            frmImport.GetImportArchivesEvent += UpdateGridView;
            frmImport.Show();
        }

        private void OnExport()
        {
            int[] allSelected = gvArvInfo.GetSelectedRows();

            if (allSelected.Length == 0)
            {
                MessageUtil.ShowWarning("请选择需要导出到Excel文件的记录!");
                return;
            }


            // 把表格记录转换为数据表格
            DataTable exportTable = GridControlHelper.Instance.ConvertToDataTable<ArchiveInfoDto>(gvArvInfo, allSelected);
            // 数据表格写入Excel文件
            ExcelHelper.Instance.DataTableExportToExcel(exportTable, "档案信息");
        }

        private void OnInput()
        {
            BindingList<ArchiveInfoDto> arvs = new BindingList<ArchiveInfoDto>();//待入柜的档案

            int[] allSelected = gvArvInfo.GetSelectedRows();

            if (allSelected.Count() == 0)
            {
                MessageUtil.ShowWarning("请选择要入柜的档案！");
                return;
            }

            foreach (int idx in allSelected)
            {
                ArchiveInfoDto item = (ArchiveInfoDto)gvArvInfo.GetRow(idx);
                arvs.Add(item);
            }

            FrmDVDeviceCtrl frmDevice = new FrmDVDeviceCtrl();
            frmDevice.ArchiveList = arvs;
            frmDevice.Tag = "Input";
            frmDevice.InformCompleted += UpdateInfo;
            frmDevice.ShowDialog();
        }

        private void UpdateInfo(string arvID)
        {
            ArchiveInfoDto arv = arvList.First(q => q.ArvID == arvID);
            if (arv != null)
            {
                arvList.Remove(arv);
            }
        }
        #endregion

        #region 搜索
        private void OnSearch()
        {
            List<QueryConditionHelper> conditions = QueryHelper.GetCondition(ctrlPanel);
            arvList = new BindingList<ArchiveInfoDto>(CallerFactory.Instance.GetService<IArvOpService>().FindArvs(conditions));
            gcArvInfo.DataSource = arvList;
        }

        private void OnExpand()
        {
            if (panelSearch.Visible)
                panelSearch.Visible = false;
            else
                panelSearch.Visible = true;
        }

        private void OnAdvanced()
        {
            List<QueryConditionHelper> conditions = QueryHelper.GetCondition(ctrlPanel);
            if(conditions!=null)
            {
                conditions.AddRange(QueryHelper.GetCondition(panelSearch));
                arvList = new BindingList<ArchiveInfoDto>(CallerFactory.Instance.GetService<IArvOpService>().FindArvs(conditions));
                gcArvInfo.DataSource = arvList;
            }
        }
        private void ClearSearch()
        {
            //cbxKeyWord.Text = "";     // 清除搜索关键字
            queryList.Clear();
        }
        #endregion

        

        /// <summary>
        /// 分页控件初始化
        /// </summary>
        //private void InitPager()
        //{
        //    myPager1.PageIndexChanged += myPager1_PageIndexChanged;
        //    BindDataWithPage();
        //}

        //private void BindDataWithPage()
        //{
        //    int totalCount = 0;
        //    arvList = new BindingList<ArchiveInfoDto>(CallerFactory.Instance.GetService<IArvOpService>().GetArvInStorage(myPager1.PageCurrent, myPager1.PageSize, ref totalCount));
        //    myPager1.NMax = totalCount;//数据总数
        //    gcArvInfo.DataSource = arvList;
        //}

        private void BindData()
        {
            arvList = new BindingList<ArchiveInfoDto>(CallerFactory.Instance.GetService<IArvOpService>().GetArvInStorage());
            if(arvList!=null&&arvList.Count!=0)
                gcArvInfo.DataSource = arvList;
           
        }

        private void FormOPArvInput_Load(object sender, EventArgs e)
        {
            panelSearch.Visible = false;
            FieldCfgDto cfg = PropertyHelper.FieldCfgItems.Find(q => q.FieldName == "ArvID");
            txtArvId.SetWatermark("请输入"+cfg==null?"ArvID":cfg.FieldShowName);
            //InitPager();
            BindData();
            GridControlHelper.Instance.AddControlButton(gvArvInfo, controlButtons);
            // 配置表格列标题和可见性
            GridControlHelper.Instance.ConfigGridViewColumns(gvArvInfo);
            gvArvInfo.BestFitColumns();
        }

        //void gcArvInfo_DataSourceChanged(object sender, EventArgs e)
        //{
        //    BindDataWithPage();
        //}

        //void myPager1_PageIndexChanged(object sender, EventArgs e)
        //{
        //    BindDataWithPage();
        //}

        private void buttonClick(object sender, EventArgs e)
        {
            switch((sender as SimpleButton).Name)
            {
                #region 档案操作
                case "btnAdd"://增加记录
                    ctrlPanel.Tag="ADD";
                    // 进入新增对话框
                    OpenFormInfo("ADD", null);
                    break;
                case "btnEdit"://修改记录
                    OnEdit();
                    break;
                case "btnDelete"://删除记录
                    OnDelete();
                    break;
                case "btnImport":
                    OnImport();
                    break;
                case "btnExport":
                    OnExport();
                    break;
                case "btnInput":
                    OnInput();
                    break;
                case "btnSetlocation":
                    OnSetLocation();
                    break;
                #endregion
                #region 搜索
                case "btnSearch"://搜索
                    OnSearch();
                    break;
                case "btnExpand"://展开下拉菜单
                    OnExpand();
                    break;
                case "btnAdvanced"://高级搜索
                    OnAdvanced();
                    break;
                #endregion
            }
        }

        private void controlButtons_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            switch (e.Button.ToolTip)
            {
                case "预览":
                    PreviewHelper.Instance.OpenPreview(gvArvInfo.GetRowCellValue(gvArvInfo.FocusedRowHandle, "Edoc"));
                    break;
                case "修改":
                    OnEditOne();
                    break;
                case "删除":
                    OnDeleteOne();
                    break;
            }
        }
    }
}
