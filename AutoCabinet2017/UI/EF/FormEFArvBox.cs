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

using NJUST.AUTO06.Utility;

using AutoCabinet2017.Helper;

using ZY.EntityFrameWork.Caller;
using ZY.EntityFrameWork.Caller.Facade;
using ZY.EntityFrameWork.Core.DBHelper;
using ZY.EntityFrameWork.Core.Model.Dto;
using ZY.EntityFrameWork.Core.Model.Entity;

namespace AutoCabinet2017.UI.EF
{
    public partial class FormArvBox : Form
    {
        // 保存系统管理的所有回转库信息
        private List<DeviceDto> listDeviceInfo = null;

        public ArvBoxDto ArvBoxDto { get { return arvBoxDto; } }
        private ArvBoxDto arvBoxDto;

        // 表格关联的档案盒信息
        private BindingList<ArvBoxDto> arvBoxs = new BindingList<ArvBoxDto>();
        // 处理表格的RadioGroup控件
        private GridRadioGroupColumnHelper radioGroupHelper;

        public FormArvBox()
        {
            InitializeComponent();

            // 设置表格数据源
            gcArvBox.DataSource = arvBoxs;

            // 处理表格中RadioGroup控件
            radioGroupHelper = new GridRadioGroupColumnHelper(gvArvBox);
            radioGroupHelper.SelectedRowChanged += new EventHandler(RadioGroupHelper_SelectedRowChanged);
        }

        /// <summary>
        /// Radio选择变动时的事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void RadioGroupHelper_SelectedRowChanged(object sender, EventArgs e)
        {
            if (radioGroupHelper.SelectedDataSourceRowIndex != -1)
            {
                toolOK.Enabled = true;
            }         
            else
            {
                toolOK.Enabled = false;
            }              
        }

        /// <summary>
        /// 设备编号变化时，确定对应的总层数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxGroupNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // linq检索List数据集
            DeviceDto dev = listDeviceInfo.Find(s => s.CabinetNo == (int)cbxGroupNo.EditValue);

            if (dev != null)
            {
                for (int i = 0; i < dev.CabinetLayers; i++)
                {
                    cbxLayerNo.Properties.Items.Add(i);
                }
            }
        }
       
        private void FrmArvBox_Load(object sender, EventArgs e)
        {
            try
            {
                // 查询设备信息
                listDeviceInfo = new List<DeviceDto>(CallerFactory.Instance.GetService<ISystemConfigService>().GetAllDevices());

                if(listDeviceInfo.Count > 0)
                {
                    // 初始化下拉框
                    foreach(DeviceDto item in listDeviceInfo)
                    {
                        cbxGroupNo.Properties.Items.Add(item.CabinetNo);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
                return;
            }
            
            toolOK.Enabled       = true;
            panelSearch.Visible = false;

            // 高级搜索面板标签的显示名称
            foreach (Control cn in panelSearch.Controls)
            {
                if (cn is Label)
                {
                    FieldCfgDto arvField = PropertyHelper.FieldCfgItems.Find(item => item.FieldName == (cn as Label).Text);
                    (cn as Label).Text = arvField.FieldShowName;
                }
            }

            // 检索当前所有档案盒信息
            arvBoxs = new BindingList<ArvBoxDto>(CallerFactory.Instance.GetService<IArvOpService>().FindArvBoxes(null)); 
            // 表格数据源关联
            gcArvBox.DataSource = arvBoxs;
        }

        #region 工具栏操作

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolOK_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // 获得选中的档案盒信息
            arvBoxDto = (ArvBoxDto)gvArvBox.GetRow(gvArvBox.GetSelectedRows()[0]);
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 展开高级检索
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
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolQuery_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<QueryCondition> conditions = new List<QueryCondition>();

            try
            {
                 // 根据常规搜索panel的内容构建搜索条件
                QueryCondition condition = new QueryCondition(edtArvBoxID.Tag.ToString(), CompareType.Include, edtArvBoxID.EditValue == null ? "" : edtArvBoxID.EditValue.ToString());
                conditions.Add(condition);
                // 查询档案盒信息
                arvBoxs = new BindingList<ArvBoxDto>(CallerFactory.Instance.GetService<IArvOpService>().FindArvBoxes(conditions));
                // 表格数据源关联
                gcArvBox.DataSource = arvBoxs;
            }
            catch(Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 高级查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdvanced_Click(object sender, EventArgs e)
        {
            List<QueryCondition> conditions = new List<QueryCondition>();

            try
            {
                // 根据常规搜索panel的内容构建搜索条件
                QueryCondition condition = new QueryCondition(edtArvBoxID.Tag.ToString(), CompareType.Include, edtArvBoxID.EditValue == null ? "" : edtArvBoxID.EditValue.ToString());
                conditions.Add(condition);

                // 构建高级查询条件
                conditions.AddRange(QueryHelper.GetIntegrativeCondition(panelSearch));
                // 查询档案盒信息
                arvBoxs = new BindingList<ArvBoxDto>(CallerFactory.Instance.GetService<IArvOpService>().FindArvBoxes(conditions));
                // 表格数据源关联
                gcArvBox.DataSource = arvBoxs;
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
            }           
        }

        #endregion
    }
}
