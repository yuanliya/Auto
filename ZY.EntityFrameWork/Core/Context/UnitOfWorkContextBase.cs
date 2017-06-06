using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ZY.EntityFrameWork.Core.DBHelper;
using ZY.EntityFrameWork.Core.Model.Entity;

namespace ZY.EntityFrameWork.Core.Context
{
    // 当程序运行时，位于内存中的EF数据实体可以处于以下五种状态之一：
    // 1. Added    : 实体对象是新创建的，数据库中没有相应的记录。
    // 2. Unchanged: 从数据库加载到内存后，实体对象属性值没有任何改变。
    // 3. Modified : 至少有一个实体对象属性值被改变。
    // 4. Deleted  : 如果用户从实体对象集合中删除了某实体对象，则它将处于此状态。注意数据库中此对象相应的记录还存在。
    // 5. Detached : 此实体对象未被EF所跟踪

    // DbEntityEntry对象，此对象包容着实体对象每个属性的三个值：
    // 1. Current Value ：当前值
    // 2. Original Value：原始值，就是从数据库中刚提取出来的值
    // 3. Database Value：数据库中对应记录的对应字段的值
    // EF为每一个需要跟踪状态的实体对象创建一个对应的DbEntityEntry对象，保存实体对象各属性的Current Value、Original Value和Database Value三个值

    // 从实体对象获得对应的DbEntityEntry对象：DbEntityEntry entry = DbContext对象.Entry(实体对象引用);

    /// <summary>
    /// 单元操作实现基类
    /// </summary>
    public class UnitOfWorkContextBase : IEFUnitOfWorkContext
    {
        public UnitOfWorkContextBase(DbContext context)
        {
            this.Context = context;
        }

        // 之所以不用UnitOfWorkContextBase直接继承HZKContext，
        // 是为了只将Context.Set<>以及封装的方法暴露出去，隐藏DBContext的其他操作
        // 增加一个无参构造函数，是为了依赖注册时不再需要注册HZKContext，其他的就可以按接口来批量注册了
        public UnitOfWorkContextBase()
        {
            this.Context = new HZKContext();
        }

        /// <summary>
        /// 获取当前使用的数据访问上下文对象
        /// </summary>
        private DbContext Context;

        /// <summary>
        /// 获取当前单元操作是否已被提交
        /// </summary>
        public bool IsCommitted { get; private set; }

        /// <summary>
        /// 提交当前单元操作的结果
        /// </summary>
        /// <returns></returns>
        public int Commit()
        {
            if (IsCommitted)
            {
                // 已经提交，直接返回
                return 0;
            }

            try
            {
                // 变化的记录提交数据库
                int result = Context.SaveChanges();
                IsCommitted = true;
              
                return result;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null && e.InnerException.InnerException is SqlException)
                {
                    SqlException sqlEx = e.InnerException.InnerException as SqlException;
                    string msg = DataHelper.GetSqlExceptionMessage(sqlEx.Number);
                    throw ExceptionHelper.ThrowDataAccessException("提交数据更新时发生异常：" + msg, sqlEx);
                }
                //if (e is System.Data.Entity.Infrastructure.DbUpdateConcurrencyException)
                //{
                //    // Update the values of the entity that failed to save from the store 
                //    e.Entries.Single().Reload();
                //    int result = Context.SaveChanges();
                //    IsCommitted = true;

                //    return result;
                //}
                //throw;
                throw ExceptionHelper.ThrowDataAccessException("提交数据更新时发生异常：" + e.InnerException.Message, e.InnerException);
            }
            //https://msdn.microsoft.com/en-us/data/jj592904
            catch (Exception e)
            {
                return 0;
            }
        }

        /// <summary>
        /// 把当前单元操作回滚成未提交状态
        /// </summary>
        public void Rollback()
        {
            IsCommitted = false;
        }

        /// <summary>
        /// 销毁上下文
        /// </summary>
        public void Dispose()
        {
            if (!IsCommitted)
            {
                Commit();
            }

            Context.Dispose();
        }

        /// <summary>
        /// 为指定的类型返回 System.Data.Entity.DbSet，这将允许对上下文中的给定实体执行 CRUD 操作。
        /// </summary>
        /// <typeparam name="TEntity"> 应为其返回一个集的实体类型。 </typeparam>
        /// <returns> 给定实体类型的 System.Data.Entity.DbSet 实例。 </returns>
        public DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return Context.Set<TEntity>();
        }

        /// <summary>
        /// 注册一个新的对象到仓储上下文中
        /// EF中Add操作有两种方式：
        /// 1、直接创建对象，调用DbSet的Add（）方法；
        /// 2、调用上下文的Entry（）方法并设置对应状态
        /// 3、调用SaveChanges（）进行提交
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entity"> 要注册的对象 </param>
        public void RegisterNew<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            EntityState state = Context.Entry(entity).State;
          
            // 实体未由上下文跟踪
            if (state == EntityState.Detached)
            {
                // 实体将由上下文跟踪，但是在数据库中还不存在
                Context.Entry(entity).State = EntityState.Added;
            }
            
            IsCommitted = false;
        }

        /// <summary>
        /// 批量注册多个新的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entities"> 要注册的对象集合 </param>
        public void RegisterNew<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity
        {
            try
            {
                // 禁止上下文自动发现实体状态改变,即禁止EF调用DetectChanges()
                // 可以加快批量注册实体的执行速度
                Context.Configuration.AutoDetectChangesEnabled = false;
               
                foreach (TEntity entity in entities)
                {
                    RegisterNew(entity);
                }
            }
            finally
            {
                Context.Configuration.AutoDetectChangesEnabled = true;
            }
        }

        /// <summary>
        /// 注册一个更改的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entity"> 要注册的对象 </param>
        public void RegisterModified<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            try
            {
                // 实体未由上下文跟踪
                if (Context.Entry(entity).State == EntityState.Detached)
                {
                    // 报错： 一个实体对象不能由多个 IEntityChangeTracker 实例引用
                    // Attach（） 用于在上下文中重新填充数据库中已存在的实体，同时设置实体状态为"Unchanged"
                    Context.Set<TEntity>().Attach(entity);
                    // 上下文重新跟踪实体后，默认实体状态为"Unchanged"；必须把实体状态改成"Modified"，才能实现Update操作
                    Context.Entry(entity).State = EntityState.Modified;
                }

                // Commit（）时提交数据库更新
                IsCommitted = false;
            }
            catch (Exception)
            {
                // http://www.cnblogs.com/guomingfeng/p/mvc-ef-update.html
                // 先让上下文获得对数据库中现有的、具有相同主键的实体的跟踪
                TEntity oldEntity = Context.Set<TEntity>().Find(entity.ID);       // 所有实体有共同主键ID
                // 再把新数据赋值到现有实体（此时数据库记录不更新）
                Context.Entry(oldEntity).CurrentValues.SetValues(entity);
                // Commit（）时提交数据库更新
                IsCommitted = false;
            }
        }

        /// <summary>
        /// 注册多个修改对象
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        public void RegisterModified<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity
        {
            try
            {
                // 禁止上下文自动发现实体状态改变,即禁止EF调用DetectChanges()
                // 可以加快批量注册实体的执行速度
                Context.Configuration.AutoDetectChangesEnabled = false;
            
                foreach (TEntity entity in entities)
                {
                    RegisterModified(entity);
                }
            }
            finally
            {
                Context.Configuration.AutoDetectChangesEnabled = true;
            }
        }

        /// <summary>
        /// 注册一个删除的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entity"> 要注册的对象 </param>
        public void RegisterDeleted<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            // 级联删除情况下不行，此时关联的对象会变成null，会报错。
            Context.Entry(entity).State = EntityState.Deleted;

            IsCommitted = false;
        }

        /// <summary>
        /// 批量注册多个删除的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entities"> 要注册的对象集合 </param>
        public void RegisterDeleted<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity
        {
            try
            {
                // 禁止上下文自动发现实体状态改变,即禁止EF调用DetectChanges()
                // 可以加快批量注册实体的执行速度
                Context.Configuration.AutoDetectChangesEnabled = false;
             
                foreach (TEntity entity in entities)
                {
                    RegisterDeleted(entity);
                }
            }
            finally
            {
                Context.Configuration.AutoDetectChangesEnabled = true;
            }
        }
    }
}
