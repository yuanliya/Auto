namespace AutoCabinet2017.UI.OP
{
    partial class FormOPArvCheck
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormOPArvCheck));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gcError = new DevExpress.XtraGrid.GridControl();
            this.gvError = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.gcCabinetStatus = new DevExpress.XtraGrid.GridControl();
            this.cvCabinetStatus = new DevExpress.XtraGrid.Views.Card.CardView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lsbInfoBoard = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.gcCorrected = new DevExpress.XtraGrid.GridControl();
            this.gvCorrected = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.toolConfig = new DevExpress.XtraBars.BarButtonItem();
            this.toolStart = new DevExpress.XtraBars.BarButtonItem();
            this.toolUpdateDB = new DevExpress.XtraBars.BarButtonItem();
            this.toolStop = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvError)).BeginInit();
            this.groupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcCabinetStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cvCabinetStatus)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcCorrected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCorrected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.LightBlue;
            this.groupBox1.Controls.Add(this.gcError);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(423, 303);
            this.groupBox1.TabIndex = 62;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "待删记录";
            // 
            // gcError
            // 
            this.gcError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcError.Location = new System.Drawing.Point(3, 17);
            this.gcError.MainView = this.gvError;
            this.gcError.Name = "gcError";
            this.gcError.Size = new System.Drawing.Size(417, 283);
            this.gcError.TabIndex = 47;
            this.gcError.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvError});
            // 
            // gvError
            // 
            this.gvError.GridControl = this.gcError;
            this.gvError.Name = "gvError";
            this.gvError.OptionsBehavior.Editable = false;
            // 
            // groupBox
            // 
            this.groupBox.BackColor = System.Drawing.Color.LightBlue;
            this.groupBox.Controls.Add(this.gcCabinetStatus);
            this.groupBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox.ForeColor = System.Drawing.Color.White;
            this.groupBox.Location = new System.Drawing.Point(307, 0);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(275, 577);
            this.groupBox.TabIndex = 61;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "盘库状态";
            // 
            // gcCabinetStatus
            // 
            this.gcCabinetStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcCabinetStatus.Location = new System.Drawing.Point(3, 17);
            this.gcCabinetStatus.MainView = this.cvCabinetStatus;
            this.gcCabinetStatus.Name = "gcCabinetStatus";
            this.gcCabinetStatus.Size = new System.Drawing.Size(269, 557);
            this.gcCabinetStatus.TabIndex = 47;
            this.gcCabinetStatus.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.cvCabinetStatus});
            // 
            // cvCabinetStatus
            // 
            this.cvCabinetStatus.FocusedCardTopFieldIndex = 0;
            this.cvCabinetStatus.GridControl = this.gcCabinetStatus;
            this.cvCabinetStatus.Name = "cvCabinetStatus";
            this.cvCabinetStatus.OptionsBehavior.Editable = false;
            this.cvCabinetStatus.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.LightBlue;
            this.groupBox2.Controls.Add(this.lsbInfoBoard);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(307, 577);
            this.groupBox2.TabIndex = 60;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "系统信息栏";
            // 
            // lsbInfoBoard
            // 
            this.lsbInfoBoard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsbInfoBoard.FormattingEnabled = true;
            this.lsbInfoBoard.ItemHeight = 12;
            this.lsbInfoBoard.Location = new System.Drawing.Point(3, 17);
            this.lsbInfoBoard.Name = "lsbInfoBoard";
            this.lsbInfoBoard.Size = new System.Drawing.Size(301, 557);
            this.lsbInfoBoard.TabIndex = 49;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.LightBlue;
            this.groupBox3.Controls.Add(this.gcCorrected);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            this.groupBox3.Location = new System.Drawing.Point(0, 303);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(423, 274);
            this.groupBox3.TabIndex = 64;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "待增记录";
            // 
            // gcCorrected
            // 
            this.gcCorrected.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcCorrected.Location = new System.Drawing.Point(3, 17);
            this.gcCorrected.MainView = this.gvCorrected;
            this.gcCorrected.Name = "gcCorrected";
            this.gcCorrected.Size = new System.Drawing.Size(417, 254);
            this.gcCorrected.TabIndex = 47;
            this.gcCorrected.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCorrected});
            // 
            // gvCorrected
            // 
            this.gvCorrected.GridControl = this.gcCorrected;
            this.gvCorrected.Name = "gvCorrected";
            this.gvCorrected.OptionsBehavior.Editable = false;
            this.gvCorrected.OptionsView.ColumnAutoWidth = false;
            this.gvCorrected.OptionsView.ShowGroupPanel = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 31);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox);
            this.splitContainer1.Panel1.Margin = new System.Windows.Forms.Padding(20);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer1.Size = new System.Drawing.Size(1009, 577);
            this.splitContainer1.SplitterDistance = 582;
            this.splitContainer1.TabIndex = 65;
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.toolConfig,
            this.toolStart,
            this.toolUpdateDB,
            this.toolStop});
            this.barManager1.MaxItemId = 4;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.toolConfig, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.toolStart, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.toolUpdateDB, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.toolStop, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.Text = "Tools";
            // 
            // toolConfig
            // 
            this.toolConfig.Caption = "配置";
            this.toolConfig.Glyph = ((System.Drawing.Image)(resources.GetObject("toolConfig.Glyph")));
            this.toolConfig.Id = 0;
            this.toolConfig.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("toolConfig.LargeGlyph")));
            this.toolConfig.Name = "toolConfig";
            this.toolConfig.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.toolConfig_ItemClick);
            // 
            // toolStart
            // 
            this.toolStart.Caption = "启动";
            this.toolStart.Glyph = ((System.Drawing.Image)(resources.GetObject("toolStart.Glyph")));
            this.toolStart.Id = 1;
            this.toolStart.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("toolStart.LargeGlyph")));
            this.toolStart.Name = "toolStart";
            this.toolStart.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.toolStart_ItemClick);
            // 
            // toolUpdateDB
            // 
            this.toolUpdateDB.Caption = "更新数据库";
            this.toolUpdateDB.Glyph = ((System.Drawing.Image)(resources.GetObject("toolUpdateDB.Glyph")));
            this.toolUpdateDB.Id = 2;
            this.toolUpdateDB.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("toolUpdateDB.LargeGlyph")));
            this.toolUpdateDB.Name = "toolUpdateDB";
            this.toolUpdateDB.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.toolUpdateDB_ItemClick);
            // 
            // toolStop
            // 
            this.toolStop.Caption = "停止";
            this.toolStop.Glyph = ((System.Drawing.Image)(resources.GetObject("toolStop.Glyph")));
            this.toolStop.Id = 3;
            this.toolStop.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("toolStop.LargeGlyph")));
            this.toolStop.Name = "toolStop";
            this.toolStop.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.toolStop_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1009, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 608);
            this.barDockControlBottom.Size = new System.Drawing.Size(1009, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 577);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1009, 31);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 577);
            // 
            // FormOPArvCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1009, 608);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormOPArvCheck";
            this.Text = "FormOPArvCheck";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormOPArvCheck_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormOPArvCheck_FormClosed);
            this.Load += new System.EventHandler(this.FormOPArvCheck_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvError)).EndInit();
            this.groupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcCabinetStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cvCabinetStatus)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcCorrected)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCorrected)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraGrid.GridControl gcError;
        private System.Windows.Forms.GroupBox groupBox;
        private DevExpress.XtraGrid.GridControl gcCabinetStatus;
        private DevExpress.XtraGrid.Views.Card.CardView cvCabinetStatus;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox lsbInfoBoard;
        private System.Windows.Forms.GroupBox groupBox3;
        private DevExpress.XtraGrid.GridControl gcCorrected;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCorrected;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraGrid.Views.Grid.GridView gvError;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem toolConfig;
        private DevExpress.XtraBars.BarButtonItem toolStart;
        private DevExpress.XtraBars.BarButtonItem toolUpdateDB;
        private DevExpress.XtraBars.BarButtonItem toolStop;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
    }
}