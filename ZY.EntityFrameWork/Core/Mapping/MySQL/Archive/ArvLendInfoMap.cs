using System.Data.Entity.ModelConfiguration;

using ZY.EntityFrameWork.Core.Model.Entity;

namespace ZY.EntityFrameWork.Core.Mapping.MySQL
{
    class ArvLendInfoMap: EntityTypeConfiguration<ArvLendInfo>
    {
        public ArvLendInfoMap()
        {
            ToTable("ArvLendInfo").HasKey(q => q.ID); // 主键
            //Property(m => m.ArvID).IsRequired();

            // 建立借阅信息和归还信息的一对一关系（EF仅支持基于主键的一对一）
            this.HasOptional(q => q.ArvReturnInfo)    // 借阅实体可以不包含归还实体
                .WithRequired();                      // 归还实体必须依赖借阅实体，即必须有借阅实体的外键
        }
    }
}
