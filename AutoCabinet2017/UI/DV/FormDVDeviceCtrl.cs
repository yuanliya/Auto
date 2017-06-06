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
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraLayout;
using DevExpress.XtraTreeList.Data;

using ZY.EntityFrameWork.Caller;
using ZY.EntityFrameWork.Caller.Facade;
using ZY.EntityFrameWork.Core.Model.Dto;

using NJUST.AUTO06.MDCONTROL.Comm;
using NJUST.AUTO06.MDCONTROL.Protocal;
using NJUST.AUTO06.Utility;

using AutoCabinet2017.Device;
using AutoCabinet2017.Helper;

namespace AutoCabinet2017.UI.DV
{
    public partial class FormDVDeviceCtrl : XtraForm
    {
        public FormDVDeviceCtrl()
        {
            InitializeComponent();
        }

        public BindingList<ArchiveInfoDto> ArchiveList { get; set; }

        // 记录写入数据库后触发的事件
        public delegate void InformCompletedEventHandler(string arvID);
        public event InformCompletedEventHandler InformCompleted;

        // 控制器对象
        private HZKController Controller = null;
        // 设备监控类
        private DeviceMonitor devMonitor = null;

        private const int MONITOR_DELAY_TIME = 200;   // 监控启动延时
        private const int MONITOR_PERIOD_TIME = 500;   // 监控周期

        private MyCache _Cache = new MyCache("colCheck");//“是否保存”列的列值

        #region 初始化
        /// <summary>
        /// 初始化设备控制及监控
        /// </summary>
        private void InitDeviceControl()
        {
            try
            {
                // 初始化控制设备，采用ModbusRTU协议支持的串口
                if (Controller == null)
                {
                    Controller = new HZKController(CommFactory.Instance.GetCommunicationProxy("主控串口"), ModbusFactory.Instance.GetProtocalProxy());
                }

                // 连接通信口
                Controller.Connect();

                // 初始化设备监控类
                devMonitor = new DeviceMonitor(Controller, MONITOR_PERIOD_TIME);
                // 订阅设备运行状态信息事件
                devMonitor.DeviceRunStatus += UpdateClipBoardInfo;
            }
            catch (Exception ex)
            {
                MessageUtil.ShowWarning(ex.Message);
            }
        }
        private void AddCheckBoxColumn()
        {
            // 为表格增加“是否保存”列，表示该记录是否保存进数据库
            //DataColumn newColumn = new DataColumn();

            //newColumn.DataType = System.Type.GetType("System.Boolean");
            //newColumn.ColumnName = "是否保存";
            //newColumn.DefaultValue = Boolean.FalseString; // 设置选择列的默认值为未选中状态
            //arvInfoTable.Columns.Add(newColumn);
            //GridColumn unbColumn = gvIntoCabinet.Columns.Add();
            GridColumn unbColumn = gvIntoCabinet.Columns.AddField("colCheck");
            unbColumn.Name = "colCheck";
            unbColumn.Caption = "是否保存";
            unbColumn.UnboundType = DevExpress.Data.UnboundColumnType.Boolean;   // 数值类型
            RepositoryItemCheckEdit checkEdit = new RepositoryItemCheckEdit();
            checkEdit.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;//空值状态下是不选中
            unbColumn.ColumnEdit = checkEdit;                                  // 列格式设置为复选框
            unbColumn.VisibleIndex = 0;
            gvIntoCabinet.CustomUnboundColumnData += (sender, e) =>
            {
                if (e.IsGetData)
                    e.Value = _Cache.GetValue(e.Row);
                if (e.IsSetData)
                    _Cache.SetValue(e.Row, e.Value);
            };
        }

       
        private void InitCheckInfo()
        {
            //gArvLend.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;//设置所有的分组不可见
            //LayoutControlGroup group=layoutControl1.Items.FindByName(this.Tag.ToString()) as LayoutControlGroup;
            //if(group==null)
            //{
            //    layoutControl1.Visible = false;
            //}
            //else
            //{
            //    group.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;//只有当前操作登记信息的分组可见
            //}
        }

        private void GetLocation(ArchiveInfoDto arv)
        {
          //  lblGroupNo.Text = arv.GroupNo.ToString();
          //  lblLayerNo.Text = arv.LayerNo.ToString();
        }

        #endregion

        #region 事件处理

        /// <summary>
        /// 更新信息栏事件
        /// </summary>
        /// <param name="info"></param>
        public void UpdateClipBoardInfo(string info)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new DeviceMonitor.DeviceRunStatusEventHandler(UpdateClipBoardInfo), new object[] { info });
            }
            else
            {
                memoClipBoard.Text += info+"\r\n";
            }
        }

        /// <summary>
        /// 订阅事件
        /// </summary>
        private void OnInformCompleted(string arvID)
        {
            if (InformCompleted != null)
            {
                InformCompleted(arvID);
            }
        }

        #endregion

        #region 控制操作

        private void btnOpenDoor_Click(object sender, EventArgs e)
        {
            //// 检查回转库编号的合理性
            //if (string.IsNullOrEmpty(lblGroupNo.Text) || (Convert.ToInt32(lblGroupNo.Text) == 0))
            //{
            //    MessageUtil.ShowWarning("回转库编号没有正确配置，请检查设置！");
            //    return;
            //}

            //// 执行开门指令
            //try
            //{
            //    Controller.OpenDoor_Execute(Convert.ToInt32(lblGroupNo.Text));
            //}
            //catch (Exception ex)
            //{
            //    MessageUtil.ShowWarning(ex.Message);
            //    return;
            //}

            //// 在信息窗口显示信息
            //UpdateClipBoardInfo("提示:开始执行开门操作!");

            //// 延时500ms启动监控
            //System.Threading.Thread.Sleep(MONITOR_DELAY_TIME);

            //// 开门指令
            //devMonitor.CtrlCommand = HZK_COMMAND.CMD_OPENDOOR;
            //// 节点号
            //devMonitor.GroupNo = Convert.ToInt32(lblGroupNo.Text);
            //// 启动监控
            //devMonitor.StartDevMonitor();
        }

        /// <summary>
        /// 关门
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCloseDoor_Click(object sender, EventArgs e)
        {
            //// 检查回转库编号的合理性
            //if (string.IsNullOrEmpty(lblGroupNo.Text) || (Convert.ToInt32(lblGroupNo.Text) == 0))
            //{
            //    MessageUtil.ShowWarning("回转库编号没有正确配置，请检查设置！");
            //    return;
            //}

            //// 执行关门指令
            //try
            //{
            //    Controller.CloseDoor_Execute(Convert.ToInt32(lblGroupNo.Text));
            //}
            //catch (Exception ex)
            //{
            //    MessageUtil.ShowWarning(ex.Message);
            //    return;
            //}

            //// 在信息窗口显示信息
            //UpdateClipBoardInfo("提示:开始执行关门操作!");

            //// 延时500ms启动监控
            //System.Threading.Thread.Sleep(MONITOR_DELAY_TIME);

            //// 关门指令
            //devMonitor.CtrlCommand = HZK_COMMAND.CMD_CLOSEDOOR;
            //// 节点号
            //devMonitor.GroupNo = Convert.ToInt32(lblGroupNo.Text);
            //// 启动监控
            //devMonitor.StartDevMonitor();
        }

        /// <summary>
        /// 运行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRun_Click(object sender, EventArgs e)
        {
            //// 检查回转库编号的合理性
            //if (string.IsNullOrEmpty(lblGroupNo.Text) || (string.IsNullOrEmpty(lblLayerNo.Text)))
            //{
            //    MessageUtil.ShowWarning("回转库存放位置信息没有正确配置，请检查！");
            //    return;
            //}

            //// 执行走层指令
            //int cabNo = Convert.ToInt32(lblGroupNo.Text);
            //int dstlayerNo = Convert.ToInt32(lblLayerNo.Text);

            //try
            //{
            //    // 执行回转库操作
            //    Controller.Run_Execute(cabNo, dstlayerNo);
            //}
            //catch (Exception ex)
            //{
            //    MessageUtil.ShowWarning(ex.Message);
            //    return;
            //}

            //// 在信息窗口显示信息
            //UpdateClipBoardInfo(String.Format("提示:{0}号回转库开始执行走层操作，目标层:第{1}层!", cabNo, dstlayerNo));

            //// 延时500ms启动监控
            //System.Threading.Thread.Sleep(MONITOR_DELAY_TIME);

            //// 走层指令
            //devMonitor.CtrlCommand = HZK_COMMAND.CMD_EXECUTE;
            //// 节点号
            //devMonitor.GroupNo = Convert.ToInt32(lblGroupNo.Text);
            //// 目的层
            //devMonitor.DstLayerNo = dstlayerNo;
            //// 启动监控
            //devMonitor.StartDevMonitor();
        }

        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, EventArgs e)
        {
            //// 检查回转库编号的合理性
            //if (string.IsNullOrEmpty(lblGroupNo.Text) || (Convert.ToInt32(lblGroupNo.Text) == 0))
            //{
            //    MessageUtil.ShowWarning("回转库编号没有正确配置，请检查设置！");
            //    return;
            //}

            //// 执行停止指令
            //try
            //{
            //    Controller.Stop_Execute(Convert.ToInt32(lblGroupNo.Text));
            //}
            //catch (Exception ex)
            //{
            //    MessageUtil.ShowWarning(ex.Message);
            //    return;
            //}

            //// 在信息窗口显示信息
            //UpdateClipBoardInfo(String.Format("提示:{0}号回转库停止运行!", lblGroupNo.Text));
        }

        #endregion

        #region 数据库操作
        /// <summary>
        /// 执行入柜操作
        /// </summary>
        private void ArvInput()
        {
            bool succeed = true;
            for (int i = 0; i < gvIntoCabinet.RowCount; i++)
            //foreach (ArchiveInfoDto arv in ArchiveList)
            {
                try
                {
                    ArchiveInfoDto arv = (ArchiveInfoDto)gvIntoCabinet.GetRow(i);
                    // 更新档案当前状态
                    arv.ArvStatus = "在库";
                    //ArchiveBLL.Instance.Update(arv, arv.ArvID);
                    CallerFactory.Instance.GetService<IArvOpService>().UpdateArvInfo(arv);

                    // 更新档案存储位置(这里不再需要)

                    // “是否保存”设置为选中
                    //arvInfoTable.Rows[idx++]["是否保存"] = true;
                    gvIntoCabinet.SetRowCellValue(i, gvIntoCabinet.Columns["colCheck"], true);
                    OnInformCompleted(arv.ArvID);
                }
                catch (Exception e)
                {
                    succeed = false;
                    MessageUtil.ShowError(e.ToString());
                }
            }

            //ArchiveList.Clear();
           // if (succeed)
              //  UpdateClipBoardInfo((String.Format("提示:所有档案信息保存入{0}号回转库第{1}层!", lblGroupNo.Text, lblLayerNo.Text)));
        }

        ///// <summary>
        ///// 执行档案借阅操作（要修改为多表操作）
        ///// </summary>
        //private void ArvLend(int idx)
        //{
        //    try
        //    {
        //        // 将借阅人信息录入数据库
        //        LendBLL.Instance.Insert(lendInfoList[idx]);

        //        // 修改记录的状态
        //        ArchiveInfo arv = ArchiveBLL.Instance.FindByID(lendInfoList[idx].ID);
        //        arv.ArvStatus = "借出";
        //        // 更新档案数据库进行更新
        //        ArchiveBLL.Instance.Update(arv, arv.ArvID);

        //        // 保存状态设置为true
        //        arvInfoTable.Rows[idx]["是否保存"] = true;
        //        // 提示信息
        //        lsbInfoBoard.Items.Add(String.Format("提示:编号为{0}的档案借阅记录保存进数据库!", arv.ArvID));
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageUtil.ShowError(ex.ToString());
        //    }
        //}

        ///// <summary>
        ///// 执行档案归还操作
        ///// </summary>
        ///// <param name="idx"></param>
        //private void ArvReturn(int idx)
        //{
        //    try
        //    {
        //        // 更新借阅归还信息表
        //        LendBLL.Instance.Update(lendInfoList[idx], lendInfoList[idx].Guid);

        //        // 更新档案状态
        //        ArchiveInfo arv = ArchiveBLL.Instance.FindByID(lendInfoList[idx].ID);

        //        arv.ArvStatus = "在库";
        //        ArchiveBLL.Instance.Update(arv, arv.ArvID);

        //        // 保存状态设置为true
        //        arvInfoTable.Rows[idx]["是否保存"] = true;
        //        // 提示信息
        //        lsbInfoBoard.Items.Add(String.Format("提示:编号为{0}的档案归还记录保存进数据库!", arv.ArvID));
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageUtil.ShowError(ex.ToString());
        //    }
        //}

        ///// <summary>
        ///// 档案出库操作
        ///// </summary>
        ///// <param name="idx">检索号</param>
        //private void ArvOut(int idx)
        //{
        //    try
        //    {
        //        if (OutCabBLL.Instance.FindByID(outInfoList[idx].ArvID) == null)
        //        {
        //            // 将借阅人信息录入数据库
        //            OutCabBLL.Instance.Insert(outInfoList[idx]);
        //        }
        //        else
        //        {//有可能有档案出库后再次入库出库，就会导致插入重复记录出错
        //            OutCabBLL.Instance.Update(outInfoList[idx], outInfoList[idx].ArvID);
        //        }


        //        // 修改记录的状态
        //        ArchiveInfo arv = ArchiveBLL.Instance.FindByID(outInfoList[idx].ArvID);

        //        if (arv != null)
        //        {
        //            arv.ArvStatus = "在档";
        //            // 更新档案数据库进行更新
        //            ArchiveBLL.Instance.Update(arv, arv.ArvID);
        //            if (ArvLocationBLL.Instance.FindByID(arv.ArvID) != null)
        //            {
        //                // 删除存储位置信息
        //                ArvLocationBLL.Instance.Delete(arv.ArvID);
        //            }
        //        }
        //        // 保存状态设置为true
        //        arvInfoTable.Rows[idx]["是否保存"] = true;
        //        // 提示信息
        //        lsbInfoBoard.Items.Add(String.Format("提示:编号为{0}的档案出库记录保存进数据库!", arv.ArvID));
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageUtil.ShowError(ex.ToString());
        //    }
        //}
        #endregion

        private void FrmDVDeviceCtrl_Load(object sender, EventArgs e)
        {
            InitDeviceControl();
            gcArvOperate.DataSource = ArchiveList;
            AddCheckBoxColumn();
            GridControlHelper.Instance.ConfigGridViewColumns(gvIntoCabinet);//配置列名及可见性
            InitCheckInfo();//根据窗体tag确定登记信息
            GetLocation((ArchiveInfoDto)gvIntoCabinet.GetRow(0));//更新位置标签
            memoClipBoard.ReadOnly = true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // 当前选中行
            int selectedIdx = gvIntoCabinet.GetSelectedRows()[0];
            object state = gvIntoCabinet.GetRowCellValue(gvIntoCabinet.FocusedRowHandle, "colCheck");
            if(state!=null&&(bool)state)
            //if ((bool)gvIntoCabinet.GetFocusedRowCellValue("colCheck"))
            //if ((bool)arvInfoTable.Rows[selectedIdx]["是否保存"])
            {
                MessageUtil.ShowWarning("当前记录已保存！");
                return;
            }

            switch (this.Tag.ToString())
            {
                case "Input":   // 档案录入界面的操作
                    ArvInput();

                    break;

                case "Lend":    // 档案借阅界面的操作
                    //ArvLend(selectedIdx);

                    break;

                case "Return":  // 档案归还界面的操作
                    //ArvReturn(selectedIdx);

                    break;

                case "Out":     // 档案出库界面的操作
                    //ArvOut(selectedIdx);

                    break;
                default:
                    break;
            }

            // “档案录入”界面的“入柜”操作，不需要处理
            if (this.Tag.ToString() != "Input")
            {
                // 操作完成事件
                //OnInformCompleted(arvInfoTable.Rows[selectedIdx][0].ToString());
                OnInformCompleted(((ArchiveInfoDto)gvIntoCabinet.GetFocusedRow()).ArvID);

                // 聚焦第一个未保存的记录
                //DataRow row = arvInfoTable.AsEnumerable().FirstOrDefault(q => !q.Field<bool>("是否保存"));
                //if (row != null)
                //{
                //    gvIntoCabinet.FocusedRowHandle = arvInfoTable.Rows.IndexOf(row);
                //}
                for(int i=0;i<gvIntoCabinet.RowCount;i++)
                {
                    if(!(bool)gvIntoCabinet.GetRowCellValue(i, "colCheck"))
                    {
                        gvIntoCabinet.FocusedRowHandle = i;
                    }
                }
            }
        }

    }
}
