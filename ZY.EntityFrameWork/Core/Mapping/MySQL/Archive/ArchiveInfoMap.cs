using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

using ZY.EntityFrameWork.Core.Model.Entity;

namespace ZY.EntityFrameWork.Core.Mapping.MySQL
{
    public class ArchiveInfoMap : EntityTypeConfiguration<ArchiveInfo>
    {
        public ArchiveInfoMap()
        {
            // 定义实体对象映射的数据表和主键
            ToTable("ArchiveInfo").HasKey(q => q.ID);
            
            // "Has"定义ArchiveInfo对ArvLendInfo的关系           
            //HasMany(t => t.LendArvs)              // ArchiveInfo和ArvLendInfo是1：N的关系
            //    .WithMany(t => t.ArchiveInfos).Map(r => r.MapLeftKey("ArvID").MapRightKey("LendID").ToTable("Arv_LendInfo"));

                // "With"定义ArvLendInfo对ArchiveInfo的关系           
                //.WithRequired(t => t.ArchiveInfo) // ArvLendInfo包含不为空的ArchiveInfo实例 
                //.HasForeignKey(t => t.ArvID);     // ArvLendInfo包含外键ArvId                          
           
            // 表字段属性--ArvID设置为必须
            //Property(m => m.ArvID).IsRequired(); 

            // 设置为自增类型（一般情况下就是自增，但是设置并发控制后就不能自增了）
            //Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            // 处理并发
            Property(p => p.RowVersion).IsConcurrencyToken();
        }
    }
}
