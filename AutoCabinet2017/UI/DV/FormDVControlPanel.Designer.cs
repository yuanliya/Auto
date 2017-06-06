namespace AutoCabinet2017.UI.DV
{
    partial class FormDVControlPanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDVControlPanel));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject5 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject6 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject7 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject8 = new DevExpress.Utils.SerializableAppearanceObject();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.memoClipboard = new DevExpress.XtraEditors.MemoEdit();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnOpenDoor = new System.Windows.Forms.Button();
            this.btnCloseDoor = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.gcDevicePanel = new DevExpress.XtraGrid.GridControl();
            this.gvDeviceInfo = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colDeviceNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalLayers = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurLayerNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colComStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.peConnect = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.colLayerNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cbxDstLayerNo = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.colDoor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tsDoorOperate = new DevExpress.XtraEditors.Repository.RepositoryItemToggleSwitch();
            this.colLayerEnable = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cbEnableGoLayer = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colDoorEnable = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cbEnableDoor = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colExecute = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnExecute = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colUpdateStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnUpdateStatus = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.imgConnStatus = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoClipboard.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcDevicePanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDeviceInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.peConnect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxDstLayerNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tsDoorOperate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbEnableGoLayer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbEnableDoor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExecute)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnUpdateStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.memoClipboard);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(351, 627);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "系统信息栏";
            // 
            // memoClipboard
            // 
            this.memoClipboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memoClipboard.Location = new System.Drawing.Point(2, 21);
            this.memoClipboard.Name = "memoClipboard";
            this.memoClipboard.Properties.ReadOnly = true;
            this.memoClipboard.Size = new System.Drawing.Size(347, 604);
            this.memoClipboard.TabIndex = 0;
            // 
            // groupControl3
            // 
            this.groupControl3.Controls.Add(this.btnUpdate);
            this.groupControl3.Controls.Add(this.btnOpenDoor);
            this.groupControl3.Controls.Add(this.btnCloseDoor);
            this.groupControl3.Controls.Add(this.btnRun);
            this.groupControl3.Controls.Add(this.btnStop);
            this.groupControl3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupControl3.Location = new System.Drawing.Point(351, 547);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(911, 80);
            this.groupControl3.TabIndex = 3;
            this.groupControl3.Text = "控制按钮";
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.LightGray;
            this.btnUpdate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUpdate.BackgroundImage")));
            this.btnUpdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUpdate.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnUpdate.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUpdate.Location = new System.Drawing.Point(409, 21);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(2);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(100, 57);
            this.btnUpdate.TabIndex = 56;
            this.btnUpdate.Text = "刷新";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnOpenDoor
            // 
            this.btnOpenDoor.BackColor = System.Drawing.Color.LightGray;
            this.btnOpenDoor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOpenDoor.BackgroundImage")));
            this.btnOpenDoor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOpenDoor.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOpenDoor.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOpenDoor.Location = new System.Drawing.Point(509, 21);
            this.btnOpenDoor.Name = "btnOpenDoor";
            this.btnOpenDoor.Size = new System.Drawing.Size(100, 57);
            this.btnOpenDoor.TabIndex = 52;
            this.btnOpenDoor.Text = "开门";
            this.btnOpenDoor.UseVisualStyleBackColor = false;
            this.btnOpenDoor.Click += new System.EventHandler(this.btnOpenDoor_Click);
            // 
            // btnCloseDoor
            // 
            this.btnCloseDoor.BackColor = System.Drawing.Color.LightGray;
            this.btnCloseDoor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCloseDoor.BackgroundImage")));
            this.btnCloseDoor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCloseDoor.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCloseDoor.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCloseDoor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCloseDoor.Location = new System.Drawing.Point(609, 21);
            this.btnCloseDoor.Name = "btnCloseDoor";
            this.btnCloseDoor.Size = new System.Drawing.Size(100, 57);
            this.btnCloseDoor.TabIndex = 53;
            this.btnCloseDoor.Text = "关门";
            this.btnCloseDoor.UseVisualStyleBackColor = false;
            this.btnCloseDoor.Click += new System.EventHandler(this.btnCloseDoor_Click);
            // 
            // btnRun
            // 
            this.btnRun.BackColor = System.Drawing.Color.LightGray;
            this.btnRun.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRun.BackgroundImage")));
            this.btnRun.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRun.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnRun.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRun.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRun.Location = new System.Drawing.Point(709, 21);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(100, 57);
            this.btnRun.TabIndex = 54;
            this.btnRun.Text = "运行";
            this.btnRun.UseVisualStyleBackColor = false;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.LightGray;
            this.btnStop.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnStop.BackgroundImage")));
            this.btnStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnStop.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnStop.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStop.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStop.Location = new System.Drawing.Point(809, 21);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(100, 57);
            this.btnStop.TabIndex = 55;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.gcDevicePanel);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(351, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(911, 547);
            this.groupControl2.TabIndex = 4;
            this.groupControl2.Text = "设备控制面板";
            // 
            // gcDevicePanel
            // 
            this.gcDevicePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcDevicePanel.Location = new System.Drawing.Point(2, 21);
            this.gcDevicePanel.MainView = this.gvDeviceInfo;
            this.gcDevicePanel.Name = "gcDevicePanel";
            this.gcDevicePanel.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.tsDoorOperate,
            this.btnExecute,
            this.btnUpdateStatus,
            this.cbEnableGoLayer,
            this.cbEnableDoor,
            this.peConnect,
            this.cbxDstLayerNo});
            this.gcDevicePanel.Size = new System.Drawing.Size(907, 524);
            this.gcDevicePanel.TabIndex = 0;
            this.gcDevicePanel.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDeviceInfo});
            // 
            // gvDeviceInfo
            // 
            this.gvDeviceInfo.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDeviceNo,
            this.colTotalLayers,
            this.colCurLayerNo,
            this.colComStatus,
            this.colLayerNo,
            this.colDoor,
            this.colLayerEnable,
            this.colDoorEnable,
            this.colExecute,
            this.colUpdateStatus});
            this.gvDeviceInfo.GridControl = this.gcDevicePanel;
            this.gvDeviceInfo.Name = "gvDeviceInfo";
            this.gvDeviceInfo.OptionsSelection.MultiSelect = true;
            this.gvDeviceInfo.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gvDeviceInfo.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
            this.gvDeviceInfo.OptionsView.ShowGroupPanel = false;
            // 
            // colDeviceNo
            // 
            this.colDeviceNo.AppearanceCell.Options.UseTextOptions = true;
            this.colDeviceNo.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDeviceNo.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colDeviceNo.AppearanceHeader.Options.UseTextOptions = true;
            this.colDeviceNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDeviceNo.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colDeviceNo.Caption = "设备编号";
            this.colDeviceNo.FieldName = "DeviceNo";
            this.colDeviceNo.Name = "colDeviceNo";
            this.colDeviceNo.Visible = true;
            this.colDeviceNo.VisibleIndex = 1;
            this.colDeviceNo.Width = 76;
            // 
            // colTotalLayers
            // 
            this.colTotalLayers.AppearanceCell.Options.UseTextOptions = true;
            this.colTotalLayers.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTotalLayers.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colTotalLayers.AppearanceHeader.Options.UseTextOptions = true;
            this.colTotalLayers.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTotalLayers.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colTotalLayers.Caption = "设备总层数";
            this.colTotalLayers.FieldName = "TotalLayers";
            this.colTotalLayers.Name = "colTotalLayers";
            this.colTotalLayers.Visible = true;
            this.colTotalLayers.VisibleIndex = 2;
            this.colTotalLayers.Width = 76;
            // 
            // colCurLayerNo
            // 
            this.colCurLayerNo.AppearanceCell.Options.UseTextOptions = true;
            this.colCurLayerNo.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCurLayerNo.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCurLayerNo.AppearanceHeader.Options.UseTextOptions = true;
            this.colCurLayerNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCurLayerNo.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCurLayerNo.Caption = "当前层";
            this.colCurLayerNo.Name = "colCurLayerNo";
            this.colCurLayerNo.Visible = true;
            this.colCurLayerNo.VisibleIndex = 3;
            this.colCurLayerNo.Width = 76;
            // 
            // colComStatus
            // 
            this.colComStatus.AppearanceHeader.Options.UseTextOptions = true;
            this.colComStatus.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colComStatus.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colComStatus.Caption = "连接状态";
            this.colComStatus.ColumnEdit = this.peConnect;
            this.colComStatus.FieldName = "IsConnected";
            this.colComStatus.Name = "colComStatus";
            this.colComStatus.Visible = true;
            this.colComStatus.VisibleIndex = 4;
            this.colComStatus.Width = 76;
            // 
            // peConnect
            // 
            this.peConnect.Name = "peConnect";
            // 
            // colLayerNo
            // 
            this.colLayerNo.AppearanceCell.Options.UseTextOptions = true;
            this.colLayerNo.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colLayerNo.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colLayerNo.AppearanceHeader.Options.UseTextOptions = true;
            this.colLayerNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colLayerNo.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colLayerNo.Caption = "目的层";
            this.colLayerNo.ColumnEdit = this.cbxDstLayerNo;
            this.colLayerNo.FieldName = "DstLayerNo";
            this.colLayerNo.Name = "colLayerNo";
            this.colLayerNo.Visible = true;
            this.colLayerNo.VisibleIndex = 5;
            this.colLayerNo.Width = 76;
            // 
            // cbxDstLayerNo
            // 
            this.cbxDstLayerNo.Appearance.Options.UseTextOptions = true;
            this.cbxDstLayerNo.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.cbxDstLayerNo.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.cbxDstLayerNo.AutoHeight = false;
            this.cbxDstLayerNo.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxDstLayerNo.Name = "cbxDstLayerNo";
            // 
            // colDoor
            // 
            this.colDoor.AppearanceHeader.Options.UseTextOptions = true;
            this.colDoor.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDoor.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colDoor.Caption = "柜门操作";
            this.colDoor.ColumnEdit = this.tsDoorOperate;
            this.colDoor.FieldName = "DoorOperateFlag";
            this.colDoor.Name = "colDoor";
            this.colDoor.Visible = true;
            this.colDoor.VisibleIndex = 6;
            this.colDoor.Width = 114;
            // 
            // tsDoorOperate
            // 
            this.tsDoorOperate.AutoHeight = false;
            this.tsDoorOperate.GlyphAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.tsDoorOperate.Name = "tsDoorOperate";
            this.tsDoorOperate.OffText = "关门";
            this.tsDoorOperate.OnText = "开门";
            // 
            // colLayerEnable
            // 
            this.colLayerEnable.AppearanceHeader.Options.UseTextOptions = true;
            this.colLayerEnable.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colLayerEnable.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colLayerEnable.Caption = "使能走层操作";
            this.colLayerEnable.ColumnEdit = this.cbEnableGoLayer;
            this.colLayerEnable.FieldName = "IsGoLayer";
            this.colLayerEnable.Name = "colLayerEnable";
            this.colLayerEnable.Visible = true;
            this.colLayerEnable.VisibleIndex = 7;
            this.colLayerEnable.Width = 76;
            // 
            // cbEnableGoLayer
            // 
            this.cbEnableGoLayer.AutoHeight = false;
            this.cbEnableGoLayer.Name = "cbEnableGoLayer";
            // 
            // colDoorEnable
            // 
            this.colDoorEnable.AppearanceHeader.Options.UseTextOptions = true;
            this.colDoorEnable.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDoorEnable.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colDoorEnable.Caption = "使能柜门操作";
            this.colDoorEnable.ColumnEdit = this.cbEnableDoor;
            this.colDoorEnable.FieldName = "IsDoorOperate";
            this.colDoorEnable.Name = "colDoorEnable";
            this.colDoorEnable.Visible = true;
            this.colDoorEnable.VisibleIndex = 8;
            this.colDoorEnable.Width = 76;
            // 
            // cbEnableDoor
            // 
            this.cbEnableDoor.AutoHeight = false;
            this.cbEnableDoor.Name = "cbEnableDoor";
            // 
            // colExecute
            // 
            this.colExecute.AppearanceHeader.Options.UseTextOptions = true;
            this.colExecute.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colExecute.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colExecute.Caption = "执行";
            this.colExecute.ColumnEdit = this.btnExecute;
            this.colExecute.Name = "colExecute";
            this.colExecute.Visible = true;
            this.colExecute.VisibleIndex = 9;
            this.colExecute.Width = 57;
            // 
            // btnExecute
            // 
            this.btnExecute.AutoHeight = false;
            this.btnExecute.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("btnExecute.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, true)});
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.btnExecute.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnExecute_ButtonClick);
            // 
            // colUpdateStatus
            // 
            this.colUpdateStatus.AppearanceHeader.Options.UseTextOptions = true;
            this.colUpdateStatus.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colUpdateStatus.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colUpdateStatus.Caption = "刷新";
            this.colUpdateStatus.ColumnEdit = this.btnUpdateStatus;
            this.colUpdateStatus.Name = "colUpdateStatus";
            this.colUpdateStatus.Visible = true;
            this.colUpdateStatus.VisibleIndex = 10;
            this.colUpdateStatus.Width = 60;
            // 
            // btnUpdateStatus
            // 
            this.btnUpdateStatus.AutoHeight = false;
            this.btnUpdateStatus.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("btnUpdateStatus.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, serializableAppearanceObject6, serializableAppearanceObject7, serializableAppearanceObject8, "", null, null, true)});
            this.btnUpdateStatus.Name = "btnUpdateStatus";
            this.btnUpdateStatus.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.btnUpdateStatus.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnUpdateStatus_ButtonClick);
            // 
            // imgConnStatus
            // 
            this.imgConnStatus.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgConnStatus.ImageStream")));
            this.imgConnStatus.TransparentColor = System.Drawing.Color.Transparent;
            this.imgConnStatus.Images.SetKeyName(0, "conn.png");
            this.imgConnStatus.Images.SetKeyName(1, "uncon.png");
            // 
            // FormDVControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 627);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl3);
            this.Controls.Add(this.groupControl1);
            this.Name = "FormDVControlPanel";
            this.Text = "设备控制面板";
            this.Load += new System.EventHandler(this.FormDVControlPanel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.memoClipboard.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcDevicePanel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDeviceInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.peConnect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxDstLayerNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tsDoorOperate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbEnableGoLayer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbEnableDoor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExecute)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnUpdateStatus)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.MemoEdit memoClipboard;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnOpenDoor;
        private System.Windows.Forms.Button btnCloseDoor;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnStop;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.GridControl gcDevicePanel;
        private DevExpress.XtraEditors.Repository.RepositoryItemToggleSwitch tsDoorOperate;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnExecute;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnUpdateStatus;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit cbEnableGoLayer;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit cbEnableDoor;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit peConnect;
        private System.Windows.Forms.ImageList imgConnStatus;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cbxDstLayerNo;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDeviceInfo;
        private DevExpress.XtraGrid.Columns.GridColumn colDeviceNo;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalLayers;
        private DevExpress.XtraGrid.Columns.GridColumn colCurLayerNo;
        private DevExpress.XtraGrid.Columns.GridColumn colComStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colLayerNo;
        private DevExpress.XtraGrid.Columns.GridColumn colDoor;
        private DevExpress.XtraGrid.Columns.GridColumn colLayerEnable;
        private DevExpress.XtraGrid.Columns.GridColumn colDoorEnable;
        private DevExpress.XtraGrid.Columns.GridColumn colExecute;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdateStatus;
    }
}