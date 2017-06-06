using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZY.EntityFrameWork.Core.Model.Entity
{
    // <summary>
    // 映射数据库中的设备表
    // </summary>
    public class Device : BaseEntity
    {
        // <summary>
        // 档案柜编号
        // </summary>
        public int CabinetNo { get; set; }

        // <summary>
        // 档案柜层数
        // </summary>
        public int CabinetLayers { get; set; }

        // <summary>
        // 档案柜每层的格数
        // </summary>
        public int CabinetCells { get; set; }
    }
}
