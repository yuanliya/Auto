using ZY.EntityFrameWork.Core.Repositories.Interface;
using ZY.EntityFrameWork.Core.Model.Entity;
using ZY.EntityFrameWork.Core.Context;

namespace ZY.EntityFrameWork.Core.Repositories.Impl
{
    public class DeviceRepository : BaseRepository<Device>, IDeviceRepository
    {
        public DeviceRepository(IUnitOfWorkContext unitContext) : base(unitContext) 
        {
        }
    }
}
