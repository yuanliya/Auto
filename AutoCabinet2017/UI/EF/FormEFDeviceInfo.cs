using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using NJUST.AUTO06.Utility;

using ZY.EntityFrameWork.Core.Model.Dto;
using ZY.EntityFrameWork.Core.DBHelper;
using ZY.EntityFrameWork.Caller;
using ZY.EntityFrameWork.Caller.Facade;

namespace AutoCabinet2017.UI.EF
{
    public partial class FormEFDeviceInfo : Form
    {
        // 待编辑的设备实体
        // 必须采用表格数据源中既有实体对象来赋值，因为EF对该实体有跟踪；
        // 否则，EF会认为是新的实体，更新会失败！
        public DeviceDto EditedDeviceDto { set; private get; }

        // 更新UI界面的表格
        public delegate void UpdateViewEventHandler(DeviceDto deviceDto);
        public event UpdateViewEventHandler OnUpdateView;

        /// <summary>
        /// 通知UI界面更新表格
        /// </summary>
        /// <param name="deviceDto">UI界面表格显示的设备实体信息</param>
        private void RaiseOnUpdateView(DeviceDto deviceDto)
        {
            if (OnUpdateView == null) return;

            OnUpdateView(deviceDto);
        }

        public FormEFDeviceInfo()
        {
            InitializeComponent();
        }

        private void FormEFDeviceInfo_Load(object sender, EventArgs e)
        {
            if(this.Tag.ToString() == "EDIT")
            {
                // 设备类实体信息
                txtDevNo.Text     = EditedDeviceDto.CabinetNo.ToString();
                txtDevLayers.Text = EditedDeviceDto.CabinetLayers.ToString();
                txtDevCells.Text = EditedDeviceDto.CabinetCells.ToString();

                // 编辑状态禁止编辑“设备编号”
                txtDevNo.Enabled = false;
            }           
        }

        /// <summary>
        /// 新增设备
        /// </summary>
        private void Add()
        {
            // 设备类实体信息
            DeviceDto devDto = new DeviceDto();

            devDto.CabinetNo = Convert.ToInt16(txtDevNo.Text);
            devDto.CabinetLayers = Convert.ToInt16(txtDevLayers.Text);
            devDto.CabinetCells  = Convert.ToInt16(txtDevCells.Text);

            try
            {
                // 添加设备
                int succeed = CallerFactory.Instance.GetService<ISystemConfigService>().Add(devDto);
                if (succeed != 0)
                {
                    MessageUtil.ShowTips("添加设备信息成功！");

                    // 更新主界面的表格
                    RaiseOnUpdateView(devDto);
                }
                else
                {
                    MessageUtil.ShowTips("添加设备信息失败！");
                }
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 更新设备
        /// </summary>
        private void Edit()
        {
            // 设备类实体信息
            EditedDeviceDto.CabinetLayers = Convert.ToInt16(txtDevLayers.Text);
            EditedDeviceDto.CabinetCells = Convert.ToInt16(txtDevCells.Text);

            try
            {
                // 编辑设备
                int succeed = CallerFactory.Instance.GetService<ISystemConfigService>().UpdateDevice(EditedDeviceDto);
                if (succeed != 0)
                {
                    MessageUtil.ShowTips("更新设备信息成功！");

                    // 更新主界面的表格
                    RaiseOnUpdateView(EditedDeviceDto);
                    // 关闭对话框
                    this.Close();
                }
                else
                {
                    MessageUtil.ShowTips("更新设备信息失败！");
                }
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 保存操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDevNo.Text) || string.IsNullOrEmpty(txtDevLayers.Text))
            {
                MessageUtil.ShowWarning("设备基本信息不能为空！");
                return;
            }

            if (this.Tag.ToString() == "ADD")
            {
                // 添加记录
                Add();

                txtDevNo.Text = "";
            }

            if (this.Tag.ToString() == "EDIT")
            {
                // 更新记录
                Edit();
            }
        }

        #region 文本框输入的有效性判断

        private void txtDevCells_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 档案柜格数只接受非负整数输入
            KeyboardInputHelper.Instance.InputInteger(e);
        }

        private void txtDevLayers_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 档案柜层数只接受非负整数输入
            KeyboardInputHelper.Instance.InputInteger(e);
        }

        private void txtDevNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 档案柜编号只接受非负整数输入
            KeyboardInputHelper.Instance.InputInteger(e);
        }

        #endregion
    }
}
