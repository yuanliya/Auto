using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

using ZY.EntityFrameWork.Core.Repositories.Interface;
using ZY.EntityFrameWork.Core.Context;
using ZY.EntityFrameWork.Core.Model.Entity;

namespace ZY.EntityFrameWork.Core.Repositories.Impl
{
    public class ModuleRepository:BaseRepository<Module>,IModuleRepository
    {
        public ModuleRepository(IUnitOfWorkContext unitContext)
            : base(unitContext) 
        {
        }
    }
}
