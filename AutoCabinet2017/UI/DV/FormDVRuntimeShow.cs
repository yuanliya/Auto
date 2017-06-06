using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HZK.UI.DV
{
    public partial class FormDVRuntimeShow : Form
    {
        // 设备总层数  
        private int totalLayers = 12;
        // 设备当前层
        private int currentLayer = 1;

        // 采用集合动态分配LED控件
        List<PictureBox> picLed = new List<PictureBox>();
        // 指示灯
        Image imgRed   = Image.FromFile(Application.StartupPath + @"\Images\DV\RedLED.png");
        Image imgGreen = Image.FromFile(Application.StartupPath + @"\Images\DV\GreenLED.png"); 

        public FormDVRuntimeShow(int totalLayers, int currentLayer)
        {
            InitializeComponent();

            this.totalLayers  = totalLayers;
            this.currentLayer = currentLayer;
        }

        /// <summary>
        /// 设置界面的回转库编号
        /// </summary>
        /// <param name="devNo">设备编号</param>
        public void SetDeviceNo(int devNo)
        {
            lblDevNo.Text = devNo.ToString();
        }

        /// <summary>
        /// 设置界面的目的层编号
        /// </summary>
        /// <param name="layerNo">目的层编号</param>
        public void SetDstLayerNo(int layerNo)
        {
            lblDstLayer.Text = layerNo.ToString();
        }

        /// <summary>
        /// 更新信息栏的信息
        /// </summary>
        /// <param name="msg">信息字符串</param>
        public void UpdateClipboard(string msg)
        {
            lsbClipboard.Items.Add(msg);
        }

        public void DrawLED()
        {
            picLed.Clear();
            pbLeds.Controls.Clear();

            for (int i = 0; i < totalLayers; i++)
            {
                // 建立一个LED灯对象
                PictureBox pic = new PictureBox();
                
                pic.BackgroundImageLayout = ImageLayout.Center;
                pic.Width  = 40;
                pic.Height = 40;
                pic.Image  = imgRed;

                // 计算当前层的LED在阵列中的位置
                int width  = pbLeds.Width;
                int height = pbLeds.Height;
                pic.Location = new Point((width - totalLayers * pic.Width) / (totalLayers + 1) * (i + 1) + i * pic.Width, (height - pic.Height) / 2 - 10);

                // 控件加入管理队列
                pbLeds.Controls.Add(pic);
                picLed.Add(pic);

                // LED灯对应的文字
                Label lb = new Label();
                lb.Text = (i + 1).ToString();
                lb.Width  = 40;
                lb.Height = 40;
                lb.Location = new Point((width - totalLayers * pic.Width) / (totalLayers + 1) * (i + 1) + i * pic.Width + 10, (height - pic.Height) / 2 + 30);

                pbLeds.Controls.Add(lb);
            }

            // 当前层指示为绿灯
            picLed[currentLayer - 1].Image = imgGreen;
        }

        public void UpdateLEDs(int preLayerNo, int nowLayerNo)
        {
            //将当前层的小灯点亮，将之前的小灯颜色恢复
            picLed[preLayerNo - 1].Image = imgRed;
            picLed[nowLayerNo - 1].Image = imgGreen;
        }

        private void FormDVRuntimeShow_Load(object sender, EventArgs e)
        {
            // 绘制默认指示灯状态
            DrawLED();
        }
    }
}
