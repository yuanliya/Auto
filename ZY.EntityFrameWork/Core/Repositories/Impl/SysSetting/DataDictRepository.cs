using ZY.EntityFrameWork.Core.Repositories.Interface;
using ZY.EntityFrameWork.Core.Model.Entity;
using ZY.EntityFrameWork.Core.Context;

namespace ZY.EntityFrameWork.Core.Repositories.Impl
{
    public class DataDictRepository : BaseRepository<DataDict>, IDataDictRepository
    {
        public DataDictRepository(IUnitOfWorkContext unitContext) : base(unitContext) 
        {
        }
    }
}
