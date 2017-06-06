using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

using ZY.EntityFrameWork.Core.Repositories.Interface;
using ZY.EntityFrameWork.Core.Context;
using ZY.EntityFrameWork.Core.Model.Entity;

namespace ZY.EntityFrameWork.Core.Repositories.Impl
{
    public class LogRepository:BaseRepository<Log>,ILogRepository
    {
        public LogRepository(IUnitOfWorkContext unitContext) : base(unitContext) 
        {
        }
    }
}
