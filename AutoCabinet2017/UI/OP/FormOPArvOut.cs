using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevExpress.XtraEditors;

using ZY.EntityFrameWork.Caller;
using ZY.EntityFrameWork.Caller.Facade;
using ZY.EntityFrameWork.Core.DBHelper;
using ZY.EntityFrameWork.Core.Model.Dto;

using NJUST.AUTO06.Utility;

using AutoCabinet2017.Helper;
using AutoCabinet2017.UI.DV;

namespace AutoCabinet2017.UI.OP
{
    public partial class FormOPArvOut : Form
    {
        public FormOPArvOut()
        {
            InitializeComponent();
        }

        BindingList<ArchiveInfoDto> arvList = new BindingList<ArchiveInfoDto>();

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

        private void OnOutput()
        {
            BindingList<ArchiveInfoDto> arvs = new BindingList<ArchiveInfoDto>();//待出库的档案
            int[] allSelected = gvArvInfo.GetSelectedRows();

            if (allSelected.Count() == 0)
            {
                MessageUtil.ShowWarning("请选择要出库的档案！");
                return;
            }

            foreach (int idx in allSelected)
            {
                ArchiveInfoDto item = (ArchiveInfoDto)gvArvInfo.GetRow(idx);
                arvs.Add(item);
            }

            //FormDVDeviceCtrl frmDevice = new FormDVDeviceCtrl();
            //frmDevice.ArchiveList = arvs;
            //frmDevice.Tag = "Output";
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

        #region 搜索
        private void OnSearch()
        {
            //List<QueryCondition> conditions = QueryHelper.GetIntegrativeCondition(ctrlPanel);
            //conditions.Add(QueryHelper.GetConditionModel("ArvStatus", CompareType.Equal, "在库"));
            //arvList = new BindingList<ArchiveInfoDto>(CallerFactory.Instance.GetService<IArvOpService>().FindArvs(conditions));
            //gcArvInfo.DataSource = arvList;
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
            //List<QueryCondition> conditions = QueryHelper.GetIntegrativeCondition(ctrlPanel);
            //if (conditions != null)
            //{
            //    conditions.Add(QueryHelper.GetConditionModel("ArvStatus", CompareType.Equal, "在库"));
            //    conditions.AddRange(QueryHelper.GetIntegrativeCondition(panelSearch));
            //    arvList = new BindingList<ArchiveInfoDto>(CallerFactory.Instance.GetService<IArvOpService>().FindArvs(conditions));
            //    gcArvInfo.DataSource = arvList;
            //}
        }

        #endregion

        private void buttonClick(object sender, EventArgs e)
        {
            switch ((sender as SimpleButton).Name)
            {
                #region 档案操作
                case "btnExport":
                    OnExport();
                    break;
                case "btnOutput":
                    OnOutput();
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

        private void FrmOPArvOutput_Load(object sender, EventArgs e)
        {
            if (PropertyHelper.CurrentUser.RoleName != "超级管理员")
            {
                RoleModuleDto dto = PropertyHelper.RoleModules.Find(q => q.ModuleTag.ToString() == "102");
                if (dto != null)
                {
                    bar1.Authorize(dto.Permissions);
                    controlButtons.Authorize(dto.Permissions);
                }
            }
            gcArvInfo.DataSource = arvList;
        }
    }
}
