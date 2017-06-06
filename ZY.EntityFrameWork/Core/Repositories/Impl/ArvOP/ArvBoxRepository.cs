using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;

using ZY.EntityFrameWork.Core.Model.Entity;
using ZY.EntityFrameWork.Core.Repositories.Interface;
using ZY.EntityFrameWork.Core.Context;

namespace ZY.EntityFrameWork.Core.Repositories.Impl
{
    public class ArvBoxRepository:BaseRepository<ArvBox>,IArvBoxRepository
    {
        public ArvBoxRepository(IUnitOfWorkContext unitContext) : base(unitContext) 
        {        
        }

        /// <summary>
        /// 查找符合Lamda表达式设定条件的记录
        /// </summary>
        /// <param name="match">Lamda查询表达式</param>
        /// <returns>记录集合</returns>
        public override IQueryable<ArvBox> Find(Expression<Func<ArvBox, bool>> match)
        {
            return base.Find(match);
        }
    }
}
