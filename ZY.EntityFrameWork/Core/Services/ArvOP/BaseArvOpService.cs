using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using ZY.EntityFrameWork.Core.Context;
using ZY.EntityFrameWork.Core.Repositories.Interface;
using ZY.EntityFrameWork.Core.Model.Entity;
using ZY.EntityFrameWork.Core.Model.Dto;
using ZY.EntityFrameWork.Core.DBHelper;

namespace ZY.EntityFrameWork.Core.Services
{
    /// <summary>
    /// 档案操作服务--面向应用层公开的接口和实现
    /// </summary>
    public partial class BaseArvOpService : BaseRootService
    {
        // 仓储类
        private IArvRepository    arvRepository { get; set; }       // 档案管理业务
        private IArvBoxRepository arvBoxRepository { get; set; }    // 档案盒管理业务
        private ILendRepository   lendRepository { get; set; }      // 借阅管理业务
        private IReturnRepository returnRepository { get; set; }    // 归还管理业务

        private IArvLendReturnRepository arvLendReturnRepository { get; set; }    // 归还管理业务

        private IOutCabRepository outCabRepository { get; set; }    // 出库管理业务
       

        /// <summary>
        /// 参数形式初始化仓储类
        /// </summary>
        /// <param name="unitContext">上下文</param>
        /// <param name="arvRepository">档案管理</param>
        /// <param name="arvBoxRepository">档案盒管理</param>
        /// <param name="lendRepository">借阅管理</param>
        /// <param name="returnRepository">归还管理</param>
        /// <param name="outCabRepository">出库管理</param>      
        public BaseArvOpService(IUnitOfWorkContext unitContext, IArvRepository arvRepository, IArvBoxRepository arvBoxRepository, ILendRepository lendRepository, IReturnRepository returnRepository, IArvLendReturnRepository arvLendReturnRepository, IOutCabRepository outCabRepository)
            : base(unitContext)
        {
            this.arvRepository    = arvRepository;        
            this.arvBoxRepository = arvBoxRepository;
            this.lendRepository   = lendRepository;
            this.returnRepository = returnRepository;
            this.outCabRepository = outCabRepository;
            this.arvLendReturnRepository = arvLendReturnRepository;
        }

        /// <summary>
        /// 利用Unity从容器中提取仓储类
        /// </summary>
        public BaseArvOpService()
        {
            arvRepository    = UnityHelper.Instance.Resolve<IArvRepository>();
            lendRepository   = UnityHelper.Instance.Resolve<ILendRepository>();
            returnRepository = UnityHelper.Instance.Resolve<IReturnRepository>();
            outCabRepository = UnityHelper.Instance.Resolve<IOutCabRepository>();
            arvBoxRepository = UnityHelper.Instance.Resolve<IArvBoxRepository>();
            arvLendReturnRepository = UnityHelper.Instance.Resolve<IArvLendReturnRepository>();
        }

        #region 档案盒操作

        /// <summary>
        /// 新增档案盒
        /// </summary>
        /// <param name="arvBox">实体类</param>
        /// <param name="isSave">立即保存进数据库</param>
        /// <returns>操作是否成功</returns>
        public int AddNewArvBox(ArvBox arvBox, bool isSave = true)
        {
            if(arvBoxRepository.CheckExists(q=>q.ArvBoxID == arvBox.ArvBoxID))
            {
                throw new Exception("试图添加档案盒编号重复的记录");
            }

            // 记录写入数据库
            return arvBoxRepository.Insert(arvBox, isSave);
        }

        /// <summary>
        /// 删除档案盒
        /// </summary>
        /// <param name="arvBox">实体类</param>
        /// <returns>操作是否成功</returns>
        public int DeleteArvBox(ArvBox arvBox)
        {
            // 删除档案盒记录
            return arvBoxRepository.Delete(arvBox.ArvBoxID);
        }

        /// <summary>
        /// 查找档案盒
        /// ToList()将立即执行数据库查询操作
        /// </summary>
        /// <param name="searchItems">查询条件</param>
        /// <returns>查询结果集</returns>
        public List<ArvBox> FindArvBoxes(IList<QueryCondition> queryItems)
        {
            if (queryItems == null)
            {
                // 查询条件为空，返回所有记录
                return arvBoxRepository.GetQueryable().ToList();
            }

            // 构建Lamda表达式
            Expression<Func<ArvBox, bool>> ep = LamdaExpressionHelper.BuildExpression<ArvBox>(queryItems);

            // 查询数据库
            return arvBoxRepository.Find(ep).ToList();                
        }

        /// <summary>
        /// 更新档案盒
        /// </summary>
        /// <param name="arvBox">实体类</param>
        /// <returns>操作是否成功</returns>
        public int UpdateArvBox(ArvBox arvBox)
        {
            // 更新数据库
            return arvBoxRepository.Update(arvBox);
        }

        #endregion

        #region 入库相关

        /// <summary>
        /// 删除档案
        /// </summary>
        /// <param name="arv">实体类型</param>
        /// <returns>操作是否成功</returns>
        public int Delete(ArchiveInfo arv)
        {
            // 不能直接执行Delete(arv)
            // 从dto map出的实体和上下文中捕捉的实体不是同一个，且无法attach，导致报错。
            // 两种解决方式，一是通过主键删除；
            // 另一种就是取消跟踪：Dbset.Where(where).AsNoTracking().FirstOrDefault<T>();
           
            // 采用实际的主键来删除
            //return arvRepository.Delete(q => q.ArvID == arv.ArvID);

            // Id是实体的自增字段，一般不出现在UI界面，作为删除有时会出错（实际没删除）
            return arvRepository.Delete(arv.ID);

            //return arvRepository.Delete(arv);
        }

        /// <summary>
        /// 批量删除档案
        /// 基于事务机理，一旦中间出错，将回滚
        /// </summary>
        /// <param name="arvs">档案集合</param>
        /// <returns>操作是否成功</returns>
        public int Delete(List<ArchiveInfo> arvs)
        {
            // 提取实体集合中的主键
            IEnumerable<string> ids = arvs.Select(q => q.ArvID);
            // 删除所有主键对应的实体记录
            return arvRepository.Delete(q => ids.Contains(q.ArvID));

            // 基于事务机理的操作
            //arvs.ForEach(q =>
            //    {
            //        // 由上下文保存删除，但并不立即执行          
            //        arvRepository.Delete(t => t.ArvID == q.ArvID, false);
            //    });

            //// 执行批量删除
            //return Context.Commit();

           // return arvRepository.Delete(arvs);
        }

        /// <summary>
        /// 查找在档档案
        /// </summary>
        /// <returns>记录集</returns>
        public List<ArchiveInfo> GetArvInDocument()
        {
            return arvRepository.Find(q => q.ArvStatus == "在档").ToList();

            // IQueryble无法映射，可能是因为它在枚举之前是并没有加载数据的原因，其他地方都要换成List或Inumerable
            // return arvRepository.Find(q => q.ArvStatus == "在库").AsQueryable().MapTo<IQueryable<ArchiveInfoDto>>();
        }

        /// <summary>
        /// 查找在库档案
        /// </summary>
        /// <returns>记录集</returns>
        public List<ArchiveInfo> GetArvInStorage()
        {
            return arvRepository.Find(q => q.ArvStatus == "在库").ToList();

            // IQueryble无法映射，可能是因为它在枚举之前是并没有加载数据的原因，其他地方都要换成List或Inumerable
            //return arvRepository.Find(q => q.ArvStatus == "在库").AsQueryable().MapTo<IQueryable<ArchiveInfoDto>>();
        }

        /// <summary>
        /// 分页查找在档档案
        /// </summary>
        /// <param name="pageIndex">页检索</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="totalCount">总记录数</param>
        /// <param name="descending">降序排列</param>
        /// <returns>记录集</returns>
        public List<ArchiveInfo> GetArvInDocument(int pageIndex, int pageSize, ref int totalCount, bool descending = false)
        {
            return arvRepository.GetPagedList(q => q.ArvStatus == "在档", pageIndex, pageSize, null, ref totalCount).ToList();
        } 

        /// <summary>
        /// 分页查找在库档案
        /// </summary>
        /// <param name="pageIndex">页检索</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="totalCount">总记录数</param>
        /// <param name="descending">降序排列</param>
        /// <returns>记录集</returns>
        public List<ArchiveInfo> GetArvInStorage(int pageIndex, int pageSize, ref int totalCount, bool descending = false)
        {
            return arvRepository.GetPagedList(q => q.ArvStatus == "在库", pageIndex, pageSize, null, ref totalCount).ToList();
        } 

        /// <summary>
        /// 按检索条件查找档案
        /// </summary>
        /// <param name="queryItems"></param>
        /// <returns>记录集</returns>
        public List<ArchiveInfo> FindArvs(IList<QueryCondition> queryItems)
        {
            // 构建Lamda表达式
            Expression<Func<ArchiveInfo, bool>> ep = LamdaExpressionHelper.BuildExpression<ArchiveInfo>(queryItems);

            return arvRepository.Find(ep).ToList();
        }

        /// <summary>
        /// 单个档案放入柜中(后期看情况第二个参数改成档案盒编号)
        /// </summary>
        /// <param name="arv">档案</param>
        /// <param name="arvBox">档案盒</param>
        /// <returns></returns>
        public int InToStorage(ArchiveInfo arv, ArvBox arvBox)
        {
            // 档案查重
            if (arvRepository.CheckExists(q => q.ArvID == arv.ArvID))
            {
                throw new Exception("试图添加档案编号重复的记录");
            }

            // 档案盒查重
            ArvBox box = arvBoxRepository.FindSingle(q => q.ArvBoxID == arvBox.ArvBoxID);
            if (box == null)
            {
                // 新的档案盒
                AddNewArvBox(arvBox, false);
                // 关联档案和所属档案盒
                arv.ArvBox = arvBox;
            }
            else
            {
                // 关联档案和所属档案盒(注意不能用arvBox，否则ArvBox表里会多一条记录，arvBox的ArvBoxID和box实体的一样！！！)
                arv.ArvBox = box;
            }
          
            // 记录保存
            arv.ArvStatus = "在档";
            //arv.ArvStatus = "在库";
            arvRepository.Insert(arv, false);
            
            // 提交数据库
            return Context.Commit();
        }

        /// <summary>
        /// 多条档案同时入库
        /// </summary>
        /// <param name="arvs">档案集合</param>
        /// <returns></returns>
        public int InToStorage(List<ArchiveInfo> arvs)
        {
            arvs.ForEach(q =>{
                if (arvRepository.CheckExists(t => q.ArvID == t.ArvID))
                {
                    throw new Exception("试图添加档案编号重复的记录");
                }

                //q.ArvStatus = "在库";
                q.ArvStatus = "在档";
            });

            // 提交数据库
            return arvRepository.Insert(arvs);
        }

        /// <summary>
        /// 更新档案信息
        /// </summary>
        /// <param name="arv">档案实体</param>
        /// <returns></returns>
        public int UpdateArvInfo(ArchiveInfo arv,ArvBox arvBox=null)
        {
            if((arvBox != null) && (arvBox.ArvBoxID != null))
            {
                ArvBox box = arvBoxRepository.FindSingle(q => q.ArvBoxID == arvBox.ArvBoxID);
                if (box == null)
                {
                    // 新的档案盒
                    AddNewArvBox(arvBox, false);
                }

                arv.ArvBoxID = arvBox.ArvBoxID;
            }
            
            // 更新数据库记录
            return arvRepository.Update(arv);
        }

        /// <summary>
        /// 批量更新档案信息
        /// </summary>
        /// <param name="arvs">档案实体集合</param>
        /// <returns></returns>
        public int UpdateArvInfos(List<ArchiveInfo> arvs,ArvBox arvBox=null)
        {
            if(arvBox!=null)
            {
                ArvBox box = arvBoxRepository.FindSingle(q => q.ArvBoxID == arvBox.ArvBoxID);
                if (box == null)
                {
                    // 新的档案盒
                    AddNewArvBox(arvBox, false);
                }
                // 关联档案和所属档案盒
                arvs.ForEach(q => q.ArvBoxID = arvBox.ArvBoxID);
            }
            
           
            // 批量更新数据库记录
            return arvRepository.Update(arvs);
        }

        #endregion

        #region 借阅相关
        /// <summary>
        /// 查找在柜档案
        /// </summary>
        /// <returns></returns>
        public List<ArchiveInfo> GetArvInCab()
        {
            return arvRepository.Find(q => q.ArvStatus == "在柜").ToList();
        }

        public int ArvLend(ArvLendInfo lendInfo, List<ArchiveInfo> infos)
        {
            //List<ArchiveInfo> arvs = new List<ArchiveInfo>();
            infos.ForEach(q =>
            {
                q.ArvStatus = "借出";
               
                arvRepository.Update(q, false);
                //ArchiveInfo arv = arvRepository.GetByKey(q.ID);
                //arvs.Add(arv);
                ArvLendReturn item=new ArvLendReturn{// 每个实体生成独一无二的ID
            ID = Guid.NewGuid().ToString("N"),ArvID=q.ID,LendID=lendInfo.ID};
                arvLendReturnRepository.Insert(item,false);
            });
            //lendInfo.ArchiveInfos = arvs;
            
            lendRepository.Insert(lendInfo, false);
            return Context.Commit();
        }

        #endregion

        #region 归还相关
        /// <summary>
        /// 查找被借阅的档案
        /// </summary>
        /// <returns></returns>
        public List<ArchiveInfo> GetArvLended()
        {
            return arvRepository.Find(q => q.ArvStatus == "借出").ToList();
        }

        public List<ArvLendReturn> GetLendInfo()
        {
            List<ArvLendReturn> list = arvLendReturnRepository.Find(q => q.ReturnID == null).ToList();
            return arvLendReturnRepository.Find(q => q.ReturnID == null).ToList();
        }

        public int ArvReturn(ArvReturnInfo returnInfo, List<ArchiveInfo> arvInfos)
        {
            arvInfos.ForEach(q =>
            {
                q.ArvStatus = "在柜";
                arvRepository.Update(q, false);
                ArvReturnInfo info = returnInfo;
                //info.ArvID = q.ArvID;                
                returnRepository.Insert(returnInfo, false);               
            });
            return Context.Commit();
        }
        #endregion

        #region 出柜相关

        public int ArvOutput(OutCabInfo outInfo, List<ArchiveInfo> arvInfos)
        {
            arvInfos.ForEach(q =>
            {
                q.ArvStatus = "在库";
                OutCabInfo outModel = outInfo;
                outModel.ArvID = q.ArvID;
                arvRepository.Update(q, false);
                outCabRepository.Insert(outModel, false);
            });
            
            return Context.Commit();
        }

        #endregion

        #region 统计
        public List<DataMap<string>> GetGroupCount<TOrderBy>(IList<QueryCondition>  filter, QueryCondition groupBy)
        {
            Expression<Func<ArchiveInfo, bool>>   f = LamdaExpressionHelper.BuildExpression<ArchiveInfo>(filter);
            Expression<Func<ArchiveInfo, string>> g = LamdaExpressionHelper.BuildKeySelector<ArchiveInfo, string>(groupBy);
           
            return arvRepository.GetGroupCount<string>(f,g).ToList();
        }

        #endregion
    }
}
