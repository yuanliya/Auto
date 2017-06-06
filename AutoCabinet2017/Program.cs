using AutoCabinet2017.UI.APP;
using AutoCabinet2017.UI.OP;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using ZY.EntityFrameWork.Core.Context;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;

namespace AutoCabinet2017
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            // 加载默认汉化资源
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-CN");
            
            DevExpress.Utils.AppearanceObject.DefaultFont = new Font("宋体", 10);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Application.Run(new FormLogin());

            // 在应用程序中定义的每个DbContext类型，在首次使用时，Entity Framework都会根据数据库中的信息在内存生成一个映射视图（mapping views），而这个操作非常耗时
            // 解决方案--应用程序初始化时一次性触发所有的DbContext进行mapping views的生成操作——调用StorageMappingItemCollection的GenerateViews()方法
            // Entity Framework的版本至少是6.0才支持
            using (var dbcontext = new HZKContext())
            {
                var objectContext = ((IObjectContextAdapter)dbcontext).ObjectContext;
                var mappingCollection = (StorageMappingItemCollection)objectContext.MetadataWorkspace.GetItemCollection(DataSpace.CSSpace);
                mappingCollection.GenerateViews(new List<EdmSchemaError>());

                Application.Run(new FormLogin());
            }
        }
    }
}
