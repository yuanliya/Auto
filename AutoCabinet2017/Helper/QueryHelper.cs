using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZY.EntityFrameWork.Core.DBHelper;

namespace AutoCabinet2017.Helper
{
    /// <summary>
    /// 构件查询条件表达式
    /// </summary>
    public class QueryHelper
    {
        /// <summary>
        /// 根据面板内所有控件内容，构建综合的查询条件
        /// </summary>
        /// <param name="panel">包含搜索内容的面板</param>
        /// <returns>查询条件集合</returns>
        public static List<QueryCondition> GetIntegrativeCondition(Panel panel)
        {
            List<QueryCondition> conditions = new List<QueryCondition>();

            // 遍历面板内的控件
            foreach(Control ctrl in panel.Controls)
            {
                string text = null;

                // 针对编辑框构建查询条件
                if(ctrl is TextEdit &&!(ctrl is DateEdit))
                {
                    // 文本框
                    text = (ctrl as TextEdit).Text;
                }
                else if(ctrl is ComboBoxEdit)
                {
                    // 下拉框
                    text = (ctrl as ComboBoxEdit).Text;
                }
                if (text == null)
                    continue;
            
                // 构建条件表达式集合
                QueryCondition condition = new QueryCondition(ctrl.Tag.ToString(), CompareType.Include, (ctrl as TextEdit).Text);
                conditions.Add(condition);
            }

            return conditions;
        }
       
        /// <summary>
        /// 针对时间区间生成查询条件
        /// </summary>
        /// <param name="from">起始时间</param>
        /// <param name="to">结束时间</param>
        /// <param name="field">字段名</param>
        /// <returns>查询条件集合</returns>
        public static List<QueryCondition> GetTimeCondition(DateTime from, DateTime to, string field)
        {
            List<QueryCondition> condition = new List<QueryCondition>();

            // 起始时间
            if (from != DateTime.MinValue)
            {
                QueryCondition cFrom = new QueryCondition()
                {
                    FieldName = field,
                    DataType  = CompareDataType.DateTime,
                    Value     = from,
                    Type      = CompareType.GreaterThan
                };

                condition.Add(cFrom);
            }

            // 截止时间
            if (to != DateTime.MinValue)
            {
                QueryCondition cTo = new QueryCondition()
                {
                    FieldName = field,
                    DataType  = CompareDataType.DateTime,
                    Value     = to,
                    Type      = CompareType.LessThan
                };

                condition.Add(cTo);
            }

            return condition;
        }

        /// <summary>
        /// 查询时间段的查询表达式
        /// </summary>
        /// <param name="days">以当天为基准，间隔的天数</param>
        /// <param name="field">查询字段</param>
        /// <returns></returns>
        public static List<QueryCondition> GetTimeCondition(int days, string field)
        {
            // ~days表示以当前时间为基准，向前的天数
            return GetTimeCondition(DateTime.Now.AddDays(~days), DateTime.MinValue, field);
        }

        /// <summary>
        /// 直接构建查询关系
        /// </summary>
        /// <param name="field">字段名</param>
        /// <param name="cType">包含关系</param>
        /// <param name="value">查询内容</param>
        /// <returns>查询条件集合</returns>
        public static List<QueryCondition> GetConditionList(string field, CompareType cType, string value)
        {
            List<QueryCondition> conditions = new List<QueryCondition>();
            
            QueryCondition conditon = new QueryCondition(field, cType, value);
            conditions.Add(conditon);
           
            return conditions;
        }

        /// <summary>
        /// 获取查询条件模型
        /// </summary>
        /// <param name="field">字段名</param>
        /// <param name="cType">包含关系</param>
        /// <param name="value">字段值</param>
        /// <returns>查询条件</returns>
        public static QueryCondition GetConditionModel(string field, CompareType cType, string value)
        {
            return new QueryCondition(field, cType, value);
        }
    }
}
