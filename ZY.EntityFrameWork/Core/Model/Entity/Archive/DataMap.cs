using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZY.EntityFrameWork.Core.Model.Entity
{
    /// <summary>
    /// 用于统计时的显示
    /// </summary>
    public class DataMap<T>
    {
        public T Key { get; set; }
        public int Value { get; set; }
    }
}
