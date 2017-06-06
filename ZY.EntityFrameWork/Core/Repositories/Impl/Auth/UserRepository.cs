using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

using ZY.EntityFrameWork.Core.Repositories.Interface;
using ZY.EntityFrameWork.Core.Context;
using ZY.EntityFrameWork.Core.Model.Entity;

namespace ZY.EntityFrameWork.Core.Repositories.Impl
{
    public class UserRepository:BaseRepository<User>,IUserRepository
    {
        public UserRepository(IUnitOfWorkContext unitContext) : base(unitContext) 
        {
        }
    }
}
