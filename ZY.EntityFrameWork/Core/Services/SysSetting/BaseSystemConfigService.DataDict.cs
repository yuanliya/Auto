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
    /// 系统配置相关服务---数据字典管理
    /// </summary>
    public partial class BaseSystemConfigService 
    {
        /// <summary>
        /// 新增字典条目
        /// </summary>
        /// <param name="item">实体</param>
        /// <returns>受影响的记录数</returns>
        public int Add(DataDict item)
        {
            return dataDictRepository.Insert(item);
        }

        /// <summary>
        /// 批量加入字典条目
        /// </summary>
        /// <param name="items">实体集合</param>
        /// <returns></returns>
        public int Add(List<DataDict> items)
        {
            return dataDictRepository.Insert(items);
        }

        /// <summary>
        /// 删除字典条目
        /// </summary>
        /// <param name="item">实体</param>
        /// <returns></returns>
        public int Delete(DataDict item)
        {
            return dataDictRepository.Delete(item.ID);
        }

        /// <summary>
        /// 批量删除字典条目
        /// </summary>
        /// <param name="items">实体集合</param>
        /// <returns></returns>
        public int Delete(List<DataDict> items)
        {
            // 提取实体集合中的主键
            IEnumerable<string> ids = items.Select(q => q.ID);
            // 删除所有主键对应的实体记录
            return deviceRepository.Delete(q => ids.Contains(q.ID));
        }

        /// <summary>
        /// 查询所有字典条目
        /// </summary>
        /// <returns>结果集</returns>
        public List<DataDict> GetAllDataDicts()
        {
            // 立即返回数据库记录
            return dataDictRepository.GetQueryable().ToList();
        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="searchItems">查询条件</param>
        /// <returns>结果集</returns>
        public List<DataDict> FindDataDicts(IList<QueryCondition> searchItems)
        {
            // 构建查询条件的Lamda树表达式
            Expression<Func<DataDict, bool>> ep = LamdaExpressionHelper.BuildExpression<DataDict>(searchItems);
            // 立即返回数据库记录
            return dataDictRepository.Find(ep).ToList();
        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="name">字典条目名称</param>
        /// <returns>结果集</returns>
        public List<DataDict> FindDataDictByName(string name)
        {
            // 立即返回数据库记录
            return dataDictRepository.Find(q => q.FieldTypeName == name).ToList();
        }

        /// <summary>
        /// 更新字典条目
        /// </summary>
        /// <param name="item">实体</param>
        /// <returns></returns>
        public int UpdateDataDict(DataDict item)
        {
            return dataDictRepository.Update(item);
        }

        /// <summary>
        /// 批量更新字典条目
        /// </summary>
        /// <param name="item">实体集合</param>
        /// <returns></returns>
        public int UpdateDataDicts(List<DataDict> items)
        {
            return dataDictRepository.Update(items);
        }
    }
}
