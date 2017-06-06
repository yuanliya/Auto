namespace AutoCabinet2017.UI.APP
{
    partial class FormAppMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAppMain));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.toolInStorage = new DevExpress.XtraBars.BarButtonItem();
            this.toolLend = new DevExpress.XtraBars.BarButtonItem();
            this.toolReturn = new DevExpress.XtraBars.BarButtonItem();
            this.toolQuery = new DevExpress.XtraBars.BarButtonItem();
            this.toolReLogin = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.toolStatusLabelDatetime = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStatusLabelUnit = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStatusLabelWelcome = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.xtraTabbedMdiMgr = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(this.components);
            this.barDockControl1 = new DevExpress.XtraBars.BarDockControl();
            this.nbcMainMenu = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
            this.nbiArvIn = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiArvOut = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiArvMove = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiArvCheck = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiArvLend = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiArvReturn = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarGroup8 = new DevExpress.XtraNavBar.NavBarGroup();
            this.nbiQuery = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiStatic = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarGroup9 = new DevExpress.XtraNavBar.NavBarGroup();
            this.nbiDBBackup = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiDBRestore = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarGroup10 = new DevExpress.XtraNavBar.NavBarGroup();
            this.nbiField = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiDict = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarGroup11 = new DevExpress.XtraNavBar.NavBarGroup();
            this.nbiDevCtrl = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiDevConfig = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiBarCode = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarGroup12 = new DevExpress.XtraNavBar.NavBarGroup();
            this.nbiUser = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiRole = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiLog = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiComm = new DevExpress.XtraNavBar.NavBarItem();
            this.peCover = new DevExpress.XtraEditors.PictureEdit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiMgr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbcMainMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.peCover.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.toolLend,
            this.toolReturn,
            this.toolQuery,
            this.toolReLogin,
            this.toolInStorage});
            this.barManager1.MaxItemId = 19;
            // 
            // bar2
            // 
            this.bar2.BarName = "Custom 2";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.toolInStorage, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.toolLend, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.toolReturn, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.toolQuery, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.toolReLogin, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.Text = "Custom 2";
            // 
            // toolInStorage
            // 
            this.toolInStorage.Caption = "入库管理";
            this.toolInStorage.Glyph = ((System.Drawing.Image)(resources.GetObject("toolInStorage.Glyph")));
            this.toolInStorage.Id = 18;
            this.toolInStorage.Name = "toolInStorage";
            this.toolInStorage.Tag = "101";
            this.toolInStorage.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.toolInStorage_ItemClick);
            // 
            // toolLend
            // 
            this.toolLend.Caption = "借阅管理";
            this.toolLend.Glyph = ((System.Drawing.Image)(resources.GetObject("toolLend.Glyph")));
            this.toolLend.Id = 8;
            this.toolLend.Name = "toolLend";
            this.toolLend.Tag = "105";
            this.toolLend.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.toolLend_ItemClick);
            // 
            // toolReturn
            // 
            this.toolReturn.Caption = "归还管理";
            this.toolReturn.Glyph = ((System.Drawing.Image)(resources.GetObject("toolReturn.Glyph")));
            this.toolReturn.Id = 9;
            this.toolReturn.Name = "toolReturn";
            this.toolReturn.Tag = "106";
            this.toolReturn.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.toolReturn_ItemClick);
            // 
            // toolQuery
            // 
            this.toolQuery.Caption = "查询管理";
            this.toolQuery.Glyph = ((System.Drawing.Image)(resources.GetObject("toolQuery.Glyph")));
            this.toolQuery.Id = 10;
            this.toolQuery.Name = "toolQuery";
            this.toolQuery.Tag = "201";
            this.toolQuery.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.toolQuery_ItemClick);
            // 
            // toolReLogin
            // 
            this.toolReLogin.Caption = "重新登录";
            this.toolReLogin.Glyph = ((System.Drawing.Image)(resources.GetObject("toolReLogin.Glyph")));
            this.toolReLogin.Id = 12;
            this.toolReLogin.Name = "toolReLogin";
            this.toolReLogin.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.toolReLogin_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1028, 47);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 600);
            this.barDockControlBottom.Size = new System.Drawing.Size(1028, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 47);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 553);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1028, 47);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 553);
            // 
            // toolStatusLabelDatetime
            // 
            this.toolStatusLabelDatetime.Name = "toolStatusLabelDatetime";
            this.toolStatusLabelDatetime.Size = new System.Drawing.Size(131, 21);
            this.toolStatusLabelDatetime.Text = "toolStripStatusLabel3";
            // 
            // toolStatusLabelUnit
            // 
            this.toolStatusLabelUnit.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.toolStatusLabelUnit.Name = "toolStatusLabelUnit";
            this.toolStatusLabelUnit.Size = new System.Drawing.Size(826, 21);
            this.toolStatusLabelUnit.Spring = true;
            this.toolStatusLabelUnit.Text = "xxxxxxxxxxxxxxxxxxxx";
            // 
            // toolStatusLabelWelcome
            // 
            this.toolStatusLabelWelcome.Name = "toolStatusLabelWelcome";
            this.toolStatusLabelWelcome.Size = new System.Drawing.Size(56, 21);
            this.toolStatusLabelWelcome.Text = "欢迎您！";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStatusLabelWelcome,
            this.toolStatusLabelUnit,
            this.toolStatusLabelDatetime});
            this.statusStrip1.Location = new System.Drawing.Point(0, 574);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1028, 26);
            this.statusStrip1.TabIndex = 20;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // xtraTabbedMdiMgr
            // 
            this.xtraTabbedMdiMgr.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.xtraTabbedMdiMgr.Appearance.Options.UseBackColor = true;
            this.xtraTabbedMdiMgr.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InAllTabPagesAndTabControlHeader;
            this.xtraTabbedMdiMgr.HeaderButtons = ((DevExpress.XtraTab.TabButtons)(((DevExpress.XtraTab.TabButtons.Prev | DevExpress.XtraTab.TabButtons.Next) 
            | DevExpress.XtraTab.TabButtons.Close)));
            this.xtraTabbedMdiMgr.HeaderButtonsShowMode = DevExpress.XtraTab.TabButtonShowMode.Always;
            this.xtraTabbedMdiMgr.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Bottom;
            this.xtraTabbedMdiMgr.MdiParent = this;
            // 
            // barDockControl1
            // 
            this.barDockControl1.CausesValidation = false;
            this.barDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControl1.Location = new System.Drawing.Point(0, 47);
            this.barDockControl1.Size = new System.Drawing.Size(1028, 0);
            // 
            // nbcMainMenu
            // 
            this.nbcMainMenu.ActiveGroup = this.navBarGroup1;
            this.nbcMainMenu.BackColor = System.Drawing.SystemColors.Control;
            this.nbcMainMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.nbcMainMenu.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nbcMainMenu.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarGroup1,
            this.navBarGroup8,
            this.navBarGroup9,
            this.navBarGroup10,
            this.navBarGroup11,
            this.navBarGroup12});
            this.nbcMainMenu.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.nbiArvIn,
            this.nbiArvOut,
            this.nbiArvMove,
            this.nbiArvLend,
            this.nbiArvReturn,
            this.nbiDBBackup,
            this.nbiDBRestore,
            this.nbiArvCheck,
            this.nbiQuery,
            this.nbiStatic,
            this.nbiField,
            this.nbiDict,
            this.nbiDevCtrl,
            this.nbiDevConfig,
            this.nbiComm,
            this.nbiBarCode,
            this.nbiUser,
            this.nbiRole,
            this.nbiLog});
            this.nbcMainMenu.Location = new System.Drawing.Point(0, 47);
            this.nbcMainMenu.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.nbcMainMenu.Name = "nbcMainMenu";
            this.nbcMainMenu.OptionsLayout.StoreAppearance = true;
            this.nbcMainMenu.OptionsNavPane.ExpandedWidth = 152;
            this.nbcMainMenu.Size = new System.Drawing.Size(152, 527);
            this.nbcMainMenu.SkinExplorerBarViewScrollStyle = DevExpress.XtraNavBar.SkinExplorerBarViewScrollStyle.ScrollBar;
            this.nbcMainMenu.TabIndex = 35;
            this.nbcMainMenu.Text = "navBarControl1";
            // 
            // navBarGroup1
            // 
            this.navBarGroup1.Caption = "仓储作业";
            this.navBarGroup1.Expanded = true;
            this.navBarGroup1.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.SmallIconsText;
            this.navBarGroup1.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiArvIn),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiArvOut),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiArvMove),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiArvCheck),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiArvLend),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiArvReturn)});
            this.navBarGroup1.Name = "navBarGroup1";
            this.navBarGroup1.SmallImage = ((System.Drawing.Image)(resources.GetObject("navBarGroup1.SmallImage")));
            this.navBarGroup1.Tag = "100";
            // 
            // nbiArvIn
            // 
            this.nbiArvIn.Caption = "入库管理";
            this.nbiArvIn.Name = "nbiArvIn";
            this.nbiArvIn.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiArvIn.SmallImage")));
            this.nbiArvIn.Tag = "101";
            this.nbiArvIn.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiItems_LinkClicked);
            // 
            // nbiArvOut
            // 
            this.nbiArvOut.Caption = "出库管理";
            this.nbiArvOut.Name = "nbiArvOut";
            this.nbiArvOut.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiArvOut.SmallImage")));
            this.nbiArvOut.Tag = "102";
            this.nbiArvOut.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiItems_LinkClicked);
            // 
            // nbiArvMove
            // 
            this.nbiArvMove.Caption = "移库管理";
            this.nbiArvMove.Name = "nbiArvMove";
            this.nbiArvMove.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiArvMove.SmallImage")));
            this.nbiArvMove.Tag = "103";
            this.nbiArvMove.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiItems_LinkClicked);
            // 
            // nbiArvCheck
            // 
            this.nbiArvCheck.Caption = "盘库管理";
            this.nbiArvCheck.Name = "nbiArvCheck";
            this.nbiArvCheck.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiArvCheck.SmallImage")));
            this.nbiArvCheck.Tag = "104";
            this.nbiArvCheck.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiItems_LinkClicked);
            // 
            // nbiArvLend
            // 
            this.nbiArvLend.Caption = "借阅管理";
            this.nbiArvLend.Name = "nbiArvLend";
            this.nbiArvLend.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiArvLend.SmallImage")));
            this.nbiArvLend.Tag = "105";
            this.nbiArvLend.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiItems_LinkClicked);
            // 
            // nbiArvReturn
            // 
            this.nbiArvReturn.Caption = "归还管理";
            this.nbiArvReturn.Name = "nbiArvReturn";
            this.nbiArvReturn.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiArvReturn.SmallImage")));
            this.nbiArvReturn.Tag = "106";
            this.nbiArvReturn.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiItems_LinkClicked);
            // 
            // navBarGroup8
            // 
            this.navBarGroup8.Caption = "报表管理";
            this.navBarGroup8.Expanded = true;
            this.navBarGroup8.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiQuery),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiStatic)});
            this.navBarGroup8.Name = "navBarGroup8";
            this.navBarGroup8.SmallImage = ((System.Drawing.Image)(resources.GetObject("navBarGroup8.SmallImage")));
            this.navBarGroup8.Tag = "200";
            // 
            // nbiQuery
            // 
            this.nbiQuery.Caption = "查询管理";
            this.nbiQuery.Name = "nbiQuery";
            this.nbiQuery.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiQuery.SmallImage")));
            this.nbiQuery.Tag = "201";
            this.nbiQuery.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiItems_LinkClicked);
            // 
            // nbiStatic
            // 
            this.nbiStatic.Caption = "统计管理";
            this.nbiStatic.Name = "nbiStatic";
            this.nbiStatic.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiStatic.SmallImage")));
            this.nbiStatic.Tag = "202";
            this.nbiStatic.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiItems_LinkClicked);
            // 
            // navBarGroup9
            // 
            this.navBarGroup9.Caption = "数据库管理";
            this.navBarGroup9.Expanded = true;
            this.navBarGroup9.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiDBBackup),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiDBRestore)});
            this.navBarGroup9.Name = "navBarGroup9";
            this.navBarGroup9.SmallImage = ((System.Drawing.Image)(resources.GetObject("navBarGroup9.SmallImage")));
            this.navBarGroup9.Tag = "300";
            // 
            // nbiDBBackup
            // 
            this.nbiDBBackup.Caption = "数据库备份";
            this.nbiDBBackup.Name = "nbiDBBackup";
            this.nbiDBBackup.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiDBBackup.SmallImage")));
            this.nbiDBBackup.Tag = "301";
            this.nbiDBBackup.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiItems1_LinkClicked);
            // 
            // nbiDBRestore
            // 
            this.nbiDBRestore.Caption = "数据库还原";
            this.nbiDBRestore.Name = "nbiDBRestore";
            this.nbiDBRestore.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiDBRestore.SmallImage")));
            this.nbiDBRestore.Tag = "302";
            this.nbiDBRestore.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiItems1_LinkClicked);
            // 
            // navBarGroup10
            // 
            this.navBarGroup10.Caption = "基本资料";
            this.navBarGroup10.Expanded = true;
            this.navBarGroup10.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiField),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiDict)});
            this.navBarGroup10.Name = "navBarGroup10";
            this.navBarGroup10.SmallImage = ((System.Drawing.Image)(resources.GetObject("navBarGroup10.SmallImage")));
            this.navBarGroup10.Tag = "400";
            // 
            // nbiField
            // 
            this.nbiField.Caption = "字段管理";
            this.nbiField.Name = "nbiField";
            this.nbiField.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiField.SmallImage")));
            this.nbiField.Tag = "401";
            this.nbiField.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiItems_LinkClicked);
            // 
            // nbiDict
            // 
            this.nbiDict.Caption = "字典管理";
            this.nbiDict.Name = "nbiDict";
            this.nbiDict.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiDict.SmallImage")));
            this.nbiDict.Tag = "402";
            this.nbiDict.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiItems_LinkClicked);
            // 
            // navBarGroup11
            // 
            this.navBarGroup11.Caption = "设备管理";
            this.navBarGroup11.Expanded = true;
            this.navBarGroup11.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiDevCtrl),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiDevConfig),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiBarCode)});
            this.navBarGroup11.Name = "navBarGroup11";
            this.navBarGroup11.SmallImage = ((System.Drawing.Image)(resources.GetObject("navBarGroup11.SmallImage")));
            this.navBarGroup11.Tag = "500";
            // 
            // nbiDevCtrl
            // 
            this.nbiDevCtrl.Caption = "设备控制";
            this.nbiDevCtrl.Name = "nbiDevCtrl";
            this.nbiDevCtrl.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiDevCtrl.SmallImage")));
            this.nbiDevCtrl.Tag = "501";
            this.nbiDevCtrl.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiItems_LinkClicked);
            // 
            // nbiDevConfig
            // 
            this.nbiDevConfig.Caption = "设备配置";
            this.nbiDevConfig.Name = "nbiDevConfig";
            this.nbiDevConfig.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiDevConfig.SmallImage")));
            this.nbiDevConfig.Tag = "502";
            this.nbiDevConfig.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiItems_LinkClicked);
            // 
            // nbiBarCode
            // 
            this.nbiBarCode.Caption = "条码设置";
            this.nbiBarCode.Name = "nbiBarCode";
            this.nbiBarCode.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiBarCode.SmallImage")));
            this.nbiBarCode.Tag = "503";
            this.nbiBarCode.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiItems_LinkClicked);
            // 
            // navBarGroup12
            // 
            this.navBarGroup12.Caption = "系统管理";
            this.navBarGroup12.Expanded = true;
            this.navBarGroup12.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiUser),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiRole),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiLog)});
            this.navBarGroup12.LargeImage = ((System.Drawing.Image)(resources.GetObject("navBarGroup12.LargeImage")));
            this.navBarGroup12.Name = "navBarGroup12";
            this.navBarGroup12.SmallImage = ((System.Drawing.Image)(resources.GetObject("navBarGroup12.SmallImage")));
            this.navBarGroup12.Tag = "600";
            // 
            // nbiUser
            // 
            this.nbiUser.Caption = "用户管理";
            this.nbiUser.Name = "nbiUser";
            this.nbiUser.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiUser.SmallImage")));
            this.nbiUser.Tag = "601";
            this.nbiUser.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiItems_LinkClicked);
            // 
            // nbiRole
            // 
            this.nbiRole.Caption = "角色管理";
            this.nbiRole.Name = "nbiRole";
            this.nbiRole.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiRole.SmallImage")));
            this.nbiRole.Tag = "602";
            this.nbiRole.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiItems_LinkClicked);
            // 
            // nbiLog
            // 
            this.nbiLog.Caption = "日志管理";
            this.nbiLog.Name = "nbiLog";
            this.nbiLog.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiLog.SmallImage")));
            this.nbiLog.Tag = "603";
            this.nbiLog.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiItems_LinkClicked);
            // 
            // nbiComm
            // 
            this.nbiComm.Caption = "串口设置";
            this.nbiComm.Name = "nbiComm";
            this.nbiComm.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiComm.SmallImage")));
            this.nbiComm.Tag = "503";
            this.nbiComm.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiItems_LinkClicked);
            // 
            // peCover
            // 
            this.peCover.Dock = System.Windows.Forms.DockStyle.Fill;
            this.peCover.EditValue = ((object)(resources.GetObject("peCover.EditValue")));
            this.peCover.Location = new System.Drawing.Point(152, 47);
            this.peCover.Name = "peCover";
            this.peCover.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.peCover.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.peCover.Size = new System.Drawing.Size(876, 527);
            this.peCover.TabIndex = 36;
            // 
            // FormAppMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 600);
            this.Controls.Add(this.peCover);
            this.Controls.Add(this.nbcMainMenu);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.barDockControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.IsMdiContainer = true;
            this.Name = "FormAppMain";
            this.Text = "自动化仓储系统控制管理软件V1.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormAppMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormAppMain_FormClosed);
            this.Load += new System.EventHandler(this.FormAppMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiMgr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbcMainMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.peCover.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStatusLabelWelcome;
        private System.Windows.Forms.ToolStripStatusLabel toolStatusLabelUnit;
        private System.Windows.Forms.ToolStripStatusLabel toolStatusLabelDatetime;
        private DevExpress.XtraTabbedMdi.XtraTabbedMdiManager xtraTabbedMdiMgr;
        private DevExpress.XtraBars.BarDockControl barDockControl1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraNavBar.NavBarControl nbcMainMenu;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup1;
        private DevExpress.XtraNavBar.NavBarItem nbiArvIn;
        private DevExpress.XtraNavBar.NavBarItem nbiArvOut;
        private DevExpress.XtraNavBar.NavBarItem nbiArvMove;
        private DevExpress.XtraNavBar.NavBarItem nbiArvCheck;
        private DevExpress.XtraNavBar.NavBarItem nbiArvLend;
        private DevExpress.XtraNavBar.NavBarItem nbiArvReturn;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup8;
        private DevExpress.XtraNavBar.NavBarItem nbiQuery;
        private DevExpress.XtraNavBar.NavBarItem nbiStatic;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup9;
        private DevExpress.XtraNavBar.NavBarItem nbiDBBackup;
        private DevExpress.XtraNavBar.NavBarItem nbiDBRestore;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup10;
        private DevExpress.XtraNavBar.NavBarItem nbiField;
        private DevExpress.XtraNavBar.NavBarItem nbiDict;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup11;
        private DevExpress.XtraNavBar.NavBarItem nbiDevCtrl;
        private DevExpress.XtraNavBar.NavBarItem nbiDevConfig;
        private DevExpress.XtraNavBar.NavBarItem nbiComm;
        private DevExpress.XtraNavBar.NavBarItem nbiBarCode;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup12;
        private DevExpress.XtraNavBar.NavBarItem nbiUser;
        private DevExpress.XtraNavBar.NavBarItem nbiRole;
        private DevExpress.XtraNavBar.NavBarItem nbiLog;
        private DevExpress.XtraEditors.PictureEdit peCover;
        private DevExpress.XtraBars.BarButtonItem toolLend;
        private DevExpress.XtraBars.BarButtonItem toolReturn;
        private DevExpress.XtraBars.BarButtonItem toolQuery;
        private DevExpress.XtraBars.BarButtonItem toolReLogin;
        private DevExpress.XtraBars.BarButtonItem toolInStorage;
    }
}