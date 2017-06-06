using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing.Imaging;
using System.Collections;
using DevExpress.XtraCharts;
using DevExpress.XtraTab;
using ZY.EntityFrameWork.Core.Model.Dto;
using ZY.EntityFrameWork.Caller;
using ZY.EntityFrameWork.Caller.Facade;
using DevExpress.XtraEditors;
using ZY.EntityFrameWork.Core.Model.Entity.Archive;
using ZY.EntityFrameWork.Core.DBHelper;
using AutoCabinet2017.Helper;


namespace AutoCabinet2017.UI.Report
{
    public partial class FormArvCount : XtraForm
    {
        public FormArvCount()
        {
            InitializeComponent();
        }

        List<ArchiveInfoDto> allArv = new List<ArchiveInfoDto>();

        string condition = null;//查询条件

        private void FormAMArvCount_Load(object sender, EventArgs e)
        {
            //allArv = new BindingList<ArchiveInfo>(ArchiveBLL.Instance.Find("*", ""));
            allArv = CallerFactory.Instance.GetService<IArvOpService>().GetArvInLib();//这样不好，应直接在service中增加统计吗
            cbxCondition.SelectedIndex = 0;

            DrawChart("ArvStatus","柱形图");//默认按档案状态显示柱形图
        }


        /// <summary>
        /// 绘制柱形图
        /// </summary>
        /// <param name="bindingTable"></param>
        public void DrawBar(DataTable bindingTable)
        {
            Series series = new Series("Series1", ViewType.Bar);
            chart.Series.Add(series);

            series.DataSource = bindingTable;

            series.ArgumentScaleType = ScaleType.Auto;
            series.ArgumentDataMember = "Argument";
            series.ValueScaleType = ScaleType.Numerical;
            series.ValueDataMembers.AddRange(new string[] { "Value" });

            ((SideBySideBarSeriesView)series.View).ColorEach = true;
            chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
        }

        public void DrawLine(DataTable bindingTable)
        {
            Series series = new Series("Series1", ViewType.Line);
            chart.Series.Add(series);

            series.DataSource = bindingTable;

            series.ArgumentScaleType = ScaleType.Auto;
            series.ArgumentDataMember = "Argument";
            series.ValueScaleType = ScaleType.Numerical;
            series.ValueDataMembers.AddRange(new string[] { "Value" });

            // Access the view-type-specific options of the series.
            ((LineSeriesView)series.View).LineMarkerOptions.Kind = MarkerKind.Triangle;
            ((LineSeriesView)series.View).LineStyle.DashStyle = DevExpress.XtraCharts.DashStyle.Dash;

            // Access the type-specific options of the diagram.
            ((XYDiagram)chart.Diagram).EnableAxisXZooming = true;

            // Hide the legend (if necessary).
            chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
        }

        /// <summary>
        /// 绘制饼状图
        /// </summary>
        /// <param name="bindingTable"></param>
        public void DrawPie(DataTable bindingTable)
        {
            Series series = new Series("Series1", ViewType.Pie);
            chart.Series.Add(series);

            series.DataSource = bindingTable;

            series.ArgumentScaleType = ScaleType.Auto;
            series.ArgumentDataMember = "Argument";
            series.ValueScaleType = ScaleType.Numerical;
            series.ValueDataMembers.AddRange(new string[] { "Value" });

            chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;

            //series.Label.TextPattern = "{A}: {VP:p0}";无法设置

            // 调整标签位置
            ((PieSeriesLabel)series.Label).Position = PieSeriesLabelPosition.TwoColumns;

            PieSeriesView myView = (PieSeriesView)series.View;

            myView.ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Value_1,
                DataFilterCondition.GreaterThanOrEqual, 9));
            myView.ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Argument,
                DataFilterCondition.NotEqual, "Others"));
            myView.ExplodeMode = PieExplodeMode.UseFilters;
            myView.ExplodedDistancePercentage = 30;
            myView.RuntimeExploding = true;
            myView.HeightToWidthRatio = 0.75;
        }

        /// <summary>
        /// 绘制柱形图
        /// </summary>
        /// <param name="bindingTable"></param>
        public void DrawBar(List<DataMap<string>> bindingTable)
        {
            Series series = new Series("Series1", ViewType.Bar);
            chart.Series.Add(series);

            series.DataSource = bindingTable;

            series.ArgumentScaleType = ScaleType.Auto;
            series.ArgumentDataMember = "Key";
            series.ValueScaleType = ScaleType.Numerical;
            series.ValueDataMembers.AddRange(new string[] { "Value" });

            ((SideBySideBarSeriesView)series.View).ColorEach = true;
            chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
        }

        public void DrawLine(List<DataMap<string>> bindingTable)
        {
            Series series = new Series("Series1", ViewType.Line);
            chart.Series.Add(series);

            series.DataSource = bindingTable;

            series.ArgumentScaleType = ScaleType.Auto;
            series.ArgumentDataMember = "Key";
            series.ValueScaleType = ScaleType.Numerical;
            series.ValueDataMembers.AddRange(new string[] { "Value" });

            // Access the view-type-specific options of the series.
            ((LineSeriesView)series.View).LineMarkerOptions.Kind = MarkerKind.Triangle;
            ((LineSeriesView)series.View).LineStyle.DashStyle = DevExpress.XtraCharts.DashStyle.Dash;

            // Access the type-specific options of the diagram.
            ((XYDiagram)chart.Diagram).EnableAxisXZooming = true;

            // Hide the legend (if necessary).
            chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
        }

        /// <summary>
        /// 绘制饼状图
        /// </summary>
        /// <param name="bindingTable"></param>
        public void DrawPie(List<DataMap<string>> bindingTable)
        {
            Series series = new Series("Series1", ViewType.Pie);
            chart.Series.Add(series);

            series.DataSource = bindingTable;

            series.ArgumentScaleType = ScaleType.Auto;
            series.ArgumentDataMember = "Key";
            series.ValueScaleType = ScaleType.Numerical;
            series.ValueDataMembers.AddRange(new string[] { "Value" });

            chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;

            //series.Label.TextPattern = "{A}: {VP:p0}";无法设置

            // 调整标签位置
            ((PieSeriesLabel)series.Label).Position = PieSeriesLabelPosition.TwoColumns;

            PieSeriesView myView = (PieSeriesView)series.View;

            myView.ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Value_1,
                DataFilterCondition.GreaterThanOrEqual, 9));
            myView.ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Argument,
                DataFilterCondition.NotEqual, "Others"));
            myView.ExplodeMode = PieExplodeMode.UseFilters;
            myView.ExplodedDistancePercentage = 30;
            myView.RuntimeExploding = true;
            myView.HeightToWidthRatio = 0.75;
        }



        /// <summary>
        /// 根据条件绘图
        /// </summary>
        /// <param name="allArv"></param>
        /// <param name="condition"></param>
        private void DrawChart(string condition,string ChartType)
        {
            List<DataMap<string>> bindingList = new List<DataMap<string>>();

            if (condition == "GroupNo")
            {
              //  List<ArvLocationDto> loc = CallerFactory.Instance.GetService<IArvOpService>().FindArvLocations(null);
              //  bindingList = loc.GroupBy(q => q.GetType().GetProperty(condition).GetValue(q, null)).Select(q => new DataMap<string> { Key = q.Key.ToString(), Value = q.Count() }).ToList();
            }
            else
            {
                QueryConditionModel model=new QueryConditionModel(condition);
                bindingList = CallerFactory.Instance.GetService<IArvOpService>().GetGroupCount<string>(null, model);
            }
           
            chart.Series.Clear();
           
            if(bindingList!=null)
            {
                if (ChartType == "柱形图")
                {
                    DrawBar(bindingList);
                }

                else if (ChartType == "折线图")
                {
                    DrawLine(bindingList);
                }
                else
                {
                    DrawPie(bindingList);
                }
            }
            

            gcCount.DataSource = bindingList;

            colValue.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            colValue.SummaryItem.DisplayFormat = "档案总数为：{0}";
        }

        private class Match
        {
            public string Key { get; set; }
            public int Value { get; set; }
        }

       
        private void toolBtnAllQuery_Click(object sender, EventArgs e)
        {

            switch (cbxCondition.Text)
            {
                case "按档案状态统计": condition = "ArvStatus";goto default;
                case "按档案类别统计": condition = "ArvType"; goto default;
                case "按部门类别": condition = "ArvUnit"; goto default;
                case "按年限统计": condition = "ArvYear"; goto default;
                case "按存储位置统计": condition = "GroupNo"; goto default;
                case "按入库时间统计": 
                    condition = "CreateTime";
                    DrawChart(condition, "折线图");
                    break;
                default:
                    
                    if(condition!=null)
                        DrawChart(condition, "柱形图");
                    break;
            }

        }

        private void cbxCondition_Click(object sender, EventArgs e)
        {
            if(cbxCondition.Text=="按入库时间统计")
            {
                cbxTime.Visible = true;
                cbxTime.SelectedIndex = cbxTime.Items.Count - 1;//默认为所有时间
            }
            else
                cbxTime.Visible = false;
        }
    }
}
