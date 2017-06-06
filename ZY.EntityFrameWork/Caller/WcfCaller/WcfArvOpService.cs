using System.Collections.Generic;
using System.Linq;
using System;
using System.Linq.Expressions;

using ZY.EntityFrameWork.Core.DBHelper;
using ZY.EntityFrameWork.Core.Model.Dto;
using ZY.EntityFrameWork.Core.Model.Entity;
using ZY.EntityFrameWork.Caller.Facade;
using ZY.EntityFrameWork.WcfSvcLib.Interface;

namespace ZY.EntityFrameWork.Caller.WcfCaller
{
    public class WcfArvOpService : ServiceProxyBase<IWcfArvOpService>, IArvOpService
    {
        public WcfArvOpService() : base("WcfArvOpService") { }

        #region 档案盒
        public int AddNewArvBox(ArvBoxDto arvBoxDto, bool isSave = true)//ArvLocationDto arvLocationDto, bool isSave = true)
        {
            return 0;
        }

        public int DeleteArvBox(ArvBoxDto arvBox)
        {
            return 0;
        }

        public List<ArvBoxDto> FindArvBoxes(IList<QueryCondition> searchItems)
        {
            return null;
        }

        public int UpdateArvBox(ArvBoxDto arvBox)
        {
            return 0;
        }


        #endregion

        #region 位置信息
        //public int AddNewLocation(ArvLocationDto dto)
        //{
        //    return this.Invoker.Invoke<int>(q => q.AddNewLocation(dto));
        //}

        //public int AddNewLocations(List<ArvLocationDto> dtos)
        //{
        //    return this.Invoker.Invoke<int>(q => q.AddNewLocations(dtos));
        //}

        public List<ArvBoxDto> FindArvLocations(IList<QueryCondition> searchItems)
        {
            return null;// this.Invoker.Invoke<List<ArvBoxDto>>(q => q.GetArvLocations(searchItems));
        }


        #endregion

        #region 入库相关

        /// <summary>
        /// 删除档案
        /// </summary>
        /// <param name="arv">实体类型</param>
        /// <returns>操作是否成功</returns>
        public int Delete(ArchiveInfoDto arvDto)
        {
            return this.Invoker.Invoke<int>(q => q.Delete(arvDto));
        }

        /// <summary>
        /// 批量删除档案
        /// </summary>
        /// <param name="arvs">档案集合</param>
        /// <returns>操作是否成功</returns>
        public int Delete(List<ArchiveInfoDto> arvDtos)
        {
            return this.Invoker.Invoke<int>(q => q.Delete(arvDtos));
        }

        /// <summary>
        /// 查找在档档案
        /// </summary>
        /// <returns></returns>
        public List<ArchiveInfoDto> GetArvInDocument()
        {
            return this.Invoker.Invoke<List<ArchiveInfoDto>>(q => q.GetArvInStorage());
        }

        /// <summary>
        /// 查找在库档案
        /// </summary>
        /// <returns></returns>
        public List<ArchiveInfoDto> GetArvInStorage()
        {
            return this.Invoker.Invoke<List<ArchiveInfoDto>>(q => q.GetArvInStorage());
        }

        /// <summary>
        /// 按检索条件查找档案
        /// </summary>
        /// <param name="queryItems"></param>
        /// <returns>记录集</returns>
        public List<ArchiveInfoDto> FindArvs(IList<QueryCondition> searchItems)
        {
            return null;
        }


        /// <summary>
        /// 分页查找在档档案
        /// </summary>
        /// <param name="pageIndex">页检索</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="totalCount">总记录数</param>
        /// <param name="descending">降序排列</param>
        /// <returns>记录集</returns>
        public List<ArchiveInfoDto> GetArvInDocument(int pageIndex, int pageSize, ref int totalCount, bool descending = false)
        {
            //return this.Invoker.Invoke<List<ArchiveInfoDto>>(q => q.GetArvInStorage(pageIndex,pageSize,ref totalCount,descending));
            return null;
        }

        /// <summary>
        /// 分页查找在库档案
        /// </summary>
        /// <param name="pageIndex">页检索</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="totalCount">总记录数</param>
        /// <param name="descending">降序排列</param>
        /// <returns>记录集</returns>
        public List<ArchiveInfoDto> GetArvInStorage(int pageIndex, int pageSize, ref int totalCount, bool descending = false)
        {
            //return this.Invoker.Invoke<List<ArchiveInfoDto>>(q => q.GetArvInStorage(pageIndex,pageSize,ref totalCount,descending));
            return null;
        }

        /// <summary>
        /// 档案入库
        /// </summary>
        /// <param name="arv"></param>
        public int InToStorage(ArchiveInfoDto arv)
        {
            return this.Invoker.Invoke<int>(q => q.InToStorage(arv));
        }

        /// <summary>
        /// 批量档案入库
        /// </summary>
        /// <param name="arvs"></param>
        /// <returns></returns>
        public int InToStorage(List<ArchiveInfoDto> arvs)
        {
            return this.Invoker.Invoke<int>(q => q.InToStorage(arvs));
        }

        ///// <summary>
        ///// 单个档案放入柜中(后期看情况第二个参数改成档案盒编号)
        ///// </summary>
        ///// <param name="dto"></param>
        ///// <param name="locDto"></param>
        ///// <returns></returns>
        //public int InputToCab(ArchiveInfoDto dto, ArvLocationDto locDto)
        //{
        //    return this.Invoker.Invoke<int>(q => q.InputToCab(dto,locDto));
        //}

        ///// <summary>
        ///// 多个档案放入同一位置
        ///// </summary>
        ///// <param name="arvs"></param>
        //public int InputToCab(List<ArchiveInfoDto> arvs, ArvLocationDto locDto)
        //{
        //    return this.Invoker.Invoke<int>(q => q.InputToCab(arvs, locDto));
        //}

        /// <summary>
        /// 更新档案信息
        /// </summary>
        /// <param name="arv">档案实体</param>
        /// <returns></returns>
        public int UpdateArvInfo(ArchiveInfoDto arvDto)
        {
            return this.Invoker.Invoke<int>(q => q.UpdateArvInfo(arvDto));
        }

        /// <summary>
        /// 批量更新档案信息
        /// </summary>
        /// <param name="arvs">档案实体集合</param>
        /// <returns></returns>
        public int UpdateArvInfos(List<ArchiveInfoDto> arvDtos, ArvBoxDto boxDto)
        {
            return this.Invoker.Invoke<int>(q => q.UpdateArvInfos(arvDtos));
        }
        #endregion

        #region 借阅相关
        /// <summary>
        /// 查找在柜档案
        /// </summary>
        /// <returns></returns>
        public List<ArchiveInfoDto> GetArvInCab()
        {
            return this.Invoker.Invoke<List<ArchiveInfoDto>>(q => q.GetArvInCab());
        }

        public int ArvLend(ArvLendInfoDto lendInfo, List<ArchiveInfoDto> arvInfos)
        {
            return this.Invoker.Invoke<int>(q => q.ArvLend(lendInfo,arvInfos));
        }

        #endregion

        #region 归还相关
        /// <summary>
        /// 查找被借阅的档案
        /// </summary>
        /// <returns></returns>
        public List<ArchiveInfoDto> GetArvLended()
        {
            return this.Invoker.Invoke<List<ArchiveInfoDto>>(q => q.GetArvLended());
        }

        public List<ArvLendInfoDto> GetLendInfo()
        {
            return null;
        }

        public int ArvReturn(ArvReturnInfoDto returnInfo, List<ArchiveInfoDto> arvInfos)
        {
            return this.Invoker.Invoke<int>(q => q.ArvReturn(returnInfo, arvInfos));
        }
        #endregion

        #region 出柜相关

        public int ArvOutput(OutCabInfoDto dto, List<ArchiveInfoDto> arvInfos)
        {
            return this.Invoker.Invoke<int>(q => q.ArvOutput(dto, arvInfos));
        }

        #endregion

        #region 统计
        public List<DataMap<string>> GetGroupCount<TOrderBy>(IList<QueryCondition> filter, QueryCondition groupBy)
        {
            return null;
        }
        #endregion
    }
}
