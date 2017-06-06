using System.Data.Entity.ModelConfiguration;

using ZY.EntityFrameWork.Core.Model.Entity;

namespace ZY.EntityFrameWork.Core.Mapping.MySQL
{
    public class RoleModuleMap : EntityTypeConfiguration<RoleModule>
    {
        public RoleModuleMap()
        {
            ToTable("RoleModuleInfo").HasKey(q => q.ID); // 主键

            HasRequired(q => q.Module)                   // RoleModule实体中，Module不能为空
                .WithMany(q => q.RoleModules)            
                .HasForeignKey(q => q.ModuleId);
            
            //HasRequired(q => q.Role).WithMany(q => q.RoleModules).HasForeignKey(q => q.RoleId);
            //HasRequired(q => q.Permission).WithMany(q => q.ModulePermissions).HasForeignKey(q => q.PermissionId);
        }
    }
}
