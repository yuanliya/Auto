using System.Data.Entity.ModelConfiguration;

using ZY.EntityFrameWork.Core.Model.Entity;

namespace ZY.EntityFrameWork.Core.Mapping.MySQL
{
    public class LogMap : EntityTypeConfiguration<Log>
    {
        public LogMap()
        {
            ToTable("LogInfo").HasKey(q => q.ID);   // 主键
        }
    }
}
