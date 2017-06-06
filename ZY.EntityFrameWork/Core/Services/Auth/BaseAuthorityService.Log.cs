using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ZY.EntityFrameWork.Core.DBHelper;
using ZY.EntityFrameWork.Core.Model.Entity;

namespace ZY.EntityFrameWork.Core.Services
{
    public partial class BaseAuthorityService
    {
        /// <summary>
        /// 查找所有日志信息
        /// </summary>
        /// <returns>结果集</returns>
         public List<Log> GetAllLog()
        {
            return logRepository.GetQueryable().ToList();
        }

        //public List<LogItem> GetLogItems(Expression<Func<LogItem, bool>> exp)
        //{
        //    return logRepository.GetQueryable().Where(exp).ToList();
        //}

        public List<Log> GetLogItems(IList<QueryCondition> condition)
        {
            Expression<Func<Log, bool>> exp = LamdaExpressionHelper.BuildExpression<Log>(condition);
            return logRepository.GetQueryable().Where(exp).ToList();
        }

        /// <summary>
        /// 根据条件删除日志
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public int DeleteLogItems(Expression<Func<Log,bool>> exp)
        {
            return logRepository.Delete(exp);
        }

    }
}
