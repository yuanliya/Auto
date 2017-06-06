using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

using ZY.EntityFrameWork.Core.Model.Entity;

namespace ZY.EntityFrameWork.Core.Repositories.Interface
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        #region 公共方法

        /// <summary>
        /// 插入实体记录
        /// </summary>
        /// <param name="entity"> 实体对象 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        int Insert(TEntity entity, bool isSave = true);

        /// <summary>
        /// 批量插入实体记录集合
        /// </summary>
        /// <param name="entities"> 实体记录集合 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        int Insert(IEnumerable<TEntity> entities, bool isSave = true);

        /// <summary>
        ///     删除指定编号的记录
        /// </summary>
        /// <param name="id"> 实体记录编号 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        int Delete(object id, bool isSave = true);

        /// <summary>
        /// 删除实体记录
        /// </summary>
        /// <param name="entity"> 实体对象 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        int Delete(TEntity entity, bool isSave = true);

        /// <summary>
        /// 删除实体记录集合
        /// </summary>
        /// <param name="entities"> 实体记录集合 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        int Delete(IEnumerable<TEntity> entities, bool isSave = true);

        /// <summary>
        /// 删除所有符合特定表达式的数据
        /// </summary>
        /// <param name="predicate"> 查询条件谓语表达式 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        int Delete(Expression<Func<TEntity, bool>> predicate, bool isSave = true);

        /// <summary>
        /// 更新实体记录
        /// </summary>
        /// <param name="entity"> 实体对象 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        int Update(TEntity entity, bool isSave = true);

        /// <summary>
        /// 批量修改实体记录
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="isSave"></param>
        /// <returns></returns>
        int Update(IEnumerable<TEntity> entities, bool isSave = true);

        /// <summary>
        /// 更新所有符合特定表达式的记录
        /// </summary>
        /// <param name="predicate"> 查询条件谓语表达式 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        int Update(Expression<Func<TEntity, bool>> predicate, bool isSave = true);

        /// <summary>
        /// 检查实体是否存在
        /// </summary>
        /// <param name="predicate">查询条件表达式</param>
        /// <returns>是否存在</returns>
        bool CheckExists(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 查找指定主键的实体记录
        /// </summary>
        /// <param name="key"> 指定主键 </param>
        /// <returns> 符合编号的记录，不存在返回null </returns>
        TEntity GetByKey(object key);

        /// <summary>
        /// 按查询表达式查找实体记录
        /// </summary>
        /// <param name="match">条件表达式</param>
        /// <returns> 符合条件的记录，不存在返回null</returns>
        TEntity FindSingle(Expression<Func<TEntity, bool>> match);
     
        /// <summary>
        /// 返回可查询的记录源
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetQueryable();

        /// <summary>
        /// 根据条件查询数据库,并返回对象集合
        /// </summary>
        /// <param name="match">条件表达式</param>
        /// <returns></returns>
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> match);

        /// <summary>
        /// 分页检索数据库
        /// </summary>
        /// <param name="match">条件表达式</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="sortPredicate">排序元素</param>
        /// <param name="totalCount">记录总数</param>
        /// <param name="descending">是否降序</param>
        /// <returns></returns>
        IEnumerable<TEntity> GetPagedList(Expression<Func<TEntity, bool>> match, int pageIndex, int pageSize, Expression<Func<TEntity, dynamic>> sortPredicate, ref int totalCount, bool descending = false);

        #endregion

        #region 统计

        /// <summary>
        /// 分组统计信息
        /// </summary>
        /// <typeparam name="TOrderBy"></typeparam>
        /// <param name="filter"></param>
        /// <param name="groupBy"></param>
        /// <returns></returns>
        IEnumerable<DataMap<TOrderBy>> GetGroupCount<TOrderBy>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TOrderBy>> groupBy);
      
        #endregion
    }
}