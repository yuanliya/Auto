using System.Data.Entity.ModelConfiguration;

using ZY.EntityFrameWork.Core.Model.Entity;

namespace ZY.EntityFrameWork.Core.Mapping.MySQL
{
    public class DataDictMap : EntityTypeConfiguration<DataDict>
    {
        public DataDictMap()
        {
            ToTable("DataDictInfo").HasKey(q => q.ID);     // 主键
            Property(q => q.FieldTypeName).IsRequired();   // 必须字段
        }
    }
}
