using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;

namespace ZY.EntityFrameWork.Core.DBHelper
{
    /// <summary>
    /// 动态创建Lamda表达式树
    /// </summary>
    public static class PredicateExtensionses
    {
        /// <summary>
        /// 创建lambda表达式：p=>true
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Expression<Func<T, bool>> True<T>() { return f => true; }

        /// <summary>
        /// 创建lambda表达式：p=>false
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        /// <summary>
        /// 将两个Lamda表达式构建“and”的连接关系
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="exp_left">“and”的左端Lamda表达式</param>
        /// <param name="exp_right">“and”的右端Lamda表达式</param>
        /// <returns>Lamda表达式</returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> exp_left, Expression<Func<T, bool>> exp_right)
        {
            var candidateExpr = Expression.Parameter(typeof(T), "candidate");
            var parameterReplacer = new ParameterReplacer(candidateExpr);

            var left  = parameterReplacer.Replace(exp_left.Body);  // 获取Lamda表达式的主体
            var right = parameterReplacer.Replace(exp_right.Body); // 获取Lamda表达式的主体
            // 创建按位AND运算的二进制Lamda表达式
            var body  = Expression.And(left, right);

            // 创建一个表示“AND”关系的Lamda表达式
            return Expression.Lambda<Func<T, bool>>(body, candidateExpr);
        }

        /// <summary>
        /// 将两个Lamda表达式构建“or”的连接关系
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="exp_left">“or”的左端Lamda表达式</param>
        /// <param name="exp_right">“or”的右端Lamda表达式</param>
        /// <returns>Lamda表达式</returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> exp_left, Expression<Func<T, bool>> exp_right)
        {
            var candidateExpr = Expression.Parameter(typeof(T), "candidate");
            var parameterReplacer = new ParameterReplacer(candidateExpr);

            var left  = parameterReplacer.Replace(exp_left.Body);
            var right = parameterReplacer.Replace(exp_right.Body);
            var body  = Expression.Or(left, right);

            // 创建一个表示“OR”关系的Lamda表达式
            return Expression.Lambda<Func<T, bool>>(body, candidateExpr);
        }

        /// <summary>
        /// 创建lambda表达式：p=>p.propertyName == propertyValue
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="column"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Equal<T>(string propertyName, object propertyValue)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "p");   //  创建参数p
            MemberExpression    member    = Expression.PropertyOrField(parameter, propertyName);
            ConstantExpression  constant  = Expression.Constant(propertyValue);     //  创建常数

            // 创建一个表示“==”比较的Lamda表达式
            return Expression.Lambda<Func<T, bool>>(Expression.Equal(member, constant), parameter);
        }

        /// <summary>
        /// 创建lambda表达式：p=>p.propertyName != propertyValue
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> NotEqual<T>(string propertyName, object propertyValue)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "p");    // 创建参数p
            MemberExpression    member    = Expression.PropertyOrField(parameter, propertyName);
            ConstantExpression  constant  = Expression.Constant(propertyValue);      // 创建常数

            // 创建一个表示“！=”比较的Lamda表达式
            return Expression.Lambda<Func<T, bool>>(Expression.NotEqual(member, constant), parameter);
        }

        /// <summary>
        /// 创建lambda表达式：p=>p.propertyName > propertyValue
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="column"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> GreaterThan<T>(string propertyName, object propertyValue)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "p"); // 创建参数p
            MemberExpression    member    = Expression.PropertyOrField(parameter, propertyName);
            ConstantExpression  constant  = Expression.Constant(propertyValue);   // 创建常数

            // 创建一个表示“>”比较的Lamda表达式
            return Expression.Lambda<Func<T, bool>>(Expression.GreaterThan(member, constant), parameter);
        }

        /// <summary>
        /// 创建lambda表达式：p=>p.propertyName < propertyValue
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="column"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> LessThan<T>(string propertyName, object propertyValue)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "p");  // 创建参数p
            MemberExpression    member    = Expression.PropertyOrField(parameter, propertyName);
            ConstantExpression  constant  = Expression.Constant(propertyValue);    // 创建常数

            // 创建一个表示“<”比较的Lamda表达式
            return Expression.Lambda<Func<T, bool>>(Expression.LessThan(member, constant), parameter);
        }

        /// <summary>
        /// 创建lambda表达式：p=>p.propertyName >= propertyValue
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="column"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> GreaterThanOrEqual<T>(string propertyName, object propertyValue)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "p"); // 创建参数p
            MemberExpression    member    = Expression.PropertyOrField(parameter, propertyName);
            ConstantExpression  constant  = Expression.Constant(propertyValue);   // 创建常数

            // 创建一个表示“>=”比较的Lamda表达式
            return Expression.Lambda<Func<T, bool>>(Expression.GreaterThanOrEqual(member, constant), parameter);
        }

        /// <summary>
        /// 创建lambda表达式：p=>p.propertyName <= propertyValue
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="column"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> LessThanOrEqual<T>(string propertyName, object propertyValue)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "p");  // 创建参数p
            MemberExpression    member    = Expression.PropertyOrField(parameter, propertyName);
            ConstantExpression  constant  = Expression.Constant(propertyValue);    // 创建常数

            // 创建一个表示“<=”比较的Lamda表达式
            return Expression.Lambda<Func<T, bool>>(Expression.LessThanOrEqual(member, constant), parameter);
        }

        /// <summary>
        /// 创建lambda表达式：p=>p.propertyName.Contains(propertyValue)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="column"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Contains<T>(string propertyName, object propertyValue, string dataType)
        {
            if ((dataType != "String") && (dataType != "Int")) return null;
 
            ParameterExpression parameter = Expression.Parameter(typeof(T), "p");
            MemberExpression    member    = Expression.PropertyOrField(parameter, propertyName);
            ConstantExpression constant   = Expression.Constant(propertyValue, typeof(string));

            MethodInfo method;
            if (dataType == "String")
            {
                method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            }
            else
            {
                method = typeof(string).GetMethod("Contains", new[] { typeof(int) });
            }          

            // 创建一个表示包含关系的Lamda表达式
            return Expression.Lambda<Func<T, bool>>(Expression.Call(member, method, constant), parameter);
        }

        /// <summary>
        /// 创建lambda表达式：!(p=>p.propertyName.Contains(propertyValue))
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="column"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> NotContains<T>(string propertyName, object propertyValue, string dataType)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "p");
            MemberExpression    member    = Expression.PropertyOrField(parameter, propertyName);
            ConstantExpression constant   = Expression.Constant(propertyValue, typeof(string));

            MethodInfo method;
            if (dataType == "String")
            {
                method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            }
            else
            {
                method = typeof(string).GetMethod("Contains", new[] { typeof(int) });
            }        

            // 创建一个表示不包含关系的Lamda表达式
            return Expression.Lambda<Func<T, bool>>(Expression.Not(Expression.Call(member, method, constant)), parameter);
        }
    }

    /// <summary>
    /// ExpressionVisitor遍历Lamda树
    /// </summary>
    internal class ParameterReplacer : ExpressionVisitor
    {
        public ParameterReplacer(ParameterExpression paramExpr)
        {
            this.ParameterExpression = paramExpr;
        }

        public ParameterExpression ParameterExpression { get; private set; }

        /// <summary>
        /// 访问Lamda表达式树
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        public Expression Replace(Expression expr)
        {
            return this.Visit(expr);
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            return this.ParameterExpression;
        }
    }
}
