namespace AutoCabinet2017.UI.EF
{
    partial class FormArvBox
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
            this.panelSearch = new System.Windows.Forms.Panel();
            this.lblLayerNo = new System.Windows.Forms.Label();
            this.lblGroupNo = new System.Windows.Forms.Label();
            this.btnAdvanced = new DevExpress.XtraEditors.SimpleButton();
            this.txtArvBoxTitle = new DevExpress.XtraEditors.TextEdit();
            this.lblArvBoxTitle = new System.Windows.Forms.Label();
            this.cbxGroupNo = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cbxLayerNo = new DevExpress.XtraEditors.ComboBoxEdit();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gcArvBox = new DevExpress.XtraGrid.GridControl();
            this.gvArvBox = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.toolBar = new DevExpress.XtraBars.Bar();
            this.toolOK = new DevExpress.XtraBars.BarButtonItem();
            this.toolCancel = new DevExpress.XtraBars.BarButtonItem();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.edtArvBoxID = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.toolQuery = new DevExpress.XtraBars.BarButtonItem();
            this.toolAdvancedQuery = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.panelSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtArvBoxTitle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxGroupNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxLayerNo.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcArvBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvArvBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelSearch
            // 
            this.panelSearch.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.panelSearch.Controls.Add(this.lblLayerNo);
            this.panelSearch.Controls.Add(this.lblGroupNo);
            this.panelSearch.Controls.Add(this.btnAdvanced);
            this.panelSearch.Controls.Add(this.txtArvBoxTitle);
            this.panelSearch.Controls.Add(this.lblArvBoxTitle);
            this.panelSearch.Controls.Add(this.cbxGroupNo);
            this.panelSearch.Controls.Add(this.cbxLayerNo);
            this.panelSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSearch.Location = new System.Drawing.Point(0, 31);
            this.panelSearch.Margin = new System.Windows.Forms.Padding(26, 4, 26, 4);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Size = new System.Drawing.Size(776, 47);
            this.panelSearch.TabIndex = 60;
            // 
            // lblLayerNo
            // 
            this.lblLayerNo.AutoSize = true;
            this.lblLayerNo.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblLayerNo.Location = new System.Drawing.Point(404, 20);
            this.lblLayerNo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLayerNo.Name = "lblLayerNo";
            this.lblLayerNo.Size = new System.Drawing.Size(51, 14);
            this.lblLayerNo.TabIndex = 96;
            this.lblLayerNo.Tag = "LayerNo";
            this.lblLayerNo.Text = "LayerNo";
            // 
            // lblGroupNo
            // 
            this.lblGroupNo.AutoSize = true;
            this.lblGroupNo.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblGroupNo.Location = new System.Drawing.Point(241, 20);
            this.lblGroupNo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGroupNo.Name = "lblGroupNo";
            this.lblGroupNo.Size = new System.Drawing.Size(55, 14);
            this.lblGroupNo.TabIndex = 94;
            this.lblGroupNo.Tag = "GroupNo";
            this.lblGroupNo.Text = "GroupNo";
            // 
            // btnAdvanced
            // 
            this.btnAdvanced.Image = global::AutoCabinet2017.Properties.Resources.find32x32;
            this.btnAdvanced.Location = new System.Drawing.Point(605, 6);
            this.btnAdvanced.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdvanced.Name = "btnAdvanced";
            this.btnAdvanced.Size = new System.Drawing.Size(76, 38);
            this.btnAdvanced.TabIndex = 93;
            this.btnAdvanced.Text = "搜索";
            this.btnAdvanced.Click += new System.EventHandler(this.btnAdvanced_Click);
            // 
            // txtArvBoxTitle
            // 
            this.txtArvBoxTitle.EnterMoveNextControl = true;
            this.txtArvBoxTitle.Location = new System.Drawing.Point(98, 12);
            this.txtArvBoxTitle.Margin = new System.Windows.Forms.Padding(4);
            this.txtArvBoxTitle.Name = "txtArvBoxTitle";
            this.txtArvBoxTitle.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtArvBoxTitle.Properties.Appearance.Options.UseFont = true;
            this.txtArvBoxTitle.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Yellow;
            this.txtArvBoxTitle.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtArvBoxTitle.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtArvBoxTitle.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Red;
            this.txtArvBoxTitle.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtArvBoxTitle.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.txtArvBoxTitle.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtArvBoxTitle.Size = new System.Drawing.Size(129, 26);
            this.txtArvBoxTitle.TabIndex = 92;
            this.txtArvBoxTitle.Tag = "ArvBoxTitle";
            // 
            // lblArvBoxTitle
            // 
            this.lblArvBoxTitle.AutoSize = true;
            this.lblArvBoxTitle.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblArvBoxTitle.Location = new System.Drawing.Point(16, 20);
            this.lblArvBoxTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblArvBoxTitle.Name = "lblArvBoxTitle";
            this.lblArvBoxTitle.Size = new System.Drawing.Size(69, 14);
            this.lblArvBoxTitle.TabIndex = 91;
            this.lblArvBoxTitle.Tag = "ArvBoxTitle";
            this.lblArvBoxTitle.Text = "ArvBoxTitle";
            // 
            // cbxGroupNo
            // 
            this.cbxGroupNo.Location = new System.Drawing.Point(296, 12);
            this.cbxGroupNo.Margin = new System.Windows.Forms.Padding(4);
            this.cbxGroupNo.Name = "cbxGroupNo";
            this.cbxGroupNo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.cbxGroupNo.Properties.Appearance.Options.UseFont = true;
            this.cbxGroupNo.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Yellow;
            this.cbxGroupNo.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.cbxGroupNo.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cbxGroupNo.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Red;
            this.cbxGroupNo.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.cbxGroupNo.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.cbxGroupNo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.cbxGroupNo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxGroupNo.Size = new System.Drawing.Size(81, 26);
            this.cbxGroupNo.TabIndex = 95;
            this.cbxGroupNo.Tag = "ArvBoxTitle";
            this.cbxGroupNo.SelectedIndexChanged += new System.EventHandler(this.cbxGroupNo_SelectedIndexChanged);
            // 
            // cbxLayerNo
            // 
            this.cbxLayerNo.Location = new System.Drawing.Point(459, 11);
            this.cbxLayerNo.Margin = new System.Windows.Forms.Padding(4);
            this.cbxLayerNo.Name = "cbxLayerNo";
            this.cbxLayerNo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.cbxLayerNo.Properties.Appearance.Options.UseFont = true;
            this.cbxLayerNo.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Yellow;
            this.cbxLayerNo.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.cbxLayerNo.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cbxLayerNo.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Red;
            this.cbxLayerNo.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.cbxLayerNo.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.cbxLayerNo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.cbxLayerNo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxLayerNo.Size = new System.Drawing.Size(79, 26);
            this.cbxLayerNo.TabIndex = 97;
            this.cbxLayerNo.Tag = "ArvBoxTitle";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gcArvBox);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 78);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(776, 356);
            this.groupBox1.TabIndex = 93;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "档案盒明细";
            // 
            // gcArvBox
            // 
            this.gcArvBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcArvBox.Location = new System.Drawing.Point(3, 17);
            this.gcArvBox.MainView = this.gvArvBox;
            this.gcArvBox.Name = "gcArvBox";
            this.gcArvBox.Size = new System.Drawing.Size(770, 336);
            this.gcArvBox.TabIndex = 1;
            this.gcArvBox.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvArvBox});
            // 
            // gvArvBox
            // 
            this.gvArvBox.GridControl = this.gcArvBox;
            this.gvArvBox.Name = "gvArvBox";
            this.gvArvBox.OptionsSelection.MultiSelect = true;
            this.gvArvBox.OptionsView.ShowGroupPanel = false;
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
            this.toolOK,
            this.toolCancel,
            this.barStaticItem1,
            this.edtArvBoxID,
            this.toolQuery,
            this.toolAdvancedQuery});
            this.barManager1.MaxItemId = 6;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1});
            // 
            // toolBar
            // 
            this.toolBar.BarName = "Tools";
            this.toolBar.DockCol = 0;
            this.toolBar.DockRow = 0;
            this.toolBar.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.toolBar.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.toolOK, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.toolCancel, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItem1),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, this.edtArvBoxID, "", false, true, true, 141),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.toolQuery, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.toolAdvancedQuery, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.toolBar.Text = "Tools";
            // 
            // toolOK
            // 
            this.toolOK.Caption = "确定";
            this.toolOK.Glyph = global::AutoCabinet2017.Properties.Resources.apply_16x16;
            this.toolOK.Id = 0;
            this.toolOK.LargeGlyph = global::AutoCabinet2017.Properties.Resources.apply_32x32;
            this.toolOK.Name = "toolOK";
            this.toolOK.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.toolOK_ItemClick);
            // 
            // toolCancel
            // 
            this.toolCancel.Caption = "取消";
            this.toolCancel.Glyph = global::AutoCabinet2017.Properties.Resources.cancel_16x16;
            this.toolCancel.Id = 1;
            this.toolCancel.LargeGlyph = global::AutoCabinet2017.Properties.Resources.cancel_32x32;
            this.toolCancel.Name = "toolCancel";
            this.toolCancel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.toolCancel_ItemClick);
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Id = 2;
            this.barStaticItem1.Name = "barStaticItem1";
            this.barStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // edtArvBoxID
            // 
            this.edtArvBoxID.Caption = "barEditItem1";
            this.edtArvBoxID.Edit = this.repositoryItemTextEdit1;
            this.edtArvBoxID.Id = 3;
            this.edtArvBoxID.Name = "edtArvBoxID";
            this.edtArvBoxID.Tag = "ArvBoxID";
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            this.repositoryItemTextEdit1.NullValuePrompt = "请输出档案盒编号查询";
            this.repositoryItemTextEdit1.NullValuePromptShowForEmptyValue = true;
            // 
            // toolQuery
            // 
            this.toolQuery.Caption = "搜索";
            this.toolQuery.Glyph = global::AutoCabinet2017.Properties.Resources.find;
            this.toolQuery.Id = 4;
            this.toolQuery.Name = "toolQuery";
            this.toolQuery.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.toolQuery_ItemClick);
            // 
            // toolAdvancedQuery
            // 
            this.toolAdvancedQuery.Caption = "高级搜索";
            this.toolAdvancedQuery.Id = 5;
            this.toolAdvancedQuery.Name = "toolAdvancedQuery";
            this.toolAdvancedQuery.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.toolAdvancedQuery_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(776, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 434);
            this.barDockControlBottom.Size = new System.Drawing.Size(776, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 403);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(776, 31);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 403);
            // 
            // FormArvBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(776, 434);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panelSearch);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FormArvBox";
            this.Text = "档案盒选择";
            this.Load += new System.EventHandler(this.FrmArvBox_Load);
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtArvBoxTitle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxGroupNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxLayerNo.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcArvBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvArvBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelSearch;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraGrid.GridControl gcArvBox;
        private DevExpress.XtraGrid.Views.Grid.GridView gvArvBox;
        private System.Windows.Forms.Label lblLayerNo;
        private System.Windows.Forms.Label lblGroupNo;
        private DevExpress.XtraEditors.SimpleButton btnAdvanced;
        private DevExpress.XtraEditors.TextEdit txtArvBoxTitle;
        private System.Windows.Forms.Label lblArvBoxTitle;
        private DevExpress.XtraEditors.ComboBoxEdit cbxGroupNo;
        private DevExpress.XtraEditors.ComboBoxEdit cbxLayerNo;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar toolBar;
        private DevExpress.XtraBars.BarButtonItem toolOK;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem toolCancel;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraBars.BarEditItem edtArvBoxID;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraBars.BarButtonItem toolQuery;
        private DevExpress.XtraBars.BarButtonItem toolAdvancedQuery;

    }
}