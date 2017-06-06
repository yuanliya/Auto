using System.Data.Entity.ModelConfiguration;

using ZY.EntityFrameWork.Core.Model.Entity;

namespace ZY.EntityFrameWork.Core.Mapping.MySQL
{
    public class RoleMap: EntityTypeConfiguration<Role>
    {
        public RoleMap()
        {
            ToTable("RoleInfo").HasKey(q => q.ID);   // 主键
            
            HasMany(q => q.Users)                    // Role和Users是1：N 
                .WithRequired(q => q.UserRole)       // User实体中，UserRole为必须，不可为空
                .HasForeignKey(q => q.RoleId);       // User实体中，RoleId为外键  

            HasMany(q => q.RoleModules)              // Role和RoleModules是1：N 
                .WithRequired(q => q.Role)           // RoleModule实体中，Role为必须，不可为空
                .HasForeignKey(q => q.RoleId);       // RoleModule实体中，RoleId为外键  
        }
    }
}
