using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ZY.EntityFrameWork.Core.Model.Entity;

namespace ZY.EntityFrameWork.Core.Context
{
    // 初始化器
    // DropCreateDatabaseIfModelChanges--仅在模型自数据库创建后发生更改时删除数据库、重新创建数据库并选择重新设置数据库的种子
    // DropCreateDatabaseAlways--首次在应用程序域中使用上下文时，重新创建数据库并可以选择重新设置数据库的种子。若要设置数据库的种子，创建派生类并重写 Seed 方法。
    // CreateDatabaseIfNotExists--在没有数据库时创建一个新的数据库
    public class SeedingDataInitializer : DropCreateDatabaseIfModelChanges<HZKContext> //DropCreateDatabaseAlways<HZKContext> //   // DropCreateDatabaseAlways<HZKContext> //   
    {
        ///// <summary>
        ///// 打开Xml文件
        ///// </summary>
        ///// <returns>包含配置信息的类实例</returns>
        //private List<Module> InitModules()
        //{
        //    string fileName =  ".\\XML\\DatabaseInfo.xml";

        //    // 打开XML文件
        //    XmlSerializeHelper<ModuleItem, ModuleXml>.OpenXmlFile(fileName);
        //    // 读取XML文件
        //    List<ModuleItem> items = XmlSerializeHelper<ModuleItem, ModuleXml>.ReadXML().Items;
        //    List<ModuleItem> parents = items.Where(q => string.IsNullOrEmpty(q.ParentTag)).ToList();

        //    List<Module> allModules = new List<Module>();
        //    parents.ForEach(q =>
        //    {
        //        List<ModuleItem> sons = items.Where(t => t.ParentTag == q.ModuleTag).ToList();
        //        Module moduleParent = new Module() { ID = Guid.NewGuid().ToString("N"), ModuleTag = q.ModuleTag, ModuleName = q.ModuleName, Enabled = q.Enabled, Permissions = q.Permissions };
        //        allModules.Add(moduleParent);
        //        sons.ForEach(t =>
        //        {
        //            Module son = new Module() { ID = Guid.NewGuid().ToString("N"), ModuleTag = t.ModuleTag, ModuleName = t.ModuleName, Enabled = t.Enabled, Permissions = t.Permissions, ParentID = moduleParent.ID };
        //            allModules.Add(son);
        //        });
        //    });
        //    return allModules;
        //}

        // 设置数据库中表的默认记录
        protected override void Seed(HZKContext context)
        {
            #region 档案信息表

            for (int i = 0; i < 6; i++)
            {
                ArchiveInfo info = new ArchiveInfo { ID = Guid.NewGuid().ToString("N"), ArvID = Convert.ToString(1000 + i), ArvStatus = "在档", CreateTime = DateTime.Today };
                context.Set<ArchiveInfo>().Add(info);
            }

            for (int i = 1; i < 9; i++)
            {
                ArvBox info = new ArvBox 
                {
                    ID = Guid.NewGuid().ToString("N"), 
                    ArvBoxID    = Convert.ToString(10000 + i), 
                    ArvBoxTitle = "box" + i,
                    GroupNo     = i,
                    LayerNo     = i+10,
                    CellNo      = 0
                };

                context.Set<ArvBox>().Add(info);
            }

            #endregion

            #region 模块信息

            // 定义数据集
            DbSet<Module> dbModule = context.Set<Module>();
            // 从XML文件中读取
          //  dbModule.AddRange(InitModules());

            // 直接生成
            dbModule.AddRange(new Module[]
            {
                new Module
                {
                    ID = Guid.NewGuid().ToString("N"), 
                    ModuleTag   = "100",
                    ModuleName  = "仓储作业",
                    Enabled     = true,
                    ChildModule = new List<Module>
                    {
                        new Module{ID = Guid.NewGuid().ToString("N"), ModuleTag="101",ModuleName="入库管理",Enabled=true,Permissions=11},                  
                        new Module{ID = Guid.NewGuid().ToString("N"), ModuleTag="102",ModuleName="出库管理",Enabled=true,Permissions=15},
                        new Module{ID = Guid.NewGuid().ToString("N"), ModuleTag="103",ModuleName="移库管理",Enabled=false,Permissions=15},
                        new Module{ID = Guid.NewGuid().ToString("N"), ModuleTag="104",ModuleName="盘库管理",Enabled=false,Permissions=15},
                        new Module{ID = Guid.NewGuid().ToString("N"), ModuleTag="105",ModuleName="借阅管理",Enabled=true,Permissions=15},
                        new Module{ID = Guid.NewGuid().ToString("N"), ModuleTag="106",ModuleName="归还管理",Enabled=true,Permissions=15},                          
                    }
                },  
                
                new Module
                {
                    ID = Guid.NewGuid().ToString("N"), 
                    ModuleTag   = "200",
                    ModuleName  = "报表管理",
                    Enabled     = true,
                    ChildModule = new List<Module>
                    {
                        new Module{ID = Guid.NewGuid().ToString("N"), ModuleTag="201",ModuleName="信息查询",Enabled=true,Permissions=15},
                        new Module{ID = Guid.NewGuid().ToString("N"), ModuleTag="202",ModuleName="信息统计",Enabled=true,Permissions=15},
                    }
                 },

                 new Module
                 {
                     ID = Guid.NewGuid().ToString("N"), 
                     ModuleTag   = "300",
                     ModuleName  = "数据库管理",
                     Enabled     = true,
                     ChildModule = new List<Module>
                     {
                         new Module{ID = Guid.NewGuid().ToString("N"), ModuleTag="301",ModuleName="数据库备份",Enabled=true,Permissions=15},
                         new Module{ID = Guid.NewGuid().ToString("N"), ModuleTag="302",ModuleName="数据库还原",Enabled=true,Permissions=15},
                     }
                 },

                 new Module
                 {
                     ID = Guid.NewGuid().ToString("N"), 
                     ModuleTag   = "400",
                     ModuleName  = "基本资料",
                     Enabled     = true,
                     ChildModule = new List<Module>
                     {
                         new Module{ID = Guid.NewGuid().ToString("N"), ModuleTag="401",ModuleName="字段管理",Enabled=true,Permissions=15},
                         new Module{ID = Guid.NewGuid().ToString("N"), ModuleTag="402",ModuleName="字典管理",Enabled=true,Permissions=15},                        
                     }
                 },
                 
                 new Module
                 {
                     ID = Guid.NewGuid().ToString("N"), 
                     ModuleTag   = "500",
                     ModuleName  = "设备管理",
                     Enabled     = true,
                     ChildModule = new List<Module>
                     {
                         new Module{ID = Guid.NewGuid().ToString("N"), ModuleTag="501",ModuleName="设备控制",Enabled=true,Permissions=15},
                         new Module{ID = Guid.NewGuid().ToString("N"), ModuleTag="502",ModuleName="设备配置",Enabled=true,Permissions=15},
                         new Module{ID = Guid.NewGuid().ToString("N"), ModuleTag="503",ModuleName="条码设置",Enabled=false,Permissions=15},
                     }
                 },
                 
                 new Module
                 {
                     ID = Guid.NewGuid().ToString("N"), 
                     ModuleTag   = "600",
                     ModuleName  = "系统管理",
                     Enabled     = true,
                     ChildModule = new List<Module>
                     {
                         new Module{ID = Guid.NewGuid().ToString("N"), ModuleTag="601",ModuleName="用户管理",Enabled=true,Permissions=15},
                         new Module{ID = Guid.NewGuid().ToString("N"), ModuleTag="602",ModuleName="角色管理",Enabled=true,Permissions=15},
                         new Module{ID = Guid.NewGuid().ToString("N"), ModuleTag="603",ModuleName="日志管理",Enabled=true,Permissions=15},
                     }
                 }
            });

            #endregion

            #region 权限管理模块

            // 角色
            DbSet<Role> dbRole = context.Set<Role>();
            dbRole.AddRange(new Role[]
            {
                new Role{ID = Guid.NewGuid().ToString("N"), RoleName = "系统管理员", Level = 1 },
                new Role{ID = Guid.NewGuid().ToString("N"), RoleName = "管理员",     Level = 2 },
                new Role{ID = Guid.NewGuid().ToString("N"), RoleName = "普通用户",   Level = 3 },
            });

            // 用户
            DbSet<User> dbUser = context.Set<User>();
            dbUser.AddRange(new User[]
            {
                new User
                { 
                    ID = Guid.NewGuid().ToString("N"),
                    UserCode = "sysadmin",
                    Password = "sysadmin123",
                    UserName = "系统管理员",
                    RoleId = dbRole.Local[0].ID     // 关联到管理员角色,注意Local属性的使用；因为数据还未写入数据库，所以不能用dbRole.ToList<Role>()[0].ID的方式
                },

                new User
                { 
                    ID = Guid.NewGuid().ToString("N"),
                    UserCode = "admin",
                    Password = "admin123",
                    UserName = "管理员",
                    RoleId = dbRole.Local[1].ID     // 关联到管理员角色,注意Local属性的使用；因为数据还未写入数据库，所以不能用dbRole.ToList<Role>()[0].ID的方式
                },

                new User
                { 
                    ID = Guid.NewGuid().ToString("N"),
                    UserCode = "sa",
                    Password = "sa123",
                    UserName = "普通用户",
                    RoleId = dbRole.Local[2].ID     // 关联到管理员角色,注意Local属性的使用；因为数据还未写入数据库，所以不能用dbRole.ToList<Role>()[0].ID的方式
                },
            });

            // 角色-模块关系
            context.Set<RoleModule>().AddRange(new RoleModule[]
            {
                // 系统管理员权限
                
                // 1、仓储业务模块(移库、盘库除外)
                new RoleModule{ID = Guid.NewGuid().ToString("N"), RoleId=dbRole.Local[0].ID, ModuleId = dbModule.Local[0].ChildModule.ToList<Module>()[0].ID,Enabled=true, Permissions=15 },
                new RoleModule{ID = Guid.NewGuid().ToString("N"), RoleId=dbRole.Local[0].ID, ModuleId = dbModule.Local[0].ChildModule.ToList<Module>()[1].ID,Enabled=true, Permissions=15 },
             //   new RoleModule{ID = Guid.NewGuid().ToString("N"), RoleId=dbRole.Local[0].ID, ModuleId = dbModule.Local[0].ChildModule.ToList<Module>()[2].ID,Enabled=true, Permissions=15 },
             //   new RoleModule{ID = Guid.NewGuid().ToString("N"), RoleId=dbRole.Local[0].ID, ModuleId = dbModule.Local[0].ChildModule.ToList<Module>()[3].ID,Enabled=true, Permissions=15 },
                new RoleModule{ID = Guid.NewGuid().ToString("N"), RoleId=dbRole.Local[0].ID, ModuleId = dbModule.Local[0].ChildModule.ToList<Module>()[4].ID,Enabled=true, Permissions=15 },
                new RoleModule{ID = Guid.NewGuid().ToString("N"), RoleId=dbRole.Local[0].ID, ModuleId = dbModule.Local[0].ChildModule.ToList<Module>()[5].ID,Enabled=true, Permissions=15 },
                // 2、报表管理全部模块
                new RoleModule{ID = Guid.NewGuid().ToString("N"), RoleId=dbRole.Local[0].ID, ModuleId = dbModule.Local[7].ChildModule.ToList<Module>()[0].ID,Enabled=true, Permissions=15 },
                new RoleModule{ID = Guid.NewGuid().ToString("N"), RoleId=dbRole.Local[0].ID, ModuleId = dbModule.Local[7].ChildModule.ToList<Module>()[1].ID,Enabled=true, Permissions=15 },
                // 3、数据库管理全部模块
                new RoleModule{ID = Guid.NewGuid().ToString("N"), RoleId=dbRole.Local[0].ID, ModuleId = dbModule.Local[10].ChildModule.ToList<Module>()[0].ID,Enabled=true, Permissions=15 },
                new RoleModule{ID = Guid.NewGuid().ToString("N"), RoleId=dbRole.Local[0].ID, ModuleId = dbModule.Local[10].ChildModule.ToList<Module>()[1].ID,Enabled=true, Permissions=15 },
                // 4、基本资料管理全部模块
                new RoleModule{ID = Guid.NewGuid().ToString("N"), RoleId=dbRole.Local[0].ID, ModuleId = dbModule.Local[13].ChildModule.ToList<Module>()[0].ID,Enabled=true, Permissions=15 },
                new RoleModule{ID = Guid.NewGuid().ToString("N"), RoleId=dbRole.Local[0].ID, ModuleId = dbModule.Local[13].ChildModule.ToList<Module>()[1].ID,Enabled=true, Permissions=15 },
                // 5、设备管理模块(标签管理除外)
                new RoleModule{ID = Guid.NewGuid().ToString("N"), RoleId=dbRole.Local[0].ID, ModuleId = dbModule.Local[16].ChildModule.ToList<Module>()[0].ID,Enabled=true, Permissions=15 },
                new RoleModule{ID = Guid.NewGuid().ToString("N"), RoleId=dbRole.Local[0].ID, ModuleId = dbModule.Local[16].ChildModule.ToList<Module>()[1].ID,Enabled=true, Permissions=15 },
                //new RoleModule{ID = Guid.NewGuid().ToString("N"), RoleId=dbRole.Local[0].ID, ModuleId = dbModule.Local[16].ChildModule.ToList<Module>()[2].ID,Enabled=true, Permissions=15 },
                // 6、系统管理全部模块
                new RoleModule{ID = Guid.NewGuid().ToString("N"), RoleId=dbRole.Local[0].ID, ModuleId = dbModule.Local[20].ChildModule.ToList<Module>()[0].ID,Enabled=true, Permissions=15 },
                new RoleModule{ID = Guid.NewGuid().ToString("N"), RoleId=dbRole.Local[0].ID, ModuleId = dbModule.Local[20].ChildModule.ToList<Module>()[1].ID,Enabled=true, Permissions=15 },
                new RoleModule{ID = Guid.NewGuid().ToString("N"), RoleId=dbRole.Local[0].ID, ModuleId = dbModule.Local[20].ChildModule.ToList<Module>()[2].ID,Enabled=true, Permissions=15 },
 
                // 管理员权限

                // 1、仓储业务模块(移库、盘库除外)
                new RoleModule{ID = Guid.NewGuid().ToString("N"), RoleId=dbRole.Local[1].ID, ModuleId = dbModule.Local[0].ChildModule.ToList<Module>()[0].ID,Enabled=true, Permissions=15 },
                new RoleModule{ID = Guid.NewGuid().ToString("N"), RoleId=dbRole.Local[1].ID, ModuleId = dbModule.Local[0].ChildModule.ToList<Module>()[1].ID,Enabled=true, Permissions=15 },
                //new RoleModule{ID = Guid.NewGuid().ToString("N"), RoleId=dbRole.Local[1].ID, ModuleId = dbModule.Local[0].ChildModule.ToList<Module>()[2].ID,Enabled=true, Permissions=15 },
                //new RoleModule{ID = Guid.NewGuid().ToString("N"), RoleId=dbRole.Local[1].ID, ModuleId = dbModule.Local[0].ChildModule.ToList<Module>()[3].ID,Enabled=true, Permissions=15 },
                new RoleModule{ID = Guid.NewGuid().ToString("N"), RoleId=dbRole.Local[1].ID, ModuleId = dbModule.Local[0].ChildModule.ToList<Module>()[4].ID,Enabled=true, Permissions=15 },
                new RoleModule{ID = Guid.NewGuid().ToString("N"), RoleId=dbRole.Local[1].ID, ModuleId = dbModule.Local[0].ChildModule.ToList<Module>()[5].ID,Enabled=true, Permissions=15 },
                // 2、报表管理全部模块
                new RoleModule{ID = Guid.NewGuid().ToString("N"), RoleId=dbRole.Local[1].ID, ModuleId = dbModule.Local[7].ChildModule.ToList<Module>()[0].ID,Enabled=true, Permissions=15 },
                new RoleModule{ID = Guid.NewGuid().ToString("N"), RoleId=dbRole.Local[1].ID, ModuleId = dbModule.Local[7].ChildModule.ToList<Module>()[1].ID,Enabled=true, Permissions=15 },
                // 3、数据库管理全部模块
                new RoleModule{ID = Guid.NewGuid().ToString("N"), RoleId=dbRole.Local[1].ID, ModuleId = dbModule.Local[10].ChildModule.ToList<Module>()[0].ID,Enabled=true, Permissions=15 },
                new RoleModule{ID = Guid.NewGuid().ToString("N"), RoleId=dbRole.Local[1].ID, ModuleId = dbModule.Local[10].ChildModule.ToList<Module>()[1].ID,Enabled=true, Permissions=15 },
                // 4、基本资料管理全部模块
                new RoleModule{ID = Guid.NewGuid().ToString("N"), RoleId=dbRole.Local[1].ID, ModuleId = dbModule.Local[13].ChildModule.ToList<Module>()[0].ID,Enabled=true, Permissions=15 },
                new RoleModule{ID = Guid.NewGuid().ToString("N"), RoleId=dbRole.Local[1].ID, ModuleId = dbModule.Local[13].ChildModule.ToList<Module>()[1].ID,Enabled=true, Permissions=15 },

                // 普通用户权限

                // 1、仓储业务模块(移库、盘库除外)--只能操作"Add"
                new RoleModule{ID = Guid.NewGuid().ToString("N"), RoleId=dbRole.Local[2].ID, ModuleId = dbModule.Local[0].ChildModule.ToList<Module>()[0].ID,Enabled=true, Permissions=1 },
                new RoleModule{ID = Guid.NewGuid().ToString("N"), RoleId=dbRole.Local[2].ID, ModuleId = dbModule.Local[0].ChildModule.ToList<Module>()[1].ID,Enabled=true, Permissions=1 },
                //new RoleModule{ID = Guid.NewGuid().ToString("N"), RoleId=dbRole.Local[2].ID, ModuleId = dbModule.Local[0].ChildModule.ToList<Module>()[2].ID,Enabled=true, Permissions=1 },
                //new RoleModule{ID = Guid.NewGuid().ToString("N"), RoleId=dbRole.Local[2].ID, ModuleId = dbModule.Local[0].ChildModule.ToList<Module>()[3].ID,Enabled=true, Permissions=1 },
                new RoleModule{ID = Guid.NewGuid().ToString("N"), RoleId=dbRole.Local[2].ID, ModuleId = dbModule.Local[0].ChildModule.ToList<Module>()[4].ID,Enabled=true, Permissions=1 },
                new RoleModule{ID = Guid.NewGuid().ToString("N"), RoleId=dbRole.Local[2].ID, ModuleId = dbModule.Local[0].ChildModule.ToList<Module>()[5].ID,Enabled=true, Permissions=1 },
                // 2、报表管理全部模块--只能操作"Add"
                new RoleModule{ID = Guid.NewGuid().ToString("N"), RoleId=dbRole.Local[2].ID, ModuleId = dbModule.Local[7].ChildModule.ToList<Module>()[0].ID,Enabled=true, Permissions=1 },
                new RoleModule{ID = Guid.NewGuid().ToString("N"), RoleId=dbRole.Local[2].ID, ModuleId = dbModule.Local[7].ChildModule.ToList<Module>()[1].ID,Enabled=true, Permissions=1 },
                // 3、数据库管理模块--数据库备份
                new RoleModule{ID = Guid.NewGuid().ToString("N"), RoleId=dbRole.Local[2].ID, ModuleId = dbModule.Local[10].ChildModule.ToList<Module>()[0].ID,Enabled=true, Permissions=15 },
            });

            #endregion

            #region 系统配置管理

            context.Set<FieldCfg>().AddRange(new FieldCfg[]
            {
                new FieldCfg{ID = Guid.NewGuid().ToString("N"), FieldName="ArvID",   FieldShowName="档案编号",IsFieldUsable=true,IsFieldVisible=true,IsKeyWord=true,FieldType="文本",Remark="档案", SerialNo = 1},
                new FieldCfg{ID = Guid.NewGuid().ToString("N"), FieldName="ArvTitle",FieldShowName="档案名称",IsFieldUsable=true,IsFieldVisible=true,IsKeyWord=true,FieldType="文本",Remark="档案", SerialNo = 2},
                new FieldCfg{ID = Guid.NewGuid().ToString("N"), FieldName="ArvBoxID",FieldShowName="档案盒编号",IsFieldUsable=true,IsFieldVisible=true,IsKeyWord=false,FieldType="文本",Remark="档案", SerialNo = 3},
                new FieldCfg{ID = Guid.NewGuid().ToString("N"), FieldName="ArvBoxTitle",FieldShowName="档案盒名称",IsFieldUsable=true,IsFieldVisible=true,IsKeyWord=false,FieldType="文本",Remark="档案", SerialNo = 4},
                new FieldCfg{ID = Guid.NewGuid().ToString("N"), FieldName="ArvUnit",FieldShowName="部门分类",IsFieldUsable=true,IsFieldVisible=true,IsKeyWord=true,FieldType="复选",Remark="档案", SerialNo = 5},
                new FieldCfg{ID = Guid.NewGuid().ToString("N"), FieldName="ArvType",FieldShowName="档案类型",IsFieldUsable=true,IsFieldVisible=true,IsKeyWord=true,FieldType="复选",Remark="档案", SerialNo = 6},
                new FieldCfg{ID = Guid.NewGuid().ToString("N"), FieldName="ArvYear",FieldShowName="所属年度",IsFieldUsable=true,IsFieldVisible=true,IsKeyWord=true,FieldType="文本",Remark="档案", SerialNo = 7},
                new FieldCfg{ID = Guid.NewGuid().ToString("N"), FieldName="LabelID",FieldShowName="条码ID",IsFieldUsable=true,IsFieldVisible=true,IsKeyWord=false,FieldType="文本",Remark="档案", SerialNo = 8},
                new FieldCfg{ID = Guid.NewGuid().ToString("N"), FieldName="ArvStatus",FieldShowName="档案状态",IsFieldUsable=false,IsFieldVisible=false,IsKeyWord=false,FieldType="文本",Remark="档案", SerialNo = 9},
                new FieldCfg{ID = Guid.NewGuid().ToString("N"), FieldName="Edoc",FieldShowName="电子档案",IsFieldUsable=false,IsFieldVisible=false,IsKeyWord=false,FieldType="文本",Remark="档案", SerialNo = 10},
                new FieldCfg{ID = Guid.NewGuid().ToString("N"), FieldName="Rsv1",FieldShowName="备用1",IsFieldUsable=false,IsFieldVisible=false,IsKeyWord=false,FieldType="复选",Remark="档案", SerialNo = 11},
                new FieldCfg{ID = Guid.NewGuid().ToString("N"), FieldName="Rsv2",FieldShowName="备用2",IsFieldUsable=false,IsFieldVisible=false,IsKeyWord=false,FieldType="文本",Remark="档案", SerialNo = 12},
                new FieldCfg{ID = Guid.NewGuid().ToString("N"), FieldName="Rsv3",FieldShowName="备用3",IsFieldUsable=false,IsFieldVisible=false,IsKeyWord=false,FieldType="复选",Remark="档案", SerialNo = 13},
                new FieldCfg{ID = Guid.NewGuid().ToString("N"), FieldName="Rsv4",FieldShowName="备用4",IsFieldUsable=false,IsFieldVisible=false,IsKeyWord=false,FieldType="文本",Remark="档案", SerialNo = 14},
                new FieldCfg{ID = Guid.NewGuid().ToString("N"), FieldName="CreateTime",FieldShowName="创建时间",IsFieldUsable=true,IsFieldVisible=true,IsKeyWord=false,FieldType="文本",Remark="档案", SerialNo = 15},
                new FieldCfg{ID = Guid.NewGuid().ToString("N"), FieldName="CreatePerson",FieldShowName="创建人",IsFieldUsable=true,IsFieldVisible=true,IsKeyWord=false,FieldType="文本",Remark="档案", SerialNo = 16},

                new FieldCfg{ID = Guid.NewGuid().ToString("N"), FieldName="GroupNo",FieldShowName="柜号",IsFieldUsable=true,IsFieldVisible=true,IsKeyWord=false,FieldType="文本",Remark="存储位置", SerialNo = 17},
                new FieldCfg{ID = Guid.NewGuid().ToString("N"), FieldName="LayerNo",FieldShowName="层号",IsFieldUsable=true,IsFieldVisible=true,IsKeyWord=false,FieldType="文本",Remark="存储位置", SerialNo = 18},
                new FieldCfg{ID = Guid.NewGuid().ToString("N"), FieldName="CellNo",FieldShowName="格号",IsFieldUsable=true,IsFieldVisible=true,IsKeyWord=false,FieldType="文本",Remark="存储位置", SerialNo = 19},

                new FieldCfg{ID = Guid.NewGuid().ToString("N"), FieldName="ArvOutReason",FieldShowName="出库缘由",IsFieldUsable=true,IsFieldVisible=true,IsKeyWord=false,FieldType="复选",Remark="档案出库", SerialNo = 20},
            });

            // 数据字典管理
            context.Set<DataDict>().AddRange(new DataDict[]
            {                      
                // 第二级
                new DataDict{ID = Guid.NewGuid().ToString("N"), FieldTypeName = "ArvType",  FieldTypeValue="单据",  FieldTypeSerialNo=0},
                new DataDict{ID = Guid.NewGuid().ToString("N"), FieldTypeName = "ArvType",  FieldTypeValue="报表",  FieldTypeSerialNo=1},
                new DataDict{ID = Guid.NewGuid().ToString("N"), FieldTypeName = "ArvUnit",  FieldTypeValue="财务部",FieldTypeSerialNo=0},
                new DataDict{ID = Guid.NewGuid().ToString("N"), FieldTypeName = "ArvUnit",  FieldTypeValue="人事部",  FieldTypeSerialNo=1},
                new DataDict{ID = Guid.NewGuid().ToString("N"), FieldTypeName = "ArvOutReason", FieldTypeValue="转移",  FieldTypeSerialNo=0},
                new DataDict{ID = Guid.NewGuid().ToString("N"), FieldTypeName = "ArvOutReason", FieldTypeValue="销毁",  FieldTypeSerialNo=1},
                
                // 第一级
                new DataDict{ID = Guid.NewGuid().ToString("N"), FieldTypeName = "分类管理", FieldTypeValue="ArvType",      FieldTypeSerialNo=0},
                new DataDict{ID = Guid.NewGuid().ToString("N"), FieldTypeName = "分类管理", FieldTypeValue="ArvUnit",      FieldTypeSerialNo=1},
                new DataDict{ID = Guid.NewGuid().ToString("N"), FieldTypeName = "分类管理", FieldTypeValue="ArvOutReason", FieldTypeSerialNo=2},
            });

            // 设备管理
            context.Set<Device>().AddRange(new Device[]
            {
                new Device{ID = Guid.NewGuid().ToString("N"), CabinetNo = 1, CabinetLayers = 12, CabinetCells = 10},
                new Device{ID = Guid.NewGuid().ToString("N"), CabinetNo = 2, CabinetLayers = 15, CabinetCells = 15},
            });

            #endregion

            // 生成数据库记录
            base.Seed(context);
        }
    }
}
