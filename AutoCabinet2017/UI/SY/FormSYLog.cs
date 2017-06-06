using AutoCabinet2017.Helper;
using NJUST.AUTO06.Utility;
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

namespace AutoCabinet2017.UI.SY
{
    public partial class FormSYLog : Form
    {
        public FormSYLog()
        {
            InitializeComponent();
        }
        private void RefreshView()
        {
            //设置列可见性
            switch (cbxLogType.Text)
            {
                case "登录日志":
                    GridControlHelper.Instance.SetColumnInvisible(gvLog, new string[] { "ID", "Level", "Thread", "Exception", "Logger" });
                    break;
                case "操作日志":
                    GridControlHelper.Instance.SetColumnInvisible(gvLog, new string[] { "ID", "Level", "Thread", "Exception", "Logger" });
                    break;
                case "错误日志":
                    GridControlHelper.Instance.SetColumnInvisible(gvLog, new string[] { "ID", "Level", "Thread", "Logger" });
                    break;
                default:
                    GridControlHelper.Instance.SetColumnInvisible(gvLog, new string[] { "ID", "Level", "Thread" });
                    break;
            }

            groupBox1.Text = cbxLogType.Text;
        }
       

        private void FormSYLog_Load(object sender, EventArgs e)
        {
            if (PropertyHelper.CurrentUser.RoleName != "超级管理员")
            {
                RoleModuleDto dto = PropertyHelper.RoleModules.Find(q => q.ModuleTag.ToString() == "604");
                if (dto != null)
                {
                    panelCtrl.Authorize(dto.Permissions);
                    //controlButtons.Authorize(dto.Permissions);
                }
            }
            gcLog.DataSource = CallerFactory.Instance.GetService<IAuthorityService>().GetAllLog();
            RefreshView();
            GridControlHelper.Instance.ConfigGridViewStyle(gvLog);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<QueryCondition> timeCondition = QueryHelper.GetTimeCondition(dtFrom.DateTime, dtTo.DateTime, "Datetime");
            timeCondition.AddRange(QueryHelper.GetIntegrativeCondition(panelCtrl));
            gcLog.DataSource = CallerFactory.Instance.GetService<IAuthorityService>().GetLog(timeCondition);
            RefreshView();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DateTime dt=DateTime.Now.AddHours(3);
            try
            {
                MessageUtil.ShowYesNoAndTips("确定删除30天内日志");
                int deleted=CallerFactory.Instance.GetService<IAuthorityService>().DeleteRecentLog();
                MessageUtil.ShowTips("共删除30天前日志{0}条！");
                LogHelper.LogOpInfo("共删除30天前日志{0}条！");
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.ToString());
                LogHelper.LogSysError("日志删除错误", ex);
            }
        }

        private void gvLog_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if(e.Column.FieldName=="Datetime")
                e.Column.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";//设置时间显示格式
        }

        private void cbxLogType_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void dtFrom_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (DateTime.Compare((DateTime)e.NewValue, dtTo.DateTime) > 0)
            {
                MessageUtil.ShowWarning("起始时间不得晚于终止时间！");
                e.Cancel = true;
            }
        }

        private void dtTo_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (DateTime.Compare(dtFrom.DateTime, (DateTime)e.NewValue) > 0)
            {
                MessageUtil.ShowWarning("起始时间不得晚于终止时间！");
                e.Cancel = true;
            }
        }


    }
}
