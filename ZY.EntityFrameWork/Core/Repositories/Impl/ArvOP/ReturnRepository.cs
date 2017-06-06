using ZY.EntityFrameWork.Core.Model.Entity;
using ZY.EntityFrameWork.Core.Context;
using ZY.EntityFrameWork.Core.Repositories.Interface;

namespace ZY.EntityFrameWork.Core.Repositories.Impl
{
    public class ReturnRepository : BaseRepository<ArvReturnInfo>, IReturnRepository
    {
        public ReturnRepository(IUnitOfWorkContext unitContext) : base(unitContext)
        {
        }
    }
}
