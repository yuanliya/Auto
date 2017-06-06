using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

using NJUST.AUTO06.Utility;

using AutoCabinet2017.Helper;

using AutoCabinet2017.UI.EF;

using ZY.EntityFrameWork.Core.Model.Dto;
using ZY.EntityFrameWork.Core.DBHelper;
using ZY.EntityFrameWork.Caller;
using ZY.EntityFrameWork.Caller.Facade;

namespace AutoCabinet2017.UI.DV
{
    public partial class FormDVDeviceConfig : Form
    {
        private BindingList<DeviceDto> devList = null;                            
        private BindingList<DeviceDto> blDev   = new BindingList<DeviceDto>();  

        public FormDVDeviceConfig()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 配置表格的控制列
        /// 只需要对表格操作一次，以后表格再关联数据源都无需再处理
        /// </summary>
        private void ConfigGridView()
        {
            // 根据列标题设定表格中不可见的列
            string[] cols = new string[] { "ID" };
            GridControlHelper.Instance.SetColumnInvisible(gvDevice, cols);

            // 在表格最后一列加入控制列
            GridControlHelper.Instance.AddControlButton(gvDevice, buttonControl, gvDevice.Columns.Count);
            GridControlHelper.Instance.ConfigGridViewStyle(gvDevice);
        }

        /// <summary>
        /// 表格绑定数据源
        /// </summary>
        private void BindDataToGridView()
        {
            try
            {
                // 表格绑定所有设备信息
                devList = new BindingList<DeviceDto>(CallerFactory.Instance.GetService<ISystemConfigService>().GetAllDevices());
                if (devList != null && devList.Count != 0)
                {
                    gcDevice.DataSource = devList;
                }               
            }
            catch (Exception ex)
            {
                MessageUtil.ShowWarning(ex.Message);
            }
        }

        private void FormDVSystemConfig_Load(object sender, EventArgs e)
        {
            if (PropertyHelper.CurrentUser.RoleName != "超级管理员")
            {
                RoleModuleDto dto = PropertyHelper.RoleModules.Find(q => q.ModuleTag.ToString() == "502");
                if (dto != null)
                {
                    toolBar.Authorize(dto.Permissions);
                    buttonControl.Authorize(dto.Permissions);
                }
            }
            // 表格绑定数据源
            BindDataToGridView();

            // 配置表格的控制列和列标题
            ConfigGridView();
        }

        #region 表格内嵌按钮的操作

        /// <summary>
        /// 表格控制列的删除操作
        /// </summary>
        private void OnDelete()
        {
            if (MessageUtil.ShowYesNoAndWarning("确实要删除这条信息吗？") == DialogResult.Yes)
            {
                try
                {
                    // 删除本条记录
                    CallerFactory.Instance.GetService<ISystemConfigService>().Delete((DeviceDto)gvDevice.GetFocusedRow());
                    // 从表格中删除记录
                    gvDevice.DeleteRow(gvDevice.FocusedRowHandle);

                    MessageUtil.ShowTips("删除成功！");
                }
                catch (Exception ex)
                {
                    MessageUtil.ShowError(ex.ToString());
                }
            }
        }

        /// <summary>
        /// 表格控制列的编辑操作
        /// </summary>
        private void OnEdit()
        {
            toolBar.Tag = "EDIT";

            FormEFDeviceInfo frmDeviceInfo = new FormEFDeviceInfo();

            // 注意！！由于是更新数据库已有记录，这里不能用新的DeviceDto实例去保存信息
            // 必须采用表格数据源中既有实体对象来更新，因为EF对该实体有跟踪；
            // 否则，EF会认为是新的实体，更新会失败！
            frmDeviceInfo.EditedDeviceDto = gvDevice.GetFocusedRow() as DeviceDto;
            // Form为编辑操作
            frmDeviceInfo.Tag = "EDIT";
            // 订阅表格更新事件
            frmDeviceInfo.OnUpdateView += UpdateViewShow;

            frmDeviceInfo.ShowDialog();
        }

        /// <summary>
        /// 表格中点击按钮操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonControl_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            switch (e.Button.ToolTip)
            {
                case "编辑":
                    OnEdit();
                    break;

                case "删除":
                    OnDelete();
                    break;

                default:
                    break;
            }
        }

        #endregion

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

        /// <summary>
        /// 更新表格显示
        /// </summary>
        /// <param name="deviceDto"></param>
        private void UpdateViewShow(DeviceDto deviceDto)
        {
            if(toolBar.Tag.ToString() == "ADD")
            {
                // 更新表格关联的实体数据集
                devList.Add(deviceDto);

                // 更新表格显示
                if (gcDevice.DataSource != devList)
                {
                    gcDevice.DataSource = devList;
                    // 设置最后一行为焦点行
                    gvDevice.FocusedRowHandle = gvDevice.RowCount - 1;
                }
            }

            if(toolBar.Tag.ToString() == "EDIT")
            {
                // 更新表格显示
                gvDevice.UpdateCurrentRow();
            }           
        }

        #region 工具栏操作

        /// <summary>
        /// 工具栏操作--新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormEFDeviceInfo frmDeviceInfo = new FormEFDeviceInfo();

            // 工具栏为新增操作
            toolBar.Tag = "ADD";
            // 对话框为新增操作
            frmDeviceInfo.Tag = "ADD";
            // 订阅表格更新事件
            frmDeviceInfo.OnUpdateView += UpdateViewShow;

            frmDeviceInfo.ShowDialog();
        }

        /// <summary>
        /// 工具栏操作--批量删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] selectedIdx = gvDevice.GetSelectedRows();
            if(selectedIdx.Length == 0)
            {
                MessageUtil.ShowWarning("请选择要删除的设备信息！");
                return;
            }

            string str = string.Format("确实要删除{0}条设备信息吗？", selectedIdx.Length);
            if (DialogResult.Yes == MessageUtil.ShowYesNoAndTips(str))
            {
                List<DeviceDto> toDelete = new List<DeviceDto>();
                foreach (int rowIndex in selectedIdx)
                {
                    // 获得待删除的设备信息
                    toDelete.Add((DeviceDto)gvDevice.GetRow(rowIndex));
                }

                try
                {
                    // 批量删除
                    CallerFactory.Instance.GetService<ISystemConfigService>().Delete(toDelete);
                    // 从表格中删除记录
                    gvDevice.DeleteSelectedRows();

                    MessageUtil.ShowTips("删除成功！");
                }
                catch (Exception ex)
                {
                    MessageUtil.ShowWarning(ex.Message);
                }
            }
        }

        /// <summary>
        /// 设备通信配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolCommConfig_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormDVSerialPortConfig frmDVSpConfig = new FormDVSerialPortConfig();
            frmDVSpConfig.Text = "串口配置";
            frmDVSpConfig.ShowDialog();
        }

        #endregion

    
    }
}
