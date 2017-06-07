using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System;

using ZY.EntityFrameWork.Core.Model.Dto;
using ZY.EntityFrameWork.Core.Model.Entity;
using ZY.EntityFrameWork.Core.Services;
using ZY.EntityFrameWork.Core.DBHelper;
using ZY.EntityFrameWork.Caller.Facade;

namespace ZY.EntityFrameWork.Caller.WinformCaller
{
    /// <summary>
    /// 档案操作服务接口
    /// </summary>
    public class WinArvOpService : IArvOpService
    {
        public BaseArvOpService baseArvOpService = new BaseArvOpService();

        #region 档案盒操作

        /// <summary>
        /// 新增档案盒
        /// </summary>
        /// <param name="arvBox">档案盒实体</param>
        /// <param name="isSave">是否立即保存</param>
        /// <returns></returns>
        public int AddNewArvBox(ArvBoxDto arvBoxDto, bool isSave = true)
        {
            // DTO类型映射为Model实体类型
            ArvBox      arvBoxModel      = arvBoxDto.MapTo<ArvBox>();

            // 执行新增档案盒操作
            return baseArvOpService.AddNewArvBox(arvBoxModel, isSave);
        }

        /// <summary>
        /// 删除档案盒
        /// </summary>
        /// <param name="arvBox">档案盒实体</param>
        /// <returns></returns>
        public int DeleteArvBox(ArvBoxDto arvBox)
        {
            // 执行删除档案盒操作
            return baseArvOpService.DeleteArvBox(arvBox.MapTo<ArvBox>());
        }

        /// <summary>
        /// 查询档案盒信息
        /// </summary>
        /// <param name="searchItems">查询条件集合</param>
        /// <returns></returns>
        public List<ArvBoxDto> FindArvBoxes(IList<QueryCondition> queryItems)
        {
            // 执行查询档案盒操作
            return baseArvOpService.FindArvBoxes(queryItems).MapTo<List<ArvBoxDto>>();
        }

        /// <summary>
        /// 更新档案盒信息
        /// </summary>
        /// <param name="arvBox">档案盒实体</param>
        /// <returns></returns>
        public int UpdateArvBox(ArvBoxDto arvBox)
        {
            // 执行更新档案盒操作
            return baseArvOpService.UpdateArvBox(arvBox.MapTo<ArvBox>());
        }

        public List<ArvBoxDto> FindArvLocations(IList<QueryCondition> queryItems)
        {
            return null;// baseArvOpService.GetArvLocations(searchItems).MapTo<List<ArvBoxDto>>();
        }

        #endregion

        #region 位置信息
        /// <summary>
        /// 判断位置信息是否已经存在
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        //public bool CheckLocationExists(ArvLocationDto dto)
        //{
        //    return baseArvOpService.CheckLocationExists(dto.MapTo<ArvLocation>());
        //}

        //public int AddNewLocation(ArvLocationDto dto)
        //{
        //    return baseArvOpService.AddNewLocation(dto.MapTo<ArvLocation>());
        //}

        //public int AddNewLocations(List<ArvLocationDto> dtos)
        //{
        //    return baseArvOpService.AddNewLocations(dtos.MapTo<List<ArvLocation>>());
        //}

        //public List<ArvLocationDto> FindArvLocations(IList<QueryConditionHelper> searchItems)
        //{
        //    return baseArvOpService.GetArvLocations(searchItems).MapTo<List<ArvLocationDto>>();
        //}


        #endregion

        #region 入库相关
        public int Delete(ArchiveInfoDto arvDto)
        {
            return baseArvOpService.Delete(arvDto.MapTo<ArchiveInfo>());
        }

        public int Delete(List<ArchiveInfoDto> arvDtos)
        {
            return baseArvOpService.Delete(arvDtos.MapTo<List<ArchiveInfo>>());
        }

        /// <summary>
        /// 查找在档档案
        /// </summary>
        /// <returns></returns>
        public List<ArchiveInfoDto> GetArvInDocument()
        {
            //IQueryble无法映射，可能是因为它在枚举之前是并没有加载数据的原因，其他地方都要换成List或Inumerable
            return baseArvOpService.GetArvInDocument().MapTo<List<ArchiveInfoDto>>();
        }

        /// <summary>
        /// 查找在库档案
        /// </summary>
        /// <returns></returns>
        public List<ArchiveInfoDto> GetArvInStorage()
        {
            //IQueryble无法映射，可能是因为它在枚举之前是并没有加载数据的原因，其他地方都要换成List或Inumerable
            return baseArvOpService.GetArvInStorage().MapTo<List<ArchiveInfoDto>>();
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
            return baseArvOpService.GetArvInDocument(pageIndex, pageSize, ref totalCount, descending).MapTo<List<ArchiveInfoDto>>();
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
            return baseArvOpService.GetArvInStorage(pageIndex, pageSize,ref totalCount, descending).MapTo<List<ArchiveInfoDto>>();
        }

        /// <summary>
        /// 按检索条件查找档案
        /// </summary>
        /// <param name="queryItems"></param>
        /// <returns>记录集</returns>
        public List<ArchiveInfoDto> FindArvs(IList<QueryCondition> searchItems)
        {
            return baseArvOpService.FindArvs(searchItems).MapTo<List<ArchiveInfoDto>>();
        }

        /// <summary>
        /// 档案入库
        /// </summary>
        /// <param name="arv"></param>
        public int InToStorage(ArchiveInfoDto arvDto)
        {
            ArvBox arvBox = arvDto.MapTo<ArvBox>();
            ArchiveInfo arv = arvDto.MapTo<ArchiveInfo>();

            return baseArvOpService.InToStorage(arv, arvBox);
        }

        /// <summary>
        /// 多条档案同时入库
        /// </summary>
        /// <param name="arvs"></param>
        /// <returns></returns>
        public int InToStorage(List<ArchiveInfoDto> arvs)
        {
            return baseArvOpService.InToStorage(arvs.MapTo<List<ArchiveInfo>>());
        }


        public int UpdateArvInfo(ArchiveInfoDto arvDto)
        {
            return baseArvOpService.UpdateArvInfo(arvDto.MapTo<ArchiveInfo>(),arvDto.MapTo<ArvBox>());
        }

        public int UpdateArvInfos(List<ArchiveInfoDto> arvDtos,ArvBoxDto boxDto)
        {
            return baseArvOpService.UpdateArvInfos(arvDtos.MapTo<List<ArchiveInfo>>(), boxDto.MapTo<ArvBox>());
        }
        #endregion

        #region 借阅相关
        /// <summary>
        /// 查找在柜档案
        /// </summary>
        /// <returns></returns>
        public List<ArchiveInfoDto> GetArvInCab()
        {
            return baseArvOpService.GetArvInCab().MapTo<List<ArchiveInfoDto>>();
        }

        public int ArvLend(ArvLendInfoDto lendInfo, List<ArchiveInfoDto> arvInfos)
        {
            List<ArchiveInfo> infos = arvInfos.MapTo<List<ArchiveInfo>>();
            ArvLendInfo info = lendInfo.MapTo<ArvLendInfo>();
            return baseArvOpService.ArvLend(info, infos);
        }

        #endregion

        #region 归还相关
        /// <summary>
        /// 查找被借阅的档案
        /// </summary>
        /// <returns></returns>
        public List<ArchiveInfoDto> GetArvLended()
        {
            return baseArvOpService.GetArvLended().MapTo<List<ArchiveInfoDto>>();
        }

        public List<ArvLendInfoDto> GetLendInfo()
        {
            return baseArvOpService.GetLendInfo().MapTo<List<ArvLendInfoDto>>();
        }


        public int ArvReturn(ArvReturnInfoDto returnInfo, List<ArvLendInfoDto> arvInfos)
        {
            List<ArvLendReturn> infos = arvInfos.MapTo<List<ArvLendReturn>>();
            ArvReturnInfo info = returnInfo.MapTo<ArvReturnInfo>();
            return baseArvOpService.ArvReturn(info, infos);
        }
        #endregion

        #region 出柜相关

        public int ArvOutput(OutCabInfoDto dto, List<ArchiveInfoDto> arvInfos)
        {
            List<ArchiveInfo> infos = arvInfos.MapTo<List<ArchiveInfo>>();
            OutCabInfo outModel = dto.MapTo<OutCabInfo>();

            return baseArvOpService.ArvOutput(outModel, infos);
        }

        #endregion

        #region 统计
        public List<DataMap<string>> GetGroupCount<TOrderBy>(IList<QueryCondition> filter, QueryCondition groupBy)
        {
            return baseArvOpService.GetGroupCount<TOrderBy>(filter,groupBy);
        }
        #endregion
    }
}
