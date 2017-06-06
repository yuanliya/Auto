using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ZY.EntityFrameWork.Core.DBHelper;
using ZY.EntityFrameWork.Core.Model.Dto;
using ZY.EntityFrameWork.Core.Model.Entity;
using ZY.EntityFrameWork.Caller.Facade;

namespace ZY.EntityFrameWork.Caller.WinformCaller
{
    /// <summary>
    /// 面向Windows UI的服务--数据字典管理
    /// 实体模型由Dto转换到Entity
    /// </summary>
    public partial class WinSystemConfigService
    {
        /// <summary>
        /// 加入字典条目
        /// </summary>
        /// <param name="item">实体</param>
        /// <returns></returns>
        public int Add(DataDictDto item)
        {
            return baseSytemConfigService.Add(item.MapTo<DataDict>());
        }

        /// <summary>
        /// 批量加入字典条目
        /// </summary>
        /// <param name="items">实体集合</param>
        /// <returns></returns>
        public int Add(List<DataDictDto> items)
        {
            return baseSytemConfigService.Add(items.MapTo<List<DataDict>>());
        }

        /// <summary>
        /// 删除字典条目
        /// </summary>
        /// <param name="item">实体</param>
        /// <returns></returns>
        public int Delete(DataDictDto item)
        {
            return baseSytemConfigService.Delete(item.MapTo<DataDict>());
        }

        /// <summary>
        /// 批量删除字典条目
        /// </summary>
        /// <param name="items">实体集合</param>
        /// <returns></returns>
        public int Delete(List<DataDictDto> items)
        {
            return baseSytemConfigService.Delete(items.MapTo<List<DataDict>>());
        }

        /// <summary>
        /// 查询所有字典条目
        /// </summary>
        /// <returns>结果集</returns>
        public List<DataDictDto> GetAllDataDicts()
        {
            return baseSytemConfigService.GetAllDataDicts().MapTo<List<DataDictDto>>();
        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="searchItems">查询条件</param>
        /// <returns>结果集</returns>
        public List<DataDictDto> FindDataDicts(IList<QueryCondition> searchItems)
        {
            return baseSytemConfigService.FindDataDicts(searchItems).MapTo<List<DataDictDto>>();
        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="name">字典条目名称</param>
        /// <returns>结果集</returns>
        public List<DataDictDto> FindDataDictsByName(string name)
        {
            return baseSytemConfigService.FindDataDictByName(name).MapTo<List<DataDictDto>>() ;
        }

        /// <summary>
        /// 更新字典条目
        /// </summary>
        /// <param name="item">实体</param>
        /// <returns></returns>
        public int UpdateDataDict(DataDictDto item)
        {
            return baseSytemConfigService.UpdateDataDict(item.MapTo<DataDict>());
        }

        /// <summary>
        /// 批量更新字典条目
        /// </summary>
        /// <param name="item">实体集合</param>
        /// <returns></returns>
        public int UpdateDataDicts(List<DataDictDto> items)
        {
            return baseSytemConfigService.UpdateDataDicts(items.MapTo<List<DataDict>>());
        }
    }
}
