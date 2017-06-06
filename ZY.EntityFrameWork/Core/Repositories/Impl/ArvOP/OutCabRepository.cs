using ZY.EntityFrameWork.Core.Model.Entity;
using ZY.EntityFrameWork.Core.Context;
using ZY.EntityFrameWork.Core.Repositories.Interface;

namespace ZY.EntityFrameWork.Core.Repositories.Impl
{
    class OutCabRepository:BaseRepository<OutCabInfo>,IOutCabRepository
    {
        public OutCabRepository(IUnitOfWorkContext unitContext) : base(unitContext)
        {
        }
    }
}
