using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ZY.EntityFrameWork.Core.Model.Dto
{
    /// <summary>
    /// 面向应用层的设备DTO实体
    /// </summary>
    public class DeviceDto : BaseDto
    {
        public DeviceDto()
        {
            // 每个实体生成独一无二的ID
            ID = Guid.NewGuid().ToString("N");
        }

        [DisplayName("回转库编号")]
        /// <summary>
        /// 档案柜编号
        /// </summary>
        public int CabinetNo { get; set; }

        [DisplayName("回转库层数")]
        /// <summary>
        /// 档案柜层数
        /// </summary>
        public int CabinetLayers { get; set; }

        [DisplayName("回转库格数")]
        /// <summary>
        /// 档案柜每层的格数
        /// </summary>       
        public int CabinetCells { get; set; }
    }
}
