namespace AutoCabinet2017.UI.FM
{
    partial class FormFMFieldCfg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFMFieldCfg));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gcField = new DevExpress.XtraGrid.GridControl();
            this.gvField = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.toolBar = new DevExpress.XtraBars.Bar();
            this.toolSave = new DevExpress.XtraBars.BarButtonItem();
            this.toolReset = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gcField);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1589, 700);
            this.groupBox1.TabIndex = 60;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "字段配置列表";
            // 
            // gcField
            // 
            this.gcField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcField.Location = new System.Drawing.Point(3, 18);
            this.gcField.MainView = this.gvField;
            this.gcField.Name = "gcField";
            this.gcField.Size = new System.Drawing.Size(1583, 679);
            this.gcField.TabIndex = 0;
            this.gcField.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvField});
            // 
            // gvField
            // 
            this.gvField.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvField.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvField.Appearance.ViewCaption.Options.UseTextOptions = true;
            this.gvField.Appearance.ViewCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvField.Appearance.ViewCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gvField.GridControl = this.gcField;
            this.gvField.IndicatorWidth = 30;
            this.gvField.Name = "gvField";
            this.gvField.OptionsView.ShowGroupPanel = false;
            this.gvField.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gvField_CustomDrawRowIndicator);
            this.gvField.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.gvField_ShowingEditor);
            this.gvField.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvField_CellValueChanging);
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.toolBar});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.toolSave,
            this.toolReset});
            this.barManager1.MaxItemId = 2;
            // 
            // toolBar
            // 
            this.toolBar.BarName = "Tools";
            this.toolBar.DockCol = 0;
            this.toolBar.DockRow = 0;
            this.toolBar.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.toolBar.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.toolSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.toolReset, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.toolBar.Text = "Tools";
            // 
            // toolSave
            // 
            this.toolSave.Caption = "保存";
            this.toolSave.Glyph = ((System.Drawing.Image)(resources.GetObject("toolSave.Glyph")));
            this.toolSave.Id = 0;
            this.toolSave.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("toolSave.LargeGlyph")));
            this.toolSave.Name = "toolSave";
            this.toolSave.Tag = "Control";
            this.toolSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.toolSave_ItemClick);
            // 
            // toolReset
            // 
            this.toolReset.Caption = "重置";
            this.toolReset.Glyph = ((System.Drawing.Image)(resources.GetObject("toolReset.Glyph")));
            this.toolReset.Id = 1;
            this.toolReset.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("toolReset.LargeGlyph")));
            this.toolReset.Name = "toolReset";
            this.toolReset.Tag = "Control";
            this.toolReset.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.toolReset_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1589, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 731);
            this.barDockControlBottom.Size = new System.Drawing.Size(1589, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 700);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1589, 31);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 700);
            // 
            // FormFMFieldCfg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1589, 731);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FormFMFieldCfg";
            this.Tag = "401";
            this.Text = "数据字典管理";
            this.Load += new System.EventHandler(this.FormViewConfig_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraGrid.GridControl gcField;
        private DevExpress.XtraGrid.Views.Grid.GridView gvField;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar toolBar;
        private DevExpress.XtraBars.BarButtonItem toolSave;
        private DevExpress.XtraBars.BarButtonItem toolReset;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
    }
}