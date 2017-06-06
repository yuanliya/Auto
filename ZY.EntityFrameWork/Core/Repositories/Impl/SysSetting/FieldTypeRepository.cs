using ZY.EntityFrameWork.Core.Context;
using ZY.EntityFrameWork.Core.Model.Entity;
using ZY.EntityFrameWork.Core.Repositories.Interface;

namespace ZY.EntityFrameWork.Core.Repositories.Impl
{
    public class FieldTypeRepository : BaseRepository<FieldCfg>, IFieldTypeRepository
    {
        public FieldTypeRepository(IUnitOfWorkContext unitContext) : base(unitContext)
        {
        }
    }
}
