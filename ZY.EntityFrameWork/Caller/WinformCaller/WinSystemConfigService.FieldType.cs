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
    /// 面向Windows UI的服务--字段管理
    /// 实体模型由Dto转换到Entity
    public partial class WinSystemConfigService : ISystemConfigService
    {
        // 定义服务操作类对象，包含所有Resposity操作接口
        public BaseSystemConfigService baseSytemConfigService = new BaseSystemConfigService();

        /// <summary>
        /// 加入字段条目
        /// </summary>
        /// <param name="item">实体</param>
        /// <returns></returns>
        public int Add(FieldCfgDto fieldDto)
        {
            return baseSytemConfigService.Add(fieldDto.MapTo<FieldCfg>());
        }

        /// <summary>
        /// 批量加入字段条目
        /// </summary>
        /// <param name="items">实体集合</param>
        /// <returns></returns>
        public int Add(List<FieldCfgDto> fieldDtos)
        {
            return baseSytemConfigService.Add(fieldDtos.MapTo<List<FieldCfg>>());
        }

        /// <summary>
        /// 删除字段条目
        /// </summary>
        /// <param name="item">实体</param>
        /// <returns></returns>
        public int Delete(FieldCfgDto fieldDto)
        {
            return baseSytemConfigService.Delete(fieldDto.MapTo<FieldCfg>());
        }

        /// <summary>
        /// 批量删除字段条目
        /// </summary>
        /// <param name="items">实体集合</param>
        /// <returns></returns>
        public int Delete(List<FieldCfgDto> fieldDtos)
        {
            return baseSytemConfigService.Delete(fieldDtos.MapTo<List<FieldCfg>>());
        }

        /// <summary>
        /// 查询所有字段条目
        /// </summary>
        /// <returns>结果集</returns>
        public List<FieldCfgDto> GetAllFieldCfgs()
        {
            return baseSytemConfigService.GetAllFieldCfgs().MapTo<List<FieldCfgDto>>();
        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="searchItems">查询条件</param>
        /// <returns>结果集</returns>
        public List<FieldCfgDto> FindFieldCfgs(IList<QueryCondition> searchItems)
        {
            return baseSytemConfigService.FindFieldCfgs(searchItems).MapTo<List<FieldCfgDto>>();
        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="name">字段条目名称</param>
        /// <returns>结果集</returns>
        public FieldCfgDto FindMappingField(string fieldName)
        {
            return baseSytemConfigService.FindMappingField(fieldName).MapTo<FieldCfgDto>();
        }

        /// <summary>
        /// 更新字段条目
        /// </summary>
        /// <param name="item">实体</param>
        /// <returns></returns>
        public int UpdateFieldCfg(FieldCfgDto fieldDto)
        {
            return baseSytemConfigService.UpdateFieldCfg(fieldDto.MapTo<FieldCfg>());
        }

        /// <summary>
        /// 批量更新字段条目
        /// </summary>
        /// <param name="item">实体集合</param>
        /// <returns></returns>
        public int UpdateFieldCfgs(List<FieldCfgDto> fieldDtos)
        {
            return baseSytemConfigService.UpdateFieldCfgs(fieldDtos.MapTo<List<FieldCfg>>());
        }
    }
}
