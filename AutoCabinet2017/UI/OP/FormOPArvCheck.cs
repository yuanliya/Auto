using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;

using DevExpress.XtraBars;


//using NJUST.AUTO06.Utility;
//using NJUST.AUTO06.NetService;
//using NJUST.AUTO06.Modbus.Device;
//using NJUST.AUTO06.Modbus.Comm;
//using NJUST.AUTO06.Modbus.Protocal;

namespace AutoCabinet2017.UI.OP
{
    public partial class FormOPArvCheck : Form
    {
        public FormOPArvCheck()
        {
            InitializeComponent();
        }

        //private AndroidDevice device;
        //private JavaScriptSerializer serializer = new JavaScriptSerializer();
        //private AndroidProtocal protocal = new AndroidProtocal();

        private Dictionary<int, IPEndPoint> ipepDict  = new Dictionary<int, IPEndPoint>();  // 存储设备编号和对应IP地址信息的字典
        private Dictionary<int, List<int>> polledDict = new Dictionary<int, List<int>>();   // 存储某设备已经盘过的层集合的字典

        private List<int> checkedGroup = new List<int>();     // 一次轮询后已经完成盘库的柜号
        //private List<Cabinet> cabinets = new List<Cabinet>(); // 待盘库的回转库
       
        //private List<ArvLocation> failureLocs   = new List<ArvLocation>(); // 错误位置
        //private List<ArvLocation> correctedLocs = new List<ArvLocation>(); // 待更正的位置

        private DataTable StatusTable;    // 盘库状态表
        private bool      isStop = false; // 是否停止监控线程

        /// <summary>
        /// 生成状态表，用于显示盘库的实时状态
        /// </summary>
        /// <returns></returns>
        private DataTable CreateStatusDatatable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("柜号");
            dt.Columns.Add("状态");
            dt.Columns.Add("已盘层数");
            dt.Columns.Add("当前层");
            dt.Columns.Add("层档案数");

            return dt;
        }

        /// <summary>
        /// 初始化盘库相关的信息
        /// </summary>
        private void InitDeviceInfoAndStatus()
        {
            //ipepDict.Clear();
            //polledDict.Clear();
            //StatusTable.Rows.Clear();

            //foreach (Cabinet cab in cabinets)
            //{
            //    // 所有设备的IP地址和端口信息
            //    String[] socket = cab.HmiIPAddr.Split(':');
            //    IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(socket[0]), Convert.ToInt32(socket[1]));

            //    ipepDict.Add(cab.CabinetNo, ipep);              // 设备对应的IP地址
            //    polledDict.Add(cab.CabinetNo, new List<int>()); // 设备已盘过的层
            //    // 初始化状态表的行
            //    StatusTable.Rows.Add(new object[] { cab.CabinetNo, "待盘库", 0, 0, 0 });
            //}
        }

        /// <summary>
        /// 加载主界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormOPArvCheck_Load(object sender, EventArgs e)
        {
            //// 设备信息
            //cabinets = CabinetBLL.Instance.Find("*", "");
            //// 创建盘库实时状态表
            //StatusTable = CreateStatusDatatable();
            //// 初始化盘库信息
            //InitDeviceInfoAndStatus();

            //// 待删记录表
            //gcError.DataSource = failureLocs;
            //GridControlHelper.Instance.ConfigGridViewColumns(gvError, PropertyHelper.ArvFieldCfgItems);
            //// 待修正记录表
            //gcCorrected.DataSource = correctedLocs;
            //GridControlHelper.Instance.ConfigGridViewColumns(gvCorrected, PropertyHelper.ArvFieldCfgItems);
            //// 设备状态表
            //gcCabinetStatus.DataSource = StatusTable;

            //// 设置设备状态表格的CardView格式
            //cvCabinetStatus.CardInterval = 3;
            //if (cvCabinetStatus.RowCount < 7)
            //{
            //    cvCabinetStatus.OptionsBehavior.AutoHorzWidth = true; // 只有一列时，宽度自适应
            //}
            //else
            //{
            //    cvCabinetStatus.CardWidth = 110;
            //}

            //// 设置与安卓Pad通信的实例的事件
            //try
            //{
            //    device = new AndroidDevice();    // 设备实例

            //    // 接收数据帧事件
            //    device.SetOnReceived(new EventHandler<IOEventArgs>(OnReceive));
            //    // 错误处理事件
            //    device.SetOnError(new EventHandler<ExceptEventArgs>(OnError));
            //}
            //catch (Exception)
            //{
            //    device = null;
            //    MessageUtil.ShowError("与设备连接失败,请重新连接！");
            //}
        }

        #region 跨线程UI操作

        /// <summary>
        /// 更新盘库状态表
        /// </summary>
        /// <param name="filter">条件</param>
        /// <param name="currentLayer">层信息</param>
        /// <param name="arvNum">层档案信息</param>
        public delegate void UpdateStatusHandler(string filter,int currentLayer,int arvNum);
        public void UpdateStatus(string filter, int currentLayer,int arvNum)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new UpdateStatusHandler(UpdateStatus),new object[]{filter,currentLayer,arvNum});
            }
            else
            {
                DataRow row = StatusTable.Select(filter)[0];

                row["状态"]     = "盘库中";
                row["已盘层数"] = Convert.ToInt16(row["已盘层数"]) + 1;
                row["当前层"]   = currentLayer;//更新状态表
                row["层档案数"] = arvNum;

                gcCorrected.RefreshDataSource();
                gcError.RefreshDataSource();
            }
        }

        /// <summary>
        /// 更新信息栏事件
        /// </summary>
        /// <param name="info"></param>
        public delegate void LogStatusEventHandler(string info);
        public void UpdateClipBoardInfo(string info)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new LogStatusEventHandler(UpdateClipBoardInfo), new object[] { info });
            }
            else
            {
                lsbInfoBoard.Items.Add(info);
            }
        }

        #endregion

        #region UDP操作事件处理

        /// <summary>
        /// 筛选出当前正确和错误操作的档案
        /// </summary>
        /// <param name="groupNo">柜号</param>
        /// <param name="layer">层号</param>
        /// <param name="currentLabels">层标签值</param>
        //private void CheckOperation(AndroidProtocal.CheckInfo checkInfo)
        //{
        //    // 档案所属的柜号和层号信息
        //    string groupNo   = checkInfo.CabinetNo.ToString();
        //    string layer     = checkInfo.LayerNo.ToString();
        //    String condition = String.Format("GroupNo='{0}' and LayerNo='{1}'", groupNo, layer);

        //    // 数据库中该柜该层存储的档案
        //    List<ArvLocation> originalLoc = ArvLocationBLL.Instance.Find("*", condition);
        //    // 读取到的所有档案标签
        //    List<string> currentLabels = checkInfo.Archives;

        //    for (int i = 0; i < currentLabels.Count; i++)
        //    {
        //        // 标签值为空
        //        if (currentLabels[i] == "00000000")
        //        {
        //            // 如果记录中该格不为空--有档案存储记录
        //            if (originalLoc.Count(q => q.CellNo == (i + 1).ToString()) > 0)
        //            {
        //                ArvLocation loc = originalLoc.Find(q => q.CellNo == (i + 1).ToString());
        //                failureLocs.Add(loc); // 该条档案位置记录错误，待删除
        //            }
        //        }
        //        else // 读到有效标签值
        //        {
        //            // 该格对应的档案信息
        //            ArvLocation loc = originalLoc.Find(q => q.CellNo == (i + 1).ToString());
        //            if (loc == null)
        //            {
        //                correctedLocs.Add(new ArvLocation
        //                {
        //                    ArvID = currentLabels[i],
        //                    GroupNo = groupNo,
        //                    LayerNo = layer,
        //                    CellNo = (i + 1).ToString()
        //                }); // 正确的信息，待增加
        //            }
        //            else if (loc.ArvID != currentLabels[i])// 档案名不符
        //            {
        //                failureLocs.Add(loc);

        //                correctedLocs.Add(new ArvLocation
        //                {
        //                    ArvID = currentLabels[i],
        //                    GroupNo = groupNo,
        //                    LayerNo = layer,
        //                    CellNo = (i + 1).ToString()
        //                }); // 正确的信息，待增加
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// 处理收到的盘库反馈信息
        /// </summary>
        /// <param name="checkInfo"></param>
        //private void OnNewStatus(AndroidProtocal.CheckInfo checkInfo)
        //{
        //    if (checkInfo.Status == "操作中" || checkInfo.Status == "操作完成")
        //    {
        //        if (!polledDict[checkInfo.CabinetNo].Contains(checkInfo.LayerNo)) // 如果还没盘该层
        //        {
        //            polledDict[checkInfo.CabinetNo].Add(checkInfo.LayerNo);       // 标记为已盘层

        //            // 更新盘库状态表，包括盘库状态、层号、档案数
        //            String filter = String.Format("柜号='{0}'", checkInfo.CabinetNo);
        //            UpdateStatus(filter, checkInfo.LayerNo, checkInfo.Archives.Count(q => q != "00000000"));

        //            // 档案标签校验
        //            CheckOperation(checkInfo);                   
        //        }
        //    }

        //    if (checkInfo.Status == "操作完成")
        //    {
        //        checkedGroup.Add(checkInfo.CabinetNo);
        //    }
        //}

        /// <summary>
        /// 收到盘库信息反馈的事件处理
        /// </summary>
        /// <param name="state"></param>
        //public void OnReceive(object sender, IOEventArgs e)
        //{
        //    String message = Encoding.UTF8.GetString(e.Dataframe, 0, e.Dataframe.Length);
        //    // 更新信息栏的信息
        //    UpdateClipBoardInfo(message);

        //    // 读取盘库反馈信息
        //    AndroidProtocal.OprationInfo info   = protocal.GetInfo(message);
        //    AndroidProtocal.CheckInfo checkInfo = (AndroidProtocal.CheckInfo)info;
            
        //    // 处理反馈信息
        //    OnNewStatus(checkInfo);
        //}

        /// <summary>
        /// 收到错误反馈的事件处理
        /// </summary>
        /// <param name="ex"></param>
        //private void OnError(object sender, ExceptEventArgs e)
        //{
        //    UpdateClipBoardInfo(e.FunctionName + e.Exception.ToString());
        //}

        #endregion

        /// <summary>
        /// 利用配置界面设置的设备信息重新初始化
        /// </summary>
        /// <param name="cabinets"></param>
        //void frmChoose_UpdateConfig(List<Cabinet> cabinets)
        //{
        //    this.cabinets = cabinets;
        //    InitDeviceInfoAndStatus();
        //}

        /// <summary>
        /// 线程--广播轮询设备状态
        /// </summary>
        private void BroadCastQuery()
        {
            // 提取本次操作的所有设备编号
            //List<int> cabs = cabinets.Select(q => q.CabinetNo).ToList();

            //while (!isStop)
            //{
            //    // 排除已完成盘库的设备
            //    List<int> newCabs = cabs.Except(checkedGroup).ToList();
            
            //    if (newCabs.Count == 0)
            //    {
            //        // 如果盘库全部完成，停止轮询
            //        isStop = true;
                    
            //        return;
            //    }
                
            //    // 查询未完成盘库的设备状态
            //    device.QueryArvStatus(newCabs);
            //    // 时间间隔3s
            //    Thread.Sleep(3000);
            //}
        }

        /// <summary>
        /// 判断盘点是否全部成功
        /// </summary>
        /// <returns></returns>
       // private DialogResult CheckFinished()
       // {
            //List<int> failedGroup = new List<int>();
           
            //foreach (Cabinet cab in cabinets)
            //{
            //    // 如果有没盘到的层，则认为该柜盘库未成功
            //    if (polledDict[cab.CabinetNo].Count != cab.CabinetLayers)
            //    {
            //        failedGroup.Add(cab.CabinetNo);
            //    }
            //}

            //return MessageUtil.ShowWarning(String.Format("{0}号回转库盘点失败!", string.Join(",", failedGroup.ToArray())));
      //  }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormOPArvCheck_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (!isStop)
            //{
            //    if (MessageUtil.ShowYesNoAndTips("操作未完成，确定要退出吗？") == DialogResult.Yes)
            //    {
            //        e.Cancel = false;
            //    }
            //    else
            //    {
            //        e.Cancel = true;
            //    }
            //}
        }

        private void FormOPArvCheck_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 这个执行不到！！！因为关掉tabpage时没有关闭窗口的
            //device.Disconnect();
        }

        #region 工具栏操作

        /// <summary>
        /// 盘库配置界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolConfig_ItemClick(object sender, ItemClickEventArgs e)
        {           
            //FormOPChoose frmChoose = new FormOPChoose();

            //frmChoose.UpdateConfig += new FormOPChoose.UpdateConfigHandler(frmChoose_UpdateConfig);
            //frmChoose.ShowDialog();  
        }

        /// <summary>
        /// 开始盘库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStart_ItemClick(object sender, ItemClickEventArgs e)
        {
            //if (device == null)
            //{
            //    try
            //    {
            //        device = new AndroidDevice();
            //        device.SetOnReceived(new EventHandler<IOEventArgs>(OnReceive));
            //        device.SetOnError(new EventHandler<ExceptEventArgs>(OnError));
            //    }
            //    catch (Exception)
            //    {
            //        device = null;
            //        MessageUtil.ShowError("与设备连接失败,请连接后重试！");
            //        return;
            //    }
            //}

            //isStop = false;

            //// 发送盘库指令，使用广播方式（这种情况下要读到所有返回帧应设置UDP读到数据后继续接收）
            //// 命令帧为“1,2,3（待盘的柜号）:盘库”，然后安卓屏判断自己是不是其中之一
            //device.SetRemotePoint(new IPEndPoint(IPAddress.Parse("192.168.100.255"), 8000)); // 广播地址
            //List<int> cabs = cabinets.Select(q => q.CabinetNo).ToList();
            //// 启动异步读取操作
            //device.Receive();
            //// 发送广播帧
            //device.CheckArvs(cabs);

            //Thread.Sleep(1000);

            //// 发送状态读取指令，使用轮询方式（也可广播）
            //Thread pollingThread = new Thread(new ThreadStart(BroadCastQuery));
            //pollingThread.Start();
        }

        /// <summary>
        /// 更新数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolUpdateDB_ItemClick(object sender, ItemClickEventArgs e)
        {
            //if (CheckFinished() == DialogResult.OK)
            //{
            //    // 删除错误记录
            //    foreach (ArvLocation loc in failureLocs)
            //    {
            //        ArvLocationBLL.Instance.Delete(loc.ArvID);
            //    }

            //    // 补充正确记录
            //    foreach (ArvLocation loc in correctedLocs)
            //    {
            //        ArvLocationBLL.Instance.Insert(loc);
            //    }

            //    MessageUtil.ShowTips("更新数据库成功！");
            //}
        }

        /// <summary>
        /// 停止盘库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStop_ItemClick(object sender, ItemClickEventArgs e)
        {
            isStop = true;
        }

        #endregion
    }
}
