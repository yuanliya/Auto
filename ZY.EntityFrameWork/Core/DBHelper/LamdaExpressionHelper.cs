using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ZY.EntityFrameWork.Core.DBHelper
{
    /// <summary>
    /// 利用查询条件集合动态构建Lamda表达式
    /// </summary>
    public static class LamdaExpressionHelper
    {
        /// <summary>
        /// 动态构建Lambda查询表达式
        /// </summary>
        /// <param name="searchItems">查询条件集合</param>
        /// <returns>Lambda查询表达式</returns>
        public static Expression<Func<T, bool>> BuildExpression<T>(IList<QueryCondition> queryItems)
        {
            if (queryItems == null) return null;

            // 创建lambda表达式：{p => true}
            var where = PredicateExtensionses.True<T>();

            // 检索条件不包含任何元素
            if (!queryItems.Any()) return @where;

            // 构建Lamda查询表达式
            foreach (var subitem in queryItems)
            {
                try
                {
                    var field = subitem.FieldName;
                    var compare = subitem.Type;
                    var type = subitem.DataType;
                    var value = subitem.Value;

                    if (string.IsNullOrEmpty(field)) continue;
                    //if (string.IsNullOrEmpty(value)) continue;
                    if (value == null) continue;
                    // Lamda表达式树,初始化默认“==”
                    Expression<Func<T, bool>> lambda = PredicateExtensionses.Equal<T>(field, value);

                    // 比较表达式
                    switch (compare)
                    {
                        case CompareType.GreaterThan:
                            lambda = PredicateExtensionses.GreaterThan<T>(field, value);
                            break;
                        case CompareType.GreaterThanOrEqual:
                            lambda = PredicateExtensionses.GreaterThanOrEqual<T>(field, value);
                            break;
                        case CompareType.LessThan:
                            lambda = PredicateExtensionses.LessThan<T>(field, value);
                            break;
                        case CompareType.LessThanOrEqual:
                            lambda = PredicateExtensionses.LessThanOrEqual<T>(field, value);
                            break;
                        case CompareType.Include:
                            // like 查询，需要调用外部int或string的Contains方法
                            if (type == CompareDataType.Int)
                            {
                                lambda = PredicateExtensionses.Contains<T>(field, value, "Int");
                            }
                            else
                            {
                                lambda = PredicateExtensionses.Contains<T>(field, value, "String");
                            }
                            break;
                        case CompareType.Equal:
                            break;
                        default:
                            break;
                    }

                    // 构建Lamda表达式树
                    @where = @where.And(lambda);
                }
                catch (Exception ex)
                {
                    ExceptionHelper.ThrowDataAccessException("服务接口层函数BuildExpression<T>()无法构建Lamda表达式!", ex);
                }
            }

            return @where;
        }

        /// <summary>
        /// 建立关键字索引的Lamda表达式
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <typeparam name="TKey">关键字</typeparam>
        /// <param name="item">查询条件</param>
        /// <returns></returns>
        public static Expression<Func<T, TKey>> BuildKeySelector<T, TKey>(QueryCondition item)
        {
            ParameterExpression param = Expression.Parameter(typeof(T), "p");        // 主体，“p”是显示的名称
            MemberExpression body = Expression.Property(param, item.FieldName);  // 表达式的类型是属性

            return Expression.Lambda<Func<T, TKey>>(body, param);
        }

        /// <summary>
        /// 构建Lamda表达式的委托
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="queryItems">查询条件集合</param>
        /// <returns>Lamda表达式的委托</returns>
        public static Func<T, bool> BuildLamda<T>(IList<QueryCondition> queryItems)
        {
            return BuildExpression<T>(queryItems).Compile();
        }
    }
}
