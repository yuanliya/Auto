using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ZY.EntityFrameWork.Caller.Facade;
using ZY.EntityFrameWork.Core.Model.Dto;
using ZY.EntityFrameWork.Core.Model.Entity;
using ZY.EntityFrameWork.Core.Services;
using ZY.EntityFrameWork.Core.DBHelper;

namespace ZY.EntityFrameWork.Caller.WinformCaller
{
    /// <summary>
    /// 面向Windows UI的服务--设备管理
    /// 实体模型由Dto转换到Entity
    public partial class WinSystemConfigService
    {
        /// <summary>
        /// 新增设备
        /// </summary>
        /// <param name="item">实体</param>
        /// <returns></returns>
        public int Add(DeviceDto deviceDto)
        {
            return baseSytemConfigService.Add(deviceDto.MapTo<Device>());
        }

        /// <summary>
        /// 删除设备
        /// </summary>
        /// <param name="item">实体</param>
        /// <returns></returns>
        public int Delete(DeviceDto deviceDto)
        {
            return baseSytemConfigService.Delete(deviceDto.MapTo<Device>());
        }

        /// <summary>
        /// 批量删除设备条目
        /// </summary>
        /// <param name="items">实体集合</param>
        /// <returns></returns>
        public int Delete(List<DeviceDto> deviceDtos)
        {
            return baseSytemConfigService.Delete(deviceDtos.MapTo<List<Device>>());
        }

        /// <summary>
        /// 查询所有设备
        /// </summary>
        /// <returns>结果集</returns>
        public List<DeviceDto> GetAllDevices()
        {
            return baseSytemConfigService.GetAllDevices().MapTo<List<DeviceDto>>();
        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="searchItems">查询条件</param>
        /// <returns>结果集</returns>
        public List<DeviceDto> FindDevices(IList<QueryCondition> searchItems)
        {
            return baseSytemConfigService.FindDevices(searchItems).MapTo<List<DeviceDto>>();
        }

        /// <summary>
        /// 更新设备
        /// </summary>
        /// <param name="item">实体</param>
        /// <returns></returns>
        public int UpdateDevice(DeviceDto deviceDto)
        {
            return baseSytemConfigService.UpdateDevice(deviceDto.MapTo<Device>());
        }
    }
}
