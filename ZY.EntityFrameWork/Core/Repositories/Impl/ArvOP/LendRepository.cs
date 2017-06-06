using ZY.EntityFrameWork.Core.Context;
using ZY.EntityFrameWork.Core.Model.Entity;
using ZY.EntityFrameWork.Core.Repositories.Interface;

namespace ZY.EntityFrameWork.Core.Repositories.Impl
{
    public class LendRepository : BaseRepository<ArvLendInfo>, ILendRepository
    {
        public LendRepository(IUnitOfWorkContext unitContext) : base(unitContext) 
        {
        }
    }
}
