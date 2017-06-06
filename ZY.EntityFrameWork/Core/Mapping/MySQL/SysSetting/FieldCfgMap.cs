using System.Data.Entity.ModelConfiguration;

using ZY.EntityFrameWork.Core.Model.Entity;

namespace ZY.EntityFrameWork.Core.Mapping.MySQL
{
    public class FieldCfgMap : EntityTypeConfiguration<FieldCfg>
    {
        public FieldCfgMap()
        {
            ToTable("FiledCfgInfo").HasKey(q => q.ID); // 主键
        }
    }
}
