using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ZY.EntityFrameWork.Core.Model.Entity;

namespace ZY.EntityFrameWork.Core.Repositories.Interface
{
    public interface IArvRepository : IBaseRepository<ArchiveInfo>
    {
        /// <summary>
        /// 读取档案全部信息到内存
        /// </summary>
        /// <returns></returns>
        IQueryable<ArchiveInfo> GetArvIncludeAll();
    }
}
