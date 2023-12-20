using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OilGas
{
    /// <summary>
    /// 加油站/A統計報表專區/地下儲油槽列管狀況報表
    /// </summary>
    public class Rpt_CarFuel_GSM_Select
    {
        internal const int shortcacheduration = 5 * 60 * 1000;
        static object lockGetAllvsCFGS = new object();

        public static IEnumerable<vw_CarFuel_GSM_Select> GetAllvsCFGS(int cachetimer = shortcacheduration)
        {
            string key = "OilGas.GetAllvsCFGS";
            var alldatas = DouHelper.Misc.GetCache<IEnumerable<vw_CarFuel_GSM_Select>>(cachetimer, key);
            lock (lockGetAllvsCFGS)
            {
                if (alldatas == null)
                {
                    using (var cxt = new OilGasModelContextExt())
                    {
                        alldatas = cxt.vw_CarFuel_GSM_Select.ToArray();
                        DouHelper.Misc.AddCache(alldatas, key);
                    }
                }
            }
            return alldatas;
        }

        public static void ResetGetAllvsCFGS()
        {
            string key = "OilGas.GetAllvsCFGS";
            DouHelper.Misc.ClearCache(key);
        }

    }
}