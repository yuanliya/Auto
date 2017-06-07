using System.Data.Entity.ModelConfiguration;

using ZY.EntityFrameWork.Core.Model.Entity;

namespace ZY.EntityFrameWork.Core.Mapping.MySQL
{
    public class ArvBoxMap : EntityTypeConfiguration<ArvBox>
    {
        public ArvBoxMap()
        {
            // 定义实体对象映射的数据表和主键
            ToTable("ArvBoxInfo").HasKey(q => q.ID); //(q => q.ArvBoxID); // ArvBox的ArvBoxID被用作ArchiveInfo实体的外键，因此必须做主键！！！

            HasMany(q => q.Arvs)                      // ArvBox和Arvs是1：N关系
                .WithOptional(q => q.ArvBox)          // Arv可以包含ArvBox实例或空 
                .HasForeignKey(q => q.ArvBoxID);      // Arv实体中包含外键ArvBoxId
        }
    }
}
