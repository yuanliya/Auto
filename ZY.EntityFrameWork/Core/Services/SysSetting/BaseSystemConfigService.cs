using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZY.EntityFrameWork.Core.Context;
using ZY.EntityFrameWork.Core.DBHelper;
using ZY.EntityFrameWork.Core.Repositories.Interface;

namespace ZY.EntityFrameWork.Core.Services
{
    /// <summary>
    /// 系统配置公开的服务
    /// </summary>
    public partial class BaseSystemConfigService : BaseRootService
    {
        private IFieldTypeRepository fieldTypeRepository;     // 字段管理
        private IDataDictRepository  dataDictRepository;      // 数据字典
        private IDeviceRepository    deviceRepository;        // 设备管理

        public BaseSystemConfigService(IUnitOfWorkContext unitContext, IDataDictRepository DataDictRepository, IFieldTypeRepository FieldTypeRepository, 
            IDeviceRepository DeviceRepository) : base(unitContext)
        {
            this.fieldTypeRepository = FieldTypeRepository;
            this.dataDictRepository  = DataDictRepository;
            this.deviceRepository    = DeviceRepository;
        }

        /// <summary>
        /// 从Unity容器中提取接口注册实例
        /// </summary>
        public BaseSystemConfigService()
        {
            fieldTypeRepository = UnityHelper.Instance.Resolve<IFieldTypeRepository>();
            dataDictRepository  = UnityHelper.Instance.Resolve<IDataDictRepository>();
            deviceRepository    = UnityHelper.Instance.Resolve<IDeviceRepository>();
        }      
    }
}
