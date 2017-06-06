using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ZY.EntityFrameWork.Core.DBHelper;
using ZY.EntityFrameWork.Core.Model.Dto;

namespace ZY.EntityFrameWork.Caller.Facade
{
    /// <summary>
    /// 面向应用层公开的服务接口--设备管理
    /// </summary>
    public partial interface ISystemConfigService
    {
        /// <summary>
        /// 新增设备
        /// </summary>
        /// <param name="item">实体</param>
        /// <returns></returns>
        int Add(DeviceDto item);

        /// <summary>
        /// 删除设备
        /// </summary>
        /// <param name="item">实体</param>
        /// <returns></returns>
        int Delete(DeviceDto item);

        /// <summary>
        /// 批量删除设备条目
        /// </summary>
        /// <param name="items">实体集合</param>
        /// <returns></returns>
        int Delete(List<DeviceDto> items);

        /// <summary>
        /// 查询所有设备
        /// </summary>
        /// <returns>结果集</returns>
        List<DeviceDto> GetAllDevices();

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="searchItems">查询条件</param>
        /// <returns>结果集</returns>
        List<DeviceDto> FindDevices(IList<QueryCondition> searchItems);

        /// <summary>
        /// 更新设备
        /// </summary>
        /// <param name="item">实体</param>
        /// <returns></returns>
        int UpdateDevice(DeviceDto item);
    }
}
