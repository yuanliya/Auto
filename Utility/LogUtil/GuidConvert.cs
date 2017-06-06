using log4net.Core;
using log4net.Layout;
using log4net.Layout.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NJUST.AUTO06.Utility.LogUtil
{
    //自定义字段ID 对应%Guid
    public class GuidConvert : PatternLayoutConverter
    {

        protected override void Convert(System.IO.TextWriter writer, LoggingEvent loggingEvent)
        {
                writer.Write(Guid.NewGuid().ToString("N"));
        }
    }

    public class GuidLayout : PatternLayout
    {
        public GuidLayout()
        {
            //添加Convert 
            this.AddConverter("Guid", typeof(GuidConvert));
        }
    }
}
