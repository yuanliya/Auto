using System.Data.Entity.ModelConfiguration;
using ZY.EntityFrameWork.Core.Model.Entity;

namespace ZY.EntityFrameWork.Core.Mapping.MySQL
{
    public class ArvLocationMap : EntityTypeConfiguration<ArvLocation>
    {
        public ArvLocationMap()
        {
            // 定义实体对象映射的数据表和主键字段
            ToTable("LocationInfo").HasKey(q => q.Id);
            // 表字段属性--ArvBoxID设置为必须
            //Property(m => m.ArvBoxID).IsRequired();
            // 实体中的Arvs和主键是1：N的关系
            HasMany(e => e.Arvs).WithOptional(m => m.ArvLocation).HasForeignKey(q=>q.ArvLocation_Id);//.HasForeignKey(e=>e.ArvID);
        }
    }
}
