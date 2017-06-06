using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevExpress.XtraBars;

using AutoCabinet2017.Helper;

using NJUST.AUTO06.Utility;

using ZY.EntityFrameWork.Core.DBHelper;

namespace AutoCabinet2017.UI.RP
{
    public partial class FormRPStatics : Form
    {
        public FormRPStatics()
        {
            InitializeComponent();
        }

        private void FormRPStatics_Load(object sender, EventArgs e)
        {
            dtFrom.Enabled = false;
            dtTo.Enabled   = false;
            radioTimeSpan.SelectedIndex = 0;
            radioLendType.SelectedIndex = 0;
        }

        /// <summary>
        /// 执行统计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolExcuteStatics_ItemClick(object sender, ItemClickEventArgs e)
        {
            List<QueryCondition> conditions = GetCondition();
        }

        List<QueryCondition> GetCondition()
        {
            List<QueryCondition> conditions;
          
            if ((Int16)radioTimeSpan.EditValue > 0)
            {
                conditions = QueryHelper.GetTimeCondition((Int16)radioTimeSpan.EditValue, "DateTime");
            }
            else
            {
                conditions = QueryHelper.GetTimeCondition(dtFrom.DateTime, dtTo.DateTime, "DateTime");
            }

            if (toolStaticsType.EditValue != null && toolStaticsType.EditValue.ToString() == "借阅统计")
            {
                conditions.Add(QueryHelper.GetConditionModel("ArvStatus", CompareType.Equal, "借出"));
            }
            
            return conditions;
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

        private void radioTimeSpan_EditValueChanged(object sender, EventArgs e)
        {
            if((Int16)radioTimeSpan.EditValue==-1)
            {
                dtFrom.Enabled = true;
                dtTo.Enabled = true;
            }
            else
            {
                dtFrom.Enabled = false;
                dtTo.Enabled = false;
            }

        }
    }
}
