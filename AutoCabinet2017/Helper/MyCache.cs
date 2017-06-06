using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCabinet2017.Helper
{
    /// <summary>
    /// 因为GridView不能保存未绑定的列值，所以通过此类来保存，通过GridView的CustomUnboundColumnData事件来实现存取
    /// </summary>
    public class MyCache
    {

        private readonly string _KeyFieldName;
        Dictionary<object, object> valuesCache = new Dictionary<object, object>();

        public MyCache(string keyFieldName)
        {
            _KeyFieldName = keyFieldName;
        }

        public object GetValue(object row)
        {
            return GetValueByKey(row);
        }

        public object GetValueByKey(object key)
        {
            object result = null;
            valuesCache.TryGetValue(key, out result);

            return result;
        }

        public void SetValue(object row, object value)
        {
            SetValueByKey(row, value);
        }

        public void SetValueByKey(object key, object value)
        {
            valuesCache[key] = value;
        }
    }
}
