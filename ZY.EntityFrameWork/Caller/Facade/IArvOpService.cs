using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using ZY.EntityFrameWork.Core.DBHelper;
using ZY.EntityFrameWork.Core.Model.Dto;
using ZY.EntityFrameWork.Core.Model.Entity;

namespace ZY.EntityFrameWork.Caller.Facade
{
    public interface IArvOpService
    {
        #region 档案盒操作

        /// <summary>
        /// 新增档案盒
        /// </summary>
        /// <param name="arvBoxDto">档案盒实体</param>
        /// <param name="arvLocDto">存储位置</param>
        /// <param name="isSave">是否立即保存</param>
        /// <returns></returns>
        int AddNewArvBox(ArvBoxDto arvBoxDto, bool isSave = true); 

        /// <summary>
        /// 删除档案盒
        /// </summary>
        /// <param name="arvBox">档案盒实体</param>
        /// <returns></returns>
        int DeleteArvBox(ArvBoxDto arvBox);

        /// <summary>
        /// 查询档案盒信息
        /// </summary>
        /// <param name="searchItems">查询条件集合</param>
        /// <returns></returns>
        List<ArvBoxDto> FindArvBoxes(IList<QueryCondition> searchItems);

        /// <summary>
        /// 更新档案盒信息
        /// </summary>
        /// <param name="arvBox">档案盒实体</param>
        /// <returns></returns>
        int UpdateArvBox(ArvBoxDto arvBox);

        /// <summary>
        /// 查找档案盒位置
        /// </summary>
        /// <param name="searchItems"></param>
        /// <returns></returns>
        List<ArvBoxDto> FindArvLocations(IList<QueryCondition> queryItems);

        #endregion

        #region 入库相关

        /// <summary>
        /// 删除档案
        /// </summary>
        /// <param name="arv"></param>
        /// <returns></returns>
        int Delete(ArchiveInfoDto arv);

        /// <summary>
        /// 批量删除档案
        /// </summary>
        /// <param name="arvs"></param>
        /// <returns></returns>
        int Delete(List<ArchiveInfoDto> arvs);

        /// <summary>
        /// 查找在档档案
        /// </summary>
        /// <returns></returns>
        List<ArchiveInfoDto> GetArvInDocument();

        /// <summary>
        /// 查找在库档案
        /// </summary>
        /// <returns></returns>
        List<ArchiveInfoDto> GetArvInStorage();

        /// <summary>
        /// 分页查找在档档案
        /// </summary>
        /// <param name="pageIndex">页检索</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="totalCount">总记录数</param>
        /// <param name="descending">降序排列</param>
        /// <returns>记录集</returns>
        List<ArchiveInfoDto> GetArvInDocument(int pageIndex, int pageSize, ref int totalCount, bool descending = false);

        /// <summary>
        /// 分页查找在库档案
        /// </summary>
        /// <param name="pageIndex">页检索</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="totalCount">总记录数</param>
        /// <param name="descending">降序排列</param>
        /// <returns>记录集</returns>
        List<ArchiveInfoDto> GetArvInStorage(int pageIndex, int pageSize, ref int totalCount, bool descending = false);

        /// <summary>
        /// 按检索条件查找档案
        /// </summary>
        /// <param name="queryItems"></param>
        /// <returns>记录集</returns>
        List<ArchiveInfoDto> FindArvs(IList<QueryCondition> queryItems);

        /// <summary>
        /// 档案入库
        /// </summary>
        /// <param name="arv">档案实体</param>
        int InToStorage(ArchiveInfoDto arv);

        /// <summary>
        /// 批量档案入库
        /// </summary>
        /// <param name="arvs">档案集合</param>
        /// <returns></returns>
        int InToStorage(List<ArchiveInfoDto> arvs);

        /// <summary>
        /// 更新档案信息
        /// </summary>
        /// <param name="arv">档案实体</param>
        /// <returns></returns>
        int UpdateArvInfo(ArchiveInfoDto arvDto);

        /// <summary>
        /// 批量更新档案信息
        /// </summary>
        /// <param name="arvs">档案实体集合</param>
        /// <returns></returns>
        int UpdateArvInfos(List<ArchiveInfoDto> arvDtos, ArvBoxDto boxDto=null);

        #endregion

        #region 借阅相关
        /// <summary>
        /// 查找在柜档案
        /// </summary>
        /// <returns></returns>
        List<ArchiveInfoDto> GetArvInCab();

        int ArvLend(ArvLendInfoDto lendInfo, List<ArchiveInfoDto> arvInfos);

        #endregion

        #region 归还相关
        /// <summary>
        /// 查找被借阅的档案
        /// </summary>
        /// <returns></returns>
        List<ArchiveInfoDto> GetArvLended();


        List<ArvLendInfoDto> GetLendInfo();

        int ArvReturn(ArvReturnInfoDto returnInfo, List<ArvLendInfoDto> arvInfos);

        #endregion

        #region 统计
        List<DataMap<string>> GetGroupCount<TOrderBy>(IList<QueryCondition> filter, QueryCondition groupBy);
        #endregion
    }
}
