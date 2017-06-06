using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;
using ZY.EntityFrameWork.Core.Model.Entity;
using ZY.EntityFrameWork.Core.Repositories.Interface;
using ZY.EntityFrameWork.Core.Context;

namespace ZY.EntityFrameWork.Core.Repositories.Impl
{
    public class ArvLocationRepository:BaseRepository<ArvLocation>,IArvLocationRepository
    {
        public ArvLocationRepository(IUnitOfWorkContext unitContext) : base(unitContext) 
        {
        }
    }
}
