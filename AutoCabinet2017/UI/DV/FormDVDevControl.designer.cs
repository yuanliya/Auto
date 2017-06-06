namespace HZK.UI.DV
{
    partial class FormDVDevControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDVDevControl));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label8 = new System.Windows.Forms.Label();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnVentilation = new System.Windows.Forms.Button();
            this.btnUnLock = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labHumUp = new System.Windows.Forms.Label();
            this.labTempUp = new System.Windows.Forms.Label();
            this.labRunDetect = new System.Windows.Forms.Label();
            this.labProDetect = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTemp = new System.Windows.Forms.TextBox();
            this.txtHum = new System.Windows.Forms.TextBox();
            this.cbxGroupNo = new System.Windows.Forms.ComboBox();
            this.cbxColumnNo = new System.Windows.Forms.ComboBox();
            this.pbLock = new System.Windows.Forms.PictureBox();
            this.pbRun = new System.Windows.Forms.PictureBox();
            this.pbTempAlarm = new System.Windows.Forms.PictureBox();
            this.pbHumityAlarm = new System.Windows.Forms.PictureBox();
            this.lsbInfoBoard = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbLock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRun)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTempAlarm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHumityAlarm)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(558, 150);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 12);
            this.label8.TabIndex = 38;
            this.label8.Text = "。";
            // 
            // btnOpen
            // 
            this.btnOpen.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOpen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOpen.Location = new System.Drawing.Point(366, 352);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 39);
            this.btnOpen.TabIndex = 97;
            this.btnOpen.Text = "打开";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnVentilation
            // 
            this.btnVentilation.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnVentilation.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVentilation.Location = new System.Drawing.Point(649, 352);
            this.btnVentilation.Name = "btnVentilation";
            this.btnVentilation.Size = new System.Drawing.Size(75, 39);
            this.btnVentilation.TabIndex = 96;
            this.btnVentilation.Text = "通风";
            this.btnVentilation.UseVisualStyleBackColor = true;
            this.btnVentilation.Click += new System.EventHandler(this.btnVentilation_Click);
            // 
            // btnUnLock
            // 
            this.btnUnLock.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnUnLock.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUnLock.Location = new System.Drawing.Point(742, 352);
            this.btnUnLock.Name = "btnUnLock";
            this.btnUnLock.Size = new System.Drawing.Size(77, 39);
            this.btnUnLock.TabIndex = 95;
            this.btnUnLock.Text = "解锁";
            this.btnUnLock.UseVisualStyleBackColor = true;
            this.btnUnLock.Click += new System.EventHandler(this.btnUnLock_Click);
            // 
            // btnStop
            // 
            this.btnStop.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStop.Location = new System.Drawing.Point(555, 352);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(76, 39);
            this.btnStop.TabIndex = 94;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(459, 352);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(78, 39);
            this.btnClose.TabIndex = 93;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label17.Location = new System.Drawing.Point(826, 162);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(28, 29);
            this.label17.TabIndex = 104;
            this.label17.Text = "%";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.Location = new System.Drawing.Point(617, 163);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(75, 38);
            this.label15.TabIndex = 103;
            this.label15.Text = "湿度";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(562, 162);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(36, 38);
            this.label9.TabIndex = 102;
            this.label9.Text = "C";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(362, 162);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 38);
            this.label4.TabIndex = 101;
            this.label4.Text = "温度";
            // 
            // labHumUp
            // 
            this.labHumUp.AutoSize = true;
            this.labHumUp.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labHumUp.ForeColor = System.Drawing.Color.Lime;
            this.labHumUp.Location = new System.Drawing.Point(750, 317);
            this.labHumUp.Name = "labHumUp";
            this.labHumUp.Size = new System.Drawing.Size(89, 19);
            this.labHumUp.TabIndex = 108;
            this.labHumUp.Text = "湿度越界";
            // 
            // labTempUp
            // 
            this.labTempUp.AutoSize = true;
            this.labTempUp.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labTempUp.ForeColor = System.Drawing.Color.Lime;
            this.labTempUp.Location = new System.Drawing.Point(624, 317);
            this.labTempUp.Name = "labTempUp";
            this.labTempUp.Size = new System.Drawing.Size(89, 19);
            this.labTempUp.TabIndex = 107;
            this.labTempUp.Text = "温度越界";
            // 
            // labRunDetect
            // 
            this.labRunDetect.AutoSize = true;
            this.labRunDetect.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labRunDetect.ForeColor = System.Drawing.Color.Lime;
            this.labRunDetect.Location = new System.Drawing.Point(499, 317);
            this.labRunDetect.Name = "labRunDetect";
            this.labRunDetect.Size = new System.Drawing.Size(60, 19);
            this.labRunDetect.TabIndex = 106;
            this.labRunDetect.Text = "运 行";
            // 
            // labProDetect
            // 
            this.labProDetect.AutoSize = true;
            this.labProDetect.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labProDetect.ForeColor = System.Drawing.Color.Lime;
            this.labProDetect.Location = new System.Drawing.Point(365, 317);
            this.labProDetect.Name = "labProDetect";
            this.labProDetect.Size = new System.Drawing.Size(60, 19);
            this.labProDetect.TabIndex = 105;
            this.labProDetect.Text = "保 护";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(359, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 38);
            this.label1.TabIndex = 115;
            this.label1.Text = "区号";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(619, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 38);
            this.label2.TabIndex = 117;
            this.label2.Text = "列号";
            // 
            // txtTemp
            // 
            this.txtTemp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTemp.Font = new System.Drawing.Font("微软雅黑", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTemp.Location = new System.Drawing.Point(440, 140);
            this.txtTemp.Name = "txtTemp";
            this.txtTemp.Size = new System.Drawing.Size(112, 71);
            this.txtTemp.TabIndex = 121;
            this.txtTemp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtHum
            // 
            this.txtHum.BackColor = System.Drawing.SystemColors.Window;
            this.txtHum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHum.Font = new System.Drawing.Font("微软雅黑", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtHum.Location = new System.Drawing.Point(708, 140);
            this.txtHum.Name = "txtHum";
            this.txtHum.Size = new System.Drawing.Size(112, 71);
            this.txtHum.TabIndex = 122;
            this.txtHum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cbxGroupNo
            // 
            this.cbxGroupNo.Font = new System.Drawing.Font("微软雅黑", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbxGroupNo.FormattingEnabled = true;
            this.cbxGroupNo.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.cbxGroupNo.Location = new System.Drawing.Point(440, 34);
            this.cbxGroupNo.Name = "cbxGroupNo";
            this.cbxGroupNo.Size = new System.Drawing.Size(112, 70);
            this.cbxGroupNo.TabIndex = 123;
            this.cbxGroupNo.SelectedIndexChanged += new System.EventHandler(this.cbxGroupNo_SelectedIndexChanged);
            // 
            // cbxColumnNo
            // 
            this.cbxColumnNo.Font = new System.Drawing.Font("微软雅黑", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbxColumnNo.FormattingEnabled = true;
            this.cbxColumnNo.Location = new System.Drawing.Point(708, 33);
            this.cbxColumnNo.Name = "cbxColumnNo";
            this.cbxColumnNo.Size = new System.Drawing.Size(112, 70);
            this.cbxColumnNo.TabIndex = 124;
            this.cbxColumnNo.SelectedIndexChanged += new System.EventHandler(this.cbxColumnNo_SelectedIndexChanged);
            // 
            // pbLock
            // 
            this.pbLock.Image = ((System.Drawing.Image)(resources.GetObject("pbLock.Image")));
            this.pbLock.Location = new System.Drawing.Point(369, 253);
            this.pbLock.Name = "pbLock";
            this.pbLock.Size = new System.Drawing.Size(51, 50);
            this.pbLock.TabIndex = 125;
            this.pbLock.TabStop = false;
            // 
            // pbRun
            // 
            this.pbRun.Image = ((System.Drawing.Image)(resources.GetObject("pbRun.Image")));
            this.pbRun.Location = new System.Drawing.Point(503, 253);
            this.pbRun.Name = "pbRun";
            this.pbRun.Size = new System.Drawing.Size(51, 50);
            this.pbRun.TabIndex = 126;
            this.pbRun.TabStop = false;
            // 
            // pbTempAlarm
            // 
            this.pbTempAlarm.Image = ((System.Drawing.Image)(resources.GetObject("pbTempAlarm.Image")));
            this.pbTempAlarm.Location = new System.Drawing.Point(637, 253);
            this.pbTempAlarm.Name = "pbTempAlarm";
            this.pbTempAlarm.Size = new System.Drawing.Size(51, 50);
            this.pbTempAlarm.TabIndex = 127;
            this.pbTempAlarm.TabStop = false;
            // 
            // pbHumityAlarm
            // 
            this.pbHumityAlarm.Image = ((System.Drawing.Image)(resources.GetObject("pbHumityAlarm.Image")));
            this.pbHumityAlarm.Location = new System.Drawing.Point(771, 253);
            this.pbHumityAlarm.Name = "pbHumityAlarm";
            this.pbHumityAlarm.Size = new System.Drawing.Size(51, 50);
            this.pbHumityAlarm.TabIndex = 128;
            this.pbHumityAlarm.TabStop = false;
            // 
            // lsbInfoBoard
            // 
            this.lsbInfoBoard.FormattingEnabled = true;
            this.lsbInfoBoard.ItemHeight = 12;
            this.lsbInfoBoard.Location = new System.Drawing.Point(9, 20);
            this.lsbInfoBoard.Name = "lsbInfoBoard";
            this.lsbInfoBoard.Size = new System.Drawing.Size(325, 352);
            this.lsbInfoBoard.TabIndex = 129;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lsbInfoBoard);
            this.groupBox1.Location = new System.Drawing.Point(3, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(340, 386);
            this.groupBox1.TabIndex = 130;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "信息栏";
            // 
            // FormDVDevControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 399);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pbHumityAlarm);
            this.Controls.Add(this.pbTempAlarm);
            this.Controls.Add(this.pbRun);
            this.Controls.Add(this.pbLock);
            this.Controls.Add(this.cbxColumnNo);
            this.Controls.Add(this.cbxGroupNo);
            this.Controls.Add(this.txtHum);
            this.Controls.Add(this.txtTemp);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labHumUp);
            this.Controls.Add(this.labTempUp);
            this.Controls.Add(this.labRunDetect);
            this.Controls.Add(this.labProDetect);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.btnVentilation);
            this.Controls.Add(this.btnUnLock);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnClose);
            this.MaximizeBox = false;
            this.Name = "FormDVDevControl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "密集架控制面板";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ControlFrm_FormClosing);
            this.Load += new System.EventHandler(this.FormDVDevControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbLock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRun)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTempAlarm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHumityAlarm)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnVentilation;
        private System.Windows.Forms.Button btnUnLock;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labHumUp;
        private System.Windows.Forms.Label labTempUp;
        private System.Windows.Forms.Label labRunDetect;
        private System.Windows.Forms.Label labProDetect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTemp;
        private System.Windows.Forms.TextBox txtHum;
        private System.Windows.Forms.ComboBox cbxGroupNo;
        private System.Windows.Forms.ComboBox cbxColumnNo;
        private System.Windows.Forms.PictureBox pbLock;
        private System.Windows.Forms.PictureBox pbRun;
        private System.Windows.Forms.PictureBox pbTempAlarm;
        private System.Windows.Forms.PictureBox pbHumityAlarm;
        private System.Windows.Forms.ListBox lsbInfoBoard;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}