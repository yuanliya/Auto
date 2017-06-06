using System.Data.Entity.ModelConfiguration;

using ZY.EntityFrameWork.Core.Model.Entity;

namespace ZY.EntityFrameWork.Core.Mapping.MySQL
{
    public class DeviceMap : EntityTypeConfiguration<Device>
    {
         public DeviceMap()
         {
             ToTable("DeviceInfo").HasKey(q => q.ID);  // 主键
             Property(m => m.CabinetNo).IsRequired();  // 必须字段
         }        
    }
}
