using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;

namespace NJUST.AUTO06.Utility
{
    /// <summary>
    /// 继承类，定义打印的具体内容
    /// </summary>
    public class MyPrinter : PrintUtilBase
    {
        List<string> listTop = new List<string>();
        List<string> listBottom = new List<string>();

        /// <summary>
        /// 打印页面前的初始化操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnBeginPrint(object sender, PrintEventArgs e)
        {
            // 调用基类的打印参数初始化函数
            base.OnBeginPrint(sender, e);

            //// 初始化需要打印的文本
            //listTop.Add("拟调入：");
            //listTop.Add("    兹转去干档字第1111号abc同志等5人档案材料共计10 卷(份)，请查收后将回执退回我部。，请查收后将回执退回我部。，请查收后将回执退回我部。，请查收后将回执退回我部。，请查收后将回执退回我部。");
            //listTop.Add("承办人：          \t");
            //listTop.Add("南京军区政治部干部信息中心");
            //listTop.Add(DateTime.Now.Year + " 年 " + DateTime.Now.Month + " 月 " + DateTime.Now.Day + " 日");

            //listBottom.Add("请退回执");
            //listBottom.Add("南京军区政治部干部信息中心：");
            //listBottom.Add("    你部转来干档字第1111号abc同志等5 人档案材料共计 11 卷(份)，已如数收到，现将回执退回。");
            //listBottom.Add("       收 件 机 关");
            //listBottom.Add("收 材 料 人");
            //listBottom.Add("       盖       章");
            //listBottom.Add("签 名 盖 章");
            //listBottom.Add("年    月     日");
        }

        /// <summary>
        /// 设置打印内容,事件在某页打印之前被调用
        /// </summary>
        /// <param name="dt">准备打印的表格</param>
        /// <param name="e">打印机参数</param>
        protected override void OnPrintPage(object sender, PrintPageEventArgs e)
        {
            int marginTop = 0;
            int x0 = Convert.ToInt32(e.MarginBounds.Left), y0 = Convert.ToInt32(e.MarginBounds.Top);

            if (pageNo == 1)
            {
                // 第一页计算表格打印参数
                CalTablePrintParams(e);

                // 打印出库单
                if (listTop.Count != 0)
                {
                    marginTop = DrawText(listTop[0].ToString(), strFmtLeft, x0, y0, e);
                    y0 += marginTop;
                    marginTop = DrawText(listTop[1].ToString(), strFmtLeft, x0, y0, e);
                    y0 += marginTop;
                    marginTop = DrawText(listTop[2].ToString(), strFmtRight, x0, y0, e);
                    y0 += marginTop;
                    marginTop = DrawText(listTop[3].ToString(), strFmtRight, x0, y0, e);
                    y0 += marginTop;
                    marginTop = DrawText(listTop[4].ToString(), strFmtRight, x0, y0, e);
                    y0 += marginTop;
                }
            }

            // 逐行打印表格
            marginTop = y0 + 10;
            while (rowIndex <= PrintDataTable.Rows.Count - 1)
            {
                DataRow dr = PrintDataTable.Rows[rowIndex];

                // 判断是否打印到了本页底部
                if ((marginTop + rowHeight[rowIndex]) >= e.MarginBounds.Top + e.MarginBounds.Height)
                {
                    // 打印脚注
                    DrawFooter(e);

                    // 进入新一页打印
                    isNewPage = true;
                    // 更新页编码
                    pageNo++;

                    e.HasMorePages = true;

                    return;
                }
                else
                {
                    if (isNewPage)
                    {
                        // 打印抬头
                        DrawHeader(e);

                        // 画表格的标题栏
                        DrawTableHeader(marginTop, e);

                        isNewPage = false;

                        marginTop += columnTitleHeight;
                    }

                    // 画表格具体内容
                    DrawTableRow(dr, rowIndex, marginTop, e);

                    marginTop += rowHeight[rowIndex];
                }

                rowIndex++;
            }

            // 调用基类函数，完成打印
            base.OnPrintPage(sender, e);
        }
    }
}
