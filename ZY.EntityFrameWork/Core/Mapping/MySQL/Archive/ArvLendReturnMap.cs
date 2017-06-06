using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

using ZY.EntityFrameWork.Core.Model.Entity;

namespace ZY.EntityFrameWork.Core.Mapping.MySQL
{
    public class ArvLendReturnMap : EntityTypeConfiguration<ArvLendReturn>
    {
        public ArvLendReturnMap()
        {
            // 定义实体对象映射的数据表和主键
            ToTable("ArvLendReturnInfo").HasKey(q => q.ID);

            HasRequired(q => q.ArchiveInfo).WithMany(q => q.ArvLendReturns).HasForeignKey(q=>q.ArvID);

            HasRequired(q => q.ArvLend).WithMany(q => q.ArvLendReturns).HasForeignKey(q => q.LendID);

            HasOptional(q => q.ArvReturn).WithMany(q => q.ArvLendReturns).HasForeignKey(q => q.ReturnID);

            // 设置为自增类型（一般情况下就是自增，但是设置并发控制后就不能自增了）
            //Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            // 处理并发
            Property(p => p.RowVersion).IsConcurrencyToken();
        }
    }
}
