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
    /// 面向应用层公开的服务接口--字段管理
    /// </summary>
    public partial interface ISystemConfigService
    {
        /// <summary>
        /// 加入字段条目
        /// </summary>
        /// <param name="item">实体</param>
        /// <returns></returns>
        int Add(FieldCfgDto item);

        /// <summary>
        /// 批量加入字段条目
        /// </summary>
        /// <param name="items">实体集合</param>
        /// <returns></returns>
        int Add(List<FieldCfgDto> items);

        /// <summary>
        /// 删除字段条目
        /// </summary>
        /// <param name="item">实体</param>
        /// <returns></returns>
        int Delete(FieldCfgDto item);

        /// <summary>
        /// 批量删除字段条目
        /// </summary>
        /// <param name="items">实体集合</param>
        /// <returns></returns>
        int Delete(List<FieldCfgDto> items);

        /// <summary>
        /// 查询所有字段条目
        /// </summary>
        /// <returns>结果集</returns>
        List<FieldCfgDto> GetAllFieldCfgs();

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="searchItems">查询条件</param>
        /// <returns>结果集</returns>
        List<FieldCfgDto> FindFieldCfgs(IList<QueryCondition> searchItems);

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="name">字段条目名称</param>
        /// <returns>结果集</returns>
        FieldCfgDto FindMappingField(string fieldName);

        /// <summary>
        /// 更新字段条目
        /// </summary>
        /// <param name="item">实体</param>
        /// <returns></returns>
        int UpdateFieldCfg(FieldCfgDto item);

        /// <summary>
        /// 批量更新字段条目
        /// </summary>
        /// <param name="item">实体集合</param>
        /// <returns></returns>
        int UpdateFieldCfgs(List<FieldCfgDto> items);
    }
}
