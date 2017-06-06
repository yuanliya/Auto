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
    /// 面向应用层公开的服务接口--数据字典管理
    /// </summary>
    public partial interface ISystemConfigService
    {
        /// <summary>
        /// 加入字典条目
        /// </summary>
        /// <param name="item">实体</param>
        /// <returns></returns>
        int Add(DataDictDto item);

        /// <summary>
        /// 批量加入字典条目
        /// </summary>
        /// <param name="items">实体集合</param>
        /// <returns></returns>
        int Add(List<DataDictDto> items);

        /// <summary>
        /// 删除字典条目
        /// </summary>
        /// <param name="item">实体</param>
        /// <returns></returns>
        int Delete(DataDictDto item);

        /// <summary>
        /// 批量删除字典条目
        /// </summary>
        /// <param name="items">实体集合</param>
        /// <returns></returns>
        int Delete(List<DataDictDto> items);

        /// <summary>
        /// 查询所有字典条目
        /// </summary>
        /// <returns>结果集</returns>
        List<DataDictDto> GetAllDataDicts();

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="searchItems">查询条件</param>
        /// <returns>结果集</returns>
        List<DataDictDto> FindDataDicts(IList<QueryCondition> searchItems);

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="name">字典条目名称</param>
        /// <returns>结果集</returns>
        List<DataDictDto> FindDataDictsByName(string name);

        /// <summary>
        /// 更新字典条目
        /// </summary>
        /// <param name="item">实体</param>
        /// <returns></returns>
        int UpdateDataDict(DataDictDto item);

        /// <summary>
        /// 批量更新字典条目
        /// </summary>
        /// <param name="item">实体集合</param>
        /// <returns></returns>
        int UpdateDataDicts(List<DataDictDto> items);
    }
}
