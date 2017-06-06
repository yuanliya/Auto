namespace AutoCabinet2017.UI.RP
{
    partial class FormRPStatics
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRPStatics));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.toolStaticsType = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.toolExcuteStatics = new DevExpress.XtraBars.BarButtonItem();
            this.barStaticItem2 = new DevExpress.XtraBars.BarStaticItem();
            this.toolExport = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.panelCondition = new System.Windows.Forms.Panel();
            this.radioLendType = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.dtTo = new DevExpress.XtraEditors.DateEdit();
            this.dtFrom = new DevExpress.XtraEditors.DateEdit();
            this.radioTimeSpan = new DevExpress.XtraEditors.RadioGroup();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.gcStaticInfo = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.chartStaticView = new DevExpress.XtraCharts.ChartControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            this.panelCondition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioLendType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFrom.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioTimeSpan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcStaticInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartStaticView)).BeginInit();
            this.SuspendLayout();
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
            this.barStaticItem1,
            this.toolStaticsType,
            this.toolExcuteStatics,
            this.barStaticItem2,
            this.toolExport});
            this.barManager1.MaxItemId = 5;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemComboBox1});
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItem1),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, this.toolStaticsType, "", false, true, true, 106),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.toolExcuteStatics, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItem2),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.toolExport, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.Text = "Tools";
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Caption = "统计类别";
            this.barStaticItem1.Id = 0;
            this.barStaticItem1.Name = "barStaticItem1";
            this.barStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // toolStaticsType
            // 
            this.toolStaticsType.Caption = "barEditItem1";
            this.toolStaticsType.Edit = this.repositoryItemComboBox1;
            this.toolStaticsType.Id = 1;
            this.toolStaticsType.Name = "toolStaticsType";
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.AutoHeight = false;
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox1.Items.AddRange(new object[] {
            "入库统计",
            "出库统计",
            "借阅统计",
            "仓位统计"});
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            // 
            // toolExcuteStatics
            // 
            this.toolExcuteStatics.Caption = "执行统计";
            this.toolExcuteStatics.Glyph = ((System.Drawing.Image)(resources.GetObject("toolExcuteStatics.Glyph")));
            this.toolExcuteStatics.Id = 2;
            this.toolExcuteStatics.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("toolExcuteStatics.LargeGlyph")));
            this.toolExcuteStatics.Name = "toolExcuteStatics";
            this.toolExcuteStatics.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.toolExcuteStatics_ItemClick);
            // 
            // barStaticItem2
            // 
            this.barStaticItem2.Id = 3;
            this.barStaticItem2.Name = "barStaticItem2";
            this.barStaticItem2.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // toolExport
            // 
            this.toolExport.Caption = "导出";
            this.toolExport.Glyph = global::AutoCabinet2017.Properties.Resources.converttorange_16x16;
            this.toolExport.Id = 4;
            this.toolExport.Name = "toolExport";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.barDockControlTop.Size = new System.Drawing.Size(1456, 39);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 695);
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.barDockControlBottom.Size = new System.Drawing.Size(1456, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 39);
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 656);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1456, 39);
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 656);
            // 
            // panelCondition
            // 
            this.panelCondition.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panelCondition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCondition.Controls.Add(this.radioLendType);
            this.panelCondition.Controls.Add(this.labelControl2);
            this.panelCondition.Controls.Add(this.labelControl1);
            this.panelCondition.Controls.Add(this.dtTo);
            this.panelCondition.Controls.Add(this.dtFrom);
            this.panelCondition.Controls.Add(this.radioTimeSpan);
            this.panelCondition.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCondition.Location = new System.Drawing.Point(0, 39);
            this.panelCondition.Margin = new System.Windows.Forms.Padding(35, 5, 35, 5);
            this.panelCondition.Name = "panelCondition";
            this.panelCondition.Size = new System.Drawing.Size(1456, 76);
            this.panelCondition.TabIndex = 72;
            // 
            // radioLendType
            // 
            this.radioLendType.Location = new System.Drawing.Point(1072, 18);
            this.radioLendType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioLendType.MenuManager = this.barManager1;
            this.radioLendType.Name = "radioLendType";
            this.radioLendType.Properties.Appearance.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.radioLendType.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioLendType.Properties.Appearance.Options.UseBackColor = true;
            this.radioLendType.Properties.Appearance.Options.UseFont = true;
            this.radioLendType.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.radioLendType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("out", "借出未还"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("history", "借阅历史")});
            this.radioLendType.Size = new System.Drawing.Size(247, 41);
            this.radioLendType.TabIndex = 5;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(839, 29);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(20, 18);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "----";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(587, 29);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 18);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "时间区间";
            // 
            // dtTo
            // 
            this.dtTo.EditValue = null;
            this.dtTo.Location = new System.Drawing.Point(880, 25);
            this.dtTo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtTo.MenuManager = this.barManager1;
            this.dtTo.Name = "dtTo";
            this.dtTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtTo.Size = new System.Drawing.Size(147, 24);
            this.dtTo.TabIndex = 2;
            this.dtTo.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.dtTo_EditValueChanging);
            // 
            // dtFrom
            // 
            this.dtFrom.EditValue = null;
            this.dtFrom.Location = new System.Drawing.Point(672, 25);
            this.dtFrom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtFrom.MenuManager = this.barManager1;
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtFrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtFrom.Size = new System.Drawing.Size(147, 24);
            this.dtFrom.TabIndex = 1;
            this.dtFrom.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.dtFrom_EditValueChanging);
            // 
            // radioTimeSpan
            // 
            this.radioTimeSpan.Location = new System.Drawing.Point(15, 18);
            this.radioTimeSpan.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioTimeSpan.MenuManager = this.barManager1;
            this.radioTimeSpan.Name = "radioTimeSpan";
            this.radioTimeSpan.Properties.Appearance.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.radioTimeSpan.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioTimeSpan.Properties.Appearance.Options.UseBackColor = true;
            this.radioTimeSpan.Properties.Appearance.Options.UseFont = true;
            this.radioTimeSpan.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.radioTimeSpan.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(30)), "近30天"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(60)), "近60天"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(90)), "近90天"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(-1)), "自定义时间")});
            this.radioTimeSpan.Size = new System.Drawing.Size(525, 41);
            this.radioTimeSpan.TabIndex = 0;
            this.radioTimeSpan.EditValueChanged += new System.EventHandler(this.radioTimeSpan_EditValueChanged);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.gcStaticInfo);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupControl1.Location = new System.Drawing.Point(0, 115);
            this.groupControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(989, 580);
            this.groupControl1.TabIndex = 79;
            this.groupControl1.Text = "明细";
            // 
            // gcStaticInfo
            // 
            this.gcStaticInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcStaticInfo.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gcStaticInfo.Location = new System.Drawing.Point(2, 27);
            this.gcStaticInfo.MainView = this.gridView1;
            this.gcStaticInfo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gcStaticInfo.MenuManager = this.barManager1;
            this.gcStaticInfo.Name = "gcStaticInfo";
            this.gcStaticInfo.Size = new System.Drawing.Size(985, 551);
            this.gcStaticInfo.TabIndex = 0;
            this.gcStaticInfo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gcStaticInfo;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.chartStaticView);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(989, 115);
            this.groupControl2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(467, 580);
            this.groupControl2.TabIndex = 80;
            this.groupControl2.Text = "图表";
            // 
            // chartStaticView
            // 
            this.chartStaticView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartStaticView.Location = new System.Drawing.Point(2, 27);
            this.chartStaticView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chartStaticView.Name = "chartStaticView";
            this.chartStaticView.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartStaticView.Size = new System.Drawing.Size(463, 551);
            this.chartStaticView.TabIndex = 78;
            // 
            // FormRPStatics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1456, 695);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.panelCondition);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormRPStatics";
            this.Text = "统计管理";
            this.Load += new System.EventHandler(this.FormRPStatics_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            this.panelCondition.ResumeLayout(false);
            this.panelCondition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioLendType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioTimeSpan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcStaticInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartStaticView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraBars.BarEditItem toolStaticsType;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraBars.BarButtonItem toolExcuteStatics;
        private DevExpress.XtraBars.BarStaticItem barStaticItem2;
        private DevExpress.XtraBars.BarButtonItem toolExport;
        private System.Windows.Forms.Panel panelCondition;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit dtTo;
        private DevExpress.XtraEditors.DateEdit dtFrom;
        private DevExpress.XtraEditors.RadioGroup radioTimeSpan;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl gcStaticInfo;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraCharts.ChartControl chartStaticView;
        private DevExpress.XtraEditors.RadioGroup radioLendType;
    }
}