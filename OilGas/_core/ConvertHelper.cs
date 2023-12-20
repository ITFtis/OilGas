using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OilGas
{
    /// <summary>
    /// ConvertHelper 的摘要描述
    /// </summary>
    public static class ConvertHelper
    {

        /// <summary>
        /// 轉日期
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static DateTime? ToDateTime(String t)
        {
            DateTime r;
            if (!DateTime.TryParse(t, out r))
                return null;
            return r;
        }

        public static DateTime? ToDateTime(object o)
        {
            if (o == null) return null;
            return ToDateTime(o.ToString());
        }

        public static string DateToString(DateTime? d)
        {
            if (d == null)
                return "";
            return Convert.ToDateTime(d).ToString("yyyy/MM/dd");
        }
		public static string BooleanToString(Boolean? b)
		{
			if (b == null || b == false)
				return "0";
			return "1";
		}
		public static int StringToInt(string value)
		{
			if (string.IsNullOrEmpty(value))
				return -1;
			return Convert.ToInt32(value);
		}
        public static int ToInt(object value)
        {
            if (value == null)
                return -1;
            int result;
            return int.TryParse(value.ToString(),out result) ? result:-1;
        }
 		public static string ControlListValueToString(string value)
		{
			if (string.IsNullOrEmpty(value))
				return null;
			return value;
		}
    }
}