using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data.Entity;
using System.ComponentModel;

using ZY.EntityFrameWork.Core.Repositories.Interface;
using ZY.EntityFrameWork.Core.Model.Entity;
using ZY.EntityFrameWork.Core.Context;
using ZY.EntityFrameWork.Core.DBHelper;

namespace ZY.EntityFrameWork.Core.Repositories.Impl
{
    /// <summary>
    /// 数据访问层基类实现层
    /// </summary>
    /// <typeparam name="TEntity">实体对象类型</typeparam>
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        #region 变量及构造函数

        /// <summary>
        /// 基于EF框架的DbContext对象
        /// </summary>
        protected IEFUnitOfWorkContext EFContext;

        /// <summary>
        /// 是否为降序
        /// </summary>
        public bool IsDescending { get; set; }

        /// <summary>
        /// 排序属性
        /// </summary>
        public string SortPropertyName { get; set; }

        /// <summary>
        /// 参数化构造函数
        /// </summary>
        /// <param name="context">DbContext对象</param>
        public BaseRepository(IUnitOfWorkContext unitContext)
        {
            if (unitContext is IEFUnitOfWorkContext)
            {
                this.EFContext = unitContext as IEFUnitOfWorkContext;
            }               

            this.IsDescending     = true;  // 默认降序
            this.SortPropertyName = "ID";  // 排序依据为“ID”
        }

        #endregion

        #region CUID操作

        /// <summary>
        /// 插入实体记录
        /// </summary>
        /// <param name="entity"> 实体对象 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Insert(TEntity entity, bool isSave = true)
        {
            // 参数有效性检查
            ArgumentValidationHelper.CheckForNullReference(entity, "传入的对象t为空");
            // 对象加入仓储上下文（暂不提交数据库）
            EFContext.RegisterNew(entity);
            // 立即提交或暂缓执行
            return isSave ? EFContext.Commit() : 0;
        }

        /// <summary>
        /// 批量插入实体记录集合
        /// </summary>
        /// <param name="entities"> 实体记录集合 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Insert(IEnumerable<TEntity> entities, bool isSave = true)
        {
            // 参数有效性检查
            ArgumentValidationHelper.CheckForNullReference(entities, "传入的对象t为空");
            // 对象加入仓储上下文（暂不提交数据库）
            EFContext.RegisterNew(entities);
            // 立即提交或暂缓执行
            return isSave ? EFContext.Commit() : 0;
        }

        /// <summary>
        /// 删除指定编号的记录
        /// </summary>
        /// <param name="id"> 实体记录编号 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Delete(object id, bool isSave = true)
        {
            // 参数有效性检查
            ArgumentValidationHelper.CheckForNullReference(id, "传入的对象id为空");
            // 实体记录在数据库是否存在
            TEntity entity = EFContext.Set<TEntity>().Find(id);
            // 记录存在则立即执行，否则返回0
            return entity != null ? Delete(entity, isSave) : 0;
        }

        /// <summary>
        /// 删除实体记录
        /// </summary>
        /// <param name="entity"> 实体对象 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Delete(TEntity entity, bool isSave = true)
        {
            // 参数有效性检查
            ArgumentValidationHelper.CheckForNullReference(entity, "传入的对象t为空");
            try
            {
                // 对象加入仓储上下文（暂不提交数据库）
                EFContext.RegisterDeleted(entity);
            }
            catch (InvalidOperationException)
            {
                Delete(entity.ID);
            }
            catch(Exception)
            {
                throw;
            }
            
            // 立即提交或暂缓执行
            return isSave ? EFContext.Commit() : 0;
        }

        /// <summary>
        /// 删除实体记录集合
        /// </summary>
        /// <param name="entities"> 实体记录集合 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Delete(IEnumerable<TEntity> entities, bool isSave = true)
        {
            // 参数有效性检查
            ArgumentValidationHelper.CheckForNullReference(entities, "传入的对象entities为空");
            // 对象加入仓储上下文（暂不提交数据库）
            EFContext.RegisterDeleted(entities);
            // 立即提交或暂缓执行
            return isSave ? EFContext.Commit() : 0;
        }

        /// <summary>
        /// 删除所有符合特定表达式的数据
        /// </summary>
        /// <param name="predicate"> 查询条件谓语表达式 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Delete(Expression<Func<TEntity, bool>> predicate, bool isSave = true)
        {
            // 参数有效性检查
            ArgumentValidationHelper.CheckForNullReference(predicate, "传入的对象predicate为空");
            // 依据条件表达式查询实体记录
            List<TEntity> entities = EFContext.Set<TEntity>().Where(predicate).ToList();
            // 记录存在则立即执行，否则返回0
            return entities.Count > 0 ? Delete(entities, isSave) : 0;
        }

        /// <summary>
        /// 更新实体记录
        /// </summary>
        /// <param name="entity"> 实体对象 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Update(TEntity entity, bool isSave = true)
        {
            // 参数有效性检查
            ArgumentValidationHelper.CheckForNullReference(entity, "传入的对象entity为空");
            // 对象加入仓储上下文（暂不提交数据库）
            EFContext.RegisterModified(entity);
            //TEntity entity0 = EFContext.
            // 立即提交或暂缓执行
            return isSave ? EFContext.Commit() : 0;
        }

        /// <summary>
        /// 批量更新实体记录
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="isSave"></param>
        /// <returns></returns>
        public virtual int Update(IEnumerable<TEntity> entities, bool isSave = true)
        {
            // 参数有效性检查
            ArgumentValidationHelper.CheckForNullReference(entities, "传入的对象entity为空");
            // 对象加入仓储上下文（暂不提交数据库）
            EFContext.RegisterModified(entities);
            // 立即提交或暂缓执行
            return isSave ? EFContext.Commit() : 0;
        }

        /// <summary>
        /// 更新所有符合特定表达式的记录
        /// </summary>
        /// <param name="predicate"> 查询条件谓语表达式 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Update(Expression<Func<TEntity, bool>> predicate, bool isSave = true)
        {
            // 参数有效性检查
            ArgumentValidationHelper.CheckForNullReference(predicate, "传入的对象predicate为空");
            // 依据条件表达式查询实体记录
            List<TEntity> entities = EFContext.Set<TEntity>().Where(predicate).ToList();
            // 记录存在则立即执行，否则返回0
            return entities.Count > 0 ? Update(entities, isSave) : 0;
        }

        #endregion

        #region 查询操作 --- 大数据集操作，IQueryable<>的效率远远高于IEnumerable<>

        // 1、返回IQueryable<TEntity>使用延迟加载技术 
        // 2、返回IEnumerable<TEntity>,需要把数据集加载到内存

        /// <summary>
        /// 检查实体是否存在
        /// </summary>
        /// <param name="predicate">查询条件表达式</param>
        /// <returns>是否存在</returns>
        public virtual bool CheckExists(Expression<Func<TEntity, bool>> predicate)
        {
            // 参数有效性检查
            ArgumentValidationHelper.CheckForNullReference(predicate, "传入的对象key为空");
            // 存在返回true，否则false
            return EFContext.Set<TEntity>().Count(predicate) == 0 ? false :true ;
        }

        /// <summary>
        /// 查找指定主键的实体记录
        /// </summary>
        /// <param name="key"> 指定主键 </param>
        /// <returns> 符合编号的记录，不存在返回null </returns>
        public virtual TEntity GetByKey(object key)
        {
            // 参数有效性检查
            ArgumentValidationHelper.CheckForNullReference(key, "传入的对象key为空");
            // 执行查询
            return EFContext.Set<TEntity>().Find(key);
        }

        /// <summary>
        /// 根据条件查询数据库,如果存在返回第一个对象
        /// </summary>
        /// <param name="match">查询条件表达式</param>
        /// <returns>存在则返回指定的第一个对象,否则返回默认值</returns>
        public virtual TEntity FindSingle(Expression<Func<TEntity, bool>> match)
        {
            // 执行查询
            return EFContext.Set<TEntity>().FirstOrDefault(match);
        }
     
        /// <summary>
        /// 返回可查询的记录源
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<TEntity> GetQueryable()
        {
            // 执行查询
            return EFContext.Set<TEntity>();
        }

        /// <summary>
        /// 根据条件查询数据库,并返回对象集合
        /// </summary>
        /// <param name="match">查询条件表达式</param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> match)
        {
            // 执行查询
            return EFContext.Set<TEntity>().Where(match);
        }

        /// <summary>
        /// 获得分页数据
        /// </summary>
        /// <param name="match">条件表达式</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页的记录数</param>
        /// <param name="sortPredicate">排序元素</param>
        /// <param name="descending">是否降序</param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetPagedList(Expression<Func<TEntity, bool>> match, int pageIndex, int pageSize, Expression<Func<TEntity, dynamic>> sortPredicate,ref int totalCount, bool descending = false)
        {
            // 参数有效性检验
            if (pageIndex <= 0)
            {
                throw new ArgumentOutOfRangeException("pageIndex", pageIndex, "页码应为大于0的整数");
            }
                
            if (pageSize <= 0)
            {
                throw new ArgumentOutOfRangeException("pageSize", pageSize, "每页的记录数应为大于0的整数");
            }
                
            // 筛选数据库记录
            var query = EFContext.Set<TEntity>().Where(match);

            totalCount = query.Count();               // 总页数
            int skip   = (pageIndex - 1) * pageSize;  // 需要跳过的记录数
            int take   = pageSize;                    // 每页的记录数

            // 按指定的排序规则排序提取记录
            if (sortPredicate != null)
            {
                if (!descending)
                {
                    // 升序排列，并按页检索记录
                    return query.OrderBy(sortPredicate.Compile()).Skip(skip).Take(take).ToList();
                }
                else
                {
                    // 降序排列，并按页检索记录
                    return query.OrderByDescending(sortPredicate.Compile()).Skip(skip).Take(take).ToList();
                }                    
            }

            // 默认按"ID"降序排列
            return query.OrderBy(q => q.ID).Skip(skip).Take(take).ToList();
            //return query.Skip(skip).Take(take).ToList();//这样竟然不可以，还不报错？？          
        }

        #endregion

        #region 统计

        /// <summary>
        /// 分组统计
        /// </summary>
        /// <typeparam name="TOrderBy">分组Key的类型</typeparam>
        /// <param name="filter">Lamda表达式，筛选过滤条件</param>
        /// <param name="groupBy">Lamda表达式，分组条件</param>
        /// <returns></returns>
        public virtual IEnumerable<DataMap<TOrderBy>> GetGroupCount<TOrderBy>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TOrderBy>> groupBy)
        {
            // 使用LINQ进行数据集操作时，LINQ 不能直接从数据集对象中查询，因为数据集对象不支持LINQ 查询
            // 需要使用AsEnumerable()/ AsQueryable()方法返回一个泛型的对象以支持LINQ 的查询操作。

            // IQueryable接口与IEnumberable接口的区别： 
            // 1、IEnumerable<T> 泛型类在调用自己的SKip 和 Take 等扩展方法之前数据就已经加载在本地内存里了；IEnumerable跑的是Linq to Object，先强制从数据库中读取所有数据到内存
            // 2、IQueryable<T> 是将Skip ,take等方法表达式翻译成T-SQL语句，再向SQL服务器发送命令，并不是把所有数据都加载到内存里来才进行条件过滤。
            // 3、.ToList()立即执行，将数据读取到内存

            if(filter == null)
            {
                return EFContext.Set<TEntity>().AsQueryable().GroupBy(groupBy).Select(q => new DataMap<TOrderBy> { Key = q.Key, Value = q.Count() }).AsEnumerable();
            }
              
            return EFContext.Set<TEntity>().AsQueryable().Where(filter).GroupBy(groupBy).Select(q => new DataMap<TOrderBy> { Key = q.Key, Value = q.Count() }).AsEnumerable();
        }

        #endregion
    }
}

