using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System;

using ZY.EntityFrameWork.Core.Model.Entity;
using ZY.EntityFrameWork.Core.Model.Dto;
using ZY.EntityFrameWork.Core.Services;
using ZY.EntityFrameWork.Core.DBHelper;

using ZY.EntityFrameWork.WcfSvcLib.Interface;

namespace ZY.EntityFrameWork.WcfSvcLib.Impl
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“ArvOPService”。
    public class WcfArvOpSvc : IWcfArvOpService
    {
        public BaseArvOpService baseArvOpService = new BaseArvOpService();

        #region 档案盒
        public int AddNewArvBox(ArvBoxDto arvBoxDto, bool isSave = true) //ArvLocationDto arvLocationDto,bool isSave=true)
        {
            ArvBox arvBoxModel = arvBoxDto.MapTo<ArvBox>();
            //ArvLocation arvLocationModel = arvLocationDto.MapTo<ArvLocation>();

            return baseArvOpService.AddNewArvBox(arvBoxModel, isSave);//arvLocationModel,isSave);
        }

        public int DeleteArvBox(ArvBoxDto arvBox)
        {
            return baseArvOpService.DeleteArvBox(arvBox.MapTo<ArvBox>());
        }

        public List<ArvBoxDto> FindArvBoxes(IList<QueryCondition> searchItems)
        {
            return baseArvOpService.FindArvBoxes(searchItems).MapTo<List<ArvBoxDto>>();
        }

        public int UpdateArvBox(ArvBoxDto arvBox)
        {
            return baseArvOpService.UpdateArvBox(arvBox.MapTo<ArvBox>());
        }

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
            return baseArvOpService.GetArvInStorage().MapTo<List<ArchiveInfoDto>>();
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
            return baseArvOpService.GetArvInStorage(pageIndex, pageSize, ref totalCount, descending).MapTo<List<ArchiveInfoDto>>();
        }

        /// <summary>
        /// 档案入库
        /// </summary>
        /// <param name="arv"></param>
        public int InToStorage(ArchiveInfoDto arvDto)
        {
            ArvBox arvBox = arvDto.MapTo<ArvBox>();
            //ArvLocation loc = arvDto.MapTo<ArvLocation>();
            ArchiveInfo arv = arvDto.MapTo<ArchiveInfo>();
            return baseArvOpService.InToStorage(arv, arvBox);//, loc);
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
            return baseArvOpService.UpdateArvInfo(arvDto.MapTo<ArchiveInfo>());
        }

        public int UpdateArvInfos(List<ArchiveInfoDto> arvDtos)
        {
            return baseArvOpService.UpdateArvInfos((arvDtos.MapTo<List<ArchiveInfo>>()));
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

        public int ArvReturn(ArvReturnInfoDto returnInfo, List<ArchiveInfoDto> arvInfos)
        {
            List<ArchiveInfo> infos = arvInfos.MapTo<List<ArchiveInfo>>();
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
    }
}
