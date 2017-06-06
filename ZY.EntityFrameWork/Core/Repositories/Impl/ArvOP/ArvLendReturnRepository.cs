using ZY.EntityFrameWork.Core.Context;
using ZY.EntityFrameWork.Core.Model.Entity;
using ZY.EntityFrameWork.Core.Repositories.Interface;

namespace ZY.EntityFrameWork.Core.Repositories.Impl
{
    public class ArvLendReturnRepository : BaseRepository<ArvLendReturn>, IArvLendReturnRepository
    {
        public ArvLendReturnRepository(IUnitOfWorkContext unitContext)
            : base(unitContext) 
        {
        }
    }
}
