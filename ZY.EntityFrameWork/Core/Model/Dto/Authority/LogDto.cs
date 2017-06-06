using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZY.EntityFrameWork.Core.Model.Dto
{
    public class LogDto : BaseDto
    {
        public LogDto()
        {
            // 每个实体生成独一无二的ID
            ID = Guid.NewGuid().ToString("N");
        }

        //日期
        [DisplayName("时间")]
        public DateTime Datetime { get; set; }

        //用户编码
        [DisplayName("用户编码")]
        public string UserCode { get; set; }

        //线程
        public string Thread { get; set; }

        //类型
        [DisplayName("日志类型")]
        public string Logger { get; set; }

        //消息
        [DisplayName("消息")]
        public string Message { get; set; }

        //错误
        [DisplayName("错误")]
        public string Exception { get; set; }

        //级别
        [DisplayName("级别")]
        public string Level { get; set; }
    }
}
