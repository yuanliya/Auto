using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZY.EntityFrameWork.Core.Model.Entity
{
    public class Log : BaseEntity
    {
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime Datetime { get; set; }

        /// <summary>
        /// 用户编码
        /// </summary>
        public string UserCode { get; set; }

        /// <summary>
        /// 线程
        /// </summary>
        public string Thread { get; set; }

        /// <summary>
        /// 类别
        /// </summary>
        public string Logger { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 错误内容
        /// </summary>
        public string Exception { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        public string Level { get; set; }
    }
}
