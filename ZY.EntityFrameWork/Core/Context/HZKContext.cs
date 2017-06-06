using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Configuration;

namespace ZY.EntityFrameWork.Core.Context
{
    // 方法1：配置MySQL数据库--[DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]

    // 方法2：配置文件 <entityFramework  codeConfigurationType="MySql.Data.Entity.MySqlEFConfiguration, MySql.Data.Entity.EF6">
    // 但数据库生成之后就不需要了
    // 以MySQL为数据库必须启动数据迁移
    // 否则会报“未为提供程序“MySql.Data.MySqlClient”找到任何 MigrationSqlGenerator”错误，
    // 在迁移配置中增加SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());
    public class HZKContext : DbContext
    {
        public HZKContext() : base(ConfigurationManager.AppSettings["Connection"].ToString())  // "HZK"对应app.config文件里面的connectionStrings
        {
            Database.SetInitializer<HZKContext>(new SeedingDataInitializer());
        }

        // 直接加载
        //public DbSet<ArchiveInfo> ArchiveInfos { get; set; }
        //public DbSet<ArvFieldCfg> ArvFieldCfgs { get; set; }
        //public DbSet<ArvLocation> ArvLocations { get; set; }
        //public DbSet<Cabinet> Cabinets { get; set; }
        //public DbSet<ArvLendInfo> LendInfoes { get; set; }
        //public DbSet<UserRight> UserRights { get; set; }
        //public DbSet<FieldType> FieldTypes { get; set; }
        //public DbSet<ModuleItems> ModuleItemses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // 关闭整个数据库模型的级联删除规则
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
           
            // 直接加载
            // modelBuilder.Configurations.Add(new ArchiveInfoMap());
            // modelBuilder.Configurations.Add(new ArvLocationMap());

            //string mapSuffix = ConvertProviderNameToSuffix(defaultConnectStr.ProviderName);
            
            // 映射文件所在程序集的后缀名
            string mapSuffix = "Mapping.MySQL";

            // 查找程序集中符合后缀名的命名空间内的所有类
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => !String.IsNullOrEmpty(type.Namespace))
                .Where(type => type.Namespace.EndsWith(mapSuffix, StringComparison.OrdinalIgnoreCase))
                .Where(type => type.BaseType != null && type.BaseType.IsGenericType
                    && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));

            foreach (var type in typesToRegister)
            {
                // 创建类实例
                dynamic configurationInstance = Activator.CreateInstance(type);
                // 加入配置集合
                modelBuilder.Configurations.Add(configurationInstance);
            }
        }
    }
}
