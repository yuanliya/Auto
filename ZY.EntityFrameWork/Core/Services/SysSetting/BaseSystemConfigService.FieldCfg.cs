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
    /// 系统配置相关服务---字段管理
    /// </summary>
    public partial class BaseSystemConfigService
    {
        /// <summary>
        /// 加入字段条目
        /// </summary>
        /// <param name="item">实体</param>
        /// <returns>受影响的记录数</returns>
        public int Add(FieldCfg item)
        {
            return fieldTypeRepository.Insert(item);
        }

        /// <summary>
        /// 批量加入字段条目
        /// </summary>
        /// <param name="items">实体集合</param>
        /// <returns>受影响的记录数</returns>
        public int Add(List<FieldCfg> items)
        {
            return fieldTypeRepository.Insert(items);
        }

        /// <summary>
        /// 删除字段条目
        /// </summary>
        /// <param name="item">实体</param>
        /// <returns>受影响的记录数</returns>
        public int Delete(FieldCfg item)
        {
            return fieldTypeRepository.Delete(item.ID);// (q => q.FieldName == item.FieldName);
        }

        /// <summary>
        /// 批量删除字段条目
        /// </summary>
        /// <param name="items">实体集合</param>
        /// <returns>受影响的记录数</returns>
        public int Delete(List<FieldCfg> items)
        {
            // 提取实体集合中的主键
            IEnumerable<string> ids = items.Select(q => q.ID);
            // 删除所有主键对应的实体记录
            return deviceRepository.Delete(q => ids.Contains(q.ID));
            //return fieldTypeRepository.Delete(items);
        }

        /// <summary>
        /// 更新字段条目
        /// </summary>
        /// <param name="item">实体</param>
        /// <returns>受影响的记录数</returns>
        public int UpdateFieldCfg(FieldCfg item)
        {
            return fieldTypeRepository.Update(item);
        }

        /// <summary>
        /// 批量更新字段条目
        /// </summary>
        /// <param name="item">实体集合</param>
        /// <returns>受影响的记录数</returns>
        public int UpdateFieldCfgs(List<FieldCfg> items)
        {
            return fieldTypeRepository.Update(items);
        }

        /// <summary>
        /// 查询所有字段条目
        /// </summary>
        /// <returns>结果集</returns>
        public List<FieldCfg> GetAllFieldCfgs()
        {
            return fieldTypeRepository.GetQueryable().ToList();
        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="searchItems">查询条件</param>
        /// <returns>结果集</returns>
        public List<FieldCfg> FindFieldCfgs(IList<QueryCondition> searchItems)
        {
            Expression<Func<FieldCfg, bool>> ep = LamdaExpressionHelper.BuildExpression<FieldCfg>(searchItems);
            return fieldTypeRepository.Find(ep).ToList();
        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="name">字段条目名称</param>
        /// <returns>结果集</returns>
        public FieldCfg FindMappingField(string fieldName)
        {
            return fieldTypeRepository.FindSingle(q => q.FieldName == fieldName);
        }
    }
}
