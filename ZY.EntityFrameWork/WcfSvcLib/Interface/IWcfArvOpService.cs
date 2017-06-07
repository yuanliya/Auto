using System;
using System.ServiceModel;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

using ZY.EntityFrameWork.Core.Model.Dto;
using ZY.EntityFrameWork.Core.DBHelper;

namespace ZY.EntityFrameWork.WcfSvcLib.Interface
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IArvOPService”。
    [ServiceContract]
    public interface IWcfArvOpService
    {
        #region 档案盒
        [OperationContract]
        int AddNewArvBox(ArvBoxDto arvBox, bool isSave = true); 

        [OperationContract]
        int DeleteArvBox(ArvBoxDto arvBox);

        [OperationContract]
        List<ArvBoxDto> FindArvBoxes(IList<QueryCondition> searchItems);

        [OperationContract]
        int UpdateArvBox(ArvBoxDto arvBox);

        #endregion

        #region 入库相关
        [OperationContract]
        int Delete(ArchiveInfoDto arv);

        [OperationContract]
        int Delete(List<ArchiveInfoDto> arvs);

        /// <summary>
        /// 查找在档档案
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<ArchiveInfoDto> GetArvInDocument();

        /// <summary>
        /// 查找在库档案
        /// </summary>
        /// <returns></returns>
        [OperationContract]     
        List<ArchiveInfoDto> GetArvInStorage();

        /// <summary>
        /// 分页查找在库档案
        /// </summary>
        /// <param name="pageIndex">页检索</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="totalCount">总记录数</param>
        /// <param name="descending">降序排列</param>
        /// <returns>记录集</returns>
        [OperationContract(Name = "GetPagedArvInStorage")]
        List<ArchiveInfoDto> GetArvInStorage(int pageIndex, int pageSize, ref int totalCount, bool descending = false);

        /// <summary>
        /// 分页查找在档档案
        /// </summary>
        /// <param name="pageIndex">页检索</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="totalCount">总记录数</param>
        /// <param name="descending">降序排列</param>
        /// <returns>记录集</returns>
        [OperationContract(Name = "GetPagedArvInDocument")]
        List<ArchiveInfoDto> GetArvInDocument(int pageIndex, int pageSize, ref int totalCount, bool descending = false);

        /// <summary>
        /// 档案入库
        /// </summary>
        /// <param name="arv"></param>
        [OperationContract(Name = "SingleInToStorage")]    
        int InToStorage(ArchiveInfoDto arv);

        /// <summary>
        /// 多条档案同时入库
        /// </summary>
        /// <param name="arvs"></param>
        /// <returns></returns>
        [OperationContract(Name = "MultiInToStorage")]      
        int InToStorage(List<ArchiveInfoDto> arvs);

        //[OperationContract(Name = "SingleInputToCab")]
        ///// <summary>
        ///// 单个档案放入柜中(后期看情况第二个参数改成档案盒编号)
        ///// </summary>
        ///// <param name="dto"></param>
        ///// <param name="locDto"></param>
        ///// <returns></returns>
        //int InputToCab(ArchiveInfoDto dto, ArvLocationDto locDto);

        //[OperationContract(Name = "MultiInputToCab")]
        ///// <summary>
        ///// 多个档案放入同一位置
        ///// </summary>
        ///// <param name="arvs"></param>
        //int InputToCab(List<ArchiveInfoDto> arvs, ArvLocationDto locDto);

        [OperationContract]
        int UpdateArvInfo(ArchiveInfoDto arvDto);

        [OperationContract]
        int UpdateArvInfos(List<ArchiveInfoDto> arvDtos);

        #endregion

        #region 借阅相关
        [OperationContract]
        /// <summary>
        /// 查找在柜档案
        /// </summary>
        /// <returns></returns>
        List<ArchiveInfoDto> GetArvInCab();

        [OperationContract]
        int ArvLend(ArvLendInfoDto lendInfo, List<ArchiveInfoDto> arvInfos);

        #endregion

        #region 归还相关
        [OperationContract]
        /// <summary>
        /// 查找被借阅的档案
        /// </summary>
        /// <returns></returns>
        List<ArchiveInfoDto> GetArvLended();

        [OperationContract]
        int ArvReturn(ArvReturnInfoDto returnInfo, List<ArvLendInfoDto> arvInfos);

        #endregion

        #region 出柜相关

        [OperationContract]
        int ArvOutput(OutCabInfoDto dto, List<ArchiveInfoDto> arvInfos);

        #endregion
    }
}
