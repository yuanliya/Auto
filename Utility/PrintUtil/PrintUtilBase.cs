using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;

namespace NJUST.AUTO06.Utility
{
    public class PrintUtilBase
    {
        public PrintUtilBase()
        {
        }

        #region 打印文档和对话框设置

        private PrintDialog     printDialog     = new PrintDialog();     // 打印设置对话框
        private PageSetupDialog pageSetupDialog = new PageSetupDialog(); // 页面设置对话框

        public  PrintDocument   printDocument   = new PrintDocument();   // 输出到打印机的打印文档

        #endregion

        #region 打印内容设置

        // 页面抬头
        public string PrintTitle { get;  set; }

        // 页面脚注
        public string PrintFooter { get; set; }

        // 打印表格
        public DataTable PrintDataTable { get; set; }

        // 打印文字
        public List<string> PrintText { get; set; }

        #endregion

        #region 打印格式设置

        // 抬头所用字体--宋体，15号，加粗
        protected Font fontHeader = new Font("宋体", 15, FontStyle.Bold);
        // 脚注、时间和页数所用字体--宋体，10号，加粗
        protected Font fontFooter = new Font("宋体", 10, FontStyle.Bold);
        // 单元格内容所用字体--宋体，9号，正常字体
        protected Font fontCell   = new Font("宋体", 9, FontStyle.Regular);
        // 表格列标题所用字体--微软雅黑，12号，加粗
        protected Font fontColumnTitle = new Font("微软雅黑", 12, FontStyle.Bold);
        // 正文所用字体--宋体，12号
        protected Font fontText = new Font("宋体", 13, FontStyle.Regular);

        // 文本对齐模式
        protected StringFormat strFmtLeft   = new StringFormat();
        protected StringFormat strFmtCenter = new StringFormat();
        protected StringFormat strFmtRight  = new StringFormat();

        #endregion

        #region 表格属性设置

        // 表格标题栏的行高
        protected int columnTitleHeight      = 30;
        // 表格每一列在打印页中的位置
        protected List<int> columnLeftMargin = new List<int>();
        // 表格每一列在打印页中的实际宽度
        protected List<int> columnWidth      = new List<int>();
        // 表格每一行在打印页中的实际高度
        protected List<int> rowHeight        = new List<int>();

        #endregion

        #region 其他参数设置

        // 表格的行检索
        protected int rowIndex = 0;
        // 页编号
        protected int pageNo   = 0;
        // 是否打印新的一页
        protected bool isNewPage  = true;

        #endregion

        /// <summary>
        /// 根据打印设置计算表格的打印参数--列宽、列的打印位置
        /// </summary>
        /// <param name="dt">准备打印的表格</param>
        /// <param name="e">打印机参数</param>
        protected void CalTablePrintParams(PrintPageEventArgs e)
        {
            int col = 0, i = 0;

            // 保存表格的列宽
            int[] columnsWidth = new int[PrintDataTable.Columns.Count];
            // 保存表格的行高
            int[] rowsHeight = new int[PrintDataTable.Rows.Count];

            #region 根据表格单元格内容和行标题，确定单元格的理想宽度值，限制最大宽度为列标题2倍
                    
            // 遍历表格，确定每一列的最大宽度和每一行的最大高度
            foreach (DataRow dr in PrintDataTable.Rows)
            {
                for (col = 0; col < PrintDataTable.Columns.Count; col++)
                {
                    // 计算表格单元格在使用当前字体显示内容时的宽度
                    int colwidth = Convert.ToInt32(e.Graphics.MeasureString(dr[col].ToString().Trim(), fontCell).Width);

                    if (colwidth > columnsWidth[col])
                    {
                        columnsWidth[col] = colwidth;
                    }            
                }
            }

            // 将表格列内容的最大宽度与列标题比较，确定每一列的最大宽度
            for (col = 0; col <= PrintDataTable.Columns.Count - 1; col++)
            {
                // 计算表格列标题在使用当前字体显示内容时的宽度
                int colWidth = Convert.ToInt32(e.Graphics.MeasureString(PrintDataTable.Columns[col].ColumnName, fontColumnTitle).Width);
                // 列标题宽度大于列宽度
                if (colWidth >= columnsWidth[col])
                {
                    columnsWidth[col] = colWidth;
                }
                else
                {
                    // 列内容宽度限制为最大3倍于列标题
                    if(columnsWidth[col] > 3*colWidth)
                    {
                        columnsWidth[col] = 3*colWidth;
                    }
                }              
            }

            #endregion

            #region 根据打印页面实际大小，按比例确定表格单元格打印宽度

            // 计算表格实际宽度
            int tableWidth = 0;
            for (i = 0; i < PrintDataTable.Columns.Count; i++)
            {
                tableWidth += columnsWidth[i];
            }

            // 根据打印页面设置的情况，确定实际的表格列宽
            int marginTop  = e.MarginBounds.Top;     // 页边距以内的左上角顶点Y坐标
            int marginLeft = e.MarginBounds.Left;    // 页边距以内的左上角顶点X坐标

            int actualColumnWidth = 0;
        
            columnLeftMargin.Clear();
            columnWidth.Clear();

            i = 0;
            columnTitleHeight = 0;
            foreach (DataColumn dc in PrintDataTable.Columns)
            {
                // 根据页面实际宽（扣除页边距）和表格宽，计算每一列的真正宽度值（表格列宽*页面宽/表格宽）
                actualColumnWidth = (int)(Math.Floor((double)((double)columnsWidth[i] * (double)e.MarginBounds.Width / (double)tableWidth)));

                // 表格标题栏的行高(用指定字体写指定内容，限制最大宽度为actualColumnWidth)
                int tmpTitleHeight = (int)(e.Graphics.MeasureString(dc.ColumnName, fontColumnTitle, actualColumnWidth).Height) + 11;
                if (tmpTitleHeight > columnTitleHeight)
                {
                    columnTitleHeight = tmpTitleHeight;
                }

                // 保存本列在页面中相对左边界的位置
                columnLeftMargin.Add(marginLeft);
                // 保存本列的实际打印宽度
                columnWidth.Add(actualColumnWidth);

                // 宽度累加
                marginLeft += actualColumnWidth;

                i++;
            }

            #endregion

            #region 根据打印页面实际大小，按比例确定表格每一行的打印高度

            int row = 0;
            rowHeight.Clear();

            foreach (DataRow dr in PrintDataTable.Rows)
            {
                int tmpRowHeight = 0;

                for (col = 0; col < PrintDataTable.Columns.Count; col++)
                {
                    // 计算表格单元格在使用当前字体显示内容时的高度
                    int cellHeight = Convert.ToInt32(e.Graphics.MeasureString(dr[col].ToString().Trim(), fontCell, columnWidth[col]).Height);

                    if (cellHeight > tmpRowHeight)
                    {
                        tmpRowHeight = cellHeight;
                    }             
                }

                rowHeight.Add(tmpRowHeight + 5);

                row++;
            }

            #endregion
        }

        /// <summary>
        /// 打印抬头
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="e"></param>
        /// <param name="RowsPerPage"></param>
        protected void DrawHeader(PrintPageEventArgs e)
        {
            // 打印抬头
            e.Graphics.DrawString(PrintTitle, fontHeader, Brushes.Black,
                                  e.MarginBounds.Left + (e.MarginBounds.Width - e.Graphics.MeasureString(PrintTitle, fontCell, e.MarginBounds.Width).Width) / 2,
                                  e.MarginBounds.Top - e.Graphics.MeasureString(PrintTitle, fontCell, e.MarginBounds.Width).Height - 40);
            // 画线
            e.Graphics.DrawLine(new Pen(Color.Black),
                                e.MarginBounds.Left,
                                e.MarginBounds.Top - 20,
                                e.MarginBounds.Left + e.MarginBounds.Width,
                                e.MarginBounds.Top - 20);
        }

        /// <summary>
        /// 打印脚注
        /// </summary>
        /// <param name="e"></param>
        protected void DrawFooter(PrintPageEventArgs e)
        {               
            // 在页面底端显示当前页数
            string pageNum = string.Format(" 第 {0} 页", pageNo.ToString());
            // 打印页数
            e.Graphics.DrawString(pageNum, fontFooter, Brushes.Black,
                                  e.MarginBounds.Left, e.MarginBounds.Top + e.MarginBounds.Height + 31);

            // 打印时间
            string curDate = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
            e.Graphics.DrawString(curDate, fontFooter, Brushes.Black,
                                  e.MarginBounds.Left + (e.MarginBounds.Width - e.Graphics.MeasureString(curDate, fontFooter, e.MarginBounds.Width).Width), 
                                  e.MarginBounds.Top + e.MarginBounds.Height + 31);

            // 打印脚注
            e.Graphics.DrawString(PrintFooter, fontFooter, Brushes.Black,
                                  e.MarginBounds.Left + (e.MarginBounds.Width - e.Graphics.MeasureString(PrintFooter, fontFooter, e.MarginBounds.Width).Width) / 2, 
                                  e.MarginBounds.Top + e.MarginBounds.Height + 31);

            // 划线
            e.Graphics.DrawLine(new Pen(Color.Black), 
                                e.MarginBounds.Left, 
                                e.MarginBounds.Top  + e.MarginBounds.Height + 20, 
                                e.MarginBounds.Left + e.MarginBounds.Width, 
                                e.MarginBounds.Top  + e.MarginBounds.Height + 20);
        }

        /// <summary>
        /// 在(x,y)为起点的区域按指定格式画文本
        /// </summary>
        /// <param name="text">文本内容</param>
        /// <param name="txtFormat">文本格式</param>
        /// <param name="x">坐标</param>
        /// <param name="y">坐标</param>
        /// <param name="e"></param>
        /// <Return>文本行距</Return>
        protected int DrawText(string text, StringFormat txtFormat, int x, int y, PrintPageEventArgs e)
        {
            int coff = 1;

            // 打印内容是否超出页面宽度
            float txtWidth = e.Graphics.MeasureString(text, fontText).Width;
            if (txtWidth > e.MarginBounds.Width)
            {
                // 计算实际行高的放大系数
                coff = (int)(txtWidth) / e.MarginBounds.Width + 1;
            }

            int txtHeight = fontText.Height * coff + 5;

            // 在指定区域打印指定文字
            e.Graphics.DrawString(text, fontText, 
                                  new SolidBrush(Color.Black),
                                  new RectangleF(x, y, e.MarginBounds.Width, txtHeight),
                                  txtFormat
                                 );

            return txtHeight;
        }

        /// <summary>
        /// 画表格的标题栏
        /// </summary>
        /// <param name="columnTop">标题栏的顶点Y坐标</param>
        /// <param name="e"></param>
        protected void DrawTableHeader(int columnTop, PrintPageEventArgs e)
        {
            int i = 0;
            
            foreach (DataColumn dc in PrintDataTable.Columns)
            {
                // 使用Color.LightGray颜色填充单元格
                e.Graphics.FillRectangle(new SolidBrush(Color.LightGray),
                                         new Rectangle((int)columnLeftMargin[i], columnTop, (int)columnWidth[i], columnTitleHeight));
                // 使用黑线画单元格
                e.Graphics.DrawRectangle(Pens.Black,
                                         new Rectangle((int)columnLeftMargin[i], columnTop, (int)columnWidth[i], columnTitleHeight));

                // 填写单元格内容
                e.Graphics.DrawString(dc.ColumnName, fontColumnTitle, Brushes.Black,
                                      new Rectangle((int)columnLeftMargin[i], columnTop, (int)columnWidth[i], columnTitleHeight),
                                      strFmtCenter);
                i++;
            }
        }

        /// <summary>
        /// 画表格的行
        /// </summary>
        /// <param name="dr">表格一行</param>
        /// <param name="columnTop">行顶点Y坐标</param>
        /// <param name="e"></param>
        protected void DrawTableRow(DataRow dr, int rowIndex, int columnTop, PrintPageEventArgs e)
        {
            int i = 0;

            // 画表格具体内容
            for (i = 0; i < PrintDataTable.Columns.Count; i++)
            {
                // 画单元格内容
                e.Graphics.DrawString(dr[i].ToString(), fontCell, Brushes.Black,
                                        new RectangleF((int)columnLeftMargin[i], (float)columnTop, (int)columnWidth[i], (float)rowHeight[rowIndex]),
                                        strFmtCenter);

                // 画单元格边界 
                e.Graphics.DrawRectangle(Pens.Black,
                                         new Rectangle((int)columnLeftMargin[i], columnTop, (int)columnWidth[i], rowHeight[rowIndex]));
            }
        }

        #region 打印相关对话框

        /// <summary>
        /// 打开“打印设置”对话框
        /// </summary>
        public void OpenPrintDialog()
        {
            // 设置打印对话框关联的打印对象
            printDialog.Document = printDocument;

            // 是否启用打印到文件
            printDialog.AllowPrintToFile = true;
            // 是否显示当前页
            printDialog.AllowCurrentPage = true;
            // 是否启用选择“按钮”
            printDialog.AllowSelection = true;
            // 是否启用“页选项”按钮
            printDialog.AllowSomePages = true;
            
            // 显示打印设置对话框
            printDialog.ShowDialog();         
        }

        /// <summary>
        /// 打开“页面设置”对话框
        /// </summary>
        public void OpenPageSetupDialog()
        {
            // 设置打印页的默认设置
            printDocument.DefaultPageSettings = pageSetupDialog.PageSettings;
            // 设置页面设置对话框关联的打印对象
            pageSetupDialog.Document = printDocument;

            // 是否启用对话框的边距部分
            pageSetupDialog.AllowMargins     = true;
            // 是否启用对话框的“横、纵向”打印
            pageSetupDialog.AllowOrientation = true;
            // 是否启用对话框的纸张部分
            pageSetupDialog.AllowPaper       = true;
            // 是否启用“打印机”按钮
            pageSetupDialog.AllowPrinter     = true;
            // 默认“纵向”
            pageSetupDialog.PageSettings.Landscape = false;

            try 
            { 
                // 显示“页面设置”对话框
                pageSetupDialog.ShowDialog();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
           
            if (System.Globalization.RegionInfo.CurrentRegion.IsMetric)
            {
                // 设置页边距
                pageSetupDialog.PageSettings.Margins = PrinterUnitConvert.Convert(pageSetupDialog.PageSettings.Margins, PrinterUnit.Display, PrinterUnit.TenthsOfAMillimeter);
            }
        }

        #endregion

        #region 打印输出前的操作

        /// <summary>
        /// 设置打印格式
        /// 在调用 Print 方法之后并在打印文档的第一页之前被调用
        /// 基类完成基本初始化，继承的子类根据打印具体内容完成其余初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnBeginPrint(object sender, PrintEventArgs e)
        {
            // 左对齐
            strFmtLeft.Alignment     = StringAlignment.Near;
            strFmtLeft.LineAlignment = StringAlignment.Near;
            strFmtLeft.Trimming      = StringTrimming.EllipsisCharacter;

            // 居中对齐
            strFmtCenter.Alignment     = StringAlignment.Center;
            strFmtCenter.LineAlignment = StringAlignment.Center;
            strFmtCenter.Trimming      = StringTrimming.EllipsisCharacter;

            // 右对齐
            strFmtRight.Alignment     = StringAlignment.Far;
            strFmtRight.LineAlignment = StringAlignment.Far;
            strFmtRight.Trimming      = StringTrimming.EllipsisCharacter;

            // 当前行编号
            rowIndex = 0;
            // 页编号
            pageNo   = 1;
            // 是否打印新的一页
            isNewPage  = true;
        }

        /// <summary>
        /// 设置打印内容,事件在某页打印之前被调用
        /// 基类完成基本的操作，具体打印由子类根据具体的模板实现
        /// </summary>
        /// <param name="dt">准备打印的表格</param>
        /// <param name="e">打印机参数</param>
        protected virtual void OnPrintPage(object sender, PrintPageEventArgs e)
        {
            // 打印脚注
            DrawFooter(e);

            // 全部打印完毕
            e.HasMorePages = false;
        }

        #endregion

        /// <summary>
        /// 打印文档
        /// </summary>
        public void PrintDocument()
        {
            // 打印预览对话框
            PrintPreviewDialog ppvw = new PrintPreviewDialog();
            ppvw.Document = printDocument;

            // 添加打印相关事件
            printDocument.BeginPrint += new PrintEventHandler(OnBeginPrint);     // 事件在调用 Print 方法之后并在打印文档的第一页之前被调用
            printDocument.PrintPage  += new PrintPageEventHandler(OnPrintPage);  // 事件在某页打印之前被调用

            if (ppvw.ShowDialog() != DialogResult.OK)
            {
                printDocument.BeginPrint -= new PrintEventHandler(OnBeginPrint);
                printDocument.PrintPage -= new PrintPageEventHandler(OnPrintPage);

                return;
            }

            // 打印文档
            printDocument.Print();   // 开始文档的打印进程

            // 删除委托事件
            printDocument.BeginPrint -= new PrintEventHandler(OnBeginPrint);
            printDocument.PrintPage -= new PrintPageEventHandler(OnPrintPage);
        }
    }
}
