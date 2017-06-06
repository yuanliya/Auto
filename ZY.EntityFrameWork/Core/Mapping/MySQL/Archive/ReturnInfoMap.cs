using System.Data.Entity.ModelConfiguration;

using ZY.EntityFrameWork.Core.Model.Entity;

namespace ZY.EntityFrameWork.Core.Mapping.MySQL
{
    class ArvReturnInfoMap : EntityTypeConfiguration<ArvReturnInfo>
    {
        public ArvReturnInfoMap()
        {
            ToTable("ArvReturnInfo").HasKey(q => q.ID); // 主键
        }
    }
}
