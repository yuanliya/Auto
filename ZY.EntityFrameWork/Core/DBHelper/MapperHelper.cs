using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using AutoMapper;

using ZY.EntityFrameWork.Core.Model.Entity;
using ZY.EntityFrameWork.Core.Model.Dto;

namespace ZY.EntityFrameWork.Core.DBHelper
{
    /// <summary>
    /// AutoMapper扩展帮助类
    /// </summary>
    public static class AutoMapperHelper
    {
        /// <summary>
        /// 建立Dto和Entity之间的映射关系，在第一次使用时调用
        /// </summary>
        static AutoMapperHelper()
        {
            Mapper.Initialize(cfg => cfg.AddProfile<MappingProfile>());
        }

        /// <summary>
        /// 执行实体映射操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T MapTo<T>(this object obj)
        {
            if (obj == null) return default(T);

            Type t = obj.GetType();

            // 同种类型之间的映射，如果添加了Map，是深复制，否则是浅复制
            //if (Mapper.Configuration.FindTypeMapFor(obj.GetType(), typeof(T)) == null)
            //{
                //if (typeof(T) != obj.GetType())
                //{
                    //return default(T);
                //}
            //}

            return Mapper.Map<T>(obj);
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                #region 映射档案信息的Dto和Model

                // AutoMap的ForMember指定每一个字段的映射规则
                // dest表示DTO，ori表示实体；
                // 将实体关联的ArvBox的GroupNo/LayerNo/CellNo/ArvBoxID/ArvBoxTitle字段映射到DTO的对应字段
                CreateMap<ArchiveInfo, ArchiveInfoDto>().ForMember(dest => dest.GroupNo, ori => ori.MapFrom(s => s.ArvBox.GroupNo))
                     .ForMember(dest => dest.LayerNo, ori => ori.MapFrom(s => s.ArvBox.LayerNo))
                     .ForMember(dest => dest.CellNo, ori => ori.MapFrom(s => s.ArvBox.CellNo))
                     .ForMember(dest => dest.ArvBoxID, ori => ori.MapFrom(s => s.ArvBox.ArvBoxID))           
                     .ForMember(dest => dest.ArvBoxTitle, ori => ori.MapFrom(s => s.ArvBox.ArvBoxTitle));
                // 映射外键
                CreateMap<ArchiveInfoDto, ArchiveInfo>().ForMember(dest => dest.ArvBoxID, ori => ori.MapFrom(s => s.ArvBoxID));

                // 档案DTO中还含有档案盒信息
                CreateMap<ArchiveInfoDto, ArvBox>();

                // 档案借阅DTO中包含有档案信息
                CreateMap<ArvLendInfoDto, ArchiveInfo>();

                #endregion

                // DTO与Model的结构和字段完全一致，直接调用CreateMap<Dto, Model>()完成映射
                CreateMap<ArvBoxDto, ArvBox>();
                CreateMap<ArvBox, ArvBoxDto>();


                CreateMap<ArvLendInfo, ArvLendInfoDto>();//.ForMember(dest=>dest.ArchiveID,ori=>ori.MapFrom(s=>s.ArchiveInfo.ArvID));
                CreateMap<ArvLendInfoDto, ArvLendInfo>();

                CreateMap<ArvLendReturn, ArvLendInfoDto>().ForMember(dest => dest.ArchiveID, ori => ori.MapFrom(s => s.ArchiveInfo.ArvID)).ForMember(dest => dest.Lender, ori => ori.MapFrom(s => s.ArvLend.Lender)).ForMember(dest => dest.LendDate, ori => ori.MapFrom(s => s.ArvLend.LendDate)).ForMember(dest => dest.ReturnDeadline, ori => ori.MapFrom(s => s.ArvLend.ReturnDeadline)).ForMember(dest => dest.LendExecuter, ori => ori.MapFrom(s => s.ArvLend.LendExecuter));

                CreateMap<ArvLendInfoDto, ArvLendReturn>();

                CreateMap<ArvReturnInfoDto, ArvReturnInfo>();
                CreateMap<ArvReturnInfo, ArvReturnInfoDto>();

                // DTO与Model的结构和字段完全一致，直接调用CreateMap<Dto, Model>()完成映射
                CreateMap<User, UserDto>().ForMember(dest => dest.RoleName, ori => ori.MapFrom(s => s.UserRole.RoleName))
                                          .ForMember(dest => dest.RoleLevel, ori => ori.MapFrom(s => s.UserRole.Level))
                                          .ForMember(dest => dest.RoleId, ori => ori.MapFrom(s => s.RoleId));
                CreateMap<UserDto, User>().ForMember(dest => dest.RoleId, ori => ori.MapFrom(q => q.RoleId));

                // DTO与Model的结构和字段完全一致，直接调用CreateMap<Dto, Model>()完成映射
                CreateMap<Module, ModuleDto>();
                CreateMap<ModuleDto, Module>();
                CreateMap<ModuleDto, ModuleDto>();//深复制

                CreateMap<RoleModule, RoleModuleDto>().ForMember(dest => dest.ModuleName, ori => ori.MapFrom(s => s.Module.ModuleName))
                                                      .ForMember(dest => dest.ModuleTag, ori => ori.MapFrom(s => s.Module.ModuleTag))
                                                      .ForMember(dest => dest.RoleName, ori => ori.MapFrom(s => s.Role.RoleName));
                CreateMap<RoleModuleDto, RoleModule>();

                #region DTO与Model的结构和字段完全一致，直接调用CreateMap<Dto, Model>()完成映射

                CreateMap<Role, RoleDto>();
                CreateMap<RoleDto, Role>();

                CreateMap<FieldCfg, FieldCfgDto>();
                CreateMap<FieldCfgDto, FieldCfg>();

                CreateMap<DataDict, DataDictDto>();
                CreateMap<DataDictDto, DataDict>();

                CreateMap<Device, DeviceDto>();
                CreateMap<DeviceDto, Device>();

                CreateMap<LogDto, Log>();
                CreateMap<Log, LogDto>();

                #endregion
            }
        }
    }
}