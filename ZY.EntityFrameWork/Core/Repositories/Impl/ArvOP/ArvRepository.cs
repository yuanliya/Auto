using ZY.EntityFrameWork.Core.Model.Entity;
using ZY.EntityFrameWork.Core.Repositories.Interface;
using ZY.EntityFrameWork.Core.Context;

using System.Linq;

namespace ZY.EntityFrameWork.Core.Repositories.Impl
{
    public class ArvRepository : BaseRepository<ArchiveInfo>, IArvRepository
    {
        public ArvRepository(IUnitOfWorkContext unitContext) : base(unitContext)
        {         
        }

        /// <summary>
        /// 查询包含所属档案盒信息的所有档案信息
        /// Include(Entity)表示“预先装入”，要求从数据库中读取全部的相关实体信息到内存
        /// Load(Entity)表示“显式装入”，用于有条件从数据库读取信息到内存
        /// </summary>
        /// <returns></returns>
        public IQueryable<ArchiveInfo> GetArvIncludeAll()
        {
            return EFContext.Set<ArchiveInfo>().Include("ArvBox");
        }
    }
}
