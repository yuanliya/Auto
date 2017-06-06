using System.Data.Entity.ModelConfiguration;

using ZY.EntityFrameWork.Core.Model.Entity;

namespace ZY.EntityFrameWork.Core.Mapping.MySQL
{
    class OutCabInfoMap : EntityTypeConfiguration<OutCabInfo>
    {
        public OutCabInfoMap()
        {
            ToTable("OutCabInfo").HasKey(q => q.ID);  // 主键
            Property(m => m.ArvID).IsRequired();
        }
    }
}
