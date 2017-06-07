using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevExpress.XtraEditors.Controls;
using DevExpress.XtraBars.Docking2010.Views.NativeMdi;
using DevExpress.XtraBars.Docking2010.Views.Tabbed;
using DevExpress.XtraEditors;
using DevExpress.XtraBars;

using ZY.EntityFrameWork.Caller;
using ZY.EntityFrameWork.Caller.Facade;
using ZY.EntityFrameWork.Core.DBHelper;
using ZY.EntityFrameWork.Core.Model.Dto;

using NJUST.AUTO06.Utility;
using NJUST.AUTO06.Utility.PreviewUtil;

using AutoCabinet2017.Helper;
using AutoCabinet2017.UI.DV;
using AutoCabinet2017.UI.EF;
using AutoCabinet2017.UI.PB;

namespace AutoCabinet2017.UI.OP
{
    public partial class FormOPArvIn : Form
    {
        public FormOPArvIn()
        {
            InitializeComponent();
        }

        // 表格关联的数据集
        private BindingList<ArchiveInfoDto> arvList = new BindingList<ArchiveInfoDto>();
        // 查询得到的表
        private BindingList<ArchiveInfoDto> queryList = new BindingList<ArchiveInfoDto>();

        private void FormOPArvIn_Load(object sender, EventArgs e)
        {
            // 工具栏默认无操作
            toolBar.Tag = "";

            // 根据用户角色设置工具栏和嵌入式按钮的使用权限
            if (PropertyHelper.CurrentUser.RoleName != "超级管理员")
            {
                RoleModuleDto dto = PropertyHelper.RoleModules.Find(q => q.ModuleTag.ToString() == "101");
                if (dto != null)
                {
                    toolBar.Authorize(dto.Permissions);
                    controlButtons.Authorize(dto.Permissions);
                }
            }

            // 默认隐藏“高级搜索”
            panelSearch.Visible = false;

            // 表格绑定所有“在档”档案
            arvList = new BindingList<ArchiveInfoDto>(CallerFactory.Instance.GetService<IArvOpService>().GetArvInDocument());
            if (arvList != null && arvList.Count != 0)
            {
                gcArvInfo.DataSource = arvList;
            }

            // ID列不可见
            //string[] cols = new string[] { "ID" };
            //GridControlHelper.Instance.SetColumnInvisible(gvArvInfo, cols);

            // 为表格加入控制列
            GridControlHelper.Instance.AddControlButton(gvArvInfo, controlButtons, gvArvInfo.Columns.Count);
            // 配置表格列标题和可见性
            GridControlHelper.Instance.ConfigGridViewColumns(gvArvInfo);
            // 表格宽度自适应
            gvArvInfo.BestFitColumns();
        }

        #region 档案操作

        /// <summary>
        /// 多条档案编辑
        /// </summary>
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
                // 添加记录集
                selectedArvItems.Add(info);
            }

            // 进入编辑对话框
            toolBar.Tag = "EDIT";
            OpenFormInfo("EDIT", selectedArvItems);

            // 刷新表格关联数据集
            gcArvInfo.RefreshDataSource();
        }

        /// <summary>
        /// 表格中点击“编辑”按钮的处理
        /// </summary>
        private void OnEditOne()
        {
            // 进入编辑对话框
            toolBar.Tag = "EDIT";

            // 建立选中档案记录集
            BindingList<ArchiveInfoDto> selectedArvItems = new BindingList<ArchiveInfoDto>();
            // 添加记录集
            selectedArvItems.Add((ArchiveInfoDto)gvArvInfo.GetFocusedRow());

            // 进入档案信息编辑框
            OpenFormInfo("EDIT", selectedArvItems);

            // 刷新表格关联数据集
            gcArvInfo.RefreshDataSource();
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
                    // 从数据库中删除多条记录
                    CallerFactory.Instance.GetService<IArvOpService>().Delete(toDelete);
                    // 从表格中删除记录
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
                    // 从数据库中删除记录
                    CallerFactory.Instance.GetService<IArvOpService>().Delete((ArchiveInfoDto)gvArvInfo.GetFocusedRow());
                    // 从表格中删除记录
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

        /// <summary>
        /// Excel导入
        /// </summary>
        private void OnImport()
        {
            FormPBExcelImport frmImport = new FormPBExcelImport();
            // 事件关联
            frmImport.GetImportArchivesEvent += UpdateGridView;
            // 显示Excel导入界面
            frmImport.Show();
        }

        /// <summary>
        /// 导出到Excel
        /// </summary>
        private void OnExport()
        {
            // 获取所有需要导出记录
            int[] allSelected = gvArvInfo.GetSelectedRows();

            if (allSelected.Length == 0)
            {
                MessageUtil.ShowWarning("请选择需要导出到Excel文件的记录!");
                return;
            }


            // 把表格记录转换为数据表格
            DataTable exportTable = GridControlHelper.Instance.ConvertToDataTable<ArchiveInfoDto>(gvArvInfo, allSelected);
            // 数据表格写入Excel文件
            ExcelHelper.Instance.DataTableExportToExcel(exportTable, "在档档案信息");
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
                if(item.ArvBoxID==null)
                {
                    MessageUtil.ShowWarning("请确定所有档案的存放位置！");
                    return;
                }
                // 更新档案当前状态
                item.ArvStatus = "在库";//测试
                arvs.Add(item);
            }

            
            CallerFactory.Instance.GetService<IArvOpService>().UpdateArvInfos((arvs.ToList()));
            //FormDVDeviceCtrl frmDevice = new FormDVDeviceCtrl();
            //frmDevice.ArchiveList = arvs;
            //frmDevice.Tag = "Input";
            //frmDevice.InformCompleted += UpdateInfo;
            //frmDevice.ShowDialog();
        }

        private void UpdateInfo(string arvID)
        {
            ArchiveInfoDto arv = arvList.First(q => q.ID == arvID); //(q => q.ArvID == arvID);
            if (arv != null)
            {
                arvList.Remove(arv);
            }
        }

        #endregion

        #region 搜索

        /// <summary>
        /// 针对档案编号的搜索
        /// </summary>
        //private void OnSearch()
        //{
        //    // 根据常规搜索panel的内容构建搜索条件
        //    List<QueryCondition> conditions = QueryHelper.GetIntegrativeCondition(ctrlPanel);
        //    // 执行搜索操作
        //    arvList = new BindingList<ArchiveInfoDto>(CallerFactory.Instance.GetService<IArvOpService>().FindArvs(conditions));
        //    // 关联到表格
        //    gcArvInfo.DataSource = arvList;
        //}

        /// <summary>
        /// 展开高级搜索面板
        /// </summary>
        private void OnExpand()
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
        /// 执行高级搜索
        /// </summary>
        private void OnAdvanced()
        {
            // 根据常规搜索panel的内容构建搜索条件
            List<QueryCondition> conditions = new List<QueryCondition>();// QueryHelper.GetIntegrativeCondition(ctrlPanel);
           // if (conditions != null)
            {
                // 根据高级搜索panel的内容构建搜索条件
                conditions.AddRange(QueryHelper.GetIntegrativeCondition(panelSearch));
                // 执行搜索操作
                arvList = new BindingList<ArchiveInfoDto>(CallerFactory.Instance.GetService<IArvOpService>().FindArvs(conditions));
                // 关联到表格
                gcArvInfo.DataSource = arvList;
            }
        }

        private void ClearSearch()
        {
            //cbxKeyWord.Text = "";     // 清除搜索关键字
            queryList.Clear();
        }

        #endregion

        private void OnSetLocation()
        {
            if (gvArvInfo.SelectedRowsCount == 0)
            {
                MessageUtil.ShowWarning("请选择需要选库的档案条目！");
                return;
            }
            FormArvBox frmArvBox = new FormArvBox();
            if (frmArvBox.ShowDialog() == DialogResult.OK)
            {
                LoadArvBox(frmArvBox.ArvBoxDto);
            }
        }

        private void LoadArvBox(ArvBoxDto arvBoxDto)
        {
            List<ArchiveInfoDto> arvs = new List<ArchiveInfoDto>();
            foreach (int index in gvArvInfo.GetSelectedRows())
            {
                ArchiveInfoDto dto = (ArchiveInfoDto)gvArvInfo.GetRow(index);
                //dto.ArvBoxID = arvBoxDto.ArvBoxID;
                //dto.ArvBoxTitle = arvBoxDto.ArvBoxTitle;
                //dto.GroupNo = arvBoxDto.GroupNo;
                //dto.LayerNo = arvBoxDto.LayerNo;
                //dto.CellNo = arvBoxDto.CellNo;

                arvs.Add(dto);
            }
            try
            {
                CallerFactory.Instance.GetService<IArvOpService>().UpdateArvInfos(arvs,arvBoxDto);//更新数据库
                BindingList<ArchiveInfoDto> bindingList = (BindingList<ArchiveInfoDto>)gcArvInfo.DataSource;//当前的绑定
                foreach (int index in gvArvInfo.GetSelectedRows())
                {
                    //更新表格显示
                    gvArvInfo.SetRowCellValue(index, "ArvBoxID", arvBoxDto.ID);//ArvBoxID);
                    gvArvInfo.SetRowCellValue(index, "ArvBoxTitle", arvBoxDto.ArvBoxTitle);
                    gvArvInfo.SetRowCellValue(index, "GroupNo", arvBoxDto.GroupNo);
                    gvArvInfo.SetRowCellValue(index, "LayerNo", arvBoxDto.LayerNo);
                    gvArvInfo.SetRowCellValue(index, "CellNo", arvBoxDto.CellNo);
                }
                MessageUtil.ShowTips("更新成功！");
                LogHelper.LogOpInfo("更新数据库");
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.ToString());
                LogHelper.LogSysError("更新失败",ex);
            }
        }

        /// <summary>
        /// 更新表格显示
        /// </summary>
        /// <param name="arch">编辑记录信息</param>
        private void UpdateGridView(ArchiveInfoDto arch)
        {
            string curOpera = null;// ctrlPanel.Tag.ToString();

            switch (curOpera)
            {
                case "Add":  // “新增”操作时对表格的更新
                    // 更新记录集
                    arvList.Add(arch);

                    if (gcArvInfo.DataSource != arvList)
                    {
                        // 表格关联数据集发生变化，更新数据集
                        gcArvInfo.DataSource = arvList;

                        ClearSearch();  // 清除搜索结果
                    }

                    break;

                case "Edit": // “修改”操作时对表格的更新
                    // 获取当前表格中记录
                    BindingList<ArchiveInfoDto> bindingList = (BindingList<ArchiveInfoDto>)gcArvInfo.DataSource;
                    // 查找被修改的记录
                    ArchiveInfoDto arv = bindingList.First(p => p.ID == arch.ID);//(p => p.ArvID == arch.ArvID);
                    // 用新的记录替代原纪录，表格自动更新
                    bindingList[bindingList.IndexOf(arv)] = arch;

                    break;

                default:
                    break;
            }
        }

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

            if (scopeList != null)
            {
                // "编辑档案"
                frmOPArvInfo.ArvInfoList = scopeList;
            }        
                
            // 订阅事件
            frmOPArvInfo.UpdateGViewDataListEvent += UpdateGridView;
            // 档案编辑对话框
            frmOPArvInfo.ShowDialog();
        }

        /// <summary>
        /// UI界面按钮点击事件的处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClick(object sender, EventArgs e)
        {
            // 根据按钮控件的Name属性执行相应操作
            switch ((sender as SimpleButton).Name)
            {
                //#region 档案操作

                //case "btnAdd":    // 增加记录
                //    ctrlPanel.Tag = "Add";
                //    // 进入新增对话框
                //    OpenFormInfo("ADD", null);
                //    break;
                //case "btnEdit":   // 修改记录
                //    ctrlPanel.Tag = "Edit";
                //    // 进入编辑对话框
                //    OnEdit();
                //    break;
                //case "btnDelete": // 删除记录
                //    ctrlPanel.Tag = "Delete";
                //    // 进入删除对话框
                //    OnDelete();
                //    break;
                //case "btnImport": // 导入记录
                //    ctrlPanel.Tag = "Import";
                //    OnImport();
                //    break;
                //case "btnExport": // 导出记录
                //    ctrlPanel.Tag = "Export";

                //    OnExport();
                //    break;
                //case "btnInStorage":  // 入库
                //    ctrlPanel.Tag = "InStorage";

                //    OnInput();
                //    break;
                //case "btnSetlocation": // 选库
                //    ctrlPanel.Tag = "Location";

                //    OnSetLocation();
                //    break;

                //default:
                //    break;

                //#endregion

                #region 搜索操作

                case "btnSearch":   // 搜索
                    //OnSearch();
                    break;
                case "btnExpand":   // 展开下拉菜单
                    OnExpand();
                    break;
                case "btnAdvanced": // 高级搜索
                    OnAdvanced();
                    break;

                #endregion
            }
        }

        /// <summary>
        /// 处理表格内的控件操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void controlButtons_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            switch (e.Button.ToolTip)
            {
                case "预览":
                    // 根据"Edoc"字段的内容打开关联的文本
                    PreviewHelper.Instance.OpenPreview(gvArvInfo.GetRowCellValue(gvArvInfo.FocusedRowHandle, "Edoc"));
                    break;
                case "修改":
                    OnEditOne();
                    break;
                case "删除":
                    OnDeleteOne();
                    break;
                default:
                    break;
            }
        }

        #region 工具栏操作

        /// <summary>
        /// 工具栏操作--新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            toolBar.Tag = "Add";
            // 进入新增对话框
            OpenFormInfo("ADD", null);
        }

        /// <summary>
        /// 工具栏操作--编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            toolBar.Tag = "Edit";
            // 进入编辑对话框
            OnEdit();
        }

        /// <summary>
        /// 工具栏操作--删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            toolBar.Tag = "Delete";
            // 进入删除对话框
            OnDelete();
        }

        /// <summary>
        /// 工具栏操作--导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolImport_ItemClick(object sender, ItemClickEventArgs e)
        {
            toolBar.Tag = "Import";
            OnImport();
        }

        /// <summary>
        /// 工具栏操作--导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolExport_ItemClick(object sender, ItemClickEventArgs e)
        {
            toolBar.Tag = "Export";
            OnExport();
        }

        /// <summary>
        /// 工具栏操作--选库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolSetLocation_ItemClick(object sender, ItemClickEventArgs e)
        {
            toolBar.Tag = "Location";
            OnSetLocation();
        }

        /// <summary>
        /// 工具栏操作--入库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolInStorage_ItemClick(object sender, ItemClickEventArgs e)
        {
            toolBar.Tag = "InStorage";
            OnInput();
        }

        #endregion

        /// <summary>
        /// 工具栏操作--高级检索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolAdvancedQuery_ItemClick(object sender, ItemClickEventArgs e)
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
        /// 工具栏操作--检索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolQuery_ItemClick(object sender, ItemClickEventArgs e)
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
                gcArvInfo.DataSource = arvList;
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
            }
        }
    }
}
