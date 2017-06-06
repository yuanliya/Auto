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
    public partial class BaseAuthorityService
    {
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="user">实体对象</param>
        /// <returns>受影响的记录数</returns>
        public int Add(User user)
        {
            if(userRepository.CheckExists(q => q.UserCode == user.UserCode))
            {
                throw new Exception("已存在的用户名！");
            }

            return userRepository.Insert(user);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="user">实体对象</param>
        /// <returns>受影响的记录数</returns>
        public int Delete(User user)
        {
            // 利用实际的主键删除
            return userRepository.Delete(q => q.UserCode == user.UserCode);
        }

        /// <summary>
        /// 批量删除用户
        /// 基于事务机理，一旦中间出错，将回滚
        /// </summary>
        /// <param name="arvs">档案集合</param>
        /// <returns>受影响的记录数</returns>
        public int Delete(List<User> users)
        {
            //// 提取实体集合中的主键
            //IEnumerable<string> ids = users.Select(q => q.UserCode);
            //// 删除所有主键对应的实体记录
            //return userRepository.Delete(q => ids.Contains(q.UserCode));

            // 基于事务机理的操作
            users.ForEach(user =>
                {
                    // 由上下文保存删除，但并不立即执行 
                    userRepository.Delete(q => q.UserCode == user.UserCode, false);
                });

            return Context.Commit();
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="user">实体对象</param>
        /// <returns>受影响的记录数</returns>
        public int Update(User user)
        {
            return userRepository.Update(user);
        }

        /// <summary>
        /// 批量更新用户信息
        /// </summary>
        /// <param name="user">实体对象集合</param>
        /// <returns>受影响的记录数</returns>
        public int Update(List<User> users)
        {
            return userRepository.Update(users);
        }

        /// <summary>
        /// 验证用户名和密码
        /// </summary>
        /// <param name="userCode">登录名</param>
        /// <param name="passWord">密码</param>
        /// <returns>符合条件的记录数</returns>
        public User CheckUser(string userCode, string passWord)
        {
            return userRepository.FindSingle(q => q.UserCode == userCode && q.Password == passWord);
        }

        /// <summary>
        /// 根据登录名确定用户的角色
        /// </summary>
        /// <param name="userName">登录名</param>
        /// <returns>角色</returns>
        public Role GetUserRole(string userName)//放在roleService中！！！
        {
            Role role = roleRepository.FindSingle(q => q.RoleName == userName);
            return role ?? null;
        }

        /// <summary>
        /// 查找所有用户
        /// </summary>
        /// <returns>用户集合</returns>
        public List<User> GetAllUsers()
        {
            return userRepository.GetQueryable().ToList();
        }

        public List<User> GetRoleUsers(string roleName)
        {
            return userRepository.Find(q => q.UserRole.RoleName == roleName).ToList();
        }

        /// <summary>
        /// 根据检索条件查找用户
        /// </summary>
        /// <param name="searchItems">检索条件</param>
        /// <returns>用户集合</returns>
        public List<User> FindUsers(List<QueryCondition> searchItems)
        {
            // 构建Lamda表达式
            Expression<Func<User, bool>> ep = LamdaExpressionHelper.BuildExpression<User>(searchItems);
            // 查找用户
            return userRepository.Find(ep).ToList();
        }

        /// <summary>
        /// 获取每页的用户列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页行数</param>
        /// <param name="total">总用户数</param>
        /// <returns></returns>
        //public List<User> GetAllUsers(int pageIndex, int pageSize, out int total)
        //{

        //    var query = this.userRepository.GetQueryable();

        //    total = query.Count();
        //    query = query.OrderBy(u => u.Id)
        //        .Skip((pageIndex - 1) * pageSize)
        //        .Take(pageSize);

        //    return query.ToList();
        //}
    }
}
