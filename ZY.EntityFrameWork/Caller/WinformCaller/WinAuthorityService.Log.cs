using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ZY.EntityFrameWork.Caller.Facade;
using ZY.EntityFrameWork.Core.DBHelper;
using ZY.EntityFrameWork.Core.Model.Dto;
using ZY.EntityFrameWork.Core.Model.Entity;
using ZY.EntityFrameWork.Core.Services;

namespace ZY.EntityFrameWork.Caller.WinformCaller
{
    public partial class WinAuthorityService
    {
        /// <summary>
        /// 查找所有日志信息
        /// </summary>
        /// <returns>日志Dto对象集</returns>
        public List<LogDto> GetAllLog()
        {
            return baseAuthorityService.GetAllLog().MapTo<List<LogDto>>();
        }

        /// <summary>
        /// 条件查找日志
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>日志Dto对象集</returns>
        public  List<LogDto> GetLog(IList<QueryCondition> condition)
        {          
            return baseAuthorityService.GetLogItems(condition).MapTo<List<LogDto>>();
        }

        //public int DeleteLogItems(Expression<Func<LogDto,bool>> exp)
        //{
        //    ParameterExpression param = Expression.Parameter(typeof(LogItem),exp.Parameters[0].Name);
        //    Expression<Func<LogItem, bool>> keySelector = (Expression<Func<LogItem, bool>>)Expression.Lambda(exp.Body, param);
        //    //keySelector = q => DateTime.Compare(q.Datetime, DateTime.Now) < 0;
        //    return baseAuthorityService.DeleteLogItems(keySelector);
        //}

        /// <summary>
        /// 删除日志
        /// </summary>
        /// <returns>操作是否成功</returns>
        public int DeleteRecentLog()
        {
            DateTime dt = DateTime.Now.AddMinutes(-20);//测试，删除20分钟前的日志
            return baseAuthorityService.DeleteLogItems(q => DateTime.Compare(q.Datetime, dt) < 0);
        }
    }
}
