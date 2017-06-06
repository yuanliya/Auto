using System.Data.Entity.ModelConfiguration;

using ZY.EntityFrameWork.Core.Model.Entity;

namespace ZY.EntityFrameWork.Core.Mapping.MySQL
{
    public class ModuleMap : EntityTypeConfiguration<Module>
    {
        public ModuleMap()
        {
            ToTable("ModuleInfo").HasKey(q => q.ID);  // 主键

            HasMany(q => q.ChildModule)               // Module和ChildModule1：N
                .WithOptional()                       // ChildModule为可选，可以为空
                .HasForeignKey(q => q.ParentID);      // ParentID为外键
        }
    }
}
