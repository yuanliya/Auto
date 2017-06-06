using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using ZY.EntityFrameWork.Core.DBHelper;
using ZY.EntityFrameWork.Core.Model.Entity;

namespace ZY.EntityFrameWork.Core.Services
{
    /// <summary>
    /// 系统配置相关的服务---设备管理
    /// </summary>
    public partial class BaseSystemConfigService
    {
        /// <summary>
        /// 新增设备
        /// </summary>
        /// <param name="item">实体</param>
        /// <returns>受影响的记录数</returns>
        public int Add(Device item)
        {
            // 查重
            if (deviceRepository.CheckExists(q => q.CabinetNo == item.CabinetNo))
            {
                throw new Exception("试图添加设备编号重复的记录");
            }

            return deviceRepository.Insert(item);
        }

        /// <summary>
        /// 删除设备
        /// </summary>
        /// <param name="item">实体</param>
        /// <returns>受影响的记录数</returns>
        public int Delete(Device item)
        {
            return deviceRepository.Delete(item.ID);// Delete(q => q.CabinetNo == item.CabinetNo);
        }

        /// <summary>
        /// 批量删除设备
        /// </summary>
        /// <param name="items">实体集合</param>
        /// <returns>受影响的记录数</returns>
        public int Delete(List<Device> items)
        {
            // 提取实体集合中的主键
            IEnumerable<string> ids = items.Select(q => q.ID);
            // 删除所有主键对应的实体记录
            return deviceRepository.Delete(q => ids.Contains(q.ID));


            //return deviceRepository.Delete(items);
        }

        /// <summary>
        /// 更新设备
        /// </summary>
        /// <param name="item">实体</param>
        /// <returns></returns>
        public int UpdateDevice(Device item)
        {
            return deviceRepository.Update(item);
        }

        /// <summary>
        /// 查询所有设备
        /// </summary>
        /// <returns>结果集</returns>
        public List<Device> GetAllDevices()
        {
            return deviceRepository.GetQueryable().ToList();
        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="searchItems">查询条件</param>
        /// <returns>结果集</returns>
        public List<Device> FindDevices(IList<QueryCondition> searchItems)
        {
            Expression<Func<Device, bool>> ep = LamdaExpressionHelper.BuildExpression<Device>(searchItems);
            return deviceRepository.Find(ep).ToList();
        }
    }
}
