namespace AutoCabinet2017.UI.PB
{
    partial class FormPBPrinter
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
            this.btnPageSet = new System.Windows.Forms.Button();
            this.btnPrintSetup = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.txtFooter = new System.Windows.Forms.TextBox();
            this.gcPrint = new DevExpress.XtraGrid.GridControl();
            this.gvPrint = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.printDialog = new System.Windows.Forms.PrintDialog();
            this.PageSetup = new System.Windows.Forms.PageSetupDialog();
            ((System.ComponentModel.ISupportInitialize)(this.gcPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPrint)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPageSet
            // 
            this.btnPageSet.Location = new System.Drawing.Point(104, 335);
            this.btnPageSet.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPageSet.Name = "btnPageSet";
            this.btnPageSet.Size = new System.Drawing.Size(100, 29);
            this.btnPageSet.TabIndex = 1;
            this.btnPageSet.Text = "页面设置";
            this.btnPageSet.UseVisualStyleBackColor = true;
            this.btnPageSet.Click += new System.EventHandler(this.btnPageSet_Click);
            // 
            // btnPrintSetup
            // 
            this.btnPrintSetup.Location = new System.Drawing.Point(252, 335);
            this.btnPrintSetup.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPrintSetup.Name = "btnPrintSetup";
            this.btnPrintSetup.Size = new System.Drawing.Size(100, 29);
            this.btnPrintSetup.TabIndex = 2;
            this.btnPrintSetup.Text = "打印设置";
            this.btnPrintSetup.UseVisualStyleBackColor = true;
            this.btnPrintSetup.Click += new System.EventHandler(this.btnPrintSetup_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(420, 335);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(100, 29);
            this.btnPrint.TabIndex = 3;
            this.btnPrint.Text = "打印";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(573, 335);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 29);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(101, 249);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "标题";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(101, 300);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "脚注";
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(148, 238);
            this.txtTitle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(524, 25);
            this.txtTitle.TabIndex = 7;
            // 
            // txtFooter
            // 
            this.txtFooter.Location = new System.Drawing.Point(148, 289);
            this.txtFooter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtFooter.Name = "txtFooter";
            this.txtFooter.Size = new System.Drawing.Size(524, 25);
            this.txtFooter.TabIndex = 8;
            // 
            // gcPrint
            // 
            this.gcPrint.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gcPrint.Location = new System.Drawing.Point(-1, 0);
            this.gcPrint.MainView = this.gvPrint;
            this.gcPrint.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gcPrint.Name = "gcPrint";
            this.gcPrint.Size = new System.Drawing.Size(817, 222);
            this.gcPrint.TabIndex = 9;
            this.gcPrint.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvPrint});
            // 
            // gvPrint
            // 
            this.gvPrint.GridControl = this.gcPrint;
            this.gvPrint.Name = "gvPrint";
            this.gvPrint.OptionsView.ShowGroupPanel = false;
            // 
            // printDialog
            // 
            this.printDialog.UseEXDialog = true;
            // 
            // FormPBPrinter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 396);
            this.Controls.Add(this.gcPrint);
            this.Controls.Add(this.txtFooter);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnPrintSetup);
            this.Controls.Add(this.btnPageSet);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormPBPrinter";
            this.Text = "打印设置";
            this.Load += new System.EventHandler(this.PrintForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPrint)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPageSet;
        private System.Windows.Forms.Button btnPrintSetup;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TextBox txtFooter;
        private DevExpress.XtraGrid.GridControl gcPrint;
        private DevExpress.XtraGrid.Views.Grid.GridView gvPrint;
        private System.Windows.Forms.PrintDialog printDialog;
        private System.Windows.Forms.PageSetupDialog PageSetup;
    }
}